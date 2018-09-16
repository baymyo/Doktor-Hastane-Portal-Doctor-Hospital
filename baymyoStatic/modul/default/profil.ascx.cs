using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_default_profil : System.Web.UI.UserControl
{
    enum ViewType
    {
        Profil,
        About,
        Contact,
        Randevu
    }

    ViewType viewPage = ViewType.Profil;
    protected override void OnInit(EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["type"]))
        {
            CommentControl1.Visible = false;
            contact1.Visible = false;
            randevu1.Visible = false;
            workArea.Visible = false;
            mapsArea.Visible = false;
            lastArticle.Visible = false;
            switch (Request.QueryString["type"])
            {
                case "about":
                    viewPage = ViewType.About;
                    CommentControl1.Visible = true;
                    break;
                case "contact":
                    viewPage = ViewType.Contact;
                    contact1.Visible = true;
                    break;
                case "randevu":
                    viewPage = ViewType.Randevu;
                    randevu1.Visible = true;
                    break;
                default:
                    viewPage = ViewType.Profil;
                    workArea.Visible = true;
                    mapsArea.Visible = true;
                    lastArticle.Visible = true;
                    break;
            }
        }
        base.OnInit(e);
    }

    public string UniqueID = "nan";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["url"]))
                {
                    using (Lib.Hesap hsp = Lib.HesapMethods.GetHesap(Request.QueryString["url"]))
                    {
                        UniqueID = hsp.ID.ToString();
                        switch (hsp.Tipi)
                        {
                            case Lib.HesapTuru.Moderator:
                                this.Page.Title = hsp.ProfilObject.Adi;
                                BAYMYO.UI.Web.Pages.AddMetaTag(this.Page, hsp.ProfilObject.Adi + " hakkında ", hsp.ProfilObject.Hakkimda);
                                break;
                            default:
                                hsp.ProfilObject.Unvan = Lib.KategoriMethods.GetKategori("unvan", hsp.ProfilObject.Unvan).Adi;
                                this.Page.Title = hsp.ProfilObject.Unvan + ' ' + hsp.Adi + ' ' + hsp.Soyadi;
                                BAYMYO.UI.Web.Pages.AddMetaTag(this.Page, hsp.Adi + " " + hsp.Soyadi + " hakkında ", hsp.ProfilObject.Hakkimda);
                                break;
                        }

                        if (!hsp.Aktif)
                        {
                            CommentControl1.Visible = false;
                            contact1.Visible = false;
                            randevu1.Visible = false;
                            workArea.Visible = false;
                            mapsArea.Visible = false;
                            lastArticle.Visible = false;
                            ltrContent.Text = MessageBox.Show(DialogResult.Warning, "Şuan erişmek istediğiniz <b>'Sayfa'</b> gösterime kapalı durumdadır nedenleri aşağıda belirtilmiştir. <br/>* Üyelik başvuru inceleme altında olabilir.<br/>* Yada yöneticilerimiz tarafından doktor üyeliği durdurulmuş olabilir.");
                            return;
                        }

                        //Sablonu Getir
                        switch (hsp.Tipi)
                        {
                            case Lib.HesapTuru.Moderator:
                                ltrContent.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath + "MedicalView.msg"));
                                ltrContent.Text = ltrContent.Text.Replace("%HastaneAdi%", hsp.ProfilObject.Adi);
                                ltrContent.Text = ltrContent.Text.Replace("%UzmanlikAlani%", Lib.KategoriMethods.GetKategori("hastaneuzmanlik", hsp.ProfilObject.UzmanlikAlaniID).Adi);
                                break;
                            case Lib.HesapTuru.Editor:
                                ltrContent.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath + "ProfilView.msg"));
                                ltrContent.Text = ltrContent.Text.Replace("%Unvan%", hsp.ProfilObject.Unvan + " ");
                                //ltrContent.Text = ltrContent.Text.Replace("%DiplomaNo%", hsp.ProfilObject.DiplomaNo);
                                //ltrContent.Text = ltrContent.Text.Replace("%TCKimlikNo%", hsp.ProfilObject.TCKimlikNo);
                                ltrContent.Text = ltrContent.Text.Replace("%UzmanlikAlani%", Lib.KategoriMethods.GetKategori("uzmanlik", hsp.ProfilObject.UzmanlikAlaniID).Adi);
                                break;
                        }

                        //Hesap Bilgi
                        ltrContent.Text = ltrContent.Text.Replace("%ImagesPath%", Settings.ImagesPath);
                        ltrContent.Text = ltrContent.Text.Replace("%Adi%", hsp.Adi);
                        ltrContent.Text = ltrContent.Text.Replace("%Soyadi%", hsp.Soyadi);
                        ltrContent.Text = ltrContent.Text.Replace("%KayitTarihi%", hsp.KayitTarihi.ToShortDateString());
                        //Profil Bilgi
                        ltrContent.Text = ltrContent.Text.Replace("%ResimUrl%", Settings.ImagesPath + ((!string.IsNullOrEmpty(hsp.ProfilObject.ResimUrl)) ? "profil/" + hsp.ProfilObject.ResimUrl : "yok.png"));
                        ltrContent.Text = ltrContent.Text.Replace("%Url%", Settings.VirtualPath + hsp.ProfilObject.Url);
                        ltrContent.Text = ltrContent.Text.Replace("%Mail%", hsp.ProfilObject.Mail);

                        using (Lib.CalismaAlani c = Lib.CalismaAlaniMethods.GetDefault(hsp.ID))
                        {
                            ltrContent.Text = ltrContent.Text.Replace("%Kurum%", c.Kurum);
                            ltrContent.Text = ltrContent.Text.Replace("%Adres%", c.Adres);
                            ltrContent.Text = ltrContent.Text.Replace("%Telefon%", c.Telefon);
                            ltrContent.Text = ltrContent.Text.Replace("%Semt%", c.Semt);
                            ltrContent.Text = ltrContent.Text.Replace("%Sehir%", c.Sehir);
                            ltrContent.Text = ltrContent.Text.Replace("%WebSitesi%", c.WebSitesi);
                            ltrContent.Text = ltrContent.Text.Replace("%RandevuAl%", Settings.RandevuAl(c.Kurum, Settings.CreateLink("randevu", hsp.ProfilObject.Url + ";" + c.ID, "go"), c.Randevu));
                        }

                        //Profildeki Baglantilar
                        ltrContent.Text = ltrContent.Text.Replace("%Iletisim%", Settings.CreateLink("iletisim", hsp.ProfilObject.Url, "go"));
                        ltrContent.Text = ltrContent.Text.Replace("%Makaleleri%", Settings.CreateLink("makaledoktor", hsp.ID, hsp.Adi + " " + hsp.Soyadi));
                        ltrContent.Text = ltrContent.Text.Replace("%Mesajlari%", Settings.CreateLink("mesajliste", hsp.ID, hsp.Adi + " " + hsp.Soyadi));
                        ltrContent.Text = ltrContent.Text.Replace("%YazarHakkinda%", Settings.CreateLink("yazarhakkinda", hsp.ProfilObject.Url, "go"));
                        switch (viewPage)
                        {
                            case ViewType.Profil:
                                #region --- Profil ---
                                this.Page.Title += " - Profil Sayfası";
                                if (!string.IsNullOrEmpty(hsp.ProfilObject.Hakkimda))
                                {
                                    ltrContent.Text = ltrContent.Text.Replace("%DetailBlock%", BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath + "ProfilAboutUs.msg")));
                                    ltrContent.Text = ltrContent.Text.Replace("%Hakkimda%", hsp.ProfilObject.Hakkimda);
                                }
                                else
                                    ltrContent.Text = ltrContent.Text.Replace("%DetailBlock%", "");
                                using (Lib.Gosterim g = new Lib.Gosterim())
                                {
                                    g.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                                    g.ModulID = "profil";
                                    g.IcerikID = hsp.ID.ToString();
                                    g.KayitTarihi = DateTime.Now;
                                    Lib.GosterimMethods.Insert(g);
                                }
                                GetData(hsp.ID);
                                #endregion
                                break;
                            case ViewType.About:
                                this.Page.Title += " - Hakkında";
                                if (Settings.CurrentUser().ProfilObject != null)
                                    CommentControl1.IsCommandActive = BAYMYO.UI.Converts.NullToString(Settings.CurrentUser().ProfilObject.Url).Equals(hsp.ProfilObject.Url);
                                else
                                    CommentControl1.IsCommandActive = false;
                                CommentControl1.ModulID = "yazarhakkinda";
                                CommentControl1.IcerikID = Request.QueryString["url"];
                                ltrContent.Text = ltrContent.Text.Replace("%DetailBlock%", "");
                                break;
                            case ViewType.Contact:
                                this.Page.Title += " - İletişim Formu";
                                contact1.HesapID = hsp.ID.ToString();
                                ltrContent.Text = ltrContent.Text.Replace("%DetailBlock%", "");
                                break;
                            case ViewType.Randevu:
                                this.Page.Title += " - Randevu Talep Formu";
                                randevu1.HesapID = hsp.ID;
                                randevu1.ModulID = "calismaalani";
                                randevu1.IcerikID = Request.QueryString["caid"];
                                ltrContent.Text = ltrContent.Text.Replace("%DetailBlock%", "");
                                break;
                        }

                        //Gösterim Bilgisi
                        ltrContent.Text = ltrContent.Text.Replace("%ModulID%", "profil");
                        ltrContent.Text = ltrContent.Text.Replace("%Gosterim%", Lib.GosterimMethods.GetCount("profil", hsp.ID.ToString()).ToString());
                    }
                }
                else
                    Response.Redirect(Settings.VirtualPath + "?ref=profil", false);

            }
            catch (Exception ex)
            {
                Response.Redirect(Settings.VirtualPath + "?ref=profil-error&ex=" + ex.Message, false);
            }
        }
    }

    private void GetData(Guid pHesapID)
    {
        using (BAYMYO.UI.Web.CustomSqlQuery data = new BAYMYO.UI.Web.CustomSqlQuery(rptMakaleler, "Makale", "KayitTarihi DESC", "Aktif=1"))
        {
            data.Where += " AND HesapID=@HesapID";
            data.Parameters.Add("HesapID", pHesapID, BAYMYO.MultiSQLClient.MSqlDbType.UniqueIdentifier);
            data.Top = 5;
            data.Execute();

            if (rptMakaleler.Items.Count < 1)
            {
                ltrMessage.Visible = true;
                ltrMessage.Text = "<div style=\"display:inline-block;\">" + MessageBox.IsNotViews() + "</div>";
            }
        }

        using (BAYMYO.UI.Web.CustomSqlQuery data = new BAYMYO.UI.Web.CustomSqlQuery(rptMuayene, "SELECT TOP(5) CalismaAlani.* FROM CalismaAlani  WHERE (CalismaAlani.HesapID=@HesapID) AND (CalismaAlani.Varsayilan=0) AND (CalismaAlani.Aktif=1)"))
        {
            data.Parameters.Add("HesapID", pHesapID, BAYMYO.MultiSQLClient.MSqlDbType.UniqueIdentifier);
            data.Execute();
            workArea.Visible = (rptMuayene.Items.Count >= 1);
        }
    }
}