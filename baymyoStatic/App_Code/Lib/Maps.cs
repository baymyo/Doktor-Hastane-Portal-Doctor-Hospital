using System;
using System.Collections.Generic;
using System.Web;

namespace Lib
{
    public class Maps
    {
        public string Lat { get; set; }
        public string Lng { get; set; }
        public string Zoom { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class MapsMethods
    {
        public static Maps GetMaps(object id)
        {
            string data = BAYMYO.UI.FileIO.ReadText(HttpContext.Current.Server.MapPath(Settings.JSonPath + "maps/" + id + ".js"));
            if (!string.IsNullOrEmpty(data))
            {
                System.Web.Script.Serialization.JavaScriptSerializer ser = new System.Web.Script.Serialization.JavaScriptSerializer();
                return ser.Deserialize<Maps>(data);
            }
            else
                return new Maps
                {
                    Lat = "36.818666685488",
                    Lng = "36.375732421875",
                    Zoom = "6",
                    Title = "Görmekte olduğunuz bölge Türkiye",
                    Description = "Haritada konumunuzu belirlemek için üzerine tıklayınız!"
                };
        }
    }
}