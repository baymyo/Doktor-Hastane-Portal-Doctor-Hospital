using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class panel_kategori_kategori : System.Web.UI.UserControl
{
    void CreateKategoriMaps(string modulName)
    {
        try
        {
            switch (modulName)
            {
                case "makale":
                    string mainFormat = "<siteMapNode url=\"{0}\" title=\"{1}\" description=\"{2}\">{3}</siteMapNode>",
                        parentFormat = "<siteMapNode url=\"{0}\" title=\"{1}\" description=\"{2}\" />",
                        descFormat = " hakkındaki içeriklere erişebilmek için tıklayınız.";

                    System.Text.StringBuilder maps = new System.Text.StringBuilder();
                    maps.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                    maps.Append("<siteMap xmlns=\"http://schemas.microsoft.com/AspNet/SiteMap-File-1.0\" >");
                    maps.AppendFormat("<siteMapNode url=\"{0}\" title=\"{1}\" description=\"{2}\">", Settings.SiteUrl, "Anasayfa", Settings.SiteDescription);

                    List<Lib.Kategori> tumKategoriler, anaKategoriler, altKategoriler_1, altKategoriler_2, altKategoriler_3, altKategoriler_4;
                    tumKategoriler = Lib.KategoriMethods.GetMenu(modulName, true);
                    anaKategoriler = tumKategoriler.FindAll(delegate(Lib.Kategori p) { return (p.ParentID == "0"); });
                    string altParent_1 = string.Empty, altParent_2 = string.Empty, altParent_3 = string.Empty, altParent_4 = string.Empty;
                    modulName += "kategori";
                    foreach (Lib.Kategori ustK in anaKategoriler)
                    {
                        altKategoriler_1 = tumKategoriler.FindAll(delegate(Lib.Kategori p) { return (p.ParentID == ustK.ID); });
                        if (altKategoriler_1.Count > 0)
                        {
                            foreach (Lib.Kategori altK_1 in altKategoriler_1)
                            {
                                altKategoriler_2 = tumKategoriler.FindAll(delegate(Lib.Kategori p) { return (p.ParentID == altK_1.ID); });
                                foreach (Lib.Kategori altK_2 in altKategoriler_2)
                                {
                                    altKategoriler_3 = tumKategoriler.FindAll(delegate(Lib.Kategori p) { return (p.ParentID == altK_2.ID); });
                                    foreach (Lib.Kategori altK_3 in altKategoriler_3)
                                    {
                                        altKategoriler_4 = tumKategoriler.FindAll(delegate(Lib.Kategori p) { return (p.ParentID == altK_3.ID); });
                                        foreach (Lib.Kategori altK_4 in altKategoriler_4)
                                            altParent_4 += string.Format(parentFormat, Settings.CreateLink(modulName, altK_4.ID, altK_4.Adi), altK_4.Adi, altK_4.Adi + descFormat);

                                        if (!string.IsNullOrEmpty(altParent_4))
                                            altParent_3 += string.Format(mainFormat, Settings.CreateLink(modulName, altK_3.ID, altK_3.Adi), altK_3.Adi, altK_3.Adi + descFormat, altParent_4);
                                        else
                                            altParent_3 += string.Format(parentFormat, Settings.CreateLink(modulName, altK_3.ID, altK_3.Adi), altK_3.Adi, altK_3.Adi + descFormat);
                                        altParent_4 = string.Empty;
                                    }

                                    if (!string.IsNullOrEmpty(altParent_3))
                                        altParent_2 += string.Format(mainFormat, Settings.CreateLink(modulName, altK_2.ID, altK_2.Adi), altK_2.Adi, altK_2.Adi + descFormat, altParent_3);
                                    else
                                        altParent_2 += string.Format(parentFormat, Settings.CreateLink(modulName, altK_2.ID, altK_2.Adi), altK_2.Adi, altK_2.Adi + descFormat);
                                    altParent_3 = string.Empty;
                                }

                                if (!string.IsNullOrEmpty(altParent_2))
                                    altParent_1 += string.Format(mainFormat, Settings.CreateLink(modulName, altK_1.ID, altK_1.Adi), altK_1.Adi, altK_1.Adi + descFormat, altParent_2);
                                else
                                    altParent_1 += string.Format(parentFormat, Settings.CreateLink(modulName, altK_1.ID, altK_1.Adi), altK_1.Adi, altK_1.Adi + descFormat);
                                altParent_2 = string.Empty;
                            }

                            if (!string.IsNullOrEmpty(altParent_1))
                                maps.AppendFormat(mainFormat, Settings.CreateLink(modulName, ustK.ID, ustK.Adi), ustK.Adi, ustK.Adi + descFormat, altParent_1);
                            else
                                maps.AppendFormat(parentFormat, Settings.CreateLink(modulName, ustK.ID, ustK.Adi), ustK.Adi, ustK.Adi + descFormat);
                            altParent_1 = string.Empty;
                        }
                        else
                            maps.AppendFormat(parentFormat, Settings.CreateLink(modulName, ustK.ID, ustK.Adi), ustK.Adi, ustK.Adi + descFormat);
                    }
                    maps.Append("</siteMapNode>");
                    maps.Append("</siteMap>");

                    BAYMYO.UI.FileIO.WriteText(Server.MapPath(Settings.VirtualPath) + "Web.sitemap", maps.ToString(), System.Text.Encoding.UTF8);
                    break;
            }
        }
        catch (Exception)
        {
        }
    }

    protected override void OnInit(EventArgs e)
    {
        CustomizeControl1.FormTitle = string.Format(Settings.FormTitleFormat, "Kategori", "Ekleme/Düzeltme Formu");
        CustomizeControl1.UpdateVisible = true;
        CustomizeControl1.RemoveVisible = !BAYMYO.UI.Converts.NullToString(Request.QueryString["kid"]).Equals("0");

        TreeView trv = new TreeView();
        trv.ID = "Kategoriler";
        trv.Width = 300;
        trv.ExpandDepth = 1;
        trv.ShowLines = true;
        trv.DataSourceID = "hierarDataSource";

        trv.NodeStyle.HorizontalPadding = Unit.Pixel(5);
        trv.NodeStyle.VerticalPadding = Unit.Pixel(5);

        trv.RootNodeStyle.BackColor = System.Drawing.Color.WhiteSmoke;
        trv.RootNodeStyle.BorderColor = System.Drawing.Color.Gray;
        trv.RootNodeStyle.ForeColor = System.Drawing.Color.OrangeRed;
        trv.RootNodeStyle.HorizontalPadding = Unit.Pixel(5);
        trv.RootNodeStyle.VerticalPadding = Unit.Pixel(5);

        trv.SelectedNodeStyle.BackColor = System.Drawing.Color.Orange;
        trv.SelectedNodeStyle.BorderColor = System.Drawing.Color.OrangeRed;
        trv.SelectedNodeStyle.ForeColor = System.Drawing.Color.White;
        trv.SelectedNodeStyle.HorizontalPadding = Unit.Pixel(5);
        trv.SelectedNodeStyle.VerticalPadding = Unit.Pixel(5);

        trv.HoverNodeStyle.BackColor = System.Drawing.Color.Wheat;
        trv.SelectedNodeStyle.BorderColor = System.Drawing.Color.OrangeRed;
        trv.HoverNodeStyle.ForeColor = System.Drawing.Color.OrangeRed;

        CustomizeControl1.AddControl("Kategoriler", trv);

        TextBox txt = new TextBox();
        txt.ID = "Adi";
        txt.MaxLength = 35;
        CustomizeControl1.AddControl("Adı", txt);

        //DropDownList ddl = new DropDownList();
        //ddl.ID = "Sira";
        //ddl.Width = 300;
        //ddl.DataValueField = "Key";
        //ddl.DataTextField = "Value";
        //ddl.DataSource = Settings.SiraNumaralari();
        //ddl.DataBind();
        //CustomizeControl1.AddControl("Sira", ddl);

        //ddl = new DropDownList();
        //ddl.ID = "Dil";
        //ddl.Width = 300;
        //ddl.DataValueField = "Key";
        //ddl.DataTextField = "Value";
        //ddl.DataSource = Settings.DilSecenekleri();
        //ddl.DataBind();
        //CustomizeControl1.AddControl("Dil", ddl);

        CheckBox chk;// = new CheckBox();
        //chk.ID = "Tab";
        //CustomizeControl1.AddControl("Tab", chk);

        chk = new CheckBox();
        chk.ID = "Menu";
        CustomizeControl1.AddControl("Menü", chk);

        chk = new CheckBox();
        chk.ID = "Aktif";
        CustomizeControl1.AddControl("Yayımla", chk);

        CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);
        CustomizeControl1.UpdateClick += new CustomizeControl.ButtonEvent(CustomizeControl1_UpdateClick);
        CustomizeControl1.RemoveClick += new CustomizeControl.ButtonEvent(CustomizeControl1_RemoveClick);

        base.OnInit(e);
    }

    void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
    {
        try
        {

            TreeView TW_Kategori = ((TreeView)controls["Kategoriler"]);
            if (TW_Kategori.SelectedNode.DataPath.Split(',').Length >= 5)
            {
                CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Alt kategori ekleme limitini doldurdurunuz, bu bölümde daha fazla alt kategori oluşturamazsınız.");
                return;
            }
            if (!string.IsNullOrEmpty(Request.QueryString["mdl"]) & !string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text))
                using (Lib.Kategori m = new Lib.Kategori())
                {
                    string orjinalID = null, parentID = TW_Kategori.SelectedNode.DataPath + ',';
                    int index = TW_Kategori.SelectedNode.ChildNodes.Count - 1;
                    bool parentActive = Settings.ParentKategori(Request.QueryString["mdl"]);
                    if (!parentActive)
                    {
                        parentID = "0";
                        if (TW_Kategori.SelectedNode.Parent != null)
                        {
                            if (TW_Kategori.SelectedNode.Parent.ChildNodes.Count > 0)
                                orjinalID = (BAYMYO.UI.Converts.NullToInt(TW_Kategori.SelectedNode.Parent.ChildNodes[TW_Kategori.SelectedNode.Parent.ChildNodes.Count - 1].DataPath) + 1).ToString("000#");
                        }
                        else if (TW_Kategori.SelectedNode.ChildNodes.Count > 0)
                            orjinalID = (BAYMYO.UI.Converts.NullToInt(TW_Kategori.SelectedNode.ChildNodes[TW_Kategori.SelectedNode.ChildNodes.Count - 1].DataPath) + 1).ToString("000#");
                        else
                            orjinalID = 1.ToString("000#");
                    }
                    else if (parentID.Equals("0,"))
                    {
                        if (index >= 0)
                            orjinalID = (BAYMYO.UI.Converts.NullToInt(TW_Kategori.SelectedNode.ChildNodes[index].DataPath) + 1).ToString("000#");
                        else
                            orjinalID = 1.ToString("000#");
                    }
                    else if (TW_Kategori.SelectedNode.ChildNodes.Count > 0)
                    {
                        if (index >= 0)
                            if (TW_Kategori.SelectedNode.ChildNodes[index].DataPath.Contains(","))
                            {
                                string[] kategoriler = new string[] { };
                                kategoriler = TW_Kategori.SelectedNode.ChildNodes[index].DataPath.Split(',');
                                orjinalID = parentID + (BAYMYO.UI.Converts.NullToInt(kategoriler[kategoriler.Length - 1]) + 1).ToString("000#");
                            }
                            else
                                orjinalID = parentID + (BAYMYO.UI.Converts.NullToInt(TW_Kategori.SelectedNode.ChildNodes[index].DataPath) + 1).ToString("000#");
                    }
                    else
                    {
                        if (index >= 0)
                            orjinalID = parentID + (BAYMYO.UI.Converts.NullToInt(TW_Kategori.SelectedNode.ChildNodes[index].DataPath) + 1).ToString("000#");
                        else
                            orjinalID = parentID + 1.ToString("000#");
                    }

                    m.ID = orjinalID;
                    if (!string.IsNullOrEmpty(m.ID))
                    {
                        if (parentID.Length > 1)
                            m.ParentID = parentID.Remove(parentID.Length - 1);
                        else
                            m.ParentID = "0";
                        m.ModulID = Request.QueryString["mdl"];
                        m.Adi = ((TextBox)controls["Adi"]).Text;
                        m.Dil = "tr-TR";
                        //m.Dil = ((DropDownList)controls["Dil"]).SelectedValue;
                        //m.Sira = (byte)((DropDownList)controls["Sira"]).SelectedIndex;
                        //m.Tab = ((CheckBox)controls["Tab"]).Checked;
                        m.Menu = ((CheckBox)controls["Menu"]).Checked;
                        m.Aktif = ((CheckBox)controls["Aktif"]).Checked;
                        if (Lib.KategoriMethods.Insert(m) > 0)
                        {
                            TW_Kategori.DataBind();
                            CreateKategoriMaps(Request.QueryString["mdl"]);
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Kayıt ekleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                            Settings.ClearControls(controls);
                        }
                    }
                }
        }
        catch (Exception)
        {
        }
    }

    void CustomizeControl1_UpdateClick(SortedDictionary<string, Control> controls)
    {
        try
        {

            if (!string.IsNullOrEmpty(Request.QueryString["mdl"]) & !string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text))
                using (Lib.Kategori m = Lib.KategoriMethods.GetKategori(Request.QueryString["mdl"], BAYMYO.UI.Converts.NullToString(((TreeView)controls["Kategoriler"]).SelectedNode.DataPath)))
                {
                    if (!string.IsNullOrEmpty(m.ID))
                    {
                        m.Adi = ((TextBox)controls["Adi"]).Text;
                        //m.Dil = ((DropDownList)controls["Dil"]).SelectedValue;
                        //m.Sira = (byte)((DropDownList)controls["Sira"]).SelectedIndex;
                        //m.Tab = ((CheckBox)controls["Tab"]).Checked;
                        m.Menu = ((CheckBox)controls["Menu"]).Checked;
                        m.Aktif = ((CheckBox)controls["Aktif"]).Checked;
                        if (Lib.KategoriMethods.Update(m) > 0)
                        {
                            ((TreeView)controls["Kategoriler"]).DataBind();
                            CreateKategoriMaps(Request.QueryString["mdl"]);
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Güncelleme işleminiz başarılı bir şekilde tamamlandı!');", true);
                        }
                    }
                }
        }
        catch (Exception)
        {
        }
    }

    void CustomizeControl1_RemoveClick(SortedDictionary<string, Control> controls)
    {
        if (!BAYMYO.UI.Converts.NullToString(((TreeView)controls["Kategoriler"]).SelectedNode.DataPath).Equals("0"))
            if (Lib.KategoriMethods.Delete(Request.QueryString["mdl"], BAYMYO.UI.Converts.NullToString(((TreeView)controls["Kategoriler"]).SelectedNode.DataPath)) > 0)
            {
                ((TreeView)controls["Kategoriler"]).DataBind();
                CreateKategoriMaps(Request.QueryString["mdl"]);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Silme işleminiz başarılı bir şekilde tamamlandı!');", true);
                Settings.ClearControls(controls);
            }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
}