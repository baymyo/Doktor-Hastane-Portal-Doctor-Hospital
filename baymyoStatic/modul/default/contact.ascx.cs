using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_default_contact : System.Web.UI.UserControl
{
    public string HesapID { get { return (string)ViewState["hesapID"]; } set { ViewState["hesapID"] = value; } }

    protected override void OnInit(EventArgs e)
    {
        CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "İletişim", "Formu");
        CustomizeControl1.SubmitText = "Gönder";
        CustomizeControl1.RemoveVisible = false;

        bool isControlActive = !Settings.IsUserActive();

        TextBox txt = new TextBox();
        txt.ID = "Adi";
        txt.CssClass = "noHtml emptyValidate";
        txt.Text = Settings.CurrentUser().Adi + " " + Settings.CurrentUser().Soyadi;
        txt.Visible = isControlActive;
        txt.MaxLength = 35;
        CustomizeControl1.AddControl("Adı Soyadı", txt);

        txt = new TextBox();
        txt.ID = "Mail";
        txt.Text = Settings.CurrentUser().Mail;
        txt.Visible = isControlActive;
        txt.CssClass = "noHtml emptyValidate mailValidate";
        txt.MaxLength = 60;
        CustomizeControl1.AddControl("e-Mail", txt);

        txt = new TextBox();
        txt.ID = "Konu";
        txt.CssClass = "noHtml emptyValidate";
        txt.MaxLength = 50;
        CustomizeControl1.AddControl("Konu", txt, "Lütfen sorunuzu kısaca özetleyecek ve düzgün konu başlıkları giriniz.");

        txt = new TextBox();
        txt.ID = "Icerik";
        txt.TextMode = TextBoxMode.MultiLine;
        txt.CssClass = "noHtml emptyValidate";
        txt.MaxLength = 1000;
        txt.Height = 200;
        CustomizeControl1.AddControl("Mesaj", txt, "Bu alana <b>1000</b> karakter soru yazabilirsiniz.");

        txt = new TextBox();
        txt.ID = "Telefon";
        txt.CssClass = "noHtml isNumber emptyValidate";
        txt.MaxLength = 16;
        CustomizeControl1.AddControl("Telefon", txt, "Sizinle iletişim kurabilmemiz için telefon numaranızı yazınız.");

        CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);

        base.OnInit(e);
    }

    void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
    {
        if (!string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Mail"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Icerik"]).Text))
            using (Lib.Mesaj msg = new Lib.Mesaj())
            {
                Lib.Hesap hsp = null;
                bool isAccountActive = false;
                if (!string.IsNullOrEmpty(HesapID))
                {
                    hsp = Lib.HesapMethods.GetHesap(BAYMYO.UI.Converts.NullToGuid(HesapID));
                    switch (hsp.Tipi)
                    {
                        case Lib.HesapTuru.None:
                        case Lib.HesapTuru.Standart:
                            isAccountActive = false;
                            break;
                        default:
                            msg.HesapID = hsp.ID;
                            isAccountActive = true;
                            break;
                    }
                }
                msg.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                msg.Adi = ((TextBox)controls["Adi"]).Text;
                msg.Mail = ((TextBox)controls["Mail"]).Text;
                msg.Telefon = ((TextBox)controls["Telefon"]).Text;
                msg.Konu = ((TextBox)controls["Konu"]).Text;
                msg.Icerik = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Icerik"]).Text, 1000);
                msg.Yanit = string.Empty;
                msg.KayitTarihi = DateTime.Now;
                msg.GuncellemeTarihi = DateTime.Now;
                msg.Durum = 1;
                msg.YoneticiOnay = false;
                msg.Aktif = false;
                if (Lib.MesajMethods.Insert(msg) > 0)
                {
                    try
                    {
                        if (isAccountActive)
                        {
                            //if (!string.IsNullOrEmpty(hsp.ProfilObject.Mail))
                            //    Settings.SendMail(hsp.ProfilObject.Mail, hsp.Adi + " " + hsp.Soyadi, msg.Mail, msg.Adi, msg.Konu, msg.Icerik, true);
                            //else
                            //    Settings.SendMail(hsp.Mail, hsp.Adi + " " + hsp.Soyadi, msg.Mail, msg.Adi, msg.Konu, msg.Icerik, true);
                        }
                        else
                            Settings.SendMail(Settings.ContactMail, Settings.ContactName, msg.Mail, msg.Adi, msg.Konu, msg.Icerik, true);
                        CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Info, "Sorunuz başarılı bir şekilde tarafımıza iletilmiştir. Kısa süre içerisinde mesajınıza cevap verilecektir ve sizinle vermiş olduğunuz bilgiler aracılığı ile iletişim kurulacaktır.");
                    }
                    catch (Exception)
                    {
                        CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, "Sunucularımızdaki yoğunlukdan dolayı mail gönderme işlemi şuan için başarısızlıkla sonuçlandı. Lütfen bu işleminizi daha sonra tekrar deneyiniz.");
                    }
                    Settings.ClearControls(controls);
                }
            }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack & this.Visible & string.IsNullOrEmpty(HesapID))
            this.Page.Title = "İletişim Formu - " + Settings.SiteTitle;
    }
}