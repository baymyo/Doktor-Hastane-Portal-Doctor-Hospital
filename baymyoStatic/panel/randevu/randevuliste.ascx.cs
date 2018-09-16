using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class panel_randevu_randevuliste : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            Settings.ProccesList("randevu", ddlIslemler);
            GetDataPaging();
        }
    }

    private void GetDataPaging()
    {
        using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(dataGrid1, "Randevu", "TarihSaat DESC", "1=1"))
        {
            if (!string.IsNullOrEmpty(Request.QueryString["durum"]))
            {
                data.Where += " AND Durum=@Durum";
                data.Parameters.Add("Durum", BAYMYO.UI.Converts.NullToByte(Request.QueryString["durum"]), BAYMYO.MultiSQLClient.MSqlDbType.TinyInt);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["onay"]))
            {
                data.Where += " AND YoneticiOnay=@YoneticiOnay";
                data.Parameters.Add("YoneticiOnay", BAYMYO.UI.Converts.NullToBool(Request.QueryString["onay"]), BAYMYO.MultiSQLClient.MSqlDbType.Bit);
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
                        Settings.ProccesApply("Randevu", "YoneticiOnay", BAYMYO.UI.Converts.NullToGuid(dataGrid1.DataKeys[item.RowIndex][0]), chkState);
                GetDataPaging();
            }
            else if (ddlIslemler.SelectedIndex == 3)
            {
                foreach (GridViewRow item in dataGrid1.Rows)
                    if (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                        using (Lib.Randevu rnd = Lib.RandevuMethods.GetRandevu(BAYMYO.UI.Converts.NullToGuid(dataGrid1.DataKeys[item.RowIndex][0])))
                        {
                            switch (rnd.Durum)
                            {
                                case 0:
                                case 1:
                                    Lib.RandevuMethods.Delete(rnd);
                                    break;
                            }
                        }
                GetDataPaging();
            }
        }
    }
}