using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_editor_yorumlar : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Settings.IsUserActive())
        {
            Response.Redirect(Settings.VirtualPath + "?l=1&ReturnUrl=" + Request.RawUrl, false);
            return;
        }

        this.Page.Title = Settings.CurrentUser().Adi + " " + Settings.CurrentUser().Soyadi + " - Yorumlarım";

        if (!this.Page.IsPostBack)
            GetDataPaging();
    }

    protected void rptComments_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "aktif":
                using (HiddenField hv = rptComments.Items[e.Item.ItemIndex].FindControl("hfID") as HiddenField)
                {
                    if (hv != null)
                        using (Lib.Yorum m = Lib.YorumMethods.GetYorum(BAYMYO.UI.Converts.NullToGuid(hv.Value)))
                        {
                            if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(m.ID))
                            {
                                m.Aktif = true;
                                Lib.YorumMethods.Update(m);
                                GetDataPaging();
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Yorum başarılı bir şekilde aktif edildi!');", true);
                            }
                        }
                }
                break;
            case "pasif":
                using (HiddenField hv = rptComments.Items[e.Item.ItemIndex].FindControl("hfID") as HiddenField)
                {
                    if (hv != null)
                        using (Lib.Yorum m = Lib.YorumMethods.GetYorum(BAYMYO.UI.Converts.NullToGuid(hv.Value)))
                        {
                            if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(m.ID))
                            {
                                m.Aktif = false;
                                Lib.YorumMethods.Update(m);
                                GetDataPaging();
                                ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Yorum başarılı bir şekilde pasif edildi!');", true);
                            }
                        }
                }
                break;
            case "remove":
                using (HiddenField hv = rptComments.Items[e.Item.ItemIndex].FindControl("hfID") as HiddenField)
                {
                    if (hv != null)
                        using (Lib.Yorum m = Lib.YorumMethods.GetYorum(BAYMYO.UI.Converts.NullToGuid(hv.Value)))
                        {
                            if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(m.ID))
                                if (Settings.CurrentUser().Mail.Equals(m.Mail))
                                {
                                    Lib.YorumMethods.Delete(m);
                                    GetDataPaging();
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Yorumunuz başarılı bir şekilde silindi!');", true);
                                }
                        }
                }
                break;
        }
    }

    private void GetDataPaging()
    {
        string query = "SELECT <%#TOP%> dbo.GetProfilResim(Y.Mail) AS ResimUrl, dbo.GetProfilUrl(Y.Mail) AS Url, dbo.GetHesapTipi(Y.Mail) Tipi, Y.* FROM Yorum Y WHERE (YoneticiOnay=1%Aktif%) AND (EXISTS(SELECT ID FROM Makale WHERE HesapID=@HesapID) OR EXISTS(SELECT ID FROM Mesaj WHERE HesapID=@HesapID) OR EXISTS(SELECT ID FROM Video WHERE HesapID=@HesapID) OR IcerikID=@Url) ORDER BY Aktif ASC, KayitTarihi DESC", queryCount = "SELECT COUNT(ID) FROM Yorum WHERE (YoneticiOnay=1%Aktif%) AND (EXISTS(SELECT ID FROM Makale WHERE HesapID=@HesapID) OR EXISTS(SELECT ID FROM Mesaj WHERE HesapID=@HesapID) OR EXISTS(SELECT ID FROM Video WHERE HesapID=@HesapID) OR IcerikID=@Url)";
        using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(query, queryCount))
        {
            data.ViewDataCount = 10;
            data.DataTargetControl = rptComments;
            data.PageNumberTargetControl = pageNumberLiteral;
            data.Parameters.Add("HesapID", Settings.CurrentUser().ID, BAYMYO.MultiSQLClient.MSqlDbType.UniqueIdentifier);
            data.Parameters.Add("Url", Settings.CurrentUser().ProfilObject.Url, BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
            if (!string.IsNullOrEmpty(Request.QueryString["aktif"]))
            {
                data.CustomDataQuery = data.CustomDataQuery.Replace("%Aktif%", " AND Aktif=@Aktif");
                data.CustomDataCountQuery = data.CustomDataCountQuery.Replace("%Aktif%", " AND Aktif=@Aktif");
                data.Parameters.Add("Aktif", Request.QueryString["aktif"], BAYMYO.MultiSQLClient.MSqlDbType.TinyInt);
            }
            else
            {
                data.CustomDataQuery = data.CustomDataQuery.Replace("%Aktif%", "");
                data.CustomDataCountQuery = data.CustomDataCountQuery.Replace("%Aktif%", "");
            }
            data.Binding();

            ltrOpenComment.Text = "<div id=\"comment-top\" class=\"job-set\"><span href=\"#\" class=\"writeOpen left\">" + string.Format("<div class=\"icon left\"></div><strong class=\"left\">İçeriklerinize Yapılan Yorumlar</strong></span>&nbsp;<span class=\"count right\">{0}</span></div>", ((data.TotalDataCount > 0) ? "Toplam (" + data.TotalDataCount + ") adet yorumum var." : "Henüz içeriklerinize yorum yapılmamış!"));
        }
    }
}