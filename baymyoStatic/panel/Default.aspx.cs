using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

public partial class panel_Default : System.Web.UI.Page
{
    protected override void OnPreInit(EventArgs e)
    {
        this.Theme = "admin";
        string modulPath = "default\\index.ascx", searchPath = Server.MapPath(Settings.PanelPath);
        DirectoryInfo directory = new DirectoryInfo(searchPath);
        FileInfo[] files = directory.GetFiles(Request.QueryString["go"] + ".ascx", SearchOption.AllDirectories);
        foreach (FileInfo file in files)
            modulPath = file.Directory.Name + "\\" + file.Name;
        this.Page.Title = modulPath.Split('\\')[0].ToUpper() + " YONETİMİ";
        files = null; directory = null; searchPath = null;
        ((ContentPlaceHolder)this.Page.Master.FindControl("plcModul")).Controls.Add((UserControl)this.Page.LoadControl(modulPath));
        modulPath = null;
        base.OnPreInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}