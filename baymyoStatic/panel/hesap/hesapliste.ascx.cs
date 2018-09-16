using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class panel_hesap_hesapliste : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            Settings.ProccesList("hesap", ddlIslemler);
            GetDataPaging();
        }
    }

    private void GetDataPaging()
    {
        using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(dataGrid1, "Hesap", "Aktif ASC, KayitTarihi DESC", "1=1"))
        {
            if (!string.IsNullOrEmpty(Request.QueryString["tipi"]))
            {
                data.Where += " AND Tipi=@Tipi";
                data.Parameters.Add("Tipi", Request.QueryString["tipi"], BAYMYO.MultiSQLClient.MSqlDbType.TinyInt);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["aktivasyon"]))
            {
                data.Where += " AND Aktivasyon=@aktivasyon";
                data.Parameters.Add("Aktivasyon", Request.QueryString["aktivasyon"], BAYMYO.MultiSQLClient.MSqlDbType.TinyInt);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["aktif"]))
            {
                data.Where += " AND Aktif=@Aktif";
                data.Parameters.Add("Aktif", Request.QueryString["aktif"], BAYMYO.MultiSQLClient.MSqlDbType.TinyInt);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["adi"]))
            {
                data.Where += " AND Adi Like @Adi";
                data.Parameters.Add("Adi", Request.QueryString["adi"] + "%", BAYMYO.MultiSQLClient.MSqlDbType.TinyInt);
            }
            data.ViewDataCount = 25;
            data.PageNumberTargetControl = pageNumberLiteral;
            data.Binding();
        }
    }

    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {
        string columnName = "Aktif"; bool chkState = false;
        switch (ddlIslemler.SelectedIndex)
        {
            case 1:
                columnName = "Yorum";
                chkState = true;
                break;
            case 2:
                columnName = "Yorum";
                chkState = false;
                break;
            case 3:
                columnName = "Abonelik";
                chkState = true;
                break;
            case 4:
                columnName = "Abonelik";
                chkState = false;
                break;
            case 5:
                columnName = "Aktivasyon";
                chkState = true;
                break;
            case 6:
                columnName = "Aktivasyon";
                chkState = false;
                break;
            case 7:
                chkState = true;
                break;
            case 8:
                chkState = false;
                break;
        }
        if (ddlIslemler.SelectedIndex > 0)
        {
            foreach (GridViewRow item in dataGrid1.Rows)
                if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                    Settings.ProccesApply("Hesap", columnName, BAYMYO.UI.Converts.NullToGuid(dataGrid1.DataKeys[item.RowIndex][0]), chkState);
            GetDataPaging();
        }
    }
}