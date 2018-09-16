using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_hesap_yorumlarim : System.Web.UI.UserControl
{
    public string tipi = "", resimUrl = "", url = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Settings.IsUserActive())
        {
            Response.Redirect(Settings.VirtualPath + "?l=1&ReturnUrl=" + Request.RawUrl, false);
            return;
        }

        this.Page.Title = Settings.CurrentUser().Adi + " " + Settings.CurrentUser().Soyadi + " - Yorumlarım";
        tipi = GetTipi(Settings.CurrentUser().Tipi);
        url = BAYMYO.UI.Converts.NullToString(Settings.CurrentUser().ProfilObject.Url);
        resimUrl = BAYMYO.UI.Converts.NullToString(Settings.CurrentUser().ProfilObject.ResimUrl);
        if (!this.Page.IsPostBack)
            GetDataPaging();
    }

    private string GetTipi(Lib.HesapTuru hesapTuru)
    {
        switch (hesapTuru)
        {
            case Lib.HesapTuru.Admin:
                return "admin";
            case Lib.HesapTuru.Moderator:
                return "moderator";
            case Lib.HesapTuru.Editor:
                return "editor";
            default:
                return "standart";
        }
    }

    protected void rptComments_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
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

        using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(rptComments, "Yorum", "KayitTarihi DESC", "Mail=@Mail"))
        {
            data.PageNumberTargetControl = pageNumberLiteral;
            data.Parameters.Add("Mail", Settings.CurrentUser().Mail, BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
            data.Binding();

            ltrOpenComment.Text = "<div id=\"comment-top\" class=\"job-set\"><span href=\"#\" class=\"writeOpen left\">" + string.Format("<div class=\"icon left\"></div><strong class=\"left\">Tüm Yorumlarım</strong></span>&nbsp;<span class=\"count right\">{0}</span></div>", ((data.TotalDataCount > 0) ? "Toplam (" + data.TotalDataCount + ") adet yorumum var." : "Henüz hiçbir içeriğe yorum yapmamışsınız!"));
        }
    }
}