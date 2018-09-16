using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_editor_randevutalebi : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        if (!Settings.IsUserActive())
        {
            CustomizeControl1.PanelVisible = false;
            Response.Redirect(Settings.VirtualPath + "?l=1", false);
            return;
        }

        //CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Randevu", "Onay Formu");
        CustomizeControl1.SubmitText = "Kaydet";
        CustomizeControl1.RemoveVisible = false;
        using (Lib.Randevu m = Lib.RandevuMethods.GetRandevu(BAYMYO.UI.Converts.NullToGuid(Request.QueryString["rndid"])))
        {
            if (!m.HesapID.Equals(Settings.CurrentUser().ID))
            {
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, "Bu alana erişim sağlayamıyorsunuz sistem sadece kendinize ait randevularınızı güncelleme hakkı vermektedir. Lütfen sadece size ait randevularınızı seçiniz!");
                CustomizeControl1.PanelVisible = false;
                return;
            }
            switch (Settings.CurrentUser().Tipi)
            {
                case Lib.HesapTuru.None:
                case Lib.HesapTuru.Standart:
                    Response.Redirect(Settings.VirtualPath + "?l=5", false);
                    return;
            }

            switch (m.ModulID)
            {
                case "calismaalani":
                    using (Lib.CalismaAlani c = Lib.CalismaAlaniMethods.GetCalismaAlani(BAYMYO.UI.Converts.NullToGuid(m.IcerikID)))
                    {
                        if (c != null)
                        {
                            ltrContent.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath + "CardView.msg"));
                            ltrContent.Text = ltrContent.Text.Replace("%Kurum%", c.Kurum);
                            ltrContent.Text = ltrContent.Text.Replace("%Adres%", c.Adres);
                            ltrContent.Text = ltrContent.Text.Replace("%Telefon%", c.Telefon);
                            ltrContent.Text = ltrContent.Text.Replace("%Semt%", c.Semt);
                            ltrContent.Text = ltrContent.Text.Replace("%Sehir%", c.Sehir);
                        }
                    }
                    break;
            }

            bool isControlActive = true;

            TextBox txt = new TextBox();
            txt.ID = "Adi";
            txt.Text = m.Adi;
            txt.Enabled = !isControlActive;
            txt.ReadOnly = isControlActive;
            txt.CssClass = "noHtml emptyValidate";
            txt.MaxLength = 35;
            CustomizeControl1.AddControl("Adınız", txt);

            txt = new TextBox();
            txt.ID = "Mail";
            txt.Text = m.Mail;
            txt.Enabled = !isControlActive;
            txt.ReadOnly = isControlActive;
            txt.CssClass = "noHtml emptyValidate mailValidate";
            txt.MaxLength = 60;
            CustomizeControl1.AddControl("Mail", txt);

            txt = new TextBox();
            txt.ID = "Telefon";
            txt.Text = m.Telefon;
            txt.CssClass = "noHtml isNumber emptyValidate";
            txt.MaxLength = 16;
            CustomizeControl1.AddControl("Telefon", txt, "* Sizinle iletişim kurabilmemiz için gereklidir.");

            txt = new TextBox();
            txt.ID = "GSM";
            txt.Text = m.GSM;
            txt.CssClass = "noHtml isNumber emptyValidate";
            txt.MaxLength = 16;
            CustomizeControl1.AddControl("GSM (Cep)", txt, "* Sizinle iletişim kurabilmemiz için gereklidir.");

            DateTimeControl cnt = this.Page.LoadControl(Settings.DateTimeControlPath) as DateTimeControl;
            cnt.ID = "Tarih";
            cnt.OlusturmaTipi = DateTimeControl.CreateType.Randevu;
            CustomizeControl1.AddControl("Randevu Tarihi/Saati", cnt, "* Randevu saatinizi ve tarihi seçiniz.");
            cnt.TarihSaat = m.TarihSaat;

            txt = new TextBox();
            txt.ID = "Icerik";
            txt.Text = m.Icerik;
            txt.TextMode = TextBoxMode.MultiLine;
            txt.CssClass = "noHtml emptyValidate";
            txt.MaxLength = 250;
            txt.Height = 100;
            CustomizeControl1.AddControl("Not", txt, "Bu alana <b>250</b> karakter soru yazabilirsiniz.");

            DropDownList ddl = new DropDownList();
            ddl.ID = "Durum";
            ddl.Width = 300;
            ddl.DataMember = "Durumlar";
            ddl.DataValueField = "Key";
            ddl.DataTextField = "Value";
            ddl.DataSource = Settings.RandevuDurumlari();
            ddl.DataBind();
            ddl.SelectedValue = m.Durum.ToString();
            CustomizeControl1.AddControl("Durum", ddl);

            CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);
        }
        base.OnInit(e);
    }

    void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
    {
        try
        {
            if ((((DateTimeControl)controls["Tarih"]).TarihSaat > DateTime.Now)
                & !string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text)
                & !string.IsNullOrEmpty(((TextBox)controls["Mail"]).Text))
                using (Lib.Randevu m = Lib.RandevuMethods.GetRandevu(BAYMYO.UI.Converts.NullToGuid(Request.QueryString["rndid"])))
                {
                    if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(m.ID))
                    {
                        m.TarihSaat = ((DateTimeControl)controls["Tarih"]).TarihSaat;
                        m.Adi = ((TextBox)controls["Adi"]).Text;
                        m.Mail = ((TextBox)controls["Mail"]).Text;
                        m.Telefon = ((TextBox)controls["Telefon"]).Text;
                        m.GSM = ((TextBox)controls["GSM"]).Text;
                        m.Icerik = ((TextBox)controls["Icerik"]).Text;
                        m.Durum = BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["Durum"]).SelectedValue);

                        if (Lib.RandevuMethods.Update(m) > 0)
                        {
                            CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Info, "Randevu güncelleme işlemi başarılı bir şekilde gerçekleştirildi.");
                            Settings.ClearControls(controls);
                        }
                    }
                }
            else
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Randevu tarihi bugünden büyük olmalı ve diğer alanlardaki bilgileri lütfen kontorol ederek tekrar deneyiniz.");

        }
        catch (Exception)
        {
            CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, "İşleminiz sırasında yoğunluk sebebi ile bir sorun meydana geldi lütfen bilgilerinizi kontrol ederek tekrar deneyiniz.");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}