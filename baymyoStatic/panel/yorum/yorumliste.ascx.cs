using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class panel_yorum_yorumliste : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            Settings.ProccesList("yorum", ddlIslemler);
            GetDataPaging();
        }
    }

    private void GetDataPaging()
    {
        using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(dataGrid1, "Yorum", "KayitTarihi DESC", "1=1"))
        {
            if (!string.IsNullOrEmpty(Request.QueryString["onay"]))
            {
                data.Where += " AND YoneticiOnay=@YoneticiOnay";
                data.Parameters.Add("YoneticiOnay", Request.QueryString["onay"], BAYMYO.MultiSQLClient.MSqlDbType.TinyInt);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["aktif"]))
            {
                data.Where += " AND Aktif=@Aktif";
                data.Parameters.Add("Aktif", Request.QueryString["aktif"], BAYMYO.MultiSQLClient.MSqlDbType.TinyInt);
            }
            data.PageNumberTargetControl = pageNumberLiteral;
            data.ViewDataCount = 25;
            data.Binding();
        }
    }

    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {
        if (ddlIslemler.SelectedIndex > 0)
        {
            bool chkState = false;
            switch (ddlIslemler.SelectedIndex)
            {
                case 1:
                case 3:
                    chkState = true;
                    break;
            }

            if (ddlIslemler.SelectedIndex == 1 || ddlIslemler.SelectedIndex == 2)
            {
                foreach (GridViewRow item in dataGrid1.Rows)
                    if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                        Settings.ProccesApply("Yorum", "YoneticiOnay", BAYMYO.UI.Converts.NullToGuid(dataGrid1.DataKeys[item.RowIndex][0]), chkState);
                GetDataPaging();
            }
            else if (ddlIslemler.SelectedIndex == 3 || ddlIslemler.SelectedIndex == 4)
            {
                foreach (GridViewRow item in dataGrid1.Rows)
                    if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                        Settings.ProccesApply("Yorum", "Aktif", BAYMYO.UI.Converts.NullToGuid(dataGrid1.DataKeys[item.RowIndex][0]), chkState);
                GetDataPaging();
            }
            else if (ddlIslemler.SelectedIndex == 5)
            {
                foreach (GridViewRow item in dataGrid1.Rows)
                    if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                        Lib.YorumMethods.Delete(BAYMYO.UI.Converts.NullToGuid(dataGrid1.DataKeys[item.RowIndex][0]));
                GetDataPaging();
            }
        }
    }
}