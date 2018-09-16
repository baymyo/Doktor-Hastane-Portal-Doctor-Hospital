using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_default_hesapliste : System.Web.UI.UserControl
{
    public string mapsUrl = Settings.ImagesPath;
    protected void Page_Load(object sender, EventArgs e)
    {
        string tableName = "";
        switch (Request.QueryString["type"])
        {
            case "doktor":
                this.Page.Title = "Doktorlar - " + Settings.SiteTitle;
                this.mapsUrl += "harita-doktor.swf";
                this.ltrInfo.Text = "Doktor Listesi";
                tableName = "Doktorlar";
                break;
            case "hastane":
                this.Page.Title = "Hastaneler - " + Settings.SiteTitle;
                this.mapsUrl += "harita-hastane.swf";
                this.ltrInfo.Text = "Hastane Listesi";
                tableName = "Hastaneler";
                break;
            default:
                this.pageNumberLiteral.Visible = true;
                this.pageNumberLiteral.Text = MessageBox.IsNotViews();
                return;
        }
        if (!this.Page.IsPostBack)
            GetDataPaging(tableName);
    }

    public string Randevu(object url, object adisoyadi, object value)
    {
        string[] valueList = value.ToString().Split(';');
        if (valueList.Length == 2)
            return Settings.RandevuAlListe(adisoyadi.ToString(), Settings.CreateLink("randevu", url + ";" + valueList[0], "go"), valueList[1]);
        return string.Empty;
    }

    private void GetDataPaging(string tableName)
    {
        using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(rptListe, tableName, "KayitTarihi DESC", "1=1"))
        {
            if (!string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                data.Where += " AND AdiSoyadi Like @Adi";
                data.Parameters.Add("Adi", "%" + Request.QueryString["q"] + "%", BAYMYO.MultiSQLClient.MSqlDbType.NVarChar);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["city"]))
            {
                data.Where += " AND Sehir=@city";
                data.Parameters.Add("city", Request.QueryString["city"].ToUpper().Trim(), BAYMYO.MultiSQLClient.MSqlDbType.NVarChar);
            }
            data.ViewDataCount = 26;
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