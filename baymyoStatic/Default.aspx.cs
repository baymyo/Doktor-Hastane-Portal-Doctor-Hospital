using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    protected override void OnPreInit(EventArgs e)
    {
        this.Theme = "default";
        string modulPath = string.Empty, searchPath = Server.MapPath(Settings.VirtualPath) + "modul\\";
        bool isGoActive = false, isLoginActive = false;
        if (!string.IsNullOrEmpty(Request.QueryString["l"]))
        {
            modulPath = Settings.GirisDurum(Request.QueryString["l"]);
            if (!string.IsNullOrEmpty(modulPath) & File.Exists(Server.MapPath(modulPath)))
            {
                ((ContentPlaceHolder)(this.Page.Master.FindControl("plcOrta"))).Controls.Add((UserControl)this.Page.LoadControl(modulPath));
                isLoginActive = true;
            }
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["go"]))
        {
            DirectoryInfo directory = new DirectoryInfo(searchPath);
            FileInfo[] files = directory.GetFiles(Request.QueryString["go"] + ".ascx", SearchOption.AllDirectories);
            foreach (FileInfo file in files)
                modulPath = "\\modul\\" + file.Directory.Name + "\\" + file.Name;
            files = null; directory = null; searchPath = null;
            if (File.Exists(Server.MapPath(modulPath)))
            {
                ((ContentPlaceHolder)(this.Page.Master.FindControl("plcOrta"))).Controls.Add((UserControl)this.Page.LoadControl(modulPath));
                isGoActive = true;
            }
        }
        using (System.Data.DataTable xmlData = Lib.Blok.GetXmlData())
        {
            xmlData.DefaultView.RowFilter = "Aktif=True AND Dil='" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' OR Aktif=True AND TumDil=True";
            xmlData.DefaultView.Sort = "Sira";
            string yerlesim;
            if (isGoActive || isLoginActive)
            {
                for (int i = 0; i < xmlData.DefaultView.Count; i++)
                {
                    if (Request.Url.AbsoluteUri.Contains(((string.IsNullOrEmpty(BAYMYO.UI.Converts.NullToString(xmlData.DefaultView[i]["Sayfa"]))) ? "*.*" : xmlData.DefaultView[i]["Sayfa"].ToString())) || BAYMYO.UI.Converts.NullToBool(xmlData.DefaultView[i]["TumSayfa"]))
                    {
                        yerlesim = xmlData.DefaultView[i]["Yer"].ToString().Split('_')[1];
                        switch (BAYMYO.UI.Converts.NullToString(xmlData.DefaultView[i]["Yer"]))
                        {
                            case "Tum_EnUst":
                            case "Icerik_EnUst":
                            case "Tum_Ust":
                            case "Icerik_Ust":
                            case "Tum_UstKemer":
                            case "Icerik_UstKemer":
                            case "Tum_Sol":
                            case "Icerik_Sol":
                            case "Tum_Sag":
                            case "Icerik_Sag":
                            case "Tum_Alt":
                            case "Icerik_Alt":
                                Lib.Blok.Add((this.Page.Master.FindControl("plc" + yerlesim)) as ContentPlaceHolder, xmlData.DefaultView[i]["SablonTipi"].ToString(), xmlData.DefaultView[i]["Adi"].ToString(), xmlData.DefaultView[i]["Baslik"].ToString());
                                break;
                            case "Tum_Orta":
                            case "Icerik_Orta":
                                if (!isLoginActive)
                                    Lib.Blok.Add((this.Page.Master.FindControl("plc" + yerlesim)) as ContentPlaceHolder, xmlData.DefaultView[i]["SablonTipi"].ToString(), xmlData.DefaultView[i]["Adi"].ToString(), xmlData.DefaultView[i]["Baslik"].ToString());
                                break;
                        }
                    }
                }
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    this.MetaDescription = ConfigurationManager.AppSettings["SiteDescription"];
                    this.MetaKeywords = ConfigurationManager.AppSettings["SiteKeywords"];
                }
                for (int i = 0; i < xmlData.DefaultView.Count; i++)
                {
                    if (Request.Url.AbsoluteUri.Contains(((string.IsNullOrEmpty(BAYMYO.UI.Converts.NullToString(xmlData.DefaultView[i]["Sayfa"]))) ? "*.*" : xmlData.DefaultView[i]["Sayfa"].ToString())) || BAYMYO.UI.Converts.NullToBool(xmlData.DefaultView[i]["TumSayfa"]))
                    {
                        yerlesim = xmlData.DefaultView[i]["Yer"].ToString().Split('_')[1];
                        switch (BAYMYO.UI.Converts.NullToString(xmlData.DefaultView[i]["Yer"]))
                        {
                            case "Tum_EnUst":
                            case "Anasayfa_EnUst":
                            case "Tum_Ust":
                            case "Anasayfa_Ust":
                            case "Tum_UstKemer":
                            case "Anasayfa_UstKemer":
                            case "Tum_Sol":
                            case "Anasayfa_Sol":
                            case "Tum_Sag":
                            case "Anasayfa_Sag":
                            case "Tum_Alt":
                            case "Anasayfa_Alt":
                            case "Tum_Orta":
                            case "Anasayfa_Orta":
                                Lib.Blok.Add((this.Page.Master.FindControl("plc" + yerlesim)) as ContentPlaceHolder, xmlData.DefaultView[i]["SablonTipi"].ToString(), xmlData.DefaultView[i]["Adi"].ToString(), xmlData.DefaultView[i]["Baslik"].ToString());
                                break;
                        }
                    }
                }
            }
        }
        base.OnPreInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(this.Page.Title))
            this.Page.Title = Settings.SiteTitle;
    }

}