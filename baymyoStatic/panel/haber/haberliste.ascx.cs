using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class panel_haber_haberliste : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            Settings.ProccesList("haber", ddlIslemler);
            GetDataPaging();
        }
    }

    private void GetDataPaging()
    {
        using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(dataGrid1, "Haber", "ID DESC"))
        {
            data.PageNumberTargetControl = pageNumberLiteral;
            data.Binding();
        }
    }

    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {
        string columnName = "Aktif"; object value = false;
        switch (ddlIslemler.SelectedIndex)
        {
            case 1:
                value = true;
                break;
        }
        if (ddlIslemler.SelectedIndex > 0)
        {
            foreach (GridViewRow item in dataGrid1.Rows)
                if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                    Settings.ProccesApply("Haber", columnName, BAYMYO.UI.Converts.NullToInt64(dataGrid1.DataKeys[item.RowIndex][0]), value);
            GetDataPaging();
        }
    }
}