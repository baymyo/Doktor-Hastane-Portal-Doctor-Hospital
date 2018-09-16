using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_makale_makaleliste : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            this.Page.Title = "Makaleler - " + Settings.SiteTitle;
            GetDataPaging();
        }
    }

    public string GetClassName()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["view"]))
            return Request.QueryString["view"] + "-list";
        else
            return "double-list";
    }

    private void GetDataPaging()
    {
        //new BAYMYO.UI.Web.DataPagers(rptListe, "Makale", "KayitTarihi DESC", "Aktif=1"))
        using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(
            "SELECT <%#TOP%> Makale.ID, Makale.HesapID, Makale.KategoriID, Makale.ResimUrl, Makale.Baslik, Makale.Ozet, Makale.KayitTarihi, Makale.Aktif, Hesap.Adi, Hesap.Soyadi FROM Makale INNER JOIN Hesap ON Makale.HesapID = Hesap.ID",
            "SELECT COUNT(Makale.ID) FROM Makale INNER JOIN Hesap ON Makale.HesapID = Hesap.ID")) 
        {
            data.CustomDataQuery += " WHERE Makale.Aktif=1";
            data.CustomDataCountQuery += " WHERE Makale.Aktif=1";
            if (!string.IsNullOrEmpty(Request.QueryString["hspid"]))
            {
                data.CustomDataQuery += " AND Makale.HesapID=@HesapID";
                data.CustomDataCountQuery += " AND Makale.HesapID=@HesapID";
                data.Parameters.Add("HesapID", BAYMYO.UI.Converts.NullToGuid(Request.QueryString["hspid"]), BAYMYO.MultiSQLClient.MSqlDbType.UniqueIdentifier);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["kid"]))
            {
                data.CustomDataQuery += " AND Makale.KategoriID Like @KategoriID";
                data.CustomDataCountQuery += " AND Makale.KategoriID Like @KategoriID";
                data.Parameters.Add("KategoriID", Request.QueryString["kid"] + "%", BAYMYO.MultiSQLClient.MSqlDbType.Int32);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                data.CustomDataQuery += " AND Makale.Baslik Like @Baslik";
                data.CustomDataCountQuery += " AND Makale.Baslik Like @Baslik";
                data.Parameters.Add("Baslik", "%" + Request.QueryString["q"] + "%", BAYMYO.MultiSQLClient.MSqlDbType.NVarChar);
            }
            data.CustomDataQuery += " ORDER BY KayitTarihi DESC";
            data.DataTargetControl = rptListe;
            data.PageNumberTargetControl = pageNumberLiteral;
            data.ViewDataCount = 25;
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