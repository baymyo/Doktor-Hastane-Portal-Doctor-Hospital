using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class panel_hesap_hesap : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Hesap", "Ekleme/Düzeltme Formu");
        using (Lib.Hesap hsp = Lib.HesapMethods.GetHesap(BAYMYO.UI.Converts.NullToGuid(Request.QueryString["uid"])))
        {
            CustomizeControl1.RemoveVisible = !string.IsNullOrEmpty(Request.QueryString["uid"]);
            if (hsp.ProfilObject == null)
                hsp.ProfilObject = new Lib.Profil();

            TextBox txt = new TextBox();
            txt.ID = "Adi";
            txt.CssClass = "noHtml emptyValidate";
            txt.Text = hsp.Adi;
            txt.MaxLength = 18;
            CustomizeControl1.AddControl("Adı", txt);

            txt = new TextBox();
            txt.ID = "Soyadi";
            txt.CssClass = "noHtml emptyValidate";
            txt.Text = hsp.Soyadi;
            txt.MaxLength = 15;
            CustomizeControl1.AddControl("Soyadı", txt);

            txt = new TextBox();
            txt.ID = "Mail";
            txt.CssClass = "noHtml emptyValidate mailValidate";
            txt.Text = hsp.Mail;
            txt.MaxLength = 90;
            CustomizeControl1.AddControl("Mail", txt, "Sisteme giriş yapmak için kullanılacaktır.");

            txt = new TextBox();
            txt.ID = "Sifre";
            txt.ToolTip = hsp.Sifre;
            txt.TextMode = TextBoxMode.Password;
            txt.MaxLength = 25;
            CustomizeControl1.AddControl("Şifre", txt, "Şifreyi değiştirmek istemiyorsanız boş bırakınız!");

            txt = new TextBox();
            txt.ID = "Roller";
            txt.Text = hsp.Roller;
            txt.MaxLength = 50;
            CustomizeControl1.AddControl("Roller", txt, string.Format("Lütfen virgül({0}) ile ayrıarak ve boşluk bırakmadan yazınız! Örnek: A{0}U{0}T{0}R", Settings.SplitFormat));

            DateTimeControl cnt = this.Page.LoadControl(Settings.DateTimeControlPath) as DateTimeControl;
            cnt.ID = "DogumTarihi";
            cnt.OlusturmaTipi = DateTimeControl.CreateType.DogumTarihi;
            CustomizeControl1.AddControl("Doğum Tarihi", cnt, "* Seçilmesi zorunlu alan.");
            cnt.TarihSaat = hsp.DogumTarihi;

            DropDownList ddl = new DropDownList();
            ddl.ID = "Cinsiyet";
            ddl.Width = 300;
            ddl.DataValueField = "Key";
            ddl.DataTextField = "Value";
            ddl.DataSource = Settings.HesapCinsiyetleri();
            ddl.DataBind();
            ddl.SelectedValue = BAYMYO.UI.Converts.NullToByte(hsp.Cinsiyet).ToString();
            CustomizeControl1.AddControl("Cinsiyet", ddl);

            ddl = new DropDownList();
            ddl.ID = "Tipi";
            ddl.Width = 300;
            ddl.DataValueField = "Key";
            ddl.DataTextField = "Value";
            ddl.DataSource = Settings.HesapTipleri();
            ddl.DataBind();
            ddl.SelectedValue = BAYMYO.UI.Converts.NullToByte(hsp.Tipi).ToString();
            CustomizeControl1.AddControl("Hesap Türü", ddl);

            CheckBox chk = new CheckBox();
            chk.ID = "Abonelik";
            chk.Checked = hsp.Abonelik;
            CustomizeControl1.AddControl("Abonelik", chk);

            chk = new CheckBox();
            chk.ID = "Aktivasyon";
            chk.Checked = hsp.Aktivasyon;
            CustomizeControl1.AddControl("Aktivasyon", chk);

            chk = new CheckBox();
            chk.ID = "Yorum";
            chk.Checked = hsp.Yorum;
            CustomizeControl1.AddControl("Yorum Durumu", chk);

            chk = new CheckBox();
            chk.ID = "Aktif";
            chk.Checked = hsp.Aktif;
            CustomizeControl1.AddControl("Hesap Durumu", chk);

            Lib.HesapTuru tipi;
            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                tipi = Settings.HesapTipi(BAYMYO.UI.Converts.NullToByte(Request.QueryString["type"]));
            else
                tipi = hsp.Tipi;
            switch (tipi)
            {
                case Lib.HesapTuru.Moderator:
                    CustomizeControl1.AddTitle("Hastane Bilgileri");

                    ddl = new DropDownList();
                    ddl.ID = "prfUnvanID";
                    ddl.Width = 450;
                    ddl.DataMember = "Kategori";
                    ddl.DataValueField = "ID";
                    ddl.DataTextField = "Adi";
                    ddl.DataSource = Lib.KategoriMethods.GetMenu("hastaneunvan", true);
                    ddl.DataBind();
                    ddl.SelectedValue = hsp.ProfilObject.Unvan;
                    CustomizeControl1.AddControl("Hastane Ünvanı", ddl, "<a href=\"" + Settings.PanelPath + "?go=kategori&mdl=hastaneunvan\">[+] Hastane Ünvanı Tanımla</a>");

                    ddl = new DropDownList();
                    ddl.ID = "prfUzmanlikAlaniID";
                    ddl.Width = 450;
                    ddl.DataMember = "Kategori";
                    ddl.DataValueField = "ID";
                    ddl.DataTextField = "Adi";
                    ddl.DataSource = Lib.KategoriMethods.GetMenu("hastaneuzmanlik", true);
                    ddl.DataBind();
                    ddl.SelectedValue = hsp.ProfilObject.UzmanlikAlaniID;
                    CustomizeControl1.AddControl("Uzmanlık Alanı", ddl, "<a href=\"" + Settings.PanelPath + "?go=kategori&mdl=hastaneuzmanlik\">[+] Hastane Uzmanlık Alanı Tanımla</a>");

                    Image hstImg = new Image();
                    hstImg.ID = "prfImageUrl";
                    hstImg.Width = 210;
                    hstImg.ImageUrl = Settings.ImagesPath + ((!string.IsNullOrEmpty(hsp.ProfilObject.ResimUrl)) ? "profil/" + hsp.ProfilObject.ResimUrl : "yok.png");
                    CustomizeControl1.AddControl("Hastane Logo", hstImg);

                    FileUpload hstFlu = new FileUpload();
                    hstFlu.ID = "prfResimUrl";
                    CustomizeControl1.AddControl("Yeni Logo Seç", hstFlu);

                    txt = new TextBox();
                    txt.ID = "prfUrl";
                    txt.Text = hsp.ProfilObject.Url;
                    txt.CssClass = "noHtml smallCharNumber emptyValidate";
                    txt.MaxLength = 50;
                    CustomizeControl1.AddControl("Url", txt, "Hastane bağlantı adresi olacaktır. Ör. " + Settings.SiteUrl + "<b class=\"toolTip titleFormat1\" title=\"Adres çubuğunda sitemizin adının yanına '/' ters slaş yaparak burada belirteceğiniz isim ile profilinizin görüntülenmesini sağlar.\">hastaneadi</b>");

                    txt = new TextBox();
                    txt.ID = "prfAdi";
                    txt.Text = hsp.ProfilObject.Adi;
                    txt.CssClass = "noHtml emptyValidate";
                    txt.MaxLength = 100;
                    CustomizeControl1.AddControl("Hastane Adı", txt, "Sayfanızda görüntülenecek olan hastane adını giriniz.");

                    txt = new TextBox();
                    txt.ID = "prfMail";
                    txt.Text = hsp.ProfilObject.Mail;
                    txt.CssClass = "noHtml emptyValidate mailValidate";
                    txt.MaxLength = 60;
                    CustomizeControl1.AddControl("Hastane Mail", txt, "Profilde gösterilecek olan mail adresidir.");

                    txt = new TextBox();
                    txt.ID = "prfHakkimda";
                    txt.Text = hsp.ProfilObject.Hakkimda;
                    txt.CssClass = "noHtml";
                    txt.Height = 150;
                    txt.TextMode = TextBoxMode.MultiLine;
                    txt.MaxLength = 1000;
                    CustomizeControl1.AddControl("Hakkinda", txt, "Bu alana <b>1000</b> karaktere kadar bilgi girişi yapabilirsiniz.");

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
                    ddl.Width = 450;
                    ddl.DataMember = "Kategori";
                    ddl.DataValueField = "ID";
                    ddl.DataTextField = "Adi";
                    ddl.DataSource = Lib.KategoriMethods.GetMenu("uzmanlik", true);
                    ddl.DataBind();
                    ddl.SelectedValue = hsp.ProfilObject.UzmanlikAlaniID;
                    CustomizeControl1.AddControl("Uzmanlık Alanı", ddl, "<a href=\"" + Settings.PanelPath + "?go=kategori&mdl=uzmanlik\">[+] Yeni Uzmanlık Alanı Ekle</a>");

                    ddl = new DropDownList();
                    ddl.ID = "prfUnvanID";
                    ddl.Width = 450;
                    ddl.DataMember = "Kategori";
                    ddl.DataValueField = "ID";
                    ddl.DataTextField = "Adi";
                    ddl.DataSource = Lib.KategoriMethods.GetMenu("unvan", true);
                    ddl.DataBind();
                    ddl.SelectedValue = hsp.ProfilObject.Unvan;
                    CustomizeControl1.AddControl("Ünvan", ddl, "<a href=\"" + Settings.PanelPath + "?go=kategori&mdl=unvan\">[+] Yeni Ünvan Ekle</a>");

                    Image img = new Image();
                    img.ID = "prfImageUrl";
                    img.Width = 210;
                    img.ImageUrl = Settings.ImagesPath + ((!string.IsNullOrEmpty(hsp.ProfilObject.ResimUrl)) ? "profil/" + hsp.ProfilObject.ResimUrl : "yok.png");
                    CustomizeControl1.AddControl("Profil Resimi", img);

                    FileUpload flu = new FileUpload();
                    flu.ID = "prfResimUrl";
                    CustomizeControl1.AddControl("Yeni Resim Seç", flu);

                    txt = new TextBox();
                    txt.ID = "prfUrl";
                    txt.Text = hsp.ProfilObject.Url;
                    txt.CssClass = "noHtml smallCharNumber emptyValidate";
                    txt.MaxLength = 50;
                    CustomizeControl1.AddControl("Url", txt, "Profil bağlantı adresi olacaktır. Ör. " + Settings.SiteUrl + "<b class=\"toolTip titleFormat1\" title=\"Adres çubuğunda sitemizin adının yanına '/' ters slaş yaparak burada belirteceğiniz isim ile profilinizin görüntülenmesini sağlar.\">adisoyadi</b>");

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
                    txt.Text = hsp.ProfilObject.Mail;
                    txt.CssClass = "noHtml emptyValidate mailValidate";
                    txt.MaxLength = 60;
                    CustomizeControl1.AddControl("Profil Mail", txt, "Profilde gösterilecek olan mail adresidir.");

                    txt = new TextBox();
                    txt.ID = "prfHakkimda";
                    txt.Text = hsp.ProfilObject.Hakkimda;
                    txt.CssClass = "noHtml";
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

            CustomizeControl1.RemoveClick += new CustomizeControl.ButtonEvent(hesap_RemoveClick);
        }
        base.OnInit(e);
    }

    void standartHesap_SubmitClick(SortedDictionary<string, Control> controls)
    {
        if (!string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Mail"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Roller"]).Text))
            using (Lib.Hesap hsp = Lib.HesapMethods.GetHesap(BAYMYO.UI.Converts.NullToGuid(Request.QueryString["uid"])))
            {
                hsp.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                hsp.Adi = ((TextBox)controls["Adi"]).Text;
                hsp.Soyadi = ((TextBox)controls["Soyadi"]).Text;
                hsp.Mail = ((TextBox)controls["Mail"]).Text;
                if (!string.IsNullOrEmpty((controls["Sifre"] as TextBox).Text.Trim()))
                {
                    string sifre = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((controls["Sifre"] as TextBox).Text, "md5");
                    if (!(controls["Sifre"] as TextBox).ToolTip.Equals(sifre))
                        hsp.Sifre = sifre;
                }
                hsp.OnayKodu = Settings.YeniOnayKodu();
                hsp.Roller = ((TextBox)controls["Roller"]).Text;
                hsp.DogumTarihi = ((DateTimeControl)controls["DogumTarihi"]).TarihSaat;
                hsp.Cinsiyet = Settings.HesapCinsiyeti(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["Cinsiyet"]).SelectedValue));
                hsp.Tipi = Settings.HesapTipi(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["Tipi"]).SelectedValue));
                hsp.Abonelik = ((CheckBox)controls["Abonelik"]).Checked;
                hsp.Aktivasyon = ((CheckBox)controls["Aktivasyon"]).Checked;
                hsp.Yorum = ((CheckBox)controls["Yorum"]).Checked;
                hsp.Aktif = ((CheckBox)controls["Aktif"]).Checked;

                if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(hsp.ID))
                {
                    if (Lib.HesapMethods.Update(hsp) > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                        if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(hsp.ProfilObject.ID))
                            Lib.ProfilMethods.Delete(hsp.ProfilObject);
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
                            if (!result.Equals(BAYMYO.UI.Converts.NullToGuid(null)))
                            {
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Kayıt işleminiz başarılı bir şekilde tamamlandı!');", true);
                                Settings.ClearControls(controls);
                            }
                            else
                                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, "Üyelik işleminiz gerçekleştirilemiyor. Lütfen bilgilerinizi kontrol edip tekrar deneyiniz!");
                            break;
                    }
                }
            }
    }

    void editorHesap_SubmitClick(SortedDictionary<string, Control> controls)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["uid"]))
            ViewState["TempID"] = Request.QueryString["uid"];
        if (!string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Mail"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Roller"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["prfUrl"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["prfDiplomaNo"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["prfMail"]).Text)
            & ((DropDownList)controls["prfUnvanID"]).SelectedIndex > 0
            & ((DropDownList)controls["prfUzmanlikAlaniID"]).SelectedIndex > 0)
            using (Lib.Hesap hsp = Lib.HesapMethods.GetHesap(BAYMYO.UI.Converts.NullToGuid(ViewState["TempID"])))
            {
                hsp.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                hsp.Adi = ((TextBox)controls["Adi"]).Text;
                hsp.Soyadi = ((TextBox)controls["Soyadi"]).Text;
                hsp.Mail = ((TextBox)controls["Mail"]).Text;
                if (!string.IsNullOrEmpty((controls["Sifre"] as TextBox).Text.Trim()))
                {
                    string sifre = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((controls["Sifre"] as TextBox).Text, "md5");
                    if (!(controls["Sifre"] as TextBox).ToolTip.Equals(sifre))
                        hsp.Sifre = sifre;
                }
                hsp.OnayKodu = Settings.YeniOnayKodu();
                hsp.Roller = ((TextBox)controls["Roller"]).Text;
                hsp.DogumTarihi = ((DateTimeControl)controls["DogumTarihi"]).TarihSaat;
                hsp.Cinsiyet = Settings.HesapCinsiyeti(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["Cinsiyet"]).SelectedValue));
                hsp.Tipi = Settings.HesapTipi(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["Tipi"]).SelectedValue));
                hsp.Abonelik = ((CheckBox)controls["Abonelik"]).Checked;
                hsp.Aktivasyon = ((CheckBox)controls["Aktivasyon"]).Checked;
                hsp.Yorum = ((CheckBox)controls["Yorum"]).Checked;
                hsp.Aktif = ((CheckBox)controls["Aktif"]).Checked;

                bool isEditor = true;
                switch (hsp.Tipi)
                {
                    case Lib.HesapTuru.Admin:
                    case Lib.HesapTuru.Moderator:
                    case Lib.HesapTuru.Editor:
                        hsp.ProfilObject.Url = ((TextBox)controls["prfUrl"]).Text;
                        hsp.ProfilObject.DiplomaNo = ((TextBox)controls["prfDiplomaNo"]).Text;
                        hsp.ProfilObject.TCKimlikNo = ((TextBox)controls["prfTCKimlikNo"]).Text;
                        hsp.ProfilObject.Adi = string.Empty;
                        hsp.ProfilObject.Mail = ((TextBox)controls["prfMail"]).Text;
                        hsp.ProfilObject.Hakkimda = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["prfHakkimda"]).Text, 1000);
                        hsp.ProfilObject.Unvan = ((DropDownList)controls["prfUnvanID"]).SelectedValue;
                        hsp.ProfilObject.UzmanlikAlaniID = ((DropDownList)controls["prfUzmanlikAlaniID"]).SelectedValue;
                        break;
                    default:
                        isEditor = false;
                        break;
                }

                if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(hsp.ID))
                {
                    if (Lib.HesapMethods.Update(hsp) > 0)
                    {
                        if ((controls["prfResimUrl"] as FileUpload).HasFile & isEditor)
                            if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "profil/" + hsp.ProfilObject.ResimUrl)))
                                hsp.ProfilObject.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["prfResimUrl"] as FileUpload, hsp.Adi + " " + hsp.Soyadi, Server.MapPath(Settings.ImagesPath + "profil/"), 137, true); ;
                        if (BAYMYO.UI.Converts.NullToGuid(null).Equals(hsp.ProfilObject.ID) & isEditor)
                        {
                            hsp.ProfilObject.ID = hsp.ID;
                            switch (Lib.ProfilMethods.Insert(hsp.ProfilObject).ToString())
                            {
                                case "":
                                case "0":
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Profil bilgilerinizi kontrol edip tekrar deneyiniz!');", true);
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
                                    ViewState["TempID"] = string.Empty;
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                                    break;
                            }
                        }
                        else if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(hsp.ProfilObject.ID) & isEditor)
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
                                    ViewState["TempID"] = string.Empty;
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                                    break;
                            }
                        }
                    }
                    else if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(hsp.ProfilObject.ID) & !isEditor)
                        Lib.ProfilMethods.Delete(hsp.ProfilObject);
                }
                else
                {
                    hsp.KayitTarihi = DateTime.Now;
                    Guid hid = BAYMYO.UI.Converts.NullToGuid(Lib.HesapMethods.Insert(hsp));
                    if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(hid) & isEditor)
                    {
                        ViewState["TempID"] = hid;
                        hsp.ID = hid;
                        hsp.ProfilObject.ID = hsp.ID;
                        hsp.ProfilObject.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["prfResimUrl"] as FileUpload, hsp.Adi + " " + hsp.Soyadi, Server.MapPath(Settings.ImagesPath + "profil/"), 137, true); ;
                        switch (Lib.ProfilMethods.Insert(hsp.ProfilObject).ToString())
                        {
                            case "":
                            case "0":
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Profil bilgilerinizi kontrol edip tekrar deneyiniz!');", true);
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
                                ViewState["TempID"] = string.Empty;
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Kayıt işleminiz başarılı bir şekilde tamamlandı!');", true);
                                break;
                        }
                        Settings.ClearControls(controls);
                    }
                }
            }
    }

    void moderatorHesap_SubmitClick(SortedDictionary<string, Control> controls)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["uid"]))
            ViewState["TempID"] = Request.QueryString["uid"];
        if (!string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Mail"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Roller"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["prfAdi"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["prfMail"]).Text)
            & ((DropDownList)controls["prfUnvanID"]).SelectedIndex > 0
            & ((DropDownList)controls["prfUzmanlikAlaniID"]).SelectedIndex > 0)
            using (Lib.Hesap hsp = Lib.HesapMethods.GetHesap(BAYMYO.UI.Converts.NullToGuid(ViewState["TempID"])))
            {
                hsp.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                hsp.Adi = ((TextBox)controls["Adi"]).Text;
                hsp.Soyadi = ((TextBox)controls["Soyadi"]).Text;
                hsp.Mail = ((TextBox)controls["Mail"]).Text;
                if (!string.IsNullOrEmpty((controls["Sifre"] as TextBox).Text.Trim()))
                {
                    string sifre = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((controls["Sifre"] as TextBox).Text, "md5");
                    if (!(controls["Sifre"] as TextBox).ToolTip.Equals(sifre))
                        hsp.Sifre = sifre;
                }
                hsp.OnayKodu = Settings.YeniOnayKodu();
                hsp.Roller = ((TextBox)controls["Roller"]).Text;
                hsp.DogumTarihi = ((DateTimeControl)controls["DogumTarihi"]).TarihSaat;
                hsp.Cinsiyet = Settings.HesapCinsiyeti(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["Cinsiyet"]).SelectedValue));
                hsp.Tipi = Settings.HesapTipi(BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["Tipi"]).SelectedValue));
                hsp.Abonelik = ((CheckBox)controls["Abonelik"]).Checked;
                hsp.Aktivasyon = ((CheckBox)controls["Aktivasyon"]).Checked;
                hsp.Yorum = ((CheckBox)controls["Yorum"]).Checked;
                hsp.Aktif = ((CheckBox)controls["Aktif"]).Checked;

                bool isEditor = true;
                switch (hsp.Tipi)
                {
                    case Lib.HesapTuru.Admin:
                    case Lib.HesapTuru.Moderator:
                    case Lib.HesapTuru.Editor:
                        hsp.ProfilObject.Url = ((TextBox)controls["prfUrl"]).Text;
                        hsp.ProfilObject.DiplomaNo = string.Empty;
                        hsp.ProfilObject.TCKimlikNo = string.Empty;
                        hsp.ProfilObject.Mail = ((TextBox)controls["prfMail"]).Text;
                        hsp.ProfilObject.Hakkimda = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["prfHakkimda"]).Text, 1000);
                        hsp.ProfilObject.Unvan = ((DropDownList)controls["prfUnvanID"]).SelectedValue;
                        hsp.ProfilObject.UzmanlikAlaniID = ((DropDownList)controls["prfUzmanlikAlaniID"]).SelectedValue;
                        break;
                    default:
                        isEditor = false;
                        break;
                }

                if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(hsp.ID))
                {
                    if (Lib.HesapMethods.Update(hsp) > 0)
                    {
                        if ((controls["prfResimUrl"] as FileUpload).HasFile & isEditor)
                            if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "profil/" + hsp.ProfilObject.ResimUrl)))
                                hsp.ProfilObject.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["prfResimUrl"] as FileUpload, hsp.Adi + " " + hsp.Soyadi, Server.MapPath(Settings.ImagesPath + "profil/"), 137, true); ;
                        if (BAYMYO.UI.Converts.NullToGuid(null).Equals(hsp.ProfilObject.ID) & isEditor)
                        {
                            hsp.ProfilObject.ID = hsp.ID;
                            switch (Lib.ProfilMethods.Insert(hsp.ProfilObject).ToString())
                            {
                                case "":
                                case "0":
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Profil bilgilerinizi kontrol edip tekrar deneyiniz!');", true);
                                    break;
                                case "URL":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz 'URL' başka bir kullanıcı tarafından kullanılmaktadır. Lütfen başka bir 'URL' yazarak tekrar deneyiniz.");
                                    break;
                                case "HASTANEADI":
                                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, "Belirttiğiniz 'Hastane Adı' başka bir hastane tarafından kullanılmaktadır. Lütfen 'Hastane Adınızı' kontrol ediniz ve tekrar deneyiniz.");
                                    break;
                                default:
                                    using (Lib.CalismaAlani m = Lib.CalismaAlaniMethods.GetDefault(hsp.ID))
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
                                    ViewState["TempID"] = string.Empty;
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                                    break;
                            }
                        }
                        else if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(hsp.ProfilObject.ID) & isEditor)
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
                                    using (Lib.CalismaAlani m = Lib.CalismaAlaniMethods.GetDefault(hsp.ID))
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
                                    ViewState["TempID"] = string.Empty;
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                                    break;
                            }
                        }
                    }
                    else if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(hsp.ProfilObject.ID) & !isEditor)
                        Lib.ProfilMethods.Delete(hsp.ProfilObject);
                }
                else
                {
                    hsp.KayitTarihi = DateTime.Now;
                    Guid hid = BAYMYO.UI.Converts.NullToGuid(Lib.HesapMethods.Insert(hsp));
                    if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(hid) & isEditor)
                    {
                        ViewState["TempID"] = hid;
                        hsp.ID = hid;
                        hsp.ProfilObject.ID = hsp.ID;
                        hsp.ProfilObject.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["prfResimUrl"] as FileUpload, hsp.Adi + " " + hsp.Soyadi, Server.MapPath(Settings.ImagesPath + "profil/"), 137, true); ;
                        switch (Lib.ProfilMethods.Insert(hsp.ProfilObject).ToString())
                        {
                            case "":
                            case "0":
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Profil bilgilerinizi kontrol edip tekrar deneyiniz!');", true);
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
                                ViewState["TempID"] = string.Empty;
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Kayıt işleminiz başarılı bir şekilde tamamlandı!');", true);
                                break;
                        }
                        Settings.ClearControls(controls);
                    }
                }
            }
    }

    void hesap_RemoveClick(SortedDictionary<string, Control> controls)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["uid"]))
        {
            using (Lib.Hesap hsp = Lib.HesapMethods.GetHesap(BAYMYO.UI.Converts.NullToGuid(Request.QueryString["uid"])))
            {
                BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.JSonPath + "maps/" + hsp.ID + ".js"));
                if (hsp.ProfilObject != null)
                {
                    if (Lib.ProfilMethods.Delete(hsp.ProfilObject) > 0)
                    {
                        BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "profil/" + hsp.ProfilObject.ResimUrl));
                        Lib.HesapMethods.Delete(hsp);
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Silme işleminiz başarılı bir şekilde tamamlandı!');", true);
                        Settings.ClearControls(controls);
                    }
                }
                else
                {
                    Lib.HesapMethods.Delete(hsp);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Silme işleminiz başarılı bir şekilde tamamlandı!');", true);
                    Settings.ClearControls(controls);
                }
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}