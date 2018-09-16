using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class panel_video_video : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Video", "Ekleme/Düzeltme Formu");
        using (Lib.Video m = Lib.VideoMethods.GetVideo(BAYMYO.UI.Converts.NullToInt64(Request.QueryString["vid"])))
        {
            CustomizeControl1.RemoveVisible = (m.ID > 0);

            TextBox txt = new TextBox();
            txt.ID = "Baslik";
            txt.CssClass = "noHtml emptyValidate";
            txt.Text = m.Baslik;
            txt.MaxLength = 75;
            CustomizeControl1.AddControl("Baslik", txt);

            txt = new TextBox();
            txt.ID = "Embed";
            txt.Text = m.Embed;
            txt.TextMode = TextBoxMode.MultiLine;
            txt.MaxLength = 750;
            CustomizeControl1.AddControl("Embed", txt, "Her hangi bir video sitesinden 'embed' kodu almanız gereklidir.");

            txt = new TextBox();
            txt.ID = "Etiket";
            txt.CssClass = "noHtml";
            txt.Text = m.Etiket;
            txt.MaxLength = 100;
            CustomizeControl1.AddControl("Etiket", txt, string.Format("Lütfen virgül({0}) ile ayrıarak ve boşluk bırakmadan yazınız! Örnek: elma{0}meyve{0}sebze{0}bahçe", Settings.SplitFormat));

            DropDownList ddl = new DropDownList();
            ddl.ID = "Kategori";
            ddl.Width = 450;
            ddl.DataMember = "Kategori";
            ddl.DataValueField = "ID";
            ddl.DataTextField = "Adi";
            ddl.DataSource = Lib.KategoriMethods.GetMenu("video", true);
            ddl.DataBind();
            ddl.SelectedValue = BAYMYO.UI.Converts.NullToString(m.KategoriID);
            CustomizeControl1.AddControl("Kategori", ddl, "<a href=\"" + Settings.PanelPath + "?go=kategori&mdl=video\">[+] Yeni Kategori Ekle</a>");

            FileUpload flu = new FileUpload();
            flu.ID = "ResimUrl";
            flu.ToolTip = m.ResimUrl;
            CustomizeControl1.AddControl("Resim Ekle", flu);

            CheckBox chk = new CheckBox();
            chk.ID = "Yorum";
            chk.Checked = m.Yorum;
            CustomizeControl1.AddControl("Yorum Aktif", chk);

            chk = new CheckBox();
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
            & !string.IsNullOrEmpty(((TextBox)controls["Embed"]).Text))
            using (Lib.Video m = Lib.VideoMethods.GetVideo(BAYMYO.UI.Converts.NullToInt64(Request.QueryString["vid"])))
            {
                m.Baslik = ((TextBox)controls["Baslik"]).Text;
                m.Embed = ((TextBox)controls["Embed"]).Text;
                m.Etiket = ((TextBox)controls["Etiket"]).Text;
                m.KategoriID = BAYMYO.UI.Converts.NullToString(((DropDownList)controls["Kategori"]).SelectedValue);
                m.Yorum = ((CheckBox)controls["Yorum"]).Checked;
                m.Aktif = ((CheckBox)controls["Aktif"]).Checked;
                if (m.ID > 0)
                {
                    if ((controls["ResimUrl"] as FileUpload).HasFile)
                        if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "video/" + m.ResimUrl)))
                            m.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["ResimUrl"] as FileUpload, Server.MapPath(Settings.ImagesPath + "video/"), 100, true);
                    if (Lib.VideoMethods.Update(m) > 0)
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                }
                else
                {
                    m.HesapID = Settings.CurrentUser().ID;
                    m.KayitTarihi = DateTime.Now;
                    m.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["ResimUrl"] as FileUpload, Server.MapPath(Settings.ImagesPath + "video/"), 100, true);
                    if (Lib.VideoMethods.Insert(m) > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Kayıt ekleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                        Settings.ClearControls(controls);
                    }
                }
            }
    }

    void CustomizeControl1_RemoveClick(SortedDictionary<string, Control> controls)
    {
        Int64 id = BAYMYO.UI.Converts.NullToInt64(Request.QueryString["vid"]);
        if (id > 0)
        {
            Settings.DeleteLinks("video", id.ToString());
            if (Lib.VideoMethods.Delete(id) > 0)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Silme işleminiz başarılı bir şekilde tamamlandı!');", true);
                Settings.ClearControls(controls);
            }
        }
        id = 0;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}