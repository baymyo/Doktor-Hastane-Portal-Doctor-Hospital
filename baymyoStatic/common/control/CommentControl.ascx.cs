using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommentControl : System.Web.UI.UserControl
{
    public string FormTitle { get { return (string)ViewState["FormTitle"]; } set { ViewState["FormTitle"] = value; } }
    public string ModulID { get { return (string)ViewState["ModulID"]; } set { ViewState["ModulID"] = value; } }
    public string IcerikID { get { return (string)ViewState["IcerikID"]; } set { ViewState["IcerikID"] = value; } }
    public bool IsCommandActive { get { return BAYMYO.UI.Converts.NullToBool(ViewState["IsCommandActive"]); } set { ViewState["IsCommandActive"] = value; } }

    protected override void OnInit(EventArgs e)
    {
        TextBox txt = new TextBox();
        txt.ID = "Adi";
        switch (Settings.CurrentUser().Tipi)
        {
            case Lib.HesapTuru.Moderator:
                txt.Text = Settings.CurrentUser().ProfilObject.Adi;
                break;
            case Lib.HesapTuru.Editor:
                txt.Text = Lib.KategoriMethods.GetKategori("unvan", Settings.CurrentUser().ProfilObject.Unvan).Adi + " " + Settings.CurrentUser().Adi + " " + Settings.CurrentUser().Soyadi;
                break;
            case Lib.HesapTuru.Standart:
                txt.Text = Settings.CurrentUser().Adi;
                break;
        }
        txt.CssClass = "noHtml emptyValidate";
        txt.MaxLength = 100;
        txt.Enabled = !Settings.CurrentUser().Aktif;
        txt.Visible = !Settings.CurrentUser().Aktif;
        CustomizeControl1.AddControl("Adınız", txt);

        txt = new TextBox();
        txt.ID = "Mail";
        txt.Text = Settings.CurrentUser().Mail;
        txt.Enabled = !Settings.CurrentUser().Aktif;
        txt.Visible = !Settings.CurrentUser().Aktif;
        txt.CssClass = "noHtml emptyValidate mailValidate";
        txt.MaxLength = 60;
        CustomizeControl1.AddControl("Mail", txt);

        txt = new TextBox();
        txt.ID = "Icerik";
        txt.CssClass = "noHtml emptyValidate";
        txt.MaxLength = 500;
        txt.TextMode = TextBoxMode.MultiLine;
        CustomizeControl1.AddControl("Yorum", txt);

        CustomizeControl1.SubmitClick += new CustomizeControl.ButtonEvent(CustomizeControl1_SubmitClick);

        base.OnInit(e);
    }

    void CustomizeControl1_SubmitClick(SortedDictionary<string, Control> controls)
    {
        writeBox.Style.Value = "display: block";
        if (((TextBox)controls["Icerik"]).Text.Length > 500)
        {
            CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Warning, string.Format("Yazmış olduğunuz yorum uzunluğu <b>{0}</b> karakterdir. Yorum alanına en fazla <b>500</b> karakter girebilirsiniz lütfen yazınızı kontrol ederek tekrar deneyiniz.", ((TextBox)controls["Icerik"]).Text.Length));
            return;
        }

        if (!string.IsNullOrEmpty(((TextBox)controls["Adi"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Mail"]).Text)
            & !string.IsNullOrEmpty(((TextBox)controls["Icerik"]).Text)
            & !string.IsNullOrEmpty(ModulID)
            & !string.IsNullOrEmpty(IcerikID)
            & CustomizeControl1.PanelVisible
            & CustomizeControl1.SubmitEnabled)
            using (Lib.Yorum m = new Lib.Yorum())
            {
                m.IP = Context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                m.Adi = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Adi"]).Text, 100);
                m.Mail = ((TextBox)controls["Mail"]).Text;
                m.ModulID = ModulID;
                m.IcerikID = IcerikID;
                m.Icerik = BAYMYO.UI.Commons.SubStringText(((TextBox)controls["Icerik"]).Text, 500);
                m.KayitTarihi = DateTime.Now;
                m.YoneticiOnay = IsCommandActive;
                m.Aktif = IsCommandActive;
                if (Lib.YorumMethods.Insert(m) > 0)
                {
                    CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Succes, "Yorumunuz başarılı bir şekilde tarafımıza gönderilmiştir, kontrol edildikten sonra yayımlanacaktır.");
                    CustomizeControl1.PanelVisible = false;
                    CustomizeControl1.SubmitEnabled = false;
                    ((TextBox)controls["Adi"]).Text = string.Empty;
                    ((TextBox)controls["Mail"]).Text = string.Empty;
                    ((TextBox)controls["Icerik"]).Text = string.Empty;
                }
            }
        else
            CustomizeControl1.MessageText = MessageBox.Show(DialogResult.Stop, "Lütfen aşağıdaki ilgili kutucukların dolu olduğundan yada geçerli değerlere sahip olduğundan emin olunuz.");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack & this.Visible)
            GetDataPaging();
    }

    protected void rptComments_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (this.IsCommandActive)
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
                                {
                                    Lib.YorumMethods.Delete(m);
                                    GetDataPaging();
                                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "x", "alert('Yorum başarılı bir şekilde kaldırıldı!');", true);
                                }
                            }
                    }
                    break;
            }
        }
    }

    private void GetDataPaging()
    {
        writeBox.Style.Value = "display: none";
        string query = "SELECT <%#TOP%> dbo.GetProfilResim(Y.Mail) AS ResimUrl, dbo.GetProfilUrl(Y.Mail) AS Url, dbo.GetHesapTipi(Y.Mail) Tipi, Y.* FROM Yorum Y WHERE ", queryCount = "SELECT COUNT(ID) FROM Yorum WHERE ";
        switch (IsCommandActive)
        {
            case true:
                query += "YoneticiOnay=1 AND ModulID=@ModulID AND IcerikID=@IcerikID ORDER BY KayitTarihi DESC";
                queryCount += "YoneticiOnay=1 AND ModulID=@ModulID AND IcerikID=@IcerikID";
                break;
            default:
                query += "YoneticiOnay=1 AND Aktif=1 AND ModulID=@ModulID AND IcerikID=@IcerikID ORDER BY KayitTarihi DESC";
                queryCount += "YoneticiOnay=1 AND Aktif=1 AND ModulID=@ModulID AND IcerikID=@IcerikID";
                break;
        }
        using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(query, queryCount))
        {
            data.ViewDataCount = 5;
            data.DataTargetControl = rptComments;
            data.PageNumberTargetControl = pageNumberLiteral;
            data.Parameters.Add("ModulID", ModulID, BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
            data.Parameters.Add("IcerikID", IcerikID, BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
            data.Binding();

            ltrOpenComment.Text = "<div id=\"comment-top\" class=\"job-set\"><span href=\"#\" class=\"writeOpen left\">" + string.Format("<div class=\"icon left\"></div><strong class=\"left\">Yorum Ekle</strong></span>&nbsp;<span class=\"count right\">{0}</span></div>", ((data.TotalDataCount > 0) ? "Toplam (" + data.TotalDataCount + ") adet yorum var." : "Bu içeriğe henüz yorum yapılmamış!"));
        }
    }
}