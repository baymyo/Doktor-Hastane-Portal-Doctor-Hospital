using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_editor_randevutalebleri : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Settings.IsUserActive())
        {
            Response.Redirect(Settings.VirtualPath + "?l=1&ReturnUrl=" + Request.RawUrl, false);
            return;
        }

        this.Page.Title = Settings.CurrentUser().Adi + " " + Settings.CurrentUser().Soyadi + " - Randevularım";

        if (!this.Page.IsPostBack)
            GetDataPaging();
    }

    private void GetDataPaging()
    {
        using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(rptListe, "Randevu", "TarihSaat DESC", "YoneticiOnay=1 AND HesapID=@HesapID"))
        {
            data.Parameters.Add("HesapID", BAYMYO.UI.Converts.NullToGuid(Settings.CurrentUser().ID), BAYMYO.MultiSQLClient.MSqlDbType.UniqueIdentifier);
            if (!string.IsNullOrEmpty(Request.QueryString["durum"]))
            {
                data.Where += " AND Durum=@Durum";
                data.Parameters.Add("Durum", BAYMYO.UI.Converts.NullToByte(Request.QueryString["durum"]), BAYMYO.MultiSQLClient.MSqlDbType.TinyInt);
            }
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

            ltrOpenComment.Text = string.Format("<h1 class=\"mmb-Title\"><span class=\"left\">Randevu Talepleri</span>&nbsp;<span class=\"count right\">{0}</span></h1>", ((data.TotalDataCount > 0) ? "Toplam (" + data.TotalDataCount + ") adet randevu var." : ""));
        }
    }
}