using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lib
{
    public class Suggest
    {
        public string Name { get; set; }
    }

    public class SuggestMethods
    {
        public static System.Collections.Generic.List<Suggest> GetList()
        {
            System.Collections.Generic.List<Suggest> rv = new System.Collections.Generic.List<Suggest>();
            rv.Add(new Suggest { Name = "İskenderun Devlet Hastanesi" });
            rv.Add(new Suggest { Name = "İskenderun Palmiye Hastanesi" });
            rv.Add(new Suggest { Name = "İskenderun Deniz Tıp Hastanesi" });
            rv.Add(new Suggest { Name = "İ.Ü. Cerrah Paşa Tıp Fakültesi" });
            rv.Add(new Suggest { Name = "İ.Ü. Çapa Fakültesi" });
            return rv;
        }
    }
}