using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class panel_sayfa_sayfaliste : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            Settings.ProccesList("sayfa", ddlIslemler);
            GetDataPaging();
        }
    }

    private void GetDataPaging()
    {
        using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(dataGrid1, "Sayfa", "ID DESC"))
        {
            data.PageNumberTargetControl = pageNumberLiteral;
            data.Binding();
        }
    }

    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {
        string columnName = "Tipi"; object value = ddlIslemler.SelectedIndex;
        switch (ddlIslemler.SelectedIndex)
        {
            case 5:
                columnName = "Aktif";
                value = true;
                break;
            case 6:
                columnName = "Aktif";
                value = false;
                break;
        }
        if (ddlIslemler.SelectedIndex > 0)
        {
            foreach (GridViewRow item in dataGrid1.Rows)
                if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                    Settings.ProccesApply("Sayfa", columnName, BAYMYO.UI.Converts.NullToInt16(dataGrid1.DataKeys[item.RowIndex][0]), value);
            GetDataPaging();
        }
    }
}