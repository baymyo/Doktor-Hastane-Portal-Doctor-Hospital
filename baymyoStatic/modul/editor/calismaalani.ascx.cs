using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_editor_calismaalani : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Muayenehane", "Ekleme/Düzeltme Formu");
        using (Lib.CalismaAlani m = Lib.CalismaAlaniMethods.GetCalismaAlani(BAYMYO.UI.Converts.NullToGuid(Request.QueryString["caid"])))
        {
            if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(m.ID) & !m.HesapID.Equals(Settings.CurrentUser().ID))
            {
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, "Bu alana erişim sağlayamıyorsunuz sistem sadece kendinize ait çalışma alanlarını güncelleme hakkı vermektedir. Lütfen sadece size ait çalışma alanlarını seçiniz seçiniz!");
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

            if (!m.Randevu & !BAYMYO.UI.Converts.NullToGuid(null).Equals(m.ID))
                if (string.IsNullOrEmpty(Request.QueryString["url"]))
                    Response.Redirect(Settings.VirtualPath + "?l=5", false);
                else
                    Response.Redirect(Request.QueryString["url"], false);

            CustomizeControl1.RemoveVisible = false;

            TextBox txt = new TextBox();
            txt.ID = "Kurum";
            txt.MaxLength = 100;
            txt.Text = m.Telefon;
            txt.CssClass = "noHtml kurum emptyValidate";
            CustomizeControl1.AddControl("Kurum", txt, "* Bu alan boş bırakılamaz!");

            txt = new TextBox();
            txt.ID = "Telefon";
            txt.MaxLength = 16;
            txt.Text = m.Telefon;
            txt.CssClass = "noHtml isNumber emptyValidate";
            CustomizeControl1.AddControl("Telefon", txt);

            txt = new TextBox();
            txt.ID = "Adres";
            txt.MaxLength = 100;
            txt.Text = m.Adres;
            txt.CssClass = "noHtml emptyValidate";
            CustomizeControl1.AddControl("Adres", txt);

            txt = new TextBox();
            txt.ID = "Semt";
            txt.MaxLength = 30;
            txt.Text = m.Semt;
            txt.CssClass = "noHtml emptyValidate";
            CustomizeControl1.AddControl("Semt(İlçe)", txt);

            txt = new TextBox();
            txt.ID = "Sehir";
            txt.MaxLength = 30;
            txt.Text = m.Sehir;
            txt.CssClass = "noHtml emptyValidate";
            CustomizeControl1.AddControl("Sehir(İL)", txt, "Belirteceğiniz <b>'İL'</b> sizi harita üzerinde bulunmanızı sağlayacaktır. Lütfen geçerli <b>'İL'</b> adı giriniz!");

            txt = new TextBox();
            txt.ID = "WebSitesi";
            txt.MaxLength = 60;
            txt.Text = m.WebSitesi;
            txt.CssClass = "noHtml";
            CustomizeControl1.AddControl("Web Sitesi", txt, "Lütfen başına 'Http://' eklemeden giriniz. Ör. www.sitenizinadi.com");

            CheckBox chk = new CheckBox();
            chk.ID = "Randevu";
            chk.Checked = m.Randevu;
            CustomizeControl1.AddControl("Randevu Aktif", chk);

            chk = new CheckBox();
            chk.ID = "Varsayilan";
            chk.Checked = m.Varsayilan;
            CustomizeControl1.AddControl("Varsayilan", chk);

            chk = new CheckBox();
            chk.ID = "Aktif";
            chk.Checked = m.Aktif;
            CustomizeControl1.AddControl("Yayımla", chk);

            CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);
        }

        base.OnInit(e);
    }

    void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
    {
        if (Settings.IsUserActive()
            & !string.IsNullOrEmpty(((TextBox)controls["Kurum"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Telefon"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Adres"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Semt"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Sehir"]).Text))
            using (Lib.CalismaAlani m = Lib.CalismaAlaniMethods.GetCalismaAlani(BAYMYO.UI.Converts.NullToGuid(Request.QueryString["caid"])))
            {
                m.Kurum = ((TextBox)controls["Kurum"]).Text;
                m.Telefon = ((TextBox)controls["Telefon"]).Text;
                m.Adres = ((TextBox)controls["Adres"]).Text;
                m.Semt = ((TextBox)controls["Semt"]).Text;
                m.Sehir = ((TextBox)controls["Sehir"]).Text;
                m.WebSitesi = ((TextBox)controls["WebSitesi"]).Text.Replace("http://", "").Replace("/", "");
                m.Randevu = ((CheckBox)controls["Randevu"]).Checked;
                m.Varsayilan = ((CheckBox)controls["Varsayilan"]).Checked;
                m.Aktif = ((CheckBox)controls["Aktif"]).Checked;

                if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(m.ID))
                {
                    if (Lib.CalismaAlaniMethods.Update(m) > 0)
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                }
                else
                {
                    m.HesapID = Settings.CurrentUser().ID;
                    if (Lib.CalismaAlaniMethods.Insert(m) > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Kayıt ekleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                        Settings.ClearControls(controls);
                    }
                }
            }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}