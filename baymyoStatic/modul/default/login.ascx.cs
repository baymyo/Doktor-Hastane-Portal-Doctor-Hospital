using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class modul_default_login : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        if (Settings.IsUserActive())
        {
            Response.Redirect(Settings.VirtualPath + "?l=5", false);
            return;
        }

        CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Kullanıcı", "Giriş Ekranı");
        //<a class=\"toolTip\" title=\"Yeni Kullanıcı kayıtı için tıkla.\" href=\"{0}?l=2\">Yeni Üye Kayıt</a>&nbsp;&nbsp;-&nbsp;&nbsp;
        CustomizeControl1.StatusText = string.Format("<a class=\"toolTip\" title=\"Şifre hatırlatma ekranı için tıklayın.\" href=\"{0}?l=3&r=sifre\">Şifremi Unuttum</a>&nbsp;&nbsp;-&nbsp;&nbsp;<a class=\"toolTip\" title=\"Aktivasyon talep formu için tıklayın.\" href=\"{0}?l=3&r=aktivasyon\">Aktivasyon Kodu</a>", Settings.VirtualPath);
        TextBox txt = new TextBox();
        txt.ID = "username";
        txt.CssClass = "noHtml emptyValidate mailValidate";
        CustomizeControl1.AddControl("Mail", txt);

        txt = new TextBox();
        txt.ID = "password";
        txt.TextMode = TextBoxMode.Password;
        CustomizeControl1.AddControl("Sifre", txt);

        CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);

        base.OnInit(e);
    }

    void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
    {
        if (!string.IsNullOrEmpty(((TextBox)controls["username"]).Text) & !string.IsNullOrEmpty(((TextBox)controls["password"]).Text))
        {
            using (BAYMYO.MultiSQLClient.MParameterCollection mparams = new BAYMYO.MultiSQLClient.MParameterCollection())
            {
                mparams.Add("Mail", ((TextBox)controls["username"]).Text, BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                mparams.Add("Sifre", System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((controls["password"] as TextBox).Text, "md5"), BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                using (Lib.Hesap m = Lib.HesapMethods.GetHesap(System.Data.CommandType.Text, "SELECT TOP(1) * FROM Hesap WHERE Mail=@Mail AND Sifre=@Sifre", mparams))
                {
                    if (m == null || BAYMYO.UI.Converts.NullToGuid(null).Equals(m.ID))
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('e-Mail ve Şifre bilgilerinizi kontrol ediniz!');", true);
                    else
                    {
                        if (!m.Aktivasyon)
                            CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Hesabınızda mail ile aktivason onayı gerçekleştirilmemiştir, lütfen mail adresinize gönderilen aktivason bağlantısını tıklayınız!");
                        else if (!m.Aktif)
                        {
                            switch (m.Tipi)
                            {
                                case Lib.HesapTuru.Admin:
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Yönetici başvurunuz incelemede henüz onaylanmamış durumdadır bizimle iletişim kurarak detaylı bilgi alabilirsiniz.");
                                    return;
                                case Lib.HesapTuru.Moderator:
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Hastane kayıt başvurunuz incelemede henüz onaylanmamış durumdadır bizimle iletişim kurarak detaylı bilgi alabilirsiniz.");
                                    return;
                                case Lib.HesapTuru.Editor:
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Doktor kayıt başvurunuz incelemede henüz onaylanmamış durumdadır bizimle iletişim kurarak detaylı bilgi alabilirsiniz.");
                                    return;
                                default:
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Hesabınız kendi isteğiniz ile yada yöneticilerimiz tarafından askıya alınmış olabilir bizimle iletişim kurarak detaylı bilgi alabilirsiniz.");
                                    return;
                            }
                        }
                        else if (string.IsNullOrEmpty(m.Roller))
                            CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, "Hesabınız üzerinde yetkilendirme işlemleri gerçekleştirilmediği için sisteme giriş yapamıyorsunuz lütfen bu konu ile ilgili bizimle iletişim kurunuz.");
                        else
                        {
                            CreateTicket(m);
                            string returnUrl = Request.QueryString["ReturnUrl"];
                            if (returnUrl == null) returnUrl = Settings.VirtualPath + "myaccount";
                            Response.Redirect(returnUrl, false);
                        }
                    }
                }
            }
        }
    }

    void CreateTicket(Lib.Hesap member)
    {
        try
        {
            Session["UserInfo"] = member;
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1, //Ticket versiyonu (şu andadaki güncel versiyon 1’dir)
                member.Adi, //ticket ile ilgili olan txtusername
                DateTime.Now, //şu anki zamanı alıyor.
                DateTime.Now.AddDays(30), //yaratılan cookie’nin zamanını ayarlıyor.
                false, //yaratılan cookie’nin IsPErsistent özelliğini false yapıyor.
                member.Roller,// kullanıcının rollerle ilgili bilgisini alıyor.
                FormsAuthentication.FormsCookiePath); // yaratılan cookie’nin yolunu belirtiyor.

            // FormsAuthenticationTicket ile yaratılan cookie’yi şifreliyoruz.
            string encTicket = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            // Eğer FormsAuthenticationTicket ile yaratılan cookie’nin expiration süresi
            //sınırsız ise, bu cookie’ye ticket nesnesinin Expiration süresi atanıyor.
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }
        catch (Exception)
        {
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}