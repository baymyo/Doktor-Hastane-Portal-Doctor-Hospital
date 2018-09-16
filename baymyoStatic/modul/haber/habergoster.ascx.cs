using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_haber_habergoster : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            using (Lib.Haber m = Lib.HaberMethods.GetHaber(BAYMYO.UI.Converts.NullToInt64(Request.QueryString["hid"])))
            {
                if (m != null)
                {
                    this.Page.Title = m.Baslik;
                    string etiket = m.Etiket;
                    if (string.IsNullOrEmpty(etiket))
                        etiket = m.Ozet;
                    BAYMYO.UI.Web.Pages.AddMetaTag(this.Page, etiket, m.Ozet);

                    if (!m.Aktif)
                    {
                        ltrContent.Text = MessageBox.Show(DialogResult.Warning, "Bu içerik gösterime kapatılmıştır. Kimler yayından kaldırabilir yazarı yada yöneticilerimiz tarafından yayından kaldırılabilir.");
                        return;
                    }

                    CommentControl1.IsCommandActive = BAYMYO.UI.Converts.NullToGuid(Settings.CurrentUser().ID).Equals(m.HesapID);
                    CommentControl1.Visible = m.Yorum;
                    CommentControl1.ModulID = "haber";
                    CommentControl1.IcerikID = Request.QueryString["hid"];

                    using (Lib.Hesap hsp = Lib.HesapMethods.GetHesap(m.HesapID))
                    {
                        ltrContent.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath + "PageView.msg"));

                        //Icerik Bilgisi
                        ltrContent.Text = ltrContent.Text.Replace("%ImagesPath%", Settings.ImagesPath);
                        ltrContent.Text = ltrContent.Text.Replace("%ResimUrl%", ((!string.IsNullOrEmpty(m.ResimUrl)) ? "<img class=\"image left\" src=" + Settings.ImagesPath + "haber/" + m.ResimUrl + " alt=\"%Baslik%\" />" : ""));
                        ltrContent.Text = ltrContent.Text.Replace("%Baslik%", m.Baslik);
                        ltrContent.Text = ltrContent.Text.Replace("%Ozet%", m.Ozet);
                        ltrContent.Text = ltrContent.Text.Replace("%KayitTarihi%", m.KayitTarihi.ToShortDateString());
                        ltrContent.Text = ltrContent.Text.Replace("%Etiket%", m.Etiket);

                        //Hesap Bilgileri
                        ltrContent.Text = ltrContent.Text.Replace("%Unvan%", "");
                        ltrContent.Text = ltrContent.Text.Replace("%UzmanlikAlani%", "");
                        ltrContent.Text = ltrContent.Text.Replace("%Adi%", hsp.Adi);
                        ltrContent.Text = ltrContent.Text.Replace("%Soyadi%", hsp.Soyadi);

                        using (Lib.Gosterim g = new Lib.Gosterim())
                        {
                            g.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                            g.ModulID = "haber";
                            g.IcerikID = m.ID.ToString();
                            g.KayitTarihi = DateTime.Now;
                            if (m.Uye)
                            {
                                if (Settings.IsUserActive())
                                {
                                    g.HesapID = Settings.CurrentUser().ID;
                                    Lib.GosterimMethods.Insert(g);
                                    ltrContent.Text = ltrContent.Text.Replace("%Icerik%", m.Icerik);
                                }
                                else
                                    ltrContent.Text = ltrContent.Text.Replace("%Icerik%", string.Format("..<br/><br/><br/>Devamını okumak için  <a href=\"{0}?l=1&ReturnUrl={1}\">üye girişi</a> yapınız yada <a href=\"{0}?l=2&ReturnUrl={1}\">ücretsiz kayıt</a> olunuz.", Settings.VirtualPath, Request.RawUrl));
                            }
                            else
                            {
                                Lib.GosterimMethods.Insert(g);
                                ltrContent.Text = ltrContent.Text.Replace("%Icerik%", m.Icerik);
                            }

                            //Gösterim Bilgisi
                            ltrContent.Text = ltrContent.Text.Replace("%ModulID%", g.ModulID);
                            ltrContent.Text = ltrContent.Text.Replace("%Gosterim%", Lib.GosterimMethods.GetCount(g.ModulID, g.IcerikID).ToString());
                        }
                    }
                }
            }
        }
    }
}