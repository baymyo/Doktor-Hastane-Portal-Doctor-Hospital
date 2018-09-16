using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_mesaj_mesajgoster : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CommentControl1.Visible = false;
            using (Lib.Mesaj m = Lib.MesajMethods.GetMesaj(BAYMYO.UI.Converts.NullToInt64(Request.QueryString["mid"])))
            {
                if (m != null)
                {
                    this.Page.Title = m.Konu + " - " + Settings.SiteTitle;
                    BAYMYO.UI.Web.Pages.AddMetaTag(this.Page, m.Konu, m.Icerik);

                    switch (Settings.CurrentUser().Tipi)
                    {
                        case Lib.HesapTuru.Admin:
                        case Lib.HesapTuru.Moderator:
                        case Lib.HesapTuru.Editor:
                            if (!m.YoneticiOnay)
                            {
                                ltrContent.Text = MessageBox.Show(DialogResult.Warning, "Bu soru uzman ekibimiz tarafından incelenmektedir inceleme işleminden sonra cevaplanmak için doktora gönderilecektir.");
                                return;
                            }
                            else if (!m.Aktif & !BAYMYO.UI.Converts.NullToGuid(Settings.CurrentUser().ID).Equals(m.HesapID))
                            {
                                ltrContent.Text = MessageBox.Show(DialogResult.Warning, "Bu soru gösterime kapatılmıştır. Sadece doktor ve hastası arasında görüntülenebilmektetir, bu konudaki duyarlılığınızdan dolayı sizlere teşekkür ederiz.");
                                return;
                            }
                            else
                                View(m);
                            break;
                        case Lib.HesapTuru.None:
                        case Lib.HesapTuru.Standart:
                            if (!m.YoneticiOnay)
                            {
                                ltrContent.Text = MessageBox.Show(DialogResult.Warning, "Bu soru uzman ekibimiz tarafından incelenmektedir inceleme işleminden sonra cevaplanmak için doktora gönderilecektir.");
                                return;
                            }
                            else if (!m.Aktif & !BAYMYO.UI.Converts.NullToString(Settings.CurrentUser().Mail).Equals(m.Mail))
                            {
                                ltrContent.Text = MessageBox.Show(DialogResult.Warning, "Bu soru gösterime kapatılmıştır. Sadece doktor ve hastası arasında görüntülenebilmektetir, bu konudaki duyarlılığınızdan dolayı sizlere teşekkür ederiz.");
                                return;
                            }
                            else
                                View(m);
                            break;
                    }
                }
            }
        }
    }

    private void View(Lib.Mesaj m)
    {
        using (Lib.Hesap hsp = Lib.HesapMethods.GetHesap(m.HesapID))
        {
            CommentControl1.Visible = !string.IsNullOrEmpty(m.Yanit);
            CommentControl1.IsCommandActive = BAYMYO.UI.Converts.NullToGuid(Settings.CurrentUser().ID).Equals(m.HesapID);
            CommentControl1.ModulID = "mesaj";
            CommentControl1.IcerikID = Request.QueryString["mid"];

            ltrContent.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath + "MessageView.msg"));

            //Icerik Bilgisi
            ltrContent.Text = ltrContent.Text.Replace("%ImagesPath%", Settings.ImagesPath);
            ltrContent.Text = ltrContent.Text.Replace("%SoranKisi%", m.Adi);
            ltrContent.Text = ltrContent.Text.Replace("%Konu%", m.Konu);
            ltrContent.Text = ltrContent.Text.Replace("%Icerik%", m.Icerik);
            ltrContent.Text = ltrContent.Text.Replace("%Yanit%", (!string.IsNullOrEmpty(m.Yanit)) ? m.Yanit + "&nbsp;&nbsp;(%GuncellemeTarihi%)" : "<b>Doktorumuz tarafından incelemeye alınmıştır kısa süre içerisinde sorunuz yanıtlayacaktır. İlginize teşekkür eder acil şifalar dileriz.</b>");
            ltrContent.Text = ltrContent.Text.Replace("%Ozet%", m.Icerik);
            ltrContent.Text = ltrContent.Text.Replace("%KayitTarihi%", m.KayitTarihi.ToShortDateString());
            ltrContent.Text = ltrContent.Text.Replace("%GuncellemeTarihi%", m.GuncellemeTarihi.ToShortDateString());

            //Hesap Bilgileri
            ltrContent.Text = ltrContent.Text.Replace("%Url%", Settings.VirtualPath + hsp.ProfilObject.Url);
            switch (hsp.Tipi)
            {
                case Lib.HesapTuru.Moderator:
                    ltrContent.Text = ltrContent.Text.Replace("%Adi%", hsp.ProfilObject.Adi);
                    ltrContent.Text = ltrContent.Text.Replace("%Soyadi%", "");
                    ltrContent.Text = ltrContent.Text.Replace("%Unvan%", "");
                    ltrContent.Text = ltrContent.Text.Replace("%UzmanlikAlani%", Lib.KategoriMethods.GetKategori("hastaneuzmanlik", hsp.ProfilObject.UzmanlikAlaniID).Adi);
                    break;
                default:
                    ltrContent.Text = ltrContent.Text.Replace("%Adi%", hsp.Adi);
                    ltrContent.Text = ltrContent.Text.Replace("%Soyadi%", hsp.Soyadi);
                    ltrContent.Text = ltrContent.Text.Replace("%Unvan%", Lib.KategoriMethods.GetKategori("unvan", hsp.ProfilObject.Unvan).Adi + " ");
                    ltrContent.Text = ltrContent.Text.Replace("%UzmanlikAlani%", Lib.KategoriMethods.GetKategori("uzmanlik", hsp.ProfilObject.UzmanlikAlaniID).Adi);
                    break;
            }

            using (Lib.Gosterim g = new Lib.Gosterim())
            {
                g.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                g.ModulID = "mesaj";
                g.IcerikID = m.ID.ToString();
                g.KayitTarihi = DateTime.Now;
                Lib.GosterimMethods.Insert(g);
                //Gösterim Bilgisi
                ltrContent.Text = ltrContent.Text.Replace("%ModulID%", g.ModulID);
                ltrContent.Text = ltrContent.Text.Replace("%Gosterim%", Lib.GosterimMethods.GetCount(g.ModulID, g.IcerikID).ToString());
            }
        }
    }
}