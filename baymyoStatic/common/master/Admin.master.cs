using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;

public partial class master_Admin : System.Web.UI.MasterPage
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            using (Lib.Hesap m = Settings.CurrentUser())
            {
                FirstName = m.Adi;
                LastName = m.Soyadi;
            }
            menuLiteral.Text = string.Format("<ul>{0}</ul>", FindMenues());
        }
    }

    public string FindMenues()
    {
        try
        {
            string returnMenu = "";
            DirectoryInfo directory = new DirectoryInfo(Server.MapPath(Settings.PanelPath));
            FileInfo[] files = directory.GetFiles("menu.myo", SearchOption.AllDirectories);
            foreach (FileInfo file in files)
                using (StreamReader sr = file.OpenText())
                {
                    returnMenu += sr.ReadToEnd().Replace("%IconsPath%", Settings.IconsPath);
                    sr.Close();
                }
            files = null; directory = null;
            return returnMenu;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}