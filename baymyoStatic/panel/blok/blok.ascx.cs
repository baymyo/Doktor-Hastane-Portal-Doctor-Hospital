using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAYMYO.UI;

public partial class panel_blok_blok : System.Web.UI.UserControl
{
    protected override void OnInit(EventArgs e)
    {
        CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Blok", "Ekleme/Düzeltme Formu");
        using (System.Data.DataTable xmlData = Lib.Blok.GetXmlData())
        {
            xmlData.DefaultView.RowFilter = "";

            System.Data.DataRowView rowItem;
            if (!string.IsNullOrEmpty(Request.QueryString["bid"]))
            {
                xmlData.DefaultView.RowFilter = "ID=" + Request.QueryString["bid"];
                if (xmlData.DefaultView.Count > 0)
                    rowItem = xmlData.DefaultView[0];
                else
                    rowItem = xmlData.DefaultView.AddNew();
            }
            else
                rowItem = xmlData.DefaultView.AddNew();

            switch (Request.QueryString["type"])
            {
                case "1":
                case "ascx":
                    AscxControls(rowItem);
                    break;
                case "2":
                case "html":
                    HtmlControls(rowItem);
                    break;
            }

            CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);
            CustomizeControl1.RemoveClick += new CustomizeControl.ButtonEvent(CustomizeControl1_RemoveClick);
        }

        base.OnInit(e);
    }

    private void AscxControls(System.Data.DataRowView dataRow)
    {
        DropDownList ddl = new DropDownList();
        ddl.ID = "Bloklar";
        ddl.Width = 300;
        ddl.DataValueField = "Key";
        ddl.DataTextField = "Value";
        ddl.DataSource = Lib.Blok.GetDosyalar(Lib.BlokTipi.Ascx);
        ddl.DataBind();
        ddl.SelectedValue = Converts.NullToString(dataRow["Adi"]);
        CustomizeControl1.AddControl("Bloklar", ddl);

        TextBox txt = new TextBox();
        txt.ID = "Baslik";
        txt.CssClass = "noHtml emptyValidate";
        txt.MaxLength = 50;
        txt.Text = Converts.NullToString(dataRow["Baslik"]);
        CustomizeControl1.AddControl("Başlık", txt, "Girilen başlık dosya adı olarak kayıt edilecektir.");

        ddl = new DropDownList();
        ddl.ID = "Yer";
        ddl.Width = 300;
        ddl.DataValueField = "Key";
        ddl.DataTextField = "Value";
        ddl.DataSource = Lib.Blok.GetYerlesimler();
        ddl.DataBind();
        ddl.SelectedValue = Converts.NullToString(dataRow["Yer"]);
        CustomizeControl1.AddControl("Yerleşim", ddl, "Bloğun gösterileceği yeri temsil eder.");

        //ddl = new DropDownList();
        //ddl.ID = "SablonTipi";
        //ddl.Width = 300;
        //ddl.DataValueField = "Key";
        //ddl.DataTextField = "Value";
        //ddl.DataSource = Lib.Blok.GetSablonTipleri(false);
        //ddl.DataBind();
        //ddl.SelectedValue = Converts.NullToString(dataRow["SablonTipi"]);
        //CustomizeControl1.AddControl("Şablonlar", ddl, "Blok üzerinde tema giydirilip, giydirilemeyeceğini belirler.");

        ddl = new DropDownList();
        ddl.ID = "Sira";
        ddl.Width = 300;
        ddl.DataValueField = "Key";
        ddl.DataTextField = "Value";
        ddl.DataSource = Settings.SiraNumaralari();
        ddl.DataBind();
        ddl.SelectedIndex = Converts.NullToInt(dataRow["Sira"]);
        CustomizeControl1.AddControl("Sıra", ddl, "Bloğun yerleşim sırasını belirler.");

        //ddl = new DropDownList();
        //ddl.ID = "Dil";
        //ddl.Width = 300;
        //ddl.DataValueField = "Key";
        //ddl.DataTextField = "Value";
        //ddl.DataSource = Settings.DilSecenekleri();
        //ddl.DataBind();
        //ddl.SelectedValue = Converts.NullToString(dataRow["Dil"]);
        //CustomizeControl1.AddControl("Dil", ddl, "Bloğun hangi dilde gösterileceğini belirler.");

        CheckBox chk;// = new CheckBox();
        //chk.ID = "TumDil";
        //chk.Checked = Converts.NullToBool(dataRow["TumDil"]);
        //CustomizeControl1.AddControl("Tüm Diller", chk);

        txt = new TextBox();
        txt.ID = "Sayfa";
        txt.CssClass = "noHtml";
        txt.MaxLength = 200;
        txt.Text = Converts.NullToString(dataRow["Sayfa"]);
        CustomizeControl1.AddControl("Sayfa", txt, "Bloğun gösterileceği sayfayı belirler.");

        chk = new CheckBox();
        chk.ID = "TumSayfa";
        chk.Checked = Converts.NullToBool(dataRow["TumSayfa"]);
        CustomizeControl1.AddControl("Tüm Sayfalar", chk);

        chk = new CheckBox();
        chk.ID = "Aktif";
        chk.Checked = Converts.NullToBool(dataRow["Aktif"]);
        CustomizeControl1.AddControl("Yayımla", chk);
    }

    private void HtmlControls(System.Data.DataRowView dataRow)
    {
        TextBox txt = new TextBox();
        txt.ID = "Baslik";
        txt.CssClass = "noHtml emptyValidate";
        txt.MaxLength = 50;
        txt.Text = Converts.NullToString(dataRow["Baslik"]);
        CustomizeControl1.AddControl("Başlık", txt, "Girilen başlık dosya adı olarak kayıt edilecektir.");

        txt = new TextBox();
        txt.ID = "Editor";
        txt.Height = 400;
        txt.TextMode = TextBoxMode.MultiLine;
        txt.CssClass = "mceAdvanced";
        txt.Text = FileIO.ReadText(Server.MapPath(Lib.Blok.HtmlPath + Converts.NullToString(dataRow["Adi"])));
        CustomizeControl1.AddControl("Editör", txt);

        DropDownList ddl = new DropDownList();
        ddl.ID = "Yer";
        ddl.Width = 300;
        ddl.DataValueField = "Key";
        ddl.DataTextField = "Value";
        ddl.DataSource = Lib.Blok.GetYerlesimler();
        ddl.DataBind();
        ddl.SelectedValue = Converts.NullToString(dataRow["Yer"]);
        CustomizeControl1.AddControl("Yerleşim", ddl, "Bloğun gösterileceği yeri temsil eder.");

        //ddl = new DropDownList();
        //ddl.ID = "SablonTipi";
        //ddl.Width = 300;
        //ddl.DataValueField = "Key";
        //ddl.DataTextField = "Value";
        //ddl.DataSource = Lib.Blok.GetSablonTipleri(true);
        //ddl.DataBind();
        //ddl.SelectedValue = Converts.NullToString(dataRow["SablonTipi"]);
        //CustomizeControl1.AddControl("Şablonlar", ddl, "Blok üzerinde tema giydirilip, giydirilemeyeceğini belirler.");

        ddl = new DropDownList();
        ddl.ID = "Sira";
        ddl.Width = 300;
        ddl.DataValueField = "Key";
        ddl.DataTextField = "Value";
        ddl.DataSource = Settings.SiraNumaralari();
        ddl.DataBind();
        ddl.SelectedIndex = Converts.NullToInt(dataRow["Sira"]);
        CustomizeControl1.AddControl("Sıra", ddl, "Bloğun yerleşim sırasını belirler.");

        //ddl = new DropDownList();
        //ddl.ID = "Dil";
        //ddl.Width = 300;
        //ddl.DataValueField = "Key";
        //ddl.DataTextField = "Value";
        //ddl.DataSource = Settings.DilSecenekleri();
        //ddl.DataBind();
        //ddl.SelectedValue = Converts.NullToString(dataRow["Dil"]);
        //CustomizeControl1.AddControl("Dil", ddl, "Bloğun hangi dilde gösterileceğini belirler.");

        CheckBox chk;// = new CheckBox();
        //chk.ID = "TumDil";
        //chk.Checked = Converts.NullToBool(dataRow["TumDil"]);
        //CustomizeControl1.AddControl("Tüm Diller", chk);

        txt = new TextBox();
        txt.ID = "Sayfa";
        txt.MaxLength = 200;
        txt.Text = Converts.NullToString(dataRow["Sayfa"]);
        CustomizeControl1.AddControl("Sayfa", txt, "Bloğun gösterileceği sayfayı belirler.");

        chk = new CheckBox();
        chk.ID = "TumSayfa";
        chk.Checked = Converts.NullToBool(dataRow["TumSayfa"]);
        CustomizeControl1.AddControl("Tüm Sayfalar", chk);

        chk = new CheckBox();
        chk.ID = "Aktif";
        chk.Checked = Converts.NullToBool(dataRow["Aktif"]);
        CustomizeControl1.AddControl("Yayımla", chk);
    }

    void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
    {
        try
        {
            /* 
             * ÖNEMLİ BİLGİ! BURADA GİZLENMİŞ KODLAR VAR!
             */
            if (!string.IsNullOrEmpty(((TextBox)controls["Baslik"]).Text)
                & !string.IsNullOrEmpty(Request.QueryString["type"]))
            {
                using (System.Data.DataTable xmlData = Lib.Blok.GetXmlData())
                {
                    string fileName = "", type = Request.QueryString["type"];
                    if (!string.IsNullOrEmpty(Request.QueryString["bid"]))
                    {
                        xmlData.DefaultView.RowFilter = "";
                        xmlData.DefaultView.RowFilter = "ID=" + Request.QueryString["bid"];
                        fileName = xmlData.DefaultView[0]["Adi"].ToString();
                        xmlData.DefaultView[0]["Baslik"] = ((TextBox)controls["Baslik"]).Text;
                        xmlData.DefaultView[0]["Yer"] = ((DropDownList)controls["Yer"]).SelectedValue;
                        //xmlData.DefaultView[0]["SablonTipi"] = ((DropDownList)controls["SablonTipi"]).SelectedValue;
                        xmlData.DefaultView[0]["Sira"] = Converts.NullToInt16(((DropDownList)controls["Sira"]).SelectedValue);
                        //xmlData.DefaultView[0]["Dil"] = ((DropDownList)controls["Dil"]).SelectedValue;
                        //xmlData.DefaultView[0]["TumDil"] = ((CheckBox)controls["TumDil"]).Checked;
                        xmlData.DefaultView[0]["Sayfa"] = ((TextBox)controls["Sayfa"]).Text;
                        xmlData.DefaultView[0]["TumSayfa"] = ((CheckBox)controls["TumSayfa"]).Checked;
                        xmlData.DefaultView[0]["Tipi"] = type;
                        xmlData.DefaultView[0]["Aktif"] = ((CheckBox)controls["Aktif"]).Checked;
                        xmlData.AcceptChanges();
                    }
                    else
                    {
                        System.Data.DataRow dr = xmlData.NewRow();
                        switch (type)
                        {
                            case "ascx":
                                fileName = ((DropDownList)controls["Bloklar"]).SelectedValue;
                                dr["SablonTipi"] = "AscxNoTheme";
                                break;
                            case "html":
                                fileName = Commons.ClearInvalidCharacter(((TextBox)controls["Baslik"]).Text);
                                fileName = fileName.Replace(".bhtml", "") + ".bhtml";
                                dr["SablonTipi"] = "HtmlNoTheme";
                                break;
                        }
                        dr["Adi"] = fileName;
                        dr["Baslik"] = ((TextBox)controls["Baslik"]).Text;
                        dr["Yer"] = ((DropDownList)controls["Yer"]).SelectedValue;
                        //dr["SablonTipi"] = ((DropDownList)controls["SablonTipi"]).SelectedValue;
                        dr["Sira"] = Converts.NullToInt16(((DropDownList)controls["Sira"]).SelectedValue);
                        dr["Dil"] = "tr-TR";//((DropDownList)controls["Dil"]).SelectedValue;
                        dr["TumDil"] = true;//((CheckBox)controls["TumDil"]).Checked;
                        dr["Sayfa"] = ((TextBox)controls["Sayfa"]).Text;
                        dr["TumSayfa"] = ((CheckBox)controls["TumSayfa"]).Checked;
                        dr["Tipi"] = type;
                        dr["Aktif"] = ((CheckBox)controls["Aktif"]).Checked;
                        xmlData.Rows.Add(dr);
                    }
                    switch (type)
                    {
                        case "html":
                            FileIO.WriteText(Server.MapPath(Lib.Blok.HtmlPath) + fileName, ((TextBox)controls["Editor"]).Text);
                            break;
                    }
                    xmlData.WriteXml(Server.MapPath(Lib.Blok.XmlPath));
                    if (string.IsNullOrEmpty(Request.QueryString["bid"]))
                        Settings.ClearControls(controls);
                    fileName = null;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void CustomizeControl1_RemoveClick(SortedDictionary<string, Control> controls)
    {
        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString["bid"]))
            {
                using (System.Data.DataTable xmlData = Lib.Blok.GetXmlData())
                {
                    xmlData.DefaultView.RowFilter = "";
                    xmlData.DefaultView.RowFilter = "ID=" + Request.QueryString["bid"];
                    if (xmlData.DefaultView.Count > 0)
                    {
                        switch (Request.QueryString["type"])
                        {
                            case "html":
                                FileIO.Remove(Server.MapPath(Lib.Blok.HtmlPath) + xmlData.DefaultView[0]["Adi"]);
                                break;
                        }
                        xmlData.DefaultView[0].Delete();
                        xmlData.AcceptChanges();
                        xmlData.WriteXml(Server.MapPath(Lib.Blok.XmlPath));
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Silme işleminiz başarılı bir şekilde tamamlandı!');", true);
                        Response.Redirect(Settings.PanelPath + "?go=blok&type=" + Request.QueryString["type"], false);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}