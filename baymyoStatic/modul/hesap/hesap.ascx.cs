using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_hesap_hesap : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        if (!Settings.IsUserActive())
        {
            CustomizeControl1.PanelVisible = false;
            Response.Redirect(Settings.VirtualPath + "?l=1&ReturnUrl=" + Request.RawUrl, false);
            return;
        }

        using (Lib.Hesap hsp = Settings.CurrentUser())
        {
            if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(hsp.ID))
            {
                this.Page.Title = hsp.Adi + " " + hsp.Soyadi + " - Hesap Ayarları";

                CustomizeControl1.AddTitle("Hesap Bilgileri");
                CustomizeControl1.RemoveVisible = false;
                TextBox txt = new TextBox();
                txt.ID = "hspAdi";
                txt.Text = hsp.Adi;
                txt.CssClass = "noHtml emptyValidate";
                txt.MaxLength = 18;
                CustomizeControl1.AddControl("Adı", txt);

                txt = new TextBox();
                txt.ID = "hspSoyadi";
                txt.CssClass = "noHtml emptyValidate";
                txt.Text = hsp.Soyadi;
                txt.MaxLength = 15;
                CustomizeControl1.AddControl("Soyadı", txt);

                txt = new TextBox();
                txt.ID = "hspMail";
                txt.CssClass = "noHtml emptyValidate mailValidate";
                txt.Text = hsp.Mail;
                txt.MaxLength = 60;
                txt.Enabled = true;
                txt.ReadOnly = true;
                CustomizeControl1.AddControl("Mail", txt, "Sisteme giriş yapmak için kullanılacaktır.");

                txt = new TextBox();
                txt.ID = "hspSifre";
                txt.CssClass = "noHtml";
                txt.ToolTip = hsp.Sifre;
                txt.TextMode = TextBoxMode.Password;
                txt.MaxLength = 25;
                CustomizeControl1.AddControl("Şifre", txt, "Şifreyi değiştirmek istemiyorsanız boş bırakınız!");

                DateTimeControl cnt = this.Page.LoadControl(Settings.DateTimeControlPath) as DateTimeControl;
                cnt.ID = "DogumTarihi";
                cnt.OlusturmaTipi = DateTimeControl.CreateType.DogumTarihi;
                CustomizeControl1.AddControl("Doğum Tarihi", cnt, "* Seçilmesi zorunlu alan.");
                cnt.TarihSaat = hsp.DogumTarihi;

                DropDownList ddl = new DropDownList();
                ddl.ID = "hspCinsiyet";
                ddl.Width = 195;
                ddl.DataValueField = "Key";
                ddl.DataTextField = "Value";
                ddl.DataSource = Settings.HesapCinsiyetleri();
                ddl.DataBind();
                ddl.SelectedValue = BAYMYO.UI.Converts.NullToByte(hsp.Cinsiyet).ToString();
                CustomizeControl1.AddControl("Cinsiyet", ddl);

                CheckBox chk = new CheckBox();
                chk.ID = "hspAbonelik";
                chk.Checked = hsp.Abonelik;
                CustomizeControl1.AddControl("Abonelik", chk);

                switch (hsp.Tipi)
                {
                    case Lib.HesapTuru.Moderator:
                        CustomizeControl1.AddTitle("Hastane Bilgileri");

                        ddl = new DropDownList();
                        ddl.ID = "prfUnvanID";
                        ddl.Width = 300;
                        ddl.DataMember = "Kategori";
                        ddl.DataValueField = "ID";
                        ddl.DataTextField = "Adi";
                        ddl.DataSource = Lib.KategoriMethods.GetMenu("hastaneunvan", true);
                        ddl.DataBind();
                        ddl.SelectedValue = hsp.ProfilObject.Unvan;
                        CustomizeControl1.AddControl("Hastane Ünvanı", ddl, "* Seçilmesi zorunlu alan!");

                        ddl = new DropDownList();
                        ddl.ID = "prfUzmanlikAlaniID";
                        ddl.Width = 300;
                        ddl.DataMember = "Kategori";
                        ddl.DataValueField = "ID";
                        ddl.DataTextField = "Adi";
                        ddl.DataSource = Lib.KategoriMethods.GetMenu("hastaneuzmanlik", true);
                        ddl.DataBind();
                        ddl.SelectedValue = hsp.ProfilObject.UzmanlikAlaniID;
                        CustomizeControl1.AddControl("Uzmanlık Alanı", ddl, "* Seçilmesi zorunlu alan!");

                        Image hstImg = new Image();
                        hstImg.ID = "prfImageUrl";
                        hstImg.Width = 136;
                        hstImg.ImageUrl = Settings.ImagesPath + ((!string.IsNullOrEmpty(hsp.ProfilObject.ResimUrl)) ? "profil/" + hsp.ProfilObject.ResimUrl : "yok.png");
                        CustomizeControl1.AddControl("Profil Resimi", hstImg);

                        FileUpload hstFlu = new FileUpload();
                        hstFlu.ID = "prfResimUrl";
                        CustomizeControl1.AddControl("Yeni Logo", hstFlu, "Logo Genişliği <b>136px</b> Yüksekliği <b>150px</b> olmalı.");

                        txt = new TextBox();
                        txt.ID = "prfUrl";
                        txt.Text = hsp.ProfilObject.Url;
                        txt.CssClass = "noHtml smallCharNumber emptyValidate";
                        txt.MaxLength = 50;
                        txt.Enabled = true;
                        txt.ReadOnly = true;
                        CustomizeControl1.AddControl("Url", txt, "Profil bağlantı adresi olacaktır. Ör. " + Settings.SiteUrl + "<b class=\"toolTip titleFormat1\" title=\"Adres çubuğunda sitemizin adının yanına '/' ters slaş yaparak burada belirteceğiniz isim ile profilinizin görüntülenmesini sağlar.\">adinizsoyadiniz</b>");

                        txt = new TextBox();
                        txt.ID = "prfAdi";
                        txt.CssClass = "noHtml emptyValidate";
                        txt.MaxLength = 100;
                        CustomizeControl1.AddControl("Hastane Adı", txt, "Sayfanızda görüntülenecek olan hastane adını giriniz.");

                        txt = new TextBox();
                        txt.ID = "prfMail";
                        txt.CssClass = "noHtml emptyValidate mailValidate";
                        txt.Text = hsp.ProfilObject.Mail;
                        txt.MaxLength = 60;
                        CustomizeControl1.AddControl("Profil Mail", txt, "Profilde gösterilecek olan mail adresidir.");

                        txt = new TextBox();
                        txt.ID = "prfHakkimda";
                        txt.CssClass = "noHtml";
                        txt.Text = hsp.ProfilObject.Hakkimda;
                        txt.Height = 150;
                        txt.TextMode = TextBoxMode.MultiLine;
                        txt.MaxLength = 1000;
                        CustomizeControl1.AddControl("Hakkimda", txt, "Bu alana <b>1000</b> karaktere kadar bilgi girişi yapabilirsiniz.");

                        using (Lib.CalismaAlani cls = Lib.CalismaAlaniMethods.GetDefault(hsp.ID))
                        {
                            CustomizeControl1.AddTitle("Hastane Adres Bilgileri");

                            txt = new TextBox();
                            txt.ID = "clsTelefon";
                            txt.MaxLength = 16;
                            txt.Text = cls.Telefon;
                            txt.CssClass = "noHtml isNumber emptyValidate";
                            CustomizeControl1.AddControl("Telefon", txt);

                            txt = new TextBox();
                            txt.ID = "clsAdres";
                            txt.MaxLength = 100;
                            txt.Text = cls.Adres;
                            txt.CssClass = "noHtml emptyValidate";
                            CustomizeControl1.AddControl("Adres", txt);

                            txt = new TextBox();
                            txt.ID = "clsSemt";
                            txt.MaxLength = 30;
                            txt.Text = cls.Semt;
                            txt.CssClass = "noHtml emptyValidate";
                            CustomizeControl1.AddControl("Semt(İlçe)", txt);

                            txt = new TextBox();
                            txt.ID = "clsSehir";
                            txt.MaxLength = 30;
                            txt.Text = cls.Sehir;
                            txt.CssClass = "noHtml emptyValidate";
                            CustomizeControl1.AddControl("Sehir(İL)", txt, "Belirteceğiniz <b>'İL'</b> sizi harita üzerinde bulunmanızı sağlayacaktır. Lütfen geçerli <b>'İL'</b> adı giriniz!");

                            txt = new TextBox();
                            txt.ID = "clsWebSitesi";
                            txt.MaxLength = 60;
                            txt.Text = cls.WebSitesi;
                            txt.CssClass = "noHtml";
                            CustomizeControl1.AddControl("Web Sitesi", txt, "Lütfen başına 'Http://' eklemeden giriniz. Ör. www.sitenizinadi.com");

                            chk = new CheckBox();
                            chk.ID = "Randevu";
                            chk.Checked = cls.Randevu;
                            CustomizeControl1.AddControl("Randevu Aktif", chk);
                        }

                        CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(moderatorHesap_SubmitClick);
                        break;
                    case Lib.HesapTuru.Editor:
                        CustomizeControl1.AddTitle("Profil Bilgileri");

                        ddl = new DropDownList();
                        ddl.ID = "prfUzmanlikAlaniID";
                        ddl.Width = 300;
                        ddl.DataMember = "Kategori";
                        ddl.DataValueField = "ID";
                        ddl.DataTextField = "Adi";
                        ddl.DataSource = Lib.KategoriMethods.GetMenu("uzmanlik", true);
                        ddl.DataBind();
                        ddl.SelectedValue = hsp.ProfilObject.UzmanlikAlaniID;
                        CustomizeControl1.AddControl("Uzmanlık Alanı", ddl, "* Seçilmesi zorunlu alan!");

                        ddl = new DropDownList();
                        ddl.ID = "prfUnvanID";
                        ddl.Width = 300;
                        ddl.DataMember = "Kategori";
                        ddl.DataValueField = "ID";
                        ddl.DataTextField = "Adi";
                        ddl.DataSource = Lib.KategoriMethods.GetMenu("unvan", true);
                        ddl.DataBind();
                        ddl.SelectedValue = hsp.ProfilObject.Unvan;
                        CustomizeControl1.AddControl("Ünvan", ddl, "* Seçilmesi zorunlu alan!");

                        Image img = new Image();
                        img.ID = "prfImageUrl";
                        img.Width = 136;
                        img.ImageUrl = Settings.ImagesPath + ((!string.IsNullOrEmpty(hsp.ProfilObject.ResimUrl)) ? "profil/" + hsp.ProfilObject.ResimUrl : "yok.png");
                        CustomizeControl1.AddControl("Profil Resimi", img);

                        FileUpload flu = new FileUpload();
                        flu.ID = "prfResimUrl";
                        CustomizeControl1.AddControl("Yeni Resimi", flu, "Resim Genişliği <b>136px</b> Yüksekliği <b>170px</b> olmalı.");

                        txt = new TextBox();
                        txt.ID = "prfUrl";
                        txt.Text = hsp.ProfilObject.Url;
                        txt.CssClass = "noHtml smallCharNumber emptyValidate";
                        txt.MaxLength = 50;
                        txt.Enabled = true;
                        txt.ReadOnly = true;
                        CustomizeControl1.AddControl("Url", txt, "Profil bağlantı adresi olacaktır. Ör. " + Settings.SiteUrl + "<b class=\"toolTip titleFormat1\" title=\"Adres çubuğunda sitemizin adının yanına '/' ters slaş yaparak burada belirteceğiniz isim ile profilinizin görüntülenmesini sağlar.\">adinizsoyadiniz</b>");

                        txt = new TextBox();
                        txt.ID = "prfDiplomaNo";
                        txt.Text = hsp.ProfilObject.DiplomaNo;
                        txt.CssClass = "noHtml isNumber emptyValidate";
                        txt.MaxLength = 15;
                        CustomizeControl1.AddControl("Diploma No", txt);

                        txt = new TextBox();
                        txt.ID = "prfTCKimlikNo";
                        txt.Text = hsp.ProfilObject.TCKimlikNo;
                        txt.CssClass = "noHtml isNumber emptyValidate";
                        txt.MaxLength = 11;
                        CustomizeControl1.AddControl("T.C. Kimlik No", txt);

                        txt = new TextBox();
                        txt.ID = "prfMail";
                        txt.CssClass = "noHtml emptyValidate mailValidate";
                        txt.Text = hsp.ProfilObject.Mail;
                        txt.MaxLength = 60;
                        CustomizeControl1.AddControl("Profil Mail", txt, "Profilde gösterilecek olan mail adresidir.");

                        txt = new TextBox();
                        txt.ID = "prfHakkimda";
                        txt.CssClass = "noHtml";
                        txt.Text = hsp.ProfilObject.Hakkimda;
                        txt.Height = 150;
                        txt.TextMode = TextBoxMode.MultiLine;
                        txt.MaxLength = 1000;
                        CustomizeControl1.AddControl("Hakkimda", txt, "Bu alana <b>1000</b> karaktere kadar bilgi girişi yapabilirsiniz.");

                        CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(editorHesap_SubmitClick);
                        break;
                    default:
                        CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(standartHesap_SubmitClick);
                        break;
                }
            }
        }

        base.OnInit(e);
    }

    void standartHesap_SubmitClick(SortedDictionary<string, Control> controls)
    {
        if (Settings.IsUserActive()
            & !string.IsNullOrEmpty(((TextBox)controls["hspAdi"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["hspMail"]).Text))
            using (Lib.Hesap hsp = Settings.CurrentUser())
            {
                hsp.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                hsp.Adi = ((TextBox)controls["hspAdi"]).Text;
                hsp.Soyadi = ((TextBox)controls["hspSoyadi"]).Text;
                hsp.Mail = ((TextBox)controls["hspMail"]).Text;
                if (!string.IsNullOrEmpty((controls["hspSifre"] as TextBox).Text.Trim()))
                {
                    string sifre = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((controls["hspSifre"] as TextBox).Text, "md5");
                    if (!hsp.Sifre.Equals(sifre))
                    {
                        hsp.Sifre = sifre;
                        string m_MailMesaj = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath) + "PasswordNew.msg");
                        m_MailMesaj = m_MailMesaj.Replace("%SiteUrl%", Settings.SiteUrl);
                        m_MailMesaj = m_MailMesaj.Replace("%SiteTitle%", Settings.SiteTitle);
                        m_MailMesaj = m_MailMesaj.Replace("%VirtualPath%", Settings.VirtualPath);
                        m_MailMesaj = m_MailMesaj.Replace("%IP%", Context.Request.ServerVariables["REMOTE_ADDR"].ToString());
                        m_MailMesaj = m_MailMesaj.Replace("%ID%", hsp.ID.ToString());
                        m_MailMesaj = m_MailMesaj.Replace("%Adi%", hsp.Adi).Replace("%Soyadi%", hsp.Soyadi);
                        m_MailMesaj = m_MailMesaj.Replace("%Mail%", hsp.Mail);
                        m_MailMesaj = m_MailMesaj.Replace("%Sifre%", ((TextBox)controls["hspSifre"]).Text);
                        Settings.SendMail(hsp.Mail, hsp.Adi + " " + hsp.Soyadi, "Şifre Değiştirildi", m_MailMesaj, true);
                        m_MailMesaj = null;
                    }
                }
                hsp.DogumTarihi = ((DateTimeControl)controls["DogumTarihi"]).TarihSaat;
                hsp.Cinsiyet = Settings.HesapCinsiyeti(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["hspCinsiyet"]).SelectedValue));
                hsp.OnayKodu = Settings.YeniOnayKodu();
                hsp.Abonelik = ((CheckBox)controls["hspAbonelik"]).Checked;

                if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(hsp.ID))
                    if (Lib.HesapMethods.Update(hsp) > 0)
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme işleminiz başarılı bir şekilde tamamlandı!');", true);
            }
    }

    void editorHesap_SubmitClick(SortedDictionary<string, Control> controls)
    {
        if (Settings.IsUserActive()
            & !string.IsNullOrEmpty(((TextBox)controls["hspAdi"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["hspMail"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["prfUrl"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["prfDiplomaNo"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["prfMail"]).Text)
            & ((DropDownList)controls["prfUnvanID"]).SelectedIndex > 0
            & ((DropDownList)controls["prfUzmanlikAlaniID"]).SelectedIndex > 0)
            using (Lib.Hesap hsp = Settings.CurrentUser())
            {
                hsp.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                hsp.Adi = ((TextBox)controls["hspAdi"]).Text;
                hsp.Soyadi = ((TextBox)controls["hspSoyadi"]).Text;
                hsp.Mail = ((TextBox)controls["hspMail"]).Text;
                if (!string.IsNullOrEmpty((controls["hspSifre"] as TextBox).Text.Trim()))
                {
                    string sifre = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((controls["hspSifre"] as TextBox).Text, "md5");
                    if (!hsp.Sifre.Equals(sifre))
                    {
                        hsp.Sifre = sifre;
                        string m_MailMesaj = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath) + "PasswordNew.msg");
                        m_MailMesaj = m_MailMesaj.Replace("%SiteUrl%", Settings.SiteUrl);
                        m_MailMesaj = m_MailMesaj.Replace("%SiteTitle%", Settings.SiteTitle);
                        m_MailMesaj = m_MailMesaj.Replace("%VirtualPath%", Settings.VirtualPath);
                        m_MailMesaj = m_MailMesaj.Replace("%IP%", Context.Request.ServerVariables["REMOTE_ADDR"].ToString());
                        m_MailMesaj = m_MailMesaj.Replace("%ID%", hsp.ID.ToString());
                        m_MailMesaj = m_MailMesaj.Replace("%Adi%", hsp.Adi).Replace("%Soyadi%", hsp.Soyadi);
                        m_MailMesaj = m_MailMesaj.Replace("%Mail%", hsp.Mail);
                        m_MailMesaj = m_MailMesaj.Replace("%Sifre%", ((TextBox)controls["hspSifre"]).Text);
                        Settings.SendMail(hsp.Mail, hsp.Adi + " " + hsp.Soyadi, "Şifre Değiştirildi", m_MailMesaj, true);
                        m_MailMesaj = null;
                    }
                }
                hsp.DogumTarihi = ((DateTimeControl)controls["DogumTarihi"]).TarihSaat;
                hsp.Cinsiyet = Settings.HesapCinsiyeti(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["hspCinsiyet"]).SelectedValue));
                hsp.OnayKodu = Settings.YeniOnayKodu();
                hsp.Abonelik = ((CheckBox)controls["hspAbonelik"]).Checked;

                hsp.ProfilObject.DiplomaNo = ((TextBox)controls["prfDiplomaNo"]).Text;
                hsp.ProfilObject.TCKimlikNo = ((TextBox)controls["prfTCKimlikNo"]).Text;
                hsp.ProfilObject.Adi = string.Empty;
                hsp.ProfilObject.Mail = ((TextBox)controls["prfMail"]).Text;
                hsp.ProfilObject.Hakkimda = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["prfHakkimda"]).Text, 1000);
                hsp.ProfilObject.Unvan = ((DropDownList)controls["prfUnvanID"]).SelectedItem.Value;
                hsp.ProfilObject.UzmanlikAlaniID = ((DropDownList)controls["prfUzmanlikAlaniID"]).SelectedItem.Value;

                if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(hsp.ID))
                {
                    if (Lib.HesapMethods.Update(hsp) > 0)
                    {
                        if ((controls["prfResimUrl"] as FileUpload).HasFile)
                            if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "profil/" + hsp.ProfilObject.ResimUrl)))
                                hsp.ProfilObject.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["prfResimUrl"] as FileUpload, hsp.Adi + " " + hsp.Soyadi, Server.MapPath(Settings.ImagesPath + "profil/"), 137, true); ;
                        if (BAYMYO.UI.Converts.NullToGuid(null).Equals(hsp.ProfilObject.ID))
                        {
                            hsp.ProfilObject.ID = hsp.ID;
                            switch (Lib.ProfilMethods.Insert(hsp.ProfilObject).ToString())
                            {
                                case "":
                                case "0":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Profil bilgilerinizi kontrol ediniz ve tekrar deneyiniz!");
                                    break;
                                case "URL":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz 'URL' başka bir kullanıcı tarafından kullanılmaktadır. Lütfen başka bir 'URL' yazarak tekrar deneyiniz.");
                                    break;
                                case "TCKIMLIKNO":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz 'T.C. Kimlik Numarası' başka bir kullanıcı tarafından kullanılmaktadır. Lütfen 'T.C. Kimik Numaranızı' kontrol ediniz ve tekrar deneyiniz.");
                                    break;
                                case "DIPLOMANO":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz 'Diploma Numarası' başka bir kullanıcı tarafından kullanılmaktadır. Lütfen 'Diploma Numaranızı' kontrol ediniz ve tekrar deneyiniz.");
                                    break;
                                default:
                                    if ((controls["prfResimUrl"] as FileUpload).HasFile)
                                        ((Image)controls["prfImageUrl"]).ImageUrl = Settings.ImagesPath + "profil/" + hsp.ProfilObject.ResimUrl;
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                                    break;
                            }
                        }
                        else
                        {
                            switch (Lib.ProfilMethods.Update(hsp.ProfilObject).ToString())
                            {
                                case "":
                                case "0":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Profil bilgilerinizi kontrol ediniz ve tekrar deneyiniz!");
                                    break;
                                case "URL":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz 'URL' başka bir kullanıcı tarafından kullanılmaktadır. Lütfen başka bir 'URL' yazarak tekrar deneyiniz.");
                                    break;
                                case "TCKIMLIKNO":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz 'T.C. Kimlik Numarası' başka bir kullanıcı tarafından kullanılmaktadır. Lütfen 'T.C. Kimik Numaranızı' kontrol ediniz ve tekrar deneyiniz.");
                                    break;
                                case "DIPLOMANO":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz 'Diploma Numarası' başka bir kullanıcı tarafından kullanılmaktadır. Lütfen 'Diploma Numaranızı' kontrol ediniz ve tekrar deneyiniz.");
                                    break;
                                default:
                                    if ((controls["prfResimUrl"] as FileUpload).HasFile)
                                        ((Image)controls["prfImageUrl"]).ImageUrl = Settings.ImagesPath + "profil/" + hsp.ProfilObject.ResimUrl;
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                                    break;
                            }
                        }
                    }
                }
            }
    }

    void moderatorHesap_SubmitClick(SortedDictionary<string, Control> controls)
    {
        if (Settings.IsUserActive()
            & !string.IsNullOrEmpty(((TextBox)controls["hspAdi"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["hspMail"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["prfUrl"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["prfAdi"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["prfMail"]).Text)
            & ((DropDownList)controls["prfUnvanID"]).SelectedIndex > 0
            & ((DropDownList)controls["prfUzmanlikAlaniID"]).SelectedIndex > 0)
            using (Lib.Hesap hsp = Settings.CurrentUser())
            {
                hsp.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                hsp.Adi = ((TextBox)controls["hspAdi"]).Text;
                hsp.Soyadi = ((TextBox)controls["hspSoyadi"]).Text;
                hsp.Mail = ((TextBox)controls["hspMail"]).Text;
                if (!string.IsNullOrEmpty((controls["hspSifre"] as TextBox).Text.Trim()))
                {
                    string sifre = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((controls["hspSifre"] as TextBox).Text, "md5");
                    if (!hsp.Sifre.Equals(sifre))
                    {
                        hsp.Sifre = sifre;
                        string m_MailMesaj = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath) + "PasswordNew.msg");
                        m_MailMesaj = m_MailMesaj.Replace("%SiteUrl%", Settings.SiteUrl);
                        m_MailMesaj = m_MailMesaj.Replace("%SiteTitle%", Settings.SiteTitle);
                        m_MailMesaj = m_MailMesaj.Replace("%VirtualPath%", Settings.VirtualPath);
                        m_MailMesaj = m_MailMesaj.Replace("%IP%", Context.Request.ServerVariables["REMOTE_ADDR"].ToString());
                        m_MailMesaj = m_MailMesaj.Replace("%ID%", hsp.ID.ToString());
                        m_MailMesaj = m_MailMesaj.Replace("%Adi%", hsp.Adi).Replace("%Soyadi%", hsp.Soyadi);
                        m_MailMesaj = m_MailMesaj.Replace("%Mail%", hsp.Mail);
                        m_MailMesaj = m_MailMesaj.Replace("%Sifre%", ((TextBox)controls["hspSifre"]).Text);
                        Settings.SendMail(hsp.Mail, hsp.Adi + " " + hsp.Soyadi, "Şifre Değiştirildi", m_MailMesaj, true);
                        m_MailMesaj = null;
                    }
                }
                hsp.DogumTarihi = ((DateTimeControl)controls["DogumTarihi"]).TarihSaat;
                hsp.Cinsiyet = Settings.HesapCinsiyeti(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["hspCinsiyet"]).SelectedValue));
                hsp.OnayKodu = Settings.YeniOnayKodu();
                hsp.Abonelik = ((CheckBox)controls["hspAbonelik"]).Checked;

                hsp.ProfilObject.DiplomaNo = string.Empty;
                hsp.ProfilObject.TCKimlikNo = string.Empty;
                hsp.ProfilObject.Adi = ((TextBox)controls["prfAdi"]).Text;
                hsp.ProfilObject.Mail = ((TextBox)controls["prfMail"]).Text;
                hsp.ProfilObject.Hakkimda = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["prfHakkimda"]).Text, 1000);
                hsp.ProfilObject.Unvan = ((DropDownList)controls["prfUnvanID"]).SelectedItem.Value;
                hsp.ProfilObject.UzmanlikAlaniID = ((DropDownList)controls["prfUzmanlikAlaniID"]).SelectedItem.Value;

                if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(hsp.ID))
                {
                    if (Lib.HesapMethods.Update(hsp) > 0)
                    {
                        if ((controls["prfResimUrl"] as FileUpload).HasFile)
                            if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "profil/" + hsp.ProfilObject.ResimUrl)))
                                hsp.ProfilObject.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["prfResimUrl"] as FileUpload, hsp.Adi + " " + hsp.Soyadi, Server.MapPath(Settings.ImagesPath + "profil/"), 137, true); ;
                        if (BAYMYO.UI.Converts.NullToGuid(null).Equals(hsp.ProfilObject.ID))
                        {
                            hsp.ProfilObject.ID = hsp.ID;
                            switch (Lib.ProfilMethods.Insert(hsp.ProfilObject).ToString())
                            {
                                case "":
                                case "0":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Profil bilgilerinizi kontrol ediniz ve tekrar deneyiniz!");
                                    break;
                                case "URL":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz 'URL' başka bir kullanıcı tarafından kullanılmaktadır. Lütfen başka bir 'URL' yazarak tekrar deneyiniz.");
                                    break;
                                case "HASTANEADI":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz 'Hastane Adı' başka bir hastane tarafından kullanılmaktadır. Lütfen 'Hastane Adınızı' kontrol ediniz ve tekrar deneyiniz.");
                                    break;
                                default:
                                    using (Lib.CalismaAlani m = Lib.CalismaAlaniMethods.GetCalismaAlani(hsp.ID))
                                    {
                                        m.HesapID = hsp.ID;
                                        m.Kurum = ((TextBox)controls["prfAdi"]).Text;
                                        m.Telefon = ((TextBox)controls["clsTelefon"]).Text;
                                        m.Adres = ((TextBox)controls["clsAdres"]).Text;
                                        m.Semt = ((TextBox)controls["clsSemt"]).Text;
                                        m.Sehir = ((TextBox)controls["clsSehir"]).Text;
                                        m.WebSitesi = ((TextBox)controls["clsWebSitesi"]).Text.Replace("http://", "").Replace("/", "");
                                        m.Randevu = ((CheckBox)controls["clsRandevu"]).Checked;
                                        m.Varsayilan = true;
                                        m.Aktif = true;
                                        if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(m.ID))
                                            Lib.CalismaAlaniMethods.Update(m);
                                        else
                                            Lib.CalismaAlaniMethods.Insert(m);
                                    }
                                    if ((controls["prfResimUrl"] as FileUpload).HasFile)
                                        ((Image)controls["prfImageUrl"]).ImageUrl = Settings.ImagesPath + "profil/" + hsp.ProfilObject.ResimUrl;
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                                    break;
                            }
                        }
                        else
                        {
                            switch (Lib.ProfilMethods.Update(hsp.ProfilObject).ToString())
                            {
                                case "":
                                case "0":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Profil bilgilerinizi kontrol ediniz ve tekrar deneyiniz!");
                                    break;
                                case "URL":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz 'URL' başka bir kullanıcı tarafından kullanılmaktadır. Lütfen başka bir 'URL' yazarak tekrar deneyiniz.");
                                    break;
                                case "HASTANEADI":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz 'Hastane Adı' başka bir hastane tarafından kullanılmaktadır. Lütfen 'Hastane Adınızı' kontrol ediniz ve tekrar deneyiniz.");
                                    break;
                                default:
                                    using (Lib.CalismaAlani m = Lib.CalismaAlaniMethods.GetCalismaAlani(hsp.ID))
                                    {
                                        m.HesapID = hsp.ID;
                                        m.Kurum = ((TextBox)controls["prfAdi"]).Text;
                                        m.Telefon = ((TextBox)controls["clsTelefon"]).Text;
                                        m.Adres = ((TextBox)controls["clsAdres"]).Text;
                                        m.Semt = ((TextBox)controls["clsSemt"]).Text;
                                        m.Sehir = ((TextBox)controls["clsSehir"]).Text;
                                        m.WebSitesi = ((TextBox)controls["clsWebSitesi"]).Text.Replace("http://", "").Replace("/", "");
                                        m.Randevu = ((CheckBox)controls["clsRandevu"]).Checked;
                                        m.Varsayilan = true;
                                        m.Aktif = true;
                                        if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(m.ID))
                                            Lib.CalismaAlaniMethods.Update(m);
                                        else
                                            Lib.CalismaAlaniMethods.Insert(m);
                                    }
                                    if ((controls["prfResimUrl"] as FileUpload).HasFile)
                                        ((Image)controls["prfImageUrl"]).ImageUrl = Settings.ImagesPath + "profil/" + hsp.ProfilObject.ResimUrl;
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                                    break;
                            }
                        }
                    }
                }
            }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
}