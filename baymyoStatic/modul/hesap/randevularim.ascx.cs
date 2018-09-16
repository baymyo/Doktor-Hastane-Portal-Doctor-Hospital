using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_hesap_randevularim : System.Web.UI.UserControl
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
        using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(rptListe, "Randevu", "TarihSaat DESC", "Mail=@Mail"))
        {
            data.Parameters.Add("Mail", BAYMYO.UI.Converts.NullToString(Settings.CurrentUser().Mail), BAYMYO.MultiSQLClient.MSqlDbType.String);
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

            ltrOpenComment.Text = string.Format("<h1 class=\"mmb-Title\"><span class=\"left\">Randevu Taleplerim</span>&nbsp;<span class=\"count right\">{0}</span></h1>", ((data.TotalDataCount > 0) ? "Toplam (" + data.TotalDataCount + ") adet randevu var." : ""));
        }
    }
    protected void rptListe_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "details":
                Label lbl = rptListe.Items[e.Item.ItemIndex].FindControl("infoLabel") as Label;
                if (!string.IsNullOrEmpty(lbl.ToolTip))
                    switch (lbl.ToolTip)
                    {
                        case "calismaalani":
                            using (Lib.CalismaAlani c = Lib.CalismaAlaniMethods.GetCalismaAlani(BAYMYO.UI.Converts.NullToGuid(lbl.Text)))
                            {
                                if (c != null)
                                {
                                    lbl.Text = BAYMYO.UI.FileIO.ReadText(Server.MapPath(Settings.TextPath + "CardView.msg"));
                                    lbl.Text = lbl.Text.Replace("%Kurum%", c.Kurum);
                                    lbl.Text = lbl.Text.Replace("%Adres%", c.Adres);
                                    lbl.Text = lbl.Text.Replace("%Telefon%", c.Telefon);
                                    lbl.Text = lbl.Text.Replace("%Semt%", c.Semt);
                                    lbl.Text = lbl.Text.Replace("%Sehir%", c.Sehir);
                                    lbl.ToolTip = "";
                                    lbl.Visible = true;
                                }
                            }
                            break;
                    }
                break;
        }
    }
}