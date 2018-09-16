using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class panel_blok_blokliste : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            GetDataPaging();
    }

    private void GetDataPaging()
    {
        dataGrid1.DataSource = Lib.Blok.GetXmlData();
        dataGrid1.DataBind();
    }

    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {
        using (System.Data.DataTable xmlData = Lib.Blok.GetXmlData())
        {
            foreach (GridViewRow item in dataGrid1.Rows)
                switch (((CheckBox)item.Cells[0].FindControl("chkSelected")).Checked)
                {
                    case true:
                        xmlData.DefaultView.RowFilter = "";
                        xmlData.DefaultView.RowFilter = "ID=" + BAYMYO.UI.Converts.NullToInt32(dataGrid1.DataKeys[item.RowIndex]["ID"]);
                        xmlData.DefaultView[0]["Sira"] = BAYMYO.UI.Converts.NullToInt16(((DropDownList)item.FindControl("ddlSira")).SelectedValue);
                        xmlData.DefaultView[0]["Yer"] = ((DropDownList)item.FindControl("ddlYer")).SelectedValue;
                        xmlData.DefaultView[0]["Dil"] = ((DropDownList)item.FindControl("ddlDil")).SelectedValue;
                        xmlData.DefaultView[0]["TumDil"] = ((CheckBox)item.FindControl("chkTumDil")).Checked;
                        xmlData.DefaultView[0]["TumSayfa"] = ((CheckBox)item.FindControl("chkTumSayfa")).Checked;
                        xmlData.DefaultView[0]["Aktif"] = ((CheckBox)item.FindControl("chkAktif")).Checked;
                        break;
                }
            xmlData.AcceptChanges();
            xmlData.WriteXml(Server.MapPath(Lib.Blok.XmlPath));
            infoLiteral.Text = "Tablo üzerindeki değişiklikler başarılı bir şekilde kaydedildi!";
        }
    }

    protected void dataGrid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataGrid1.PageIndex = e.NewPageIndex;
        GetDataPaging();
    }
}