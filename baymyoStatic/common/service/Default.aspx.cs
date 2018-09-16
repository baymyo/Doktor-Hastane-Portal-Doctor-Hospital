using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class common_service_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [System.Web.Services.WebMethod]
    public static bool MapsSaved(string point, string zoom, string title, string description)
    {
        if (Settings.IsUserActive() & !string.IsNullOrEmpty(point))
        {
            System.Threading.Thread.Sleep(3000);
            Lib.Hesap hsp = Settings.CurrentUser();
            if (hsp != null)
            {
                if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(hsp.ID) & hsp.Aktif)
                {
                    switch (hsp.Tipi)
                    {
                        case Lib.HesapTuru.None:
                        case Lib.HesapTuru.Admin:
                        case Lib.HesapTuru.Standart:
                            return false;
                        default:
                            string[] kordinatlar = point.Replace("(", "").Replace(")", "").Replace(" ", "").Split(',');
                            Settings.CreateJSonData("maps", hsp.ID.ToString(),
                               new Lib.Maps
                               {
                                   Lat = kordinatlar[0],
                                   Lng = kordinatlar[1],
                                   Zoom = zoom,
                                   Title = title,
                                   Description = description
                               });
                            return true;
                    }
                }
            }
        }
        return false;
    }

    [System.Web.Services.WebMethod]
    public static bool MapsRemove()
    {
        if (Settings.IsUserActive())
        {
            System.Threading.Thread.Sleep(3000);
            Lib.Hesap hsp = Settings.CurrentUser();
            if (hsp != null)
                if (!BAYMYO.UI.Converts.NullToGuid(null).Equals(hsp.ID) & hsp.Aktif)
                {
                    BAYMYO.UI.FileIO.Remove(HttpContext.Current.Server.MapPath(Settings.JSonPath + "maps/" + hsp.ID + ".js"));
                    return true;
                }
        }
        return false;
    }
}