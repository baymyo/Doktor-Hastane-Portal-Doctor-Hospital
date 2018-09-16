using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_sayfa_sayfagoster : System.Web.UI.UserControl
{
    public Lib.Sayfa m;
    protected void Page_Load(object sender, EventArgs e)
    {
        m = Lib.SayfaMethods.GetSayfa(BAYMYO.UI.Converts.NullToInt16(Request.QueryString["sid"]));
        if (m != null)
            if (!m.Aktif)
            {
                this.Page.Title = "Aradığınız içerik bulunamadı!";
                m.Icerik = MessageBox.Show(DialogResult.Warning, "Bu içerik gösterime kapatılmıştır. Kimler yayından kaldırabilir yazarı yada yöneticilerimiz tarafından yayından kaldırılabilir.");
            }
            else
            {
                this.Page.Title = BAYMYO.UI.Web.Pages.ClearHtml(m.Baslik);
                BAYMYO.UI.Web.Pages.AddMetaTag(this.Page, m.Baslik, BAYMYO.UI.Web.Pages.ClearHtml(m.Icerik));
            }
    }
}