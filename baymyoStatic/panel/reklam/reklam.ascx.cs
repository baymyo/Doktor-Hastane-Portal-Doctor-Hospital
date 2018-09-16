using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class panel_reklam_reklam : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Reklam", "Ekleme/Düzeltme Formu");
        using (Lib.Reklam m = Lib.ReklamMethods.GetReklam(BAYMYO.UI.Converts.NullToInt(Request.QueryString["rklid"])))
        {
            CustomizeControl1.RemoveVisible = (m.ID > 0);

            TextBox txt = new TextBox();
            txt.ID = "BannerName";
            txt.CssClass = "noHtml emptyValidate";
            txt.Text = m.BannerName;
            txt.MaxLength = 75;
            CustomizeControl1.AddControl("Reklam Adı", txt, "Liste üzerinde görünen isim!");

            FileUpload flu = new FileUpload();
            flu.ID = "ImageUrl";
            flu.ToolTip = m.ImageUrl;
            CustomizeControl1.AddControl("Reklam Dosyası", flu);

            txt = new TextBox();
            txt.ID = "NavigateUrl";
            txt.CssClass = "noHtml";
            txt.Text = m.NavigateUrl;
            txt.MaxLength = 75;
            CustomizeControl1.AddControl("Bağlantı Adresi", txt, "Örnek; http://www.siteadresi.com");

            txt = new TextBox();
            txt.ID = "AlternateText";
            txt.CssClass = "noHtml";
            txt.Text = m.AlternateText;
            txt.MaxLength = 100;
            CustomizeControl1.AddControl("Alternatif Yazı", txt, "Alternatif yazı, resimin görüntülenemediği durumlar için.");

            txt = new TextBox();
            txt.ID = "Keyword";
            txt.CssClass = "noHtml";
            txt.Text = m.Keyword;
            txt.MaxLength = 100;
            CustomizeControl1.AddControl("Keywords", txt, string.Format("Lütfen virgül({0}) ile ayrıarak ve boşluk bırakmadan yazınız! Örnek: elma{0}meyve{0}sebze{0}bahçe", Settings.SplitFormat));

            txt = new TextBox();
            txt.ID = "Impressions";
            txt.CssClass = "noHtml isNumber";
            txt.Text = m.Impressions.ToString();
            txt.MaxLength = 100;
            CustomizeControl1.AddControl("Impressions", txt, "Sadece sayısal değer giriniz.");

            DropDownList ddl = new DropDownList();
            ddl.ID = "ReklamTipleri";
            ddl.Width = 300;
            ddl.DataValueField = "Key";
            ddl.DataTextField = "Value";
            ddl.DataSource = Settings.ReklamTipleri();
            ddl.DataBind();
            ddl.SelectedValue = m.Width + "x" + m.Height;
            CustomizeControl1.AddControl("Reklam Tipleri", ddl);

            CheckBox chk = new CheckBox();
            chk.ID = "IsActive";
            chk.Checked = m.IsActive;
            CustomizeControl1.AddControl("Yayımla", chk);

            CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);
            CustomizeControl1.RemoveClick += new CustomizeControl.ButtonEvent(CustomizeControl1_RemoveClick);
        }

        base.OnInit(e);
    }

    void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
    {
        if (!string.IsNullOrEmpty(((TextBox)controls["BannerName"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["NavigateUrl"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Impressions"]).Text))
            using (Lib.Reklam m = Lib.ReklamMethods.GetReklam(BAYMYO.UI.Converts.NullToInt(Request.QueryString["rklid"])))
            {
                m.BannerName = ((TextBox)controls["BannerName"]).Text;
                m.NavigateUrl = ((TextBox)controls["NavigateUrl"]).Text;
                m.AlternateText = ((TextBox)controls["AlternateText"]).Text;
                m.Keyword = ((TextBox)controls["Keyword"]).Text;
                m.Impressions = BAYMYO.UI.Converts.NullToInt(((TextBox)controls["Impressions"]).Text);
                m.Width = BAYMYO.UI.Converts.NullToInt(((DropDownList)controls["ReklamTipleri"]).SelectedValue.Split('x')[0]);
                m.Height = BAYMYO.UI.Converts.NullToInt(((DropDownList)controls["ReklamTipleri"]).SelectedValue.Split('x')[1]);
                m.IsActive = ((CheckBox)controls["IsActive"]).Checked;
                if (m.ID > 0)
                {
                    if ((controls["ImageUrl"] as FileUpload).HasFile)
                        if (BAYMYO.UI.FileIO.Remove(Server.MapPath(m.ImageUrl)))
                            m.ImageUrl = Settings.ImagesPath + "reklam/" + BAYMYO.UI.FileIO.Upload(controls["ImageUrl"] as FileUpload, Server.MapPath(Settings.ImagesPath + "reklam/"));
                    if (Lib.ReklamMethods.Update(m) > 0)
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                }
                else if ((controls["ImageUrl"] as FileUpload).HasFile)
                {
                    m.ImageUrl = Settings.ImagesPath + "reklam/" + BAYMYO.UI.FileIO.Upload(controls["ImageUrl"] as FileUpload, Server.MapPath(Settings.ImagesPath + "reklam/"));
                    if (Lib.ReklamMethods.Insert(m) > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Kayıt ekleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                        Settings.ClearControls(controls);
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Lütfen reklam dosyası seçininiz!');", true);
            }
    }

    void CustomizeControl1_RemoveClick(SortedDictionary<string, Control> controls)
    {
        using (Lib.Reklam m = Lib.ReklamMethods.GetReklam(BAYMYO.UI.Converts.NullToInt(Request.QueryString["rklid"])))
        {
            if (m != null)
                if (BAYMYO.UI.FileIO.Remove(Server.MapPath(m.ImageUrl)))
                    if (Lib.ReklamMethods.Delete(m) > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Silme işleminiz başarılı bir şekilde tamamlandı!');", true);
                        Settings.ClearControls(controls);
                    }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}