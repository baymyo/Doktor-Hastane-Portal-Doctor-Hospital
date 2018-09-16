using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_hesap_sorduklarim : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Settings.IsUserActive())
        {
            Response.Redirect(Settings.VirtualPath + "?l=1&ReturnUrl=" + Request.RawUrl, false);
            return;
        }

        this.Page.Title = Settings.CurrentUser().Adi + " " + Settings.CurrentUser().Soyadi + " - Sorduğum Sorular";

        if (!this.Page.IsPostBack)
            GetDataPaging();
    }

    private void GetDataPaging()
    {
        using (BAYMYO.UI.Web.DataPagers data = new BAYMYO.UI.Web.DataPagers(
            "SELECT <%#TOP%> Profil.Url, (Kategori.Adi +' '+ Hesap.Adi +' '+ Hesap.Soyadi) as Uzman, Mesaj.* FROM Mesaj INNER JOIN Hesap ON Mesaj.HesapID = Hesap.ID INNER JOIN Profil ON Hesap.ID = Profil.ID INNER JOIN Kategori ON Profil.Unvan = Kategori.ID WHERE Kategori.ModulID='unvan' AND Mesaj.Mail=@Mail ORDER BY GuncellemeTarihi DESC",
            "SELECT COUNT(Mesaj.ID) FROM Mesaj INNER JOIN Hesap ON Mesaj.HesapID = Hesap.ID INNER JOIN Profil ON Hesap.ID = Profil.ID INNER JOIN Kategori ON Profil.Unvan = Kategori.ID WHERE Kategori.ModulID='unvan' AND Mesaj.Mail=@Mail"))
        {
            data.Parameters.Add("Mail", Settings.CurrentUser().Mail, BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
            data.ViewDataCount = 25;
            data.DataTargetControl = rptListe;
            data.PageNumberTargetControl = pageNumberLiteral;
            data.Binding();
            if (data.TotalDataCount < 1)
            {
                pageNumberLiteral.Visible = true;
                pageNumberLiteral.Text = MessageBox.IsNotViews();
            }
            else
                jobSet.Visible = (data.TotalPageCount > 1);
        }
    }
}