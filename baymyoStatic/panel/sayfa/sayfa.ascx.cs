using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class panel_sayfa_sayfa : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Sayfa", "Tanımlama");
        using (Lib.Sayfa m = Lib.SayfaMethods.GetSayfa(BAYMYO.UI.Converts.NullToInt16(Request.QueryString["sid"])))
        {
            CustomizeControl1.RemoveVisible = (m.ID > 0);

            TextBox txt = new TextBox();
            txt.ID = "Baslik";
            txt.CssClass = "noHtml emptyValidate";
            txt.Text = m.Baslik;
            txt.MaxLength = 50;
            CustomizeControl1.AddControl("Başlık", txt);

            txt = new TextBox();
            txt.ID = "Icerik";
            txt.Height = 400;
            txt.Text = m.Icerik;
            txt.CssClass = "mceAdvanced";
            txt.TextMode = TextBoxMode.MultiLine;
            CustomizeControl1.AddControl("İçerik", txt);

            DropDownList ddl = new DropDownList();
            ddl.ID = "Tipi";
            ddl.Width = 300;
            ddl.DataValueField = "Key";
            ddl.DataTextField = "Value";
            ddl.DataSource = Settings.SayfaTipleri();
            ddl.DataBind();
            ddl.SelectedValue = m.Tipi.ToString();
            CustomizeControl1.AddControl("Gösterim", ddl);

            ddl = new DropDownList();
            ddl.ID = "Dil";
            ddl.Width = 300;
            ddl.DataValueField = "Key";
            ddl.DataTextField = "Value";
            ddl.DataSource = Settings.DilSecenekleri();
            ddl.DataBind();
            ddl.SelectedValue = m.Dil;
            CustomizeControl1.AddControl("Dil", ddl);

            CheckBox chk = new CheckBox();
            chk.ID = "Aktif";
            chk.Checked = m.Aktif;
            CustomizeControl1.AddControl("Yayımla", chk);

            CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);
            CustomizeControl1.RemoveClick += new CustomizeControl.ButtonEvent(CustomizeControl1_RemoveClick);
        }

        base.OnInit(e);
    }

    void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
    {
        if (!string.IsNullOrEmpty(((TextBox)controls["Baslik"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Icerik"]).Text))
            using (Lib.Sayfa m = Lib.SayfaMethods.GetSayfa(BAYMYO.UI.Converts.NullToInt16(Request.QueryString["sid"])))
            {
                m.Baslik = ((TextBox)controls["Baslik"]).Text;
                m.Icerik = ((TextBox)controls["Icerik"]).Text;
                m.Tipi = BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["Tipi"]).SelectedValue);
                m.Dil = ((DropDownList)controls["Dil"]).SelectedValue;
                m.Aktif = ((CheckBox)controls["Aktif"]).Checked;
                if (m.ID > 0)
                {
                    if (Lib.SayfaMethods.Update(m) > 0)
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                }
                else
                {
                    m.HesapID = Settings.CurrentUser().ID;
                    m.KayitTarihi = DateTime.Now;
                    if (Lib.SayfaMethods.Insert(m) > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Kayıt ekleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                        Settings.ClearControls(controls);
                    }
                }
            }
    }

    void CustomizeControl1_RemoveClick(SortedDictionary<string, Control> controls)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["sid"]))
            if (Lib.SayfaMethods.Delete(BAYMYO.UI.Converts.NullToInt16(Request["sid"])) > 0)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Silme işleminiz başarılı bir şekilde tamamlandı!');", true);
                Settings.ClearControls(controls);
            }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}