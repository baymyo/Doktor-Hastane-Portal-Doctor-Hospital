using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_haber_haberliste : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            this.Page.Title = "Haberler - " + Settings.SiteTitle;
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
        using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(
            "SELECT <%#TOP%> Haber.ID, Haber.KategoriID, Kategori.Adi AS KategoriAdi, Haber.ResimUrl, Haber.Baslik, Haber.Ozet, Haber.KayitTarihi, Haber.Aktif FROM Haber INNER JOIN Kategori ON Haber.KategoriID = Kategori.ID",
            "SELECT COUNT(Haber.ID) FROM Haber INNER JOIN Kategori ON Haber.KategoriID = Kategori.ID"))
        {
            data.CustomDataQuery += " WHERE Kategori.ModulID='haber' AND Haber.Aktif=1";
            data.CustomDataCountQuery += " WHERE Kategori.ModulID='haber' AND Haber.Aktif=1";
            if (!string.IsNullOrEmpty(Request.QueryString["kid"]))
            {
                data.Where += " AND KategoriID Like @KategoriID";
                data.Parameters.Add("KategoriID", Request.QueryString["kid"] + "%", BAYMYO.MultiSQLClient.MSqlDbType.Int32);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                data.Where += " AND Baslik Like @Baslik";
                data.Parameters.Add("Baslik", "%" + Request.QueryString["q"] + "%", BAYMYO.MultiSQLClient.MSqlDbType.NVarChar);
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