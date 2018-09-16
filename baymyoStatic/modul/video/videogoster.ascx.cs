using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_video_videogoster : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            using (Lib.Video m = Lib.VideoMethods.GetVideo(BAYMYO.UI.Converts.NullToInt64(Request.QueryString["vid"])))
            {
                if (m != null)
                {
                    this.Page.Title = m.Baslik;
                    string etiket = m.Etiket;
                    if (string.IsNullOrEmpty(etiket))
                        etiket = m.Baslik;
                    BAYMYO.UI.Web.Pages.AddMetaTag(this.Page, etiket, m.Baslik);

                    switch (Settings.CurrentUser().Tipi)
                    {
                        case Lib.HesapTuru.Admin:
                        case Lib.HesapTuru.Moderator:
                        case Lib.HesapTuru.Editor:
                            if (!m.Aktif & !BAYMYO.UI.Converts.NullToGuid(Settings.CurrentUser().ID).Equals(m.HesapID))
                            {
                                CommentControl1.Visible = false;
                                ltrContent.Text = MessageBox.Show(DialogResult.Warning, "Bu içerik gösterime kapatılmıştır. Kimler yayından kaldırabilir yazarı yada yöneticilerimiz tarafından yayından kaldırılabilir.");
                                return;
                            }
                            else
                                View(m);
                            break;
                        case Lib.HesapTuru.None:
                        case Lib.HesapTuru.Standart:
                            if (!m.Aktif)
                            {
                                CommentControl1.Visible = false;
                                ltrContent.Text = MessageBox.Show(DialogResult.Warning, "Bu içerik gösterime kapatılmıştır. Kimler yayından kaldırabilir yazarı yada yöneticilerimiz tarafından yayından kaldırılabilir.");
                                return;
                            }
                            else
                                View(m);
                            break;
                    }
                }
            }
        }
        else
            this.Page.Title = "Video - " + Settings.SiteTitle;
    }

    private void View(Lib.Video m)
    {
        using (Lib.Hesap hsp = Lib.HesapMethods.GetHesap(m.HesapID))
        {
            CommentControl1.IsCommandActive = BAYMYO.UI.Converts.NullToGuid(Settings.CurrentUser().ID).Equals(m.HesapID);
            CommentControl1.Visible = m.Yorum;
            CommentControl1.ModulID = "video";
            CommentControl1.IcerikID = Request.QueryString["vid"];

            ltrContent.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath + "VideoView.msg"));

            //Icerik Bilgisi
            ltrContent.Text = ltrContent.Text.Replace("%ImagesPath%", Settings.ImagesPath);
            ltrContent.Text = ltrContent.Text.Replace("%ResimUrl%", ((!string.IsNullOrEmpty(m.ResimUrl)) ? "<img class=\"image left\" src=" + Settings.ImagesPath + "makale/" + m.ResimUrl + " alt=\"%Baslik%\" />" : ""));
            ltrContent.Text = ltrContent.Text.Replace("%Baslik%", m.Baslik);
            ltrContent.Text = ltrContent.Text.Replace("%KayitTarihi%", m.KayitTarihi.ToShortDateString());
            ltrContent.Text = ltrContent.Text.Replace("%Etiket%", m.Etiket);

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
                CommentControl1.Visible = m.Yorum;
                g.ModulID = "video";
                g.IcerikID = m.ID.ToString();
                g.KayitTarihi = DateTime.Now;
                Lib.GosterimMethods.Insert(g);
                ltrContent.Text = ltrContent.Text.Replace("%Icerik%", m.Embed);

                //Gösterim Bilgisi
                ltrContent.Text = ltrContent.Text.Replace("%ModulID%", g.ModulID);
                ltrContent.Text = ltrContent.Text.Replace("%Gosterim%", Lib.GosterimMethods.GetCount(g.ModulID, g.IcerikID).ToString());
            }
        }
    }
}