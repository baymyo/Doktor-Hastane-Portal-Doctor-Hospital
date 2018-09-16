using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_editor_calismaalanlari : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        switch (Settings.CurrentUser().Tipi)
        {
            case Lib.HesapTuru.None:
            case Lib.HesapTuru.Standart:
                Response.Redirect(Settings.VirtualPath + "?l=5", false);
                return;
        }

        this.Page.Title = "Muayenehaneler - " + Settings.CurrentUser().Adi + " " + Settings.CurrentUser().Soyadi;
        if (!this.Page.IsPostBack)
            GetData();
    }

    private void GetData()
    {
        using (BAYMYO.UI.Web.CustomSqlQuery data = new BAYMYO.UI.Web.CustomSqlQuery(rptMuayene, "CalismaAlani", "Kurum", "HesapID=@HesapID"))
        {
            data.Top = 10;
            data.Parameters.Add("HesapID", Settings.CurrentUser().ID, BAYMYO.MultiSQLClient.MSqlDbType.UniqueIdentifier);
            data.Execute();
        }
    }
}