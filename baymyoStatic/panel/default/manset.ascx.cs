using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class panel_manset_manset : System.Web.UI.UserControl
{
    private void CreateXml()
    {
        try
        {
            using (System.Data.DataTable dt = new System.Data.DataTable("Manset"))
            {
                using (BAYMYO.UI.Web.CustomSqlQuery query = new BAYMYO.UI.Web.CustomSqlQuery(dt, "Manset", "KayitTarihi DESC", "Aktif=1"))
                {
                    query.Top = 9;
                    query.Execute();
                    dt.WriteXml(Server.MapPath(Settings.XmlPath + "manset.xml"), System.Data.XmlWriteMode.WriteSchema);
                }
            }
        }
        catch (Exception)
        {
        }
    }

    string modulID, icerikID, modulPath;
    protected override void OnInit(EventArgs e)
    {
        CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Manşet", "Ekleme/Düzeltme Formu");
        modulID = Request.QueryString["mdl"].Trim();
        icerikID = Request.QueryString["mcid"].Trim();
        modulPath = Settings.ImagesPath + "manset/" + modulID + "/";
        using (Lib.Manset m = Lib.MansetMethods.GetManset(modulID, icerikID))
        {
            CustomizeControl1.RemoveVisible = !(BAYMYO.UI.Converts.NullToGuid(null).Equals(m.ID));

            Image img = new Image();
            img.ID = "Resim";
            img.ToolTip = m.ResimUrl;
            if (!string.IsNullOrEmpty(m.ResimUrl))
                img.ImageUrl = modulPath + m.ResimUrl;
            else
                img.ImageUrl = Settings.ImagesPath + "yok.png";
            CustomizeControl1.AddControl("Büyük Resim", img);

            FileUpload flu = new FileUpload();
            flu.ID = "ResimUrl";
            flu.ToolTip = m.ResimUrl;
            CustomizeControl1.AddControl("Büyük Resim", flu);

            img = new Image();
            img.ID = "KucukResim";
            img.ToolTip = m.ResimKucuk;
            if (!string.IsNullOrEmpty(m.ResimKucuk))
                img.ImageUrl = modulPath + m.ResimKucuk;
            else
                img.ImageUrl = Settings.ImagesPath + "yok.png";
            CustomizeControl1.AddControl("Küçük Resim", img);

            flu = new FileUpload();
            flu.ID = "ResimKucuk";
            flu.ToolTip = m.ResimKucuk;
            CustomizeControl1.AddControl("Küçük Resim", flu);

            TextBox txt = new TextBox();
            txt.ID = "Baslik";
            txt.CssClass = "noHtml emptyValidate";
            txt.Text = m.Baslik;
            txt.MaxLength = 35;
            CustomizeControl1.AddControl("Başlık", txt, "İçeriğe dikkat çekecek başlık giriniz.");

            txt = new TextBox();
            txt.ID = "Aciklama";
            txt.CssClass = "noHtml";
            txt.Text = m.Aciklama;
            txt.TextMode = TextBoxMode.MultiLine;
            txt.MaxLength = 250;
            CustomizeControl1.AddControl("Açıklama", txt, "Açıklama girmezseniz manşet gösterim kısımında sadece resim görünecektir.");

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
        if (!string.IsNullOrEmpty(Request.QueryString["mdl"])
            & !string.IsNullOrEmpty(((TextBox)controls["Baslik"]).Text))
            using (Lib.Manset m = Lib.MansetMethods.GetManset(modulID, icerikID))
            {
                m.ModulID = modulID;
                m.IcerikID = icerikID;
                m.Baslik = ((TextBox)controls["Baslik"]).Text;
                m.Aciklama = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Aciklama"]).Text, 250);
                m.Aktif = ((CheckBox)controls["Aktif"]).Checked;
                if (!(BAYMYO.UI.Converts.NullToGuid(null).Equals(m.ID)))
                {
                    if ((controls["ResimUrl"] as FileUpload).HasFile)
                        if (BAYMYO.UI.FileIO.Remove(Server.MapPath(modulPath + m.ResimUrl)))
                            m.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["ResimUrl"] as FileUpload, m.Baslik + "-buyuk", Server.MapPath(modulPath), 647);
                    if ((controls["ResimKucuk"] as FileUpload).HasFile)
                        if (BAYMYO.UI.FileIO.Remove(Server.MapPath(modulPath + m.ResimKucuk)))
                            m.ResimKucuk = BAYMYO.UI.FileIO.UploadImage(controls["ResimKucuk"] as FileUpload, m.Baslik + "-kucuk", Server.MapPath(modulPath), 53);
                    if (Lib.MansetMethods.Update(m) > 0)
                    {
                        ((Image)controls["Resim"]).ImageUrl = modulPath + m.ResimUrl;
                        ((Image)controls["KucukResim"]).ImageUrl = modulPath + m.ResimKucuk;
                        CreateXml();
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                    }
                }
                else
                {
                    m.KayitTarihi = DateTime.Now;
                    m.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["ResimUrl"] as FileUpload, m.Baslik + "-buyuk", Server.MapPath(modulPath), 647);
                    m.ResimKucuk = BAYMYO.UI.FileIO.UploadImage(controls["ResimKucuk"] as FileUpload, m.Baslik + "-kucuk", Server.MapPath(modulPath), 53);
                    if (Lib.MansetMethods.Insert(m) > 0)
                    {
                        ((Image)controls["Resim"]).ImageUrl = modulPath + m.ResimUrl;
                        ((Image)controls["KucukResim"]).ImageUrl = modulPath + m.ResimKucuk;
                        CreateXml();
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Kayıt ekleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                    }
                }
            }
    }

    void CustomizeControl1_RemoveClick(SortedDictionary<string, Control> controls)
    {
        using (Lib.Manset m = Lib.MansetMethods.GetManset(modulID, icerikID))
        {
            if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "manset/" + m.ModulID + "/" + m.ResimUrl)))
                if (Lib.MansetMethods.Delete(m) > 0)
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