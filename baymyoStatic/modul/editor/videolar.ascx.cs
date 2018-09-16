using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_editor_videolar : System.Web.UI.UserControl
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

        this.Page.Title = "Videolar - " + Settings.CurrentUser().Adi + " " + Settings.CurrentUser().Soyadi;
        if (!this.Page.IsPostBack)
            GetDataPaging();
    }

    private void GetDataPaging()
    {
        using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(rptListe, "Video", "ID DESC", "HesapID=@HesapID"))
        {
            data.Parameters.Add("HesapID", Settings.CurrentUser().ID, BAYMYO.MultiSQLClient.MSqlDbType.UniqueIdentifier);
            if (!string.IsNullOrEmpty(Request.QueryString["aktif"]))
            {
                data.Where += " AND Aktif=@Aktif";
                data.Parameters.Add("Aktif", Request.QueryString["aktif"], BAYMYO.MultiSQLClient.MSqlDbType.TinyInt);
            }
            data.ViewDataCount = 25;
            data.PageNumberTargetControl = pageNumberLiteral;
            data.Binding();

            if (data.TotalDataCount < 1)
            {
                pageNumberLiteral.Visible = true;
                pageNumberLiteral.Text = MessageBox.IsNotViews();
            }
            else
                jobSet.Visible = (data.TotalPageCount > 1);
        }
    }
}