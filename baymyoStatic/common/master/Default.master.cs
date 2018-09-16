using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class common_master_Default : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            this.Page.Title = Settings.SiteTitle;
            if (!string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                srcInput.Value = Request.QueryString["q"];
                srcOption.Value = Request.QueryString["type"];
            }
            CreateMenu();
        }
    }

    private void CreateMenu()
    {
        string menu = "";
        switch (Settings.IsUserActive())
        {
            case true:
                Int64 toplamSayi = 0, hesaplar = 0, randevular = 0, randevutalepleri = 0, randevularim = 0, sorular = 0, sorulanlar = 0, sorduklarim = 0, yorumlar = 0, yorumlanlar = 0, yorumlarim = 0;
                switch (Settings.CurrentUser().Tipi)
                {
                    case Lib.HesapTuru.Admin:
                        #region --- Menu ---
                        menu = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath) + "MenuUserAdmin.msg");
                        menu = menu.Replace("%Url%", Settings.CurrentUser().ProfilObject.Url);
                        hesaplar = Lib.HesapMethods.GetCount(false);
                        switch ((hesaplar > 0))
                        {
                            case true:
                                menu = menu.Replace("%hesaplar%", "(" + hesaplar + ")").Replace("%hesaplarCss%", " class=\"active\" title=\"Onay bekleyen (" + hesaplar + ") hesap var.\"").Replace("%hesaplarQuery%", "&aktivasyon=1&aktif=0");
                                break;
                            default:
                                menu = menu.Replace("%hesaplar%", "").Replace("%hesaplarCss%", "").Replace("%hesaplarQuery%", "");
                                break;
                        }

                        randevular = Lib.RandevuMethods.GetCount(1, false);
                        switch ((randevular > 0))
                        {
                            case true:
                                menu = menu.Replace("%randevular%", "(" + randevular + ")").Replace("%randevularCss%", " class=\"active\" title=\"Onay bekleyen (" + randevular + ") randevu var.\"").Replace("%randevularQuery%", "&onay=0&durum=1");
                                break;
                            default:
                                menu = menu.Replace("%randevular%", "").Replace("%randevularCss%", "").Replace("%randevularQuery%", "");
                                break;
                        }

                        sorular = Lib.MesajMethods.GetCount(1, false);
                        switch ((sorular > 0))
                        {
                            case true:
                                menu = menu.Replace("%sorular%", "(" + sorular + ")").Replace("%sorularCss%", " class=\"active\" title=\"Onay bekleyen (" + sorular + ") soru var.\"").Replace("%sorularQuery%", "&onay=0&aktif=0");
                                break;
                            default:
                                menu = menu.Replace("%sorular%", "").Replace("%sorularCss%", "").Replace("%sorularQuery%", "");
                                break;
                        }

                        yorumlar = Lib.YorumMethods.GetCount(false);
                        switch ((yorumlar > 0))
                        {
                            case true:
                                menu = menu.Replace("%yorumlar%", "(" + yorumlar + ")").Replace("%yorumlarCss%", " class=\"active\" title=\"Onay bekleyen (" + yorumlar + ") yorum var.\"").Replace("%yorumlarQuery%", "&onay=0&aktif=0");
                                break;
                            default:
                                menu = menu.Replace("%yorumlar%", "").Replace("%yorumlarCss%", "").Replace("%yorumlarQuery%", "");
                                break;
                        }
                        #endregion
                        break;
                    case Lib.HesapTuru.Moderator:
                        #region --- Menu ---
                        menu = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath) + "MenuUserModerator.msg");
                        menu = menu.Replace("%Url%", Settings.CurrentUser().ProfilObject.Url);

                        randevutalepleri = Lib.RandevuMethods.GetCount(Settings.CurrentUser().ID, 1, true);
                        switch ((randevutalepleri > 0))
                        {
                            case true:
                                menu = menu.Replace("%randevutalepleri%", "(" + randevutalepleri + ")").Replace("%randevutalepleriCss%", " class=\"active\" title=\"Onay bekleyen (" + randevutalepleri + ") randevu var.\"").Replace("%randevutalepleriQuery%", "?go=randevutalebleri&durum=1");
                                break;
                            default:
                                menu = menu.Replace("%randevutalepleri%", "").Replace("%randevutalepleriCss%", "").Replace("%randevutalepleriQuery%", "randevutalebleri");
                                break;
                        }

                        sorulanlar = Lib.MesajMethods.GetCount(Settings.CurrentUser().ID, 1, false);
                        switch ((sorulanlar > 0))
                        {
                            case true:
                                menu = menu.Replace("%sorulanlar%", "(" + sorulanlar + ")").Replace("%sorulanlarCss%", " class=\"active\" title=\"Cevaplanmayı bekleyen (" + sorulanlar + ") soru var.\"").Replace("%sorulanlarQuery%", "?go=mesajlar&aktif=0");
                                break;
                            default:
                                menu = menu.Replace("%sorulanlar%", "").Replace("%sorulanlarCss%", "").Replace("%sorulanlarQuery%", "mesajlar");
                                break;
                        }

                        yorumlanlar = Lib.YorumMethods.GetCount(Settings.CurrentUser().ID, Settings.CurrentUser().ProfilObject.Url, true, false);
                        switch ((yorumlanlar > 0))
                        {
                            case true:
                                menu = menu.Replace("%yorumlanlar%", "(" + yorumlanlar + ")").Replace("%yorumlanlarCss%", " class=\"active\" title=\"Onay bekleyen (" + yorumlanlar + ") yorum var.\"").Replace("%yorumlanlarQuery%", "?go=yorumlar&aktif=0");
                                break;
                            default:
                                menu = menu.Replace("%yorumlanlar%", "").Replace("%yorumlanlarCss%", "").Replace("%yorumlanlarQuery%", "yorumlar");
                                break;
                        }
                        #endregion
                        break;
                    case Lib.HesapTuru.Editor:
                        #region --- Menu ---
                        menu = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath) + "MenuUserEditor.msg");
                        menu = menu.Replace("%Url%", Settings.CurrentUser().ProfilObject.Url);

                        randevutalepleri = Lib.RandevuMethods.GetCount(Settings.CurrentUser().ID, 1, true);
                        switch ((randevutalepleri > 0))
                        {
                            case true:
                                menu = menu.Replace("%randevutalepleri%", "(" + randevutalepleri + ")").Replace("%randevutalepleriCss%", " class=\"active\" title=\"Onay bekleyen (" + randevutalepleri + ") randevu var.\"").Replace("%randevutalepleriQuery%", "?go=randevutalebleri&durum=1");
                                break;
                            default:
                                menu = menu.Replace("%randevutalepleri%", "").Replace("%randevutalepleriCss%", "").Replace("%randevutalepleriQuery%", "randevutalebleri");
                                break;
                        }

                        sorulanlar = Lib.MesajMethods.GetCount(Settings.CurrentUser().ID, 1, false);
                        switch ((sorulanlar > 0))
                        {
                            case true:
                                menu = menu.Replace("%sorulanlar%", "(" + sorulanlar + ")").Replace("%sorulanlarCss%", " class=\"active\" title=\"Cevaplanmayı bekleyen (" + sorulanlar + ") soru var.\"").Replace("%sorulanlarQuery%", "?go=mesajlar&aktif=0");
                                break;
                            default:
                                menu = menu.Replace("%sorulanlar%", "").Replace("%sorulanlarCss%", "").Replace("%sorulanlarQuery%", "mesajlar");
                                break;
                        }

                        yorumlanlar = Lib.YorumMethods.GetCount(Settings.CurrentUser().ID, Settings.CurrentUser().ProfilObject.Url, true, false);
                        switch ((yorumlanlar > 0))
                        {
                            case true:
                                menu = menu.Replace("%yorumlanlar%", "(" + yorumlanlar + ")").Replace("%yorumlanlarCss%", " class=\"active\" title=\"Onay bekleyen (" + yorumlanlar + ") yorum var.\"").Replace("%yorumlanlarQuery%", "?go=yorumlar&aktif=0");
                                break;
                            default:
                                menu = menu.Replace("%yorumlanlar%", "").Replace("%yorumlanlarCss%", "").Replace("%yorumlanlarQuery%", "yorumlar");
                                break;
                        }
                        #endregion
                        break;
                    default:
                        menu = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath) + "MenuUserStandart.msg");
                        break;
                }

                #region --- Standart Menu ---
                randevularim = Lib.RandevuMethods.GetCount(Settings.CurrentUser().Mail, 1);
                switch ((randevularim > 0))
                {
                    case true:
                        menu = menu.Replace("%randevularim%", "(" + randevularim + ")").Replace("%randevularimCss%", " class=\"active\" title=\"Onay bekleyen (" + randevularim + ") randevum var.\"");
                        break;
                    default:
                        menu = menu.Replace("%randevularim%", "").Replace("%randevularimCss%", "");
                        break;
                }

                sorduklarim = Lib.MesajMethods.GetCount(Settings.CurrentUser().Mail, 1, false);
                switch ((sorduklarim > 0))
                {
                    case true:
                        menu = menu.Replace("%sorduklarim%", "(" + sorduklarim + ")").Replace("%sorduklarimCss%", " class=\"active\" title=\"Cevaplanmayı bekleyen (" + sorduklarim + ") sorum var.\"");
                        break;
                    default:
                        menu = menu.Replace("%sorduklarim%", "").Replace("%sorduklarimCss%", "");
                        break;
                }

                yorumlarim = Lib.YorumMethods.GetCount(Settings.CurrentUser().Mail, false);
                switch ((yorumlarim > 0))
                {
                    case true:
                        menu = menu.Replace("%yorumlarim%", "(" + yorumlarim + ")").Replace("%yorumlarimCss%", " class=\"active\" title=\"Onay bekleyen (" + yorumlarim + ") yorumum var.\"");
                        break;
                    default:
                        menu = menu.Replace("%yorumlarim%", "").Replace("%yorumlarimCss%", "");
                        break;
                }

                menu = menu.Replace("%Adi%", Settings.CurrentUser().Adi);
                menu = menu.Replace("%Soyadi%", Settings.CurrentUser().Soyadi);

                toplamSayi = (hesaplar + randevular + randevutalepleri + randevularim + sorular + sorulanlar + sorduklarim + yorumlar + yorumlarim + yorumlanlar);

                switch ((toplamSayi > 0))
                {
                    case true:
                        menu = menu.Replace("%toplamSayi%", "<span class=\"toolTip totalCount\" title=\"Onay bekleyen (" + toplamSayi + ") işlem var.\">" + toplamSayi + "</span>");
                        break;
                    default:
                        menu = menu.Replace("%toplamSayi%", "");
                        break;
                }
                #endregion
                break;
            default:
                menu = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath) + "MenuDefault.msg");
                break;
        }
        ltrTopLink.Text = menu.Replace("%VirtualPath%", Settings.VirtualPath);
    }

    protected void NavigationMenu_DataBound(object sender, EventArgs e)
    {
        if (NavigationMenu.SelectedItem != null)
            this.Page.Title = NavigationMenu.SelectedItem.Text + " - " + Settings.SiteTitle;
    }

    protected void srcButton_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(srcInput.Value) & !srcInput.Value.Equals("Aranacak Kelime ..."))
        {
            switch (srcOption.Value)
            {
                case "":
                    return;
                case "makale":
                    Response.Redirect(Settings.VirtualPath + "?go=" + srcOption.Value + "liste&type=" + srcOption.Value + "&kid=" + Request.QueryString["kid"] + "&hspid=" + Request.QueryString["hspid"] + "&q=" + srcInput.Value, false);
                    break;
                case "video":
                    Response.Redirect(Settings.VirtualPath + "?go=" + srcOption.Value + "liste&type=" + srcOption.Value + "&hspid=" + Request.QueryString["hspid"] + "&q=" + srcInput.Value, false);
                    break;
                case "soru":
                    Response.Redirect(Settings.VirtualPath + "?go=" + srcOption.Value + "liste&type=" + srcOption.Value + "&hspid=" + Request.QueryString["hspid"] + "&q=" + srcInput.Value, false);
                    break;
                case "haber":
                    Response.Redirect(Settings.VirtualPath + "?go=" + srcOption.Value + "liste&type=" + srcOption.Value + "&q=" + srcInput.Value, false);
                    break;
                case "doktor":
                case "hastane":
                    Response.Redirect(Settings.VirtualPath + "?go=hesapliste&type=" + srcOption.Value + "&q=" + srcInput.Value, false);
                    break;
            }
        }
    }
}
