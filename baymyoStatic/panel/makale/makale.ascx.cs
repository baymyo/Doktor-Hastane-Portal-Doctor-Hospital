using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class panel_makale_makale : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Makale", "Ekleme/Düzeltme Formu");
        using (Lib.Makale m = Lib.MakaleMethods.GetMakale(BAYMYO.UI.Converts.NullToInt64(Request.QueryString["mklid"])))
        {
            CustomizeControl1.RemoveVisible = (m.ID > 0);

            TextBox txt = new TextBox();
            txt.ID = "Baslik";
            txt.CssClass = "noHtml emptyValidate";
            txt.Text = m.Baslik;
            txt.MaxLength = 75;
            CustomizeControl1.AddControl("Baslik", txt);

            txt = new TextBox();
            txt.ID = "Ozet";
            txt.CssClass = "noHtml emptyValidate";
            txt.Text = m.Ozet;
            txt.TextMode = TextBoxMode.MultiLine;
            txt.MaxLength = 250;
            CustomizeControl1.AddControl("Ozet", txt, "Liste ve RSS'ler için gösterilecek içeriktir.");

            txt = new TextBox();
            txt.ID = "Icerik";
            txt.Height = 400;
            txt.Text = m.Icerik;
            txt.CssClass = "mceAdvanced";
            txt.TextMode = TextBoxMode.MultiLine;
            CustomizeControl1.AddControl("Editör", txt);

            txt = new TextBox();
            txt.ID = "Etiket";
            txt.CssClass = "noHtml";
            txt.Text = m.Etiket;
            txt.MaxLength = 100;
            CustomizeControl1.AddControl("Etiket", txt, string.Format("Lütfen virgül({0}) ile ayrıarak ve boşluk bırakmadan yazınız! Örnek: elma{0}meyve{0}sebze{0}bahçe", Settings.SplitFormat));

            txt = new TextBox();
            txt.ID = "Tarih";
            txt.CssClass = "noHtml";
            txt.CssClass = "dateTimePicker";
            if (m.KayitTarihi.Year > 2000)
                txt.Text = m.KayitTarihi.ToString();
            else
                txt.Text = DateTime.Now.ToShortDateString();
            CustomizeControl1.AddControl("Tarih", txt);

            DropDownList ddl = new DropDownList();
            ddl.ID = "Kategori";
            ddl.Width = 450;
            ddl.DataMember = "Kategori";
            ddl.DataValueField = "ID";
            ddl.DataTextField = "Adi";
            List<Lib.Kategori> kategoriler = Lib.KategoriMethods.GetMenu("makale", true);
            ListItem item = null;
            foreach (Lib.Kategori kategori in kategoriler)
            {
                switch (kategori.ParentID)
                {
                    case "":
                        item = new ListItem(kategori.Adi, kategori.ID);
                        item.Attributes.CssStyle.Value = "padding-left: 5px;background: #f5f5f5; color: #454545;";
                        break;
                    case "0":
                        item = new ListItem("+ " + kategori.Adi, kategori.ID);
                        item.Attributes.CssStyle.Value = "padding-left: 25px;background: #f5f5f5; color: #fe760c; font-weight: bold;";
                        break;
                    default:
                        item = new ListItem("-> " + kategori.Adi, kategori.ID);
                        item.Attributes.CssStyle.Value = string.Format("padding-left: {0}px;background: #f5f5f5; color: #454545;", (BAYMYO.UI.Converts.NullToInt(kategori.ParentID.Split(',').Length + 1) * 25));
                        break;
                }
                ddl.Items.Add(item);
            }
            kategoriler.Clear();
            ddl.SelectedValue = BAYMYO.UI.Converts.NullToString(m.KategoriID);
            CustomizeControl1.AddControl("Kategori", ddl);

            ddl = new DropDownList();
            ddl.ID = "Durum";
            ddl.Width = 300;
            ddl.DataValueField = "Key";
            ddl.DataTextField = "Value";
            ddl.DataSource = Settings.MakaleDurumlari();
            ddl.DataBind();
            ddl.SelectedValue = BAYMYO.UI.Converts.NullToByte(m.Durum).ToString();
            CustomizeControl1.AddControl("Durumu", ddl);

            FileUpload flu = new FileUpload();
            flu.ID = "ResimUrl";
            flu.ToolTip = m.ResimUrl;
            CustomizeControl1.AddControl("Resim Ekle", flu);

            CheckBox chk = new CheckBox();
            chk.ID = "Uye";
            chk.Checked = m.Uye;
            CustomizeControl1.AddControl("Sadece Üyeler", chk);

            chk = new CheckBox();
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
            & !string.IsNullOrEmpty(((TextBox)controls["Ozet"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Icerik"]).Text))
            using (Lib.Makale m = Lib.MakaleMethods.GetMakale(BAYMYO.UI.Converts.NullToInt64(Request.QueryString["mklid"])))
            {
                m.Baslik = ((TextBox)controls["Baslik"]).Text;
                m.Ozet = ((TextBox)controls["Ozet"]).Text;
                m.Icerik = ((TextBox)controls["Icerik"]).Text;
                m.Etiket = ((TextBox)controls["Etiket"]).Text;
                if (!string.IsNullOrEmpty(((TextBox)controls["Tarih"]).Text))
                    m.KayitTarihi = BAYMYO.UI.Converts.NullToDateTime(((TextBox)controls["Tarih"]).Text + " " + DateTime.Now.ToLongTimeString());
                else
                    m.KayitTarihi = DateTime.Now;
                m.KategoriID = BAYMYO.UI.Converts.NullToString(((DropDownList)controls["Kategori"]).SelectedValue);
                m.Durum = BAYMYO.UI.Converts.NullToByte(((DropDownList)controls["Durum"]).SelectedValue);
                m.Uye = ((CheckBox)controls["Uye"]).Checked;
                m.Yorum = ((CheckBox)controls["Yorum"]).Checked;
                m.Aktif = ((CheckBox)controls["Aktif"]).Checked;
                if (m.ID > 0)
                {
                    if ((controls["ResimUrl"] as FileUpload).HasFile)
                        if (BAYMYO.UI.FileIO.Remove(Server.MapPath(Settings.ImagesPath + "makale/" + m.ResimUrl)))
                            m.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["ResimUrl"] as FileUpload, m.Baslik, Server.MapPath(Settings.ImagesPath + "makale/"), 290, true);
                    if (Lib.MakaleMethods.Update(m) > 0)
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                }
                else
                {
                    m.HesapID = Settings.CurrentUser().ID;
                    m.ResimUrl = BAYMYO.UI.FileIO.UploadImage(controls["ResimUrl"] as FileUpload, m.Baslik, Server.MapPath(Settings.ImagesPath + "makale/"), 290, true);
                    if (Lib.MakaleMethods.Insert(m) > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Kayıt ekleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                        Settings.ClearControls(controls);
                    }
                }
            }
    }

    void CustomizeControl1_RemoveClick(SortedDictionary<string, Control> controls)
    {
        Int64 id = BAYMYO.UI.Converts.NullToInt64(Request.QueryString["mklid"]);
        if (id > 0)
        {
            Settings.DeleteLinks("makale", id.ToString());
            if (Lib.MakaleMethods.Delete(id) > 0)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Silme işleminiz başarılı bir şekilde tamamlandı!');", true);
                Settings.ClearControls(controls);
            }
        }
        id = 0;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Title = "Yeni Makale - " + Settings.CurrentUser().Adi + " " + Settings.CurrentUser().Soyadi;
    }
}