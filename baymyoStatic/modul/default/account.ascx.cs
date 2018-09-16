using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_default_account : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        if (Settings.IsUserActive())
        {
            CustomizeControl1.MessageText = MessageBox.AccessDenied();
            CustomizeControl1.PanelVisible = false;
            return;
        }

        switch (Request.QueryString["r"])
        {
            case "sifre":
                TextBox txt = new TextBox();
                txt.ID = "sifre";
                CustomizeControl1.AddControl("Yeni Şifre", txt);
                CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);
                break;
        }
        base.OnInit(e);
    }

    void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["g"]) & !string.IsNullOrEmpty(Request.QueryString["s"]))
        {
            using (TextBox txtSifre = controls["sifre"] as TextBox)
            {
                if (!string.IsNullOrEmpty(txtSifre.Text.Trim()))
                {
                    using (BAYMYO.MultiSQLClient.MParameterCollection param = new BAYMYO.MultiSQLClient.MParameterCollection())
                    {
                        param.Add("ID", BAYMYO.UI.Converts.NullToGuid(Request.QueryString["g"]), BAYMYO.MultiSQLClient.MSqlDbType.UniqueIdentifier);
                        param.Add("OnayKodu", Request.QueryString["s"], BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                        using (Lib.Hesap m = Lib.HesapMethods.GetHesap(System.Data.CommandType.Text, "SELECT TOP(1) * FROM Hesap WHERE ID=@ID AND OnayKodu=@OnayKodu AND Aktivasyon=1 AND Aktif=1", param))
                        {
                            if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(m.ID))
                            {
                                m.OnayKodu = Settings.YeniOnayKodu();
                                m.Sifre = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtSifre.Text.Trim(), "md5");
                                if (Lib.HesapMethods.Update(m) > 0)
                                {
                                    string m_MailMesaj = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath) + "PasswordNew.msg");
                                    m_MailMesaj = m_MailMesaj.Replace("%SiteUrl%", Settings.SiteUrl);
                                    m_MailMesaj = m_MailMesaj.Replace("%SiteTitle%", Settings.SiteTitle);
                                    m_MailMesaj = m_MailMesaj.Replace("%VirtualPath%", Settings.VirtualPath);
                                    m_MailMesaj = m_MailMesaj.Replace("%ReturnUrl", Request.QueryString["ReturnUrl"]);
                                    m_MailMesaj = m_MailMesaj.Replace("%IP%", Context.Request.ServerVariables["REMOTE_ADDR"].ToString());
                                    m_MailMesaj = m_MailMesaj.Replace("%ID%", m.ID.ToString());
                                    m_MailMesaj = m_MailMesaj.Replace("%Adi%", m.Adi).Replace("%Soyadi%", m.Soyadi);
                                    m_MailMesaj = m_MailMesaj.Replace("%Mail%", m.Mail);
                                    m_MailMesaj = m_MailMesaj.Replace("%Sifre%", txtSifre.Text);
                                    Settings.SendMail(m.Mail, m.Adi + " " + m.Soyadi, "Şifre Değiştirildi", m_MailMesaj, true);
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Succes, string.Format("Şifre değiştirme işleminiz başarılı bir şekilde gerçekleştirildi. Lütfen sisteme giriş yapmak için <a class=\"toolTip\" title=\"Üye girişi yapmak için tıklayın!\" href=\"{0}\">buraya tıklayın</a>.", Settings.VirtualPath + "?l=1&ReturnUrl=" + Request.QueryString["ReturnUrl"]));
                                    CustomizeControl1.PanelVisible = false;
                                    m_MailMesaj = null;
                                }
                                else
                                {
                                    CustomizeControl1.MessageText = MessageBox.UnSuccessful();
                                    CustomizeControl1.PanelVisible = false;
                                }
                            }
                            else
                            {
                                CustomizeControl1.MessageText = MessageBox.UnSuccessful();
                                CustomizeControl1.PanelVisible = false;
                            }
                        }
                    }
                }
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["g"]) & !string.IsNullOrEmpty(Request.QueryString["s"]))
            {
                switch (Request.QueryString["r"])
                {
                    case "sifre":
                        this.Page.Title = "Yeni Şifre İşlemi";
                        CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Info, "Yeni şifrenizi ilgili alana giriniz ve gönder düğmesine tıklayınız.");
                        CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Şifre", "Değiştirme Formu");
                        CustomizeControl1.PanelVisible = true;
                        break;
                    case "aktivasyon":
                        this.Page.Title = "Aktivasyon İşlemi";
                        CustomizeControl1.PanelVisible = false;
                        using (BAYMYO.MultiSQLClient.MParameterCollection param = new BAYMYO.MultiSQLClient.MParameterCollection())
                        {
                            param.Add("ID", BAYMYO.UI.Converts.NullToGuid(Request.QueryString["g"]), BAYMYO.MultiSQLClient.MSqlDbType.UniqueIdentifier);
                            param.Add("OnayKodu", Request.QueryString["s"], BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                            using (Lib.Hesap m = Lib.HesapMethods.GetHesap(System.Data.CommandType.Text, "SELECT TOP(1) * FROM Hesap WHERE ID=@ID AND OnayKodu=@OnayKodu AND Aktivasyon=0 AND Aktif=0", param))
                            {
                                if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(m.ID))
                                {
                                    m.OnayKodu = Settings.YeniOnayKodu();
                                    m.Aktivasyon = true;
                                    switch (m.Tipi)
                                    {
                                        case Lib.HesapTuru.Admin:
                                        case Lib.HesapTuru.Moderator:
                                        case Lib.HesapTuru.Editor:
                                            m.Aktif = false;
                                            break;
                                        default:
                                            m.Aktif = true;
                                            break;
                                    }
                                    if (Lib.HesapMethods.Update(m) > 0)
                                        CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Succes, string.Format("Aktivasyon işleminiz başarılı bir şekilde gerçekleştirildi. Lütfen sisteme giriş yapmak için <a class=\"toolTip\" title=\"Üye girişi yapmak için tıklayın!\" href=\"{0}\">buraya tıklayın</a>.", Settings.VirtualPath + "?l=1&ReturnUrl=" + Request.QueryString["ReturnUrl"]));
                                    else
                                        CustomizeControl1.MessageText = MessageBox.UnSuccessful();
                                }
                                else
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, string.Format("Bu işlemi daha önce gerçekleştirdiğiniz için tekrar aktivasyon işlemi gerçekleştiremezsiniz. Lütfen sisteme giriş yapmak için <a class=\"toolTip\" title=\"Üye girişi yapmak için tıklayın!\" href=\"{0}\">buraya tıklayın</a>.", Settings.VirtualPath + "?l=1&ReturnUrl=" + Request.QueryString["ReturnUrl"]));
                            }
                        }
                        break;
                    default:
                        CustomizeControl1.MessageText = MessageBox.UnSuccessful();
                        CustomizeControl1.PanelVisible = false;
                        return;
                }
            }
            else
            {
                CustomizeControl1.MessageText = MessageBox.UnSuccessful();
                CustomizeControl1.PanelVisible = false;
            }
        }
    }
}