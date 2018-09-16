using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAYMYO;

public partial class modul_default_randevu : System.Web.UI.UserControl
{
    public Guid HesapID { get { return BAYMYO.UI.Converts.NullToGuid(ViewState["hesapID"]); } set { ViewState["hesapID"] = value; } }
    public string ModulID { get { return (string)ViewState["ModulID"]; } set { ViewState["ModulID"] = value; } }
    public object IcerikID { get { return (string)ViewState["IcerikID"]; } set { ViewState["IcerikID"] = value; } }

    protected override void OnInit(EventArgs e)
    {
        //CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Randevu", "Talep Formu");
        CustomizeControl1.FormTitleVisible = true;
        CustomizeControl1.SubmitText = "Randevu Talep Et";
        CustomizeControl1.RemoveVisible = false;

        if (!string.IsNullOrEmpty(Request.QueryString["caid"]))
            using (Lib.CalismaAlani c = Lib.CalismaAlaniMethods.GetCalismaAlani(BAYMYO.UI.Converts.NullToGuid(Request.QueryString["caid"])))
            {
                if (c != null)
                {
                    CustomizeControl1.MessageText = "<h1 class=\"mmb-Title\">Randevu Talep Formu</h1><div class=\"clear\"></div>";
                    CustomizeControl1.MessageText += BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath + "CardView.msg"));
                    CustomizeControl1.MessageText = CustomizeControl1.MessageText.Replace("%Kurum%", c.Kurum);
                    CustomizeControl1.MessageText = CustomizeControl1.MessageText.Replace("%Adres%", c.Adres);
                    CustomizeControl1.MessageText = CustomizeControl1.MessageText.Replace("%Telefon%", c.Telefon);
                    CustomizeControl1.MessageText = CustomizeControl1.MessageText.Replace("%Semt%", c.Semt);
                    CustomizeControl1.MessageText = CustomizeControl1.MessageText.Replace("%Sehir%", c.Sehir);
                    CustomizeControl1.MessageVisible = true;
                }
            }

        bool isUserActive = !Settings.IsUserActive();
        CustomizeControl1.PanelEnabled = !isUserActive;

        if (isUserActive)
            CustomizeControl1.MessageText += "<div class=\"clear\"></div>" + MessageBox.Show(DialogResult.Warning, string.Format("Randevu alabilmek için üye olmanız gereklidir, sizde <a href=\"{0}?l=1&ReturnUrl={1}\"><b>Üye Girişi</b></a> yapınız yada <a href=\"{0}?l=2&type=standart&ReturnUrl={1}\"><b>Ücretsiz Kayıt</b></a> olunuz.", Settings.VirtualPath, Request.RawUrl));

        TextBox txt = new TextBox();
        txt.ID = "Adi";
        txt.Text = Settings.CurrentUser().Adi + " " + Settings.CurrentUser().Soyadi;
        txt.Enabled = !isUserActive;
        txt.Visible = isUserActive;
        txt.CssClass = "noHtml emptyValidate";
        txt.MaxLength = 35;
        CustomizeControl1.AddControl("Adınız", txt);

        txt = new TextBox();
        txt.ID = "Mail";
        txt.Text = Settings.CurrentUser().Mail;
        txt.Enabled = !isUserActive;
        txt.Visible = isUserActive;
        txt.CssClass = "noHtml emptyValidate mailValidate";
        txt.MaxLength = 60;
        CustomizeControl1.AddControl("Mail", txt);

        txt = new TextBox();
        txt.ID = "Telefon";
        txt.CssClass = "noHtml isNumber emptyValidate";
        txt.MaxLength = 16;
        CustomizeControl1.AddControl("Telefon", txt, "* Sizinle iletişim kurabilmemiz için gereklidir.");

        txt = new TextBox();
        txt.ID = "GSM";
        txt.CssClass = "noHtml isNumber emptyValidate";
        txt.MaxLength = 16;
        CustomizeControl1.AddControl("GSM (Cep)", txt, "* Sizinle iletişim kurabilmemiz için gereklidir.");

        DateTimeControl cnt = this.Page.LoadControl(Settings.DateTimeControlPath) as DateTimeControl;
        cnt.ID = "Tarih";
        cnt.OlusturmaTipi = DateTimeControl.CreateType.Randevu;
        CustomizeControl1.AddControl("Randevu Tarihi/Saati", cnt, "* Randevu saatinizi ve tarihi seçiniz.");

        txt = new TextBox();
        txt.ID = "Icerik";
        txt.TextMode = TextBoxMode.MultiLine;
        txt.CssClass = "noHtml emptyValidate";
        txt.MaxLength = 250;
        txt.Height = 100;
        CustomizeControl1.AddControl("Not", txt, "Bu alana <b>250</b> karakter soru yazabilirsiniz.");

        CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);
        base.OnInit(e);
    }

    void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
    {
        try
        {
            if ((((DateTimeControl)controls["Tarih"]).TarihSaat > DateTime.Now)
                & !string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["Mail"]).Text)
                & IcerikID != null
                & Settings.IsUserActive())
                using (Lib.Randevu m = new Lib.Randevu())
                {
                    m.HesapID = HesapID;
                    m.ModulID = ModulID;
                    m.IcerikID = IcerikID.ToString();
                    m.Adi = ((TextBox)controls["Adi"]).Text;
                    m.Mail = ((TextBox)controls["Mail"]).Text;
                    m.Telefon = ((TextBox)controls["Telefon"]).Text;
                    m.GSM = ((TextBox)controls["GSM"]).Text;
                    m.Icerik = ((TextBox)controls["Icerik"]).Text;
                    m.TarihSaat = ((DateTimeControl)controls["Tarih"]).TarihSaat;
                    m.Durum = 1;
                    m.YoneticiOnay = false;

                    if (Settings.CurrentUser().ID.Equals(HesapID))
                    {
                        m.Durum = 2;
                        m.YoneticiOnay = true;
                    }

                    if (Lib.RandevuMethods.Insert(m) > 0)
                    {
                        CustomizeControl1.MessageText += "<div class=\"clear\"></div>" + MessageBox.Show(DialogResult.Info, "Randevu talebiniz başarılı bir şekilde tarafımıza iletildi! Editörlerimiz tarafından incelenerek randevunuz ilgili doktora iletilecektir. Sonrasında ilgili doktor veya sekreteri sizinle irtibata geçecek ve randevu gün/saatinizi teyid edecektir.");
                        Settings.ClearControls(controls);
                    }
                }
            else
                CustomizeControl1.MessageText += "<div class=\"clear\"></div>" + MessageBox.Show(DialogResult.Stop, "Randevu tarihi bugünden büyük olmalı ve diğer alanlardaki bilgileri lütfen kontorol ederek tekrar deneyiniz.");

        }
        catch (Exception)
        {
            CustomizeControl1.MessageText += "<div class=\"clear\"></div>" + MessageBox.Show(DialogResult.Error, "İşleminiz sırasında yoğunluk sebebi ile bir sorun meydana geldi lütfen bilgilerinizi kontrol ederek tekrar deneyiniz.");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}