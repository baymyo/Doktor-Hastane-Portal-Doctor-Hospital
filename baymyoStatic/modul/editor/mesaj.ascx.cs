using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class modul_editor_mesaj : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        if (!Settings.IsUserActive())
        {
            CustomizeControl1.PanelVisible = false;
            Response.Redirect(Settings.VirtualPath + "?l=1", false);
            return;
        }

        CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Soru", "Yanıtlama Formu");
        using (Lib.Mesaj m = Lib.MesajMethods.GetMesaj(BAYMYO.UI.Converts.NullToInt64(Request.QueryString["mid"])))
        {
            if (m.ID > 0)
            {
                if (!m.HesapID.Equals(Settings.CurrentUser().ID))
                {
                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Error, "Bu alana erişim sağlayamıyorsunuz sistem sadece kendinize ait soruları yanıtlama hakkı vermektedir. Lütfen sadece size sorulan, soruları seçiniz!");
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

                CustomizeControl1.RemoveVisible = true;

                TextBox txt = new TextBox();
                txt.ID = "Adi";
                txt.CssClass = "noHtml emptyValidate";
                txt.Text = m.Adi;
                txt.MaxLength = 35;
                txt.Enabled = false;
                txt.ReadOnly = true;
                CustomizeControl1.AddControl("Adı", txt);

                txt = new TextBox();
                txt.ID = "Mail";
                txt.CssClass = "noHtml emptyValidate mailValidate";
                txt.Text = m.Mail;
                txt.MaxLength = 60;
                txt.Enabled = false;
                txt.ReadOnly = true;
                CustomizeControl1.AddControl("Mail", txt);

                txt = new TextBox();
                txt.ID = "Telefon";
                txt.CssClass = "noHtml isNumber emptyValidate";
                txt.Text = m.Telefon;
                txt.MaxLength = 16;
                txt.Enabled = false;
                txt.ReadOnly = true;
                CustomizeControl1.AddControl("Telefon", txt);

                txt = new TextBox();
                txt.ID = "Konu";
                txt.CssClass = "noHtml emptyValidate";
                txt.Text = m.Konu;
                txt.MaxLength = 50;
                CustomizeControl1.AddControl("Konu", txt);

                txt = new TextBox();
                txt.ID = "Icerik";
                txt.CssClass = "noHtml emptyValidate";
                txt.Text = m.Icerik;
                txt.TextMode = TextBoxMode.MultiLine;
                txt.MaxLength = 1000;
                txt.Height = 200;
                CustomizeControl1.AddControl("Soru", txt);

                txt = new TextBox();
                txt.ID = "Yanit";
                txt.CssClass = "noHtml emptyValidate";
                txt.Text = m.Yanit;
                txt.TextMode = TextBoxMode.MultiLine;
                txt.MaxLength = 1500;
                txt.Height = 200;
                CustomizeControl1.AddControl("Yanit", txt);

                DropDownList ddl = new DropDownList();
                ddl.ID = "Durum";
                ddl.Width = 450;
                ddl.DataMember = "Durumlar";
                ddl.DataValueField = "Key";
                ddl.DataTextField = "Value";
                ddl.DataSource = Settings.MesajDurumlari();
                ddl.DataBind();

                if (string.IsNullOrEmpty(m.Yanit))
                    ddl.SelectedIndex = 1;
                else
                    ddl.SelectedValue = m.Durum.ToString();
                CustomizeControl1.AddControl("Durum", ddl);

                ddl = new DropDownList();
                ddl.ID = "Aktif";
                ddl.Width = 450;
                ddl.DataMember = "YayimlamaDurumlari";
                ddl.DataValueField = "Key";
                ddl.DataTextField = "Value";
                ddl.DataSource = Settings.YayimlamaDurumlari();
                ddl.DataBind();
                CustomizeControl1.AddControl("Kime Görünsün", ddl);

                CheckBox chk = new CheckBox();
                chk.ID = "MailGonder";
                chk.Checked = false;
                CustomizeControl1.AddControl("Mail Gönder", chk);

                CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);
                CustomizeControl1.RemoveClick += new CustomizeControl.ButtonEvent(CustomizeControl1_RemoveClick);
            }
        }

        base.OnInit(e);
    }

    void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
    {
        if (!string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Mail"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Icerik"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Yanit"]).Text))
            using (Lib.Mesaj m = Lib.MesajMethods.GetMesaj(BAYMYO.UI.Converts.NullToInt64(Request.QueryString["mid"])))
            {
                if (m.HesapID.Equals(Settings.CurrentUser().ID))
                {
                    m.Adi = ((TextBox)controls["Adi"]).Text;
                    m.Mail = ((TextBox)controls["Mail"]).Text;
                    m.Telefon = ((TextBox)controls["Telefon"]).Text;
                    m.Konu = ((TextBox)controls["Konu"]).Text;
                    m.Icerik = ((TextBox)controls["Icerik"]).Text;
                    m.Yanit = ((TextBox)controls["Yanit"]).Text;
                    m.Durum = BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["Durum"]).SelectedValue);
                    m.Aktif = BAYMYO.UI.Converts.NullToBool(((DropDownList)controls["Aktif"]).SelectedValue);
                    if (m.ID > 0)
                    {
                        m.GuncellemeTarihi = DateTime.Now;
                        if (Lib.MesajMethods.Update(m) > 0)
                        {
                            if (((CheckBox)controls["MailGonder"]).Checked)
                            {
                                if (Settings.SendMail(m.Mail, m.Adi, Settings.ContactMail, Settings.ContactName, m.Konu, m.Icerik, true))
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme ve Mail gönderme işleminiz başarılı bir şekilde tamamlandı.!');", true);
                                else
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Mail gönderilemedi fakat güncelleme işlemi tamamlandı!');", true);
                            }
                            else
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                        }
                    }
                    else
                    {
                        m.HesapID = Settings.CurrentUser().ID;
                        m.KayitTarihi = DateTime.Now;
                        m.GuncellemeTarihi = DateTime.Now;
                        if (Lib.MesajMethods.Insert(m) > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Kayıt ekleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                            Settings.ClearControls(controls);
                        }
                    }
                }
            }
    }

    void CustomizeControl1_RemoveClick(SortedDictionary<string, Control> controls)
    {
        Int64 id = BAYMYO.UI.Converts.NullToInt64(Request["mid"]);
        if (id > 0)
        {
            Settings.DeleteLinks("mesaj", id.ToString());
            if (Lib.MesajMethods.Delete(id) > 0)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Silme işleminiz başarılı bir şekilde tamamlandı!');", true);
                Settings.ClearControls(controls);
            }
        }
        id = 0;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "Soru Yanıtlama - " + Settings.CurrentUser().Adi + " " + Settings.CurrentUser().Soyadi;
    }
}