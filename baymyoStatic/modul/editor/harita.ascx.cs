using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class modul_editor_harita : System.Web.UI.UserControl
{
    public Lib.Maps maps = new Lib.Maps();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Settings.IsUserActive())
            {
                maps = Lib.MapsMethods.GetMaps(Settings.CurrentUser().ID);
                if (!Page.IsPostBack )
                    if (maps.Title.Equals("Görmekte olduğunuz bölge Türkiye") & maps.Description.Equals("Haritada konumunuzu belirlemek için üzerine tıklayınız!"))
                    {
                        switch (Settings.CurrentUser().Tipi)
                        {
                            case Lib.HesapTuru.Moderator:
                                baslik.Text = Settings.CurrentUser().ProfilObject.Adi;
                                adres.Text = Lib.KategoriMethods.GetKategori("hastaneuzmanlik", Settings.CurrentUser().ProfilObject.UzmanlikAlaniID).Adi;
                                break;
                            case Lib.HesapTuru.Editor:
                                baslik.Text = Lib.KategoriMethods.GetKategori("unvan", Settings.CurrentUser().ProfilObject.Unvan).Adi + " " + Settings.CurrentUser().Adi + " " + Settings.CurrentUser().Soyadi;
                                adres.Text = Lib.KategoriMethods.GetKategori("uzmanlik", Settings.CurrentUser().ProfilObject.UzmanlikAlaniID).Adi;
                                break;
                        }
                    }
                    else
                    {
                        baslik.Text = maps.Title;
                        adres.Text = maps.Description;
                    }
            }
            else
                Response.Redirect(Settings.VirtualPath + "?l=1&ReturnUrl=" + Request.RawUrl, false);
        }
        catch (Exception)
        {
            Response.Redirect(Settings.VirtualPath + "?l=1&ReturnUrl=" + Request.RawUrl, false);
        }
    }
}