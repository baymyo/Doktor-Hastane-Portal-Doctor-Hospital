using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_mesaj_mesajliste : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            this.Page.Title = "Sorular ve Cevapları - " + Settings.SiteTitle;
            GetDataPaging();
        }
    }

    private void GetDataPaging()
    {
        using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(rptListe, "Mesaj", "GuncellemeTarihi DESC", "Aktif=1"))
        {
            if (!string.IsNullOrEmpty(Request.QueryString["hspid"]))
            {
                data.Where += " AND HesapID=@HesapID";
                data.Parameters.Add("HesapID", BAYMYO.UI.Converts.NullToGuid(Request.QueryString["hspid"].Replace(',', '-')), BAYMYO.MultiSQLClient.MSqlDbType.UniqueIdentifier);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                data.Where += " AND Konu Like @Konu";
                data.Parameters.Add("Konu", "%" + Request.QueryString["q"] + "%", BAYMYO.MultiSQLClient.MSqlDbType.NVarChar);
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