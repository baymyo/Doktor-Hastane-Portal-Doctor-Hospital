using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_default_register : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        if (Settings.IsUserActive())
        {
            Response.Redirect(Settings.VirtualPath + "?l=5", false);
            return;
        }
        CustomizeControl1.RemoveVisible = false;
        pnlUyelikTipi.Visible = false;
        switch (BAYMYO.UI.Converts.NullToString(Request.QueryString["type"]))
        {
            case "2":
            case "editor":
                CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Doktor", "Başvuru Formu");
                StandartHesap("Doktor ");
                EditorHesap();
                CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(editorHesap_SubmitClick);
                break;
            case "3":
            case "moderator":
                CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Hastane", "Kayıt Formu");
                StandartHesap("Yetkili ");
                ModeratorHesap();
                CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(moderatorHesap_SubmitClick);
                break;
            case "4":
            case "standart":
                CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Standart", "Üye Formu");
                StandartHesap("");
                CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(standartHesap_SubmitClick);
                break;
            default:
                pnlUyelikTipi.Visible = true;
                CustomizeControl1.Visible = false;
                break;
        }
        base.OnInit(e);
    }

    void StandartHesap(string onEk)
    {
        TextBox txt = new TextBox();
        txt.ID = "rgsAdi";
        txt.CssClass = "noHtml emptyValidate";
        txt.MaxLength = 18;
        CustomizeControl1.AddControl(onEk + "Adı", txt);

        txt = new TextBox();
        txt.ID = "rgsSoyadi";
        txt.MaxLength = 15;
        CustomizeControl1.AddControl(onEk + "Soyadı", txt);

        txt = new TextBox();
        txt.ID = "rgsMail";
        txt.CssClass = "noHtml emptyValidate mailValidate";
        txt.MaxLength = 60;
        CustomizeControl1.AddControl("Giriş Maili", txt, "* Bu mail adresi kimseyle paylaşılmaz sadece sisteme giriş için kullanılır.");

        txt = new TextBox();
        txt.ID = "rgsSifre";
        txt.CssClass = "noHtml emptyValidate";
        txt.TextMode = TextBoxMode.Password;
        txt.MaxLength = 25;
        CustomizeControl1.AddControl("Şifre", txt, "* Sisteme giriş yapmanız için gerekli olacak.");

        DateTimeControl cnt = this.Page.LoadControl(Settings.DateTimeControlPath) as DateTimeControl;
        cnt.ID = "DogumTarihi";
        cnt.OlusturmaTipi = DateTimeControl.CreateType.DogumTarihi;
        CustomizeControl1.AddControl("Doğum Tarihi", cnt, "* Seçilmesi zorunlu alan.");

        DropDownList ddl = new DropDownList();
        ddl.ID = "rgsCinsiyet";
        ddl.Width = 195;
        ddl.DataValueField = "Key";
        ddl.DataTextField = "Value";
        ddl.DataSource = Settings.HesapCinsiyetleri();
        ddl.DataBind();
        CustomizeControl1.AddControl("Cinsiyet", ddl);

        CheckBox chk = new CheckBox();
        chk.ID = "rgsAbonelik";
        chk.Checked = true;
        CustomizeControl1.AddControl("Abonelik", chk);
    }

    void EditorHesap()
    {
        CustomizeControl1.AddTitle("Profil Bilgileri");

        DropDownList ddl = new DropDownList();
        ddl.ID = "prfUzmanlikAlaniID";
        ddl.Width = 300;
        ddl.DataMember = "Kategori";
        ddl.DataValueField = "ID";
        ddl.DataTextField = "Adi";
        ddl.DataSource = Lib.KategoriMethods.GetMenu("uzmanlik", true);
        ddl.DataBind();
        CustomizeControl1.AddControl("Uzmanlık Alanı", ddl, "* Seçilmesi zorunlu alan!");

        ddl = new DropDownList();
        ddl.ID = "prfUnvanID";
        ddl.Width = 300;
        ddl.DataMember = "Kategori";
        ddl.DataValueField = "ID";
        ddl.DataTextField = "Adi";
        ddl.DataSource = Lib.KategoriMethods.GetMenu("unvan", true);
        ddl.DataBind();
        CustomizeControl1.AddControl("Ünvan", ddl, "* Seçilmesi zorunlu alan!");

        FileUpload flu = new FileUpload();
        flu.ID = "prfResimUrl";
        CustomizeControl1.AddControl("Profil Resimi", flu, "Resim Genişliği <b>136px</b> Yüksekliği <b>150px</b> olmalı.");

        TextBox txt = new TextBox();
        txt.ID = "prfUrl";
        txt.CssClass = "noHtml smallCharNumber emptyValidate";
        txt.MaxLength = 50;
        CustomizeControl1.AddControl("Url", txt, "Profil bağlantı adresi olacaktır. Ör. " + Settings.SiteUrl + "<b class=\"toolTip titleFormat1\" title=\"Adres çubuğunda sitemizin adının yanına '/' ters slaş yaparak burada belirteceğiniz isim ile profilinizin görüntülenmesini sağlar.\">adinizsoyadiniz</b>");

        txt = new TextBox();
        txt.ID = "prfDiplomaNo";
        txt.CssClass = "noHtml isNumber emptyValidate";
        txt.MaxLength = 15;
        CustomizeControl1.AddControl("Diploma No", txt, "* Bu alana gireceğiniz bilgi doğrultusunda başvurunuz onaylanacaktır.");

        txt = new TextBox();
        txt.ID = "prfTCKimlikNo";
        txt.CssClass = "noHtml isNumber emptyValidate";
        txt.MaxLength = 11;
        CustomizeControl1.AddControl("T.C. Kimlik No", txt);

        txt = new TextBox();
        txt.ID = "prfMail";
        txt.CssClass = "noHtml emptyValidate mailValidate";
        txt.MaxLength = 60;
        CustomizeControl1.AddControl("Profil Maili", txt, "Herkese açık mail adresi olacaktır ve bu mail profilinizde görüntülenir.");

        txt = new TextBox();
        txt.ID = "prfHakkimda";
        txt.CssClass = "noHtml";
        txt.Height = 150;
        txt.TextMode = TextBoxMode.MultiLine;
        txt.MaxLength = 1000;
        CustomizeControl1.AddControl("Hakkimda", txt, "Bu alana <b>1000</b> karaktere kadar bilgi girişi yapabilirsiniz.");
    }

    void ModeratorHesap()
    {
        CustomizeControl1.AddTitle("Hastane Genel Bilgileri");

        DropDownList ddl = new DropDownList();
        ddl.ID = "prfUnvanID";
        ddl.Width = 300;
        ddl.DataMember = "Kategori";
        ddl.DataValueField = "ID";
        ddl.DataTextField = "Adi";
        ddl.DataSource = Lib.KategoriMethods.GetMenu("hastaneunvan", true);
        ddl.DataBind();
        CustomizeControl1.AddControl("Hastane Ünvanı", ddl, "* Seçilmesi zorunlu alan!");

        ddl = new DropDownList();
        ddl.ID = "prfUzmanlikAlaniID";
        ddl.Width = 300;
        ddl.DataMember = "Kategori";
        ddl.DataValueField = "ID";
        ddl.DataTextField = "Adi";
        ddl.DataSource = Lib.KategoriMethods.GetMenu("hastaneuzmanlik", true);
        ddl.DataBind();
        CustomizeControl1.AddControl("Uzmanlık Alanı", ddl, "* Seçilmesi zorunlu alan!");

        FileUpload flu = new FileUpload();
        flu.ID = "prfResimUrl";
        CustomizeControl1.AddControl("Hastane Logo", flu, "Logo Genişliği <b>136px</b> Yüksekliği <b>150px</b> olmalı.");

        TextBox txt = new TextBox();
        txt.ID = "prfUrl";
        txt.CssClass = "noHtml smallCharNumber emptyValidate";
        txt.MaxLength = 50;
        CustomizeControl1.AddControl("Url", txt, "Profil bağlantı adresi olacaktır. Ör. " + Settings.SiteUrl + "<b class=\"toolTip titleFormat1\" title=\"Adres çubuğunda sitemizin adının yanına '/' ters slaş yaparak burada belirteceğiniz isim ile profilinizin görüntülenmesini sağlar.\">hastaneadi</b>");

        txt = new TextBox();
        txt.ID = "prfAdi";
        txt.CssClass = "noHtml emptyValidate";
        txt.MaxLength = 60;
        CustomizeControl1.AddControl("Hastane Adı", txt, "Sayfanızda görüntülenecek olan hastane adını giriniz.");

        txt = new TextBox();
        txt.ID = "prfMail";
        txt.CssClass = "noHtml emptyValidate mailValidate";
        txt.MaxLength = 60;
        CustomizeControl1.AddControl("Hastane Maili", txt, "Herkese açık mail adresi olacaktır ve bu mail profilinizde görüntülenir.");

        txt = new TextBox();
        txt.ID = "prfHakkimda";
        txt.CssClass = "noHtml";
        txt.Height = 150;
        txt.TextMode = TextBoxMode.MultiLine;
        txt.MaxLength = 1000;
        CustomizeControl1.AddControl("Hakkinda", txt, "Bu alana <b>1000</b> karaktere kadar bilgi girişi yapabilirsiniz.");

        CustomizeControl1.AddTitle("Hastane İletişim ve Adres Bilgileri");

        txt = new TextBox();
        txt.ID = "clsTelefon";
        txt.MaxLength = 16;
        txt.CssClass = "noHtml isNumber emptyValidate";
        CustomizeControl1.AddControl("Telefon", txt);

        txt = new TextBox();
        txt.ID = "clsAdres";
        txt.MaxLength = 100;
        txt.CssClass = "noHtml emptyValidate";
        CustomizeControl1.AddControl("Adres", txt);

        txt = new TextBox();
        txt.ID = "clsSemt";
        txt.MaxLength = 30;
        txt.CssClass = "noHtml emptyValidate";
        CustomizeControl1.AddControl("Semt(İlçe)", txt);

        txt = new TextBox();
        txt.ID = "clsSehir";
        txt.MaxLength = 30;
        txt.CssClass = "noHtml emptyValidate";
        CustomizeControl1.AddControl("Sehir(İL)", txt, "Belirteceğiniz <b>'İL'</b> sizi harita üzerinde bulunmanızı sağlayacaktır. Lütfen geçerli <b>'İL'</b> adı giriniz!");

        txt = new TextBox();
        txt.ID = "clsWebSitesi";
        txt.MaxLength = 60;
        txt.CssClass = "noHtml";
        CustomizeControl1.AddControl("Web Sitesi", txt, "Lütfen başına 'Http://' eklemeden giriniz. Ör. www.sitenizinadi.com");

        CheckBox chk = new CheckBox();
        chk.ID = "clsRandevu";
        CustomizeControl1.AddControl("Randevu Aktif", chk);
    }

    void standartHesap_SubmitClick(SortedDictionary<string, Control> controls)
    {
        try
        {
            if (!string.IsNullOrEmpty(((TextBox)controls["rgsAdi"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["rgsMail"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["rgsSifre"]).Text))
                using (Lib.Hesap hsp = new Lib.Hesap())
                {
                    hsp.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    hsp.Adi = ((TextBox)controls["rgsAdi"]).Text;
                    hsp.Soyadi = ((TextBox)controls["rgsSoyadi"]).Text;
                    hsp.Mail = ((TextBox)controls["rgsMail"]).Text;
                    hsp.Sifre = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((controls["rgsSifre"] as TextBox).Text, "md5");
                    hsp.Roller = "U";
                    hsp.OnayKodu = Settings.YeniOnayKodu();
                    hsp.DogumTarihi = ((DateTimeControl)controls["DogumTarihi"]).TarihSaat;
                    hsp.Cinsiyet = Settings.HesapCinsiyeti(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["rgsCinsiyet"]).SelectedValue));
                    hsp.Tipi = Lib.HesapTuru.Standart;
                    hsp.Abonelik = ((CheckBox)controls["rgsAbonelik"]).Checked;
                    hsp.Yorum = true;
                    hsp.Aktivasyon = false;
                    hsp.Aktif = false;
                    hsp.KayitTarihi = DateTime.Now;
                    string result = BAYMYO.UI.Converts.NullToString(Lib.HesapMethods.Insert(hsp));
                    switch (result)
                    {
                        case "EMAIL":
                            CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Kayıt olmak istediğiniz 'E-Mail' adresi başkası tarafından kullanılıyor! Lütfen başka bir 'E-Mail' adresi ile tekrar deneyiniz yada eğer bu e-mail adresinin sizin olduğundan eminseniz şifremi unuttum kısımından tekrar şifre talebinde bulununuz!");
                            break;
                        default:
                            Guid hid = BAYMYO.UI.Converts.NullToGuid(result);
                            if (!hid.Equals(BAYMYO.UI.Converts.NullToGuid(null)))
                            {
                                hsp.ID = hid;
                                Success(hsp);
                            }
                            else
                                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, "Üyelik işleminiz gerçekleştirilemiyor. Lütfen bilgilerinizi kontrol edip tekrar deneyiniz!");
                            break;
                    }
                }
        }
        catch (Exception)
        {
        }
    }

    void editorHesap_SubmitClick(SortedDictionary<string, Control> controls)
    {
        try
        {
            if (!string.IsNullOrEmpty(((TextBox)controls["rgsAdi"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["rgsMail"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["rgsSifre"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["prfUrl"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["prfDiplomaNo"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["prfMail"]).Text)
                & ((DropDownList)controls["prfUnvanID"]).SelectedIndex > 0
                & ((DropDownList)controls["prfUzmanlikAlaniID"]).SelectedIndex > 0)
            {
                if (Settings.InSlangyUrl.Contains(";" + ((TextBox)controls["prfUrl"]).Text + ";"))
                {
                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, string.Format("<b>{0}</b> doktor için belirtiğiniz <b>'{1}'</b> Url argo kelime içeriyor, yöneticilerimiz küfürlü içeriklere onay vermemektedir. Lütfen argo içermeyen bir 'URL' girerek ve tekrar deneyiniz.", ((TextBox)controls["hspAdi"]).Text, ((TextBox)controls["prfUrl"]).Text));
                    return;
                }
                else if (Settings.InValidUrl.Contains(";" + ((TextBox)controls["prfUrl"]).Text + ";"))
                {
                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, string.Format("<b>{0}</b> doktor için belirtiğiniz <b>'{1}'</b> Url sistemimiz tarafından kullanılıyor. Lütfen farklı bir 'URL' girerek ve tekrar deneyiniz.", ((TextBox)controls["hspAdi"]).Text, ((TextBox)controls["prfUrl"]).Text));
                    return;
                }
                else if (((TextBox)controls["prfUrl"]).Text.Length < 6)
                {
                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, string.Format("<b>{0}</b> doktor için belirtiğiniz <b>'{1}'</b> Url en az 6 karakter olmalıdır. Lütfen farklı bir 'URL' girerek ve tekrar deneyiniz.", ((TextBox)controls["hspAdi"]).Text, ((TextBox)controls["prfUrl"]).Text));
                    return;
                }
                using (Lib.Hesap hsp = Lib.HesapMethods.GetHesap(BAYMYO.UI.Converts.NullToGuid(ViewState["TempID"])))
                {
                    hsp.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    hsp.Adi = ((TextBox)controls["rgsAdi"]).Text;
                    hsp.Soyadi = ((TextBox)controls["rgsSoyadi"]).Text;
                    hsp.Mail = ((TextBox)controls["rgsMail"]).Text;
                    hsp.Sifre = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((controls["rgsSifre"] as TextBox).Text, "md5");
                    hsp.Roller = "E,U";
                    hsp.Tipi = Lib.HesapTuru.Editor;
                    hsp.OnayKodu = Settings.YeniOnayKodu();
                    hsp.DogumTarihi = ((DateTimeControl)controls["DogumTarihi"]).TarihSaat;
                    hsp.Cinsiyet = Settings.HesapCinsiyeti(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["rgsCinsiyet"]).SelectedValue));
                    hsp.Abonelik = ((CheckBox)controls["rgsAbonelik"]).Checked;
                    hsp.Yorum = true;
                    hsp.Aktivasyon = false;
                    hsp.Aktif = false;
                    hsp.KayitTarihi = DateTime.Now;

                    hsp.ProfilObject.Url = ((TextBox)controls["prfUrl"]).Text;
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
                                        Success(hsp);
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
                                        Success(hsp);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        hsp.KayitTarihi = DateTime.Now;
                        string result = BAYMYO.UI.Converts.NullToString(Lib.HesapMethods.Insert(hsp));
                        switch (result)
                        {
                            case "EMAIL":
                                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Kayıt olmak istediğiniz 'E-Mail' adresi başkası tarafından kullanılıyor! Lütfen başka bir 'E-Mail' adresi ile tekrar deneyiniz yada eğer bu e-mail adresinin sizin olduğundan eminseniz şifremi unuttum kısımından tekrar şifre talebinde bulununuz!");
                                break;
                            default:
                                Guid hid = BAYMYO.UI.Converts.NullToGuid(result);
                                if (!hid.Equals(BAYMYO.UI.Converts.NullToGuid(null)))
                                {
                                    ViewState["TempID"] = hid;
                                    hsp.ID = hid;
                                    hsp.ProfilObject.ID = hid;
                                    hsp.ProfilObject.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["prfResimUrl"] as FileUpload, hsp.Adi + " " + hsp.Soyadi, Server.MapPath(Settings.ImagesPath + "profil/"), 136, true);
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
                                            Success(hsp);
                                            break;
                                    }
                                }
                                else
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, "Üyelik işleminiz gerçekleştirilemiyor. Lütfen bilgilerinizi kontrol edip tekrar deneyiniz!");
                                break;
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
        }
    }

    void moderatorHesap_SubmitClick(SortedDictionary<string, Control> controls)
    {
        try
        {
            if (!string.IsNullOrEmpty(((TextBox)controls["rgsAdi"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["rgsMail"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["rgsSifre"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["prfAdi"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["prfMail"]).Text)
                & ((DropDownList)controls["prfUnvanID"]).SelectedIndex > 0
                & ((DropDownList)controls["prfUzmanlikAlaniID"]).SelectedIndex > 0)
            {
                if (Settings.InSlangyUrl.Contains(";" + ((TextBox)controls["prfUrl"]).Text + ";"))
                {
                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, string.Format("<b>{0}</b> doktor için belirtiğiniz <b>'{1}'</b> Url argo kelime içeriyor, yöneticilerimiz küfürlü içeriklere onay vermemektedir. Lütfen argo içermeyen bir 'URL' girerek ve tekrar deneyiniz.", ((TextBox)controls["hspAdi"]).Text, ((TextBox)controls["prfUrl"]).Text));
                    return;
                }
                else if (Settings.InValidUrl.Contains(";" + ((TextBox)controls["prfUrl"]).Text + ";"))
                {
                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, string.Format("<b>{0}</b> doktor için belirtiğiniz <b>'{1}'</b> Url sistemimiz tarafından kullanılıyor. Lütfen farklı bir 'URL' girerek ve tekrar deneyiniz.", ((TextBox)controls["hspAdi"]).Text, ((TextBox)controls["prfUrl"]).Text));
                    return;
                }
                else if (((TextBox)controls["prfUrl"]).Text.Length < 6)
                {
                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, string.Format("<b>{0}</b> doktor için belirtiğiniz <b>'{1}'</b> Url en az 6 karakter olmalıdır. Lütfen farklı bir 'URL' girerek ve tekrar deneyiniz.", ((TextBox)controls["hspAdi"]).Text, ((TextBox)controls["prfUrl"]).Text));
                    return;
                }
                using (Lib.Hesap hsp = Lib.HesapMethods.GetHesap(BAYMYO.UI.Converts.NullToGuid(ViewState["TempID"])))
                {
                    hsp.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    hsp.Adi = ((TextBox)controls["rgsAdi"]).Text;
                    hsp.Soyadi = ((TextBox)controls["rgsSoyadi"]).Text;
                    hsp.Mail = ((TextBox)controls["rgsMail"]).Text;
                    hsp.Sifre = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((controls["rgsSifre"] as TextBox).Text, "md5");
                    hsp.Roller = "H,E,U";
                    hsp.Tipi = Lib.HesapTuru.Moderator;
                    hsp.OnayKodu = Settings.YeniOnayKodu();
                    hsp.DogumTarihi = ((DateTimeControl)controls["DogumTarihi"]).TarihSaat;
                    hsp.Cinsiyet = Settings.HesapCinsiyeti(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["rgsCinsiyet"]).SelectedValue));
                    hsp.Abonelik = ((CheckBox)controls["rgsAbonelik"]).Checked;
                    hsp.Yorum = true;
                    hsp.Aktivasyon = false;
                    hsp.Aktif = false;
                    hsp.KayitTarihi = DateTime.Now;

                    hsp.ProfilObject.Url = ((TextBox)controls["prfUrl"]).Text;
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
                                        using (Lib.CalismaAlani m = new Lib.CalismaAlani())
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
                                            Lib.CalismaAlaniMethods.Insert(m);
                                        }
                                        Success(hsp);
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
                                        using (Lib.CalismaAlani m = new Lib.CalismaAlani())
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
                                            Lib.CalismaAlaniMethods.Insert(m);
                                        }
                                        Success(hsp);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        hsp.KayitTarihi = DateTime.Now;
                        string result = BAYMYO.UI.Converts.NullToString(Lib.HesapMethods.Insert(hsp));
                        switch (result)
                        {
                            case "EMAIL":
                                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Kayıt olmak istediğiniz 'E-Mail' adresi başkası tarafından kullanılıyor! Lütfen başka bir 'E-Mail' adresi ile tekrar deneyiniz yada eğer bu e-mail adresinin sizin olduğundan eminseniz şifremi unuttum kısımından tekrar şifre talebinde bulununuz!");
                                break;
                            default:
                                Guid hid = BAYMYO.UI.Converts.NullToGuid(result);
                                if (!hid.Equals(BAYMYO.UI.Converts.NullToGuid(null)))
                                {
                                    ViewState["TempID"] = hid;
                                    hsp.ID = hid;
                                    hsp.ProfilObject.ID = hid;
                                    hsp.ProfilObject.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["prfResimUrl"] as FileUpload, hsp.Adi + " " + hsp.Soyadi, Server.MapPath(Settings.ImagesPath + "profil/"), 136, true);
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
                                            using (Lib.CalismaAlani m = new Lib.CalismaAlani())
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
                                                Lib.CalismaAlaniMethods.Insert(m);
                                            }
                                            Success(hsp);
                                            break;
                                    }
                                }
                                else
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, "Hastane kayıt işleminiz gerçekleştirilemiyor. Lütfen bilgilerinizi kontrol edip tekrar deneyiniz!");
                                break;
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
        }
    }

    void Success(Lib.Hesap hsp)
    {
        try
        {
            string m_MailMesaj = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath) + "Activation.msg");
            m_MailMesaj = m_MailMesaj.Replace("%SiteUrl%", Settings.SiteUrl);
            m_MailMesaj = m_MailMesaj.Replace("%SiteTitle%", Settings.SiteTitle);
            m_MailMesaj = m_MailMesaj.Replace("%VirtualPath%", Settings.VirtualPath);
            m_MailMesaj = m_MailMesaj.Replace("%ReturnUrl", Request.QueryString["ReturnUrl"]);
            m_MailMesaj = m_MailMesaj.Replace("%ID%", hsp.ID.ToString());
            m_MailMesaj = m_MailMesaj.Replace("%Adi%", hsp.Adi).Replace("%Soyadi%", hsp.Soyadi);
            m_MailMesaj = m_MailMesaj.Replace("%OnayKodu%", hsp.OnayKodu);
            Settings.SendMail(hsp.Mail, hsp.Adi + " " + hsp.Soyadi, "Aktivasyon Maili", m_MailMesaj, true);
            m_MailMesaj = null;
            Response.Redirect(Settings.VirtualPath + "?l=3&r=aktivasyon&ReturnUrl=" + Request.QueryString["ReturnUrl"], false);
        }
        catch (Exception)
        {
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}