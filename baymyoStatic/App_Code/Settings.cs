using System;
using System.Collections.Generic;
using System.Web;


public enum QueryType
{
    None,
    Date,
    Name,
    Populer,
    WeekOfThe,
    EditorChoice
}

public static class Settings
{
    public const string FormTitleFormat = "<span class=\"titleFormat1\">{0}</span>&nbsp;<span class=\"titleFormat2\">{1}</span>";
    public const string SplitFormat = "<span style=\"font-size:12pt;font-weight:bolder;color:#cc0000\">,</span>";

    #region --- Info ---
    public static string SiteUrl
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["SiteUrl"]; }
    }
    public static string SiteTitle
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["SiteTitle"]; }
    }
    public static string SiteDescription
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["SiteDescription"]; }
    }
    public static string SiteKeywords
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["SiteKeywords"]; }
    }
    #endregion

    #region --- Mail ---
    public static string ContactName
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["ContactName"]; }
    }

    public static string ContactMail
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["ContactMail"]; }
    }
    #endregion

    #region --- Path ---
    public const string InSlangyUrl = ";orospu;amciklar;pezevenk;amcik;anasinisikim;anasini;dolandirici;fahise;gotveren;hirsiz;ibne;kaltak;orospu;pezevenk;pust;sikerim;sikim;sikisken;sokarim;sulalesini;yarak;yarrakkafa;amkafa;amcikkafa;amciksuyu;amsuyu;aminakoyarim;aminakoyum;gottensikim;aminisikim;amcikkafali;amsuyu;aminakoydugum;sikisken;sevisken;senisikim;onusikim;gottensikim;ananinaminisikim;bacinisikim;karinisikim;sulalenisikim;anneannenisikim;annenisikim;sex;seks;seksi;seksseks;sexsex;sexybaby;seksibebek;seksikiz;duldul;dulkari;dulkiz;kizlikzari;kizlikzaripatlak;patlakzar;";
    public const string InValidUrl = ";turkiyeli;turkum;milletim;istiklal;ataturk;mustafakemalataturk;kemalataturk;mustafaataturk;http;www;wwwwww;images;common;adminpanel;adminpanel;administrator;aboutus;logout;register;remember;account;myaccount;password;activation;contact;article;articleadd;articleekle;telefon;magaza;magazalar;mesajlar;magazanet;alisveris;alisverisler;doktorlarnet;doktor;doktorlar;doktors;doktorum;doktorculuk;doctor;doctors;medical;medikal;hastalik;hastalar;anasayfa;yonetim;panel;yonetimpanel;yonetimpaneli;iletisim;hakkimizda;makale;makaleekle;makaleadd;yenimakale;makalelerim;makaleler;makalelerim;yenimakale;haberler;mesajlar;sorular;sorduklarim;okuduklarim;yorumlarim;cevaplar;cevaplanlar;cevapladiklarim;";

    public const string DateTimeControlPath = "~/common/control/DateTimeControl.ascx";

    public static string VirtualPath
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["VirtualPath"]; }
    }

    public static string PanelPath
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["PanelPath"]; }
    }

    public static string AppDataPath
    {
        get { return Settings.VirtualPath + "data/"; }
    }

    public static string ImagesPath
    {
        get { return Settings.VirtualPath + "images/"; }
    }

    public static string IconsPath
    {
        get { return Settings.VirtualPath + "images/icons/"; }
    }

    public static string TextPath
    {
        get { return Settings.AppDataPath + "views/"; }
    }

    public static string JSonPath
    {
        get { return Settings.AppDataPath + "json/"; }
    }

    public static string XmlPath
    {
        get { return Settings.AppDataPath + "xml/"; }
    }
    #endregion

    #region --- Data ---
    public static Lib.Hesap CurrentUser()
    {
        if (HttpContext.Current.Session["UserInfo"] != null)
            return HttpContext.Current.Session["UserInfo"] as Lib.Hesap;
        else
            return new Lib.Hesap();
    }
    public static bool IsUserActive()
    {
        return (!BAYMYO.UI.Converts.NullToGuid(null).Equals(CurrentUser().ID) & CurrentUser().Aktif);
    }
    public static bool ParentKategori(string modulID)
    {
        switch (modulID)
        {
            case "makale":
                return true;
            default:
                return false;
        }
    }

    public static string YeniOnayKodu()
    {
        return Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5);
    }
    public static string GirisDurum(string value)
    {
        switch (value)
        {
            case "0":
            case "logout":
                return "\\modul\\default\\logout.ascx";
            case "1":
            case "login":
                return "\\modul\\default\\login.ascx";
            case "2":
            case "register":
                return "\\modul\\default\\register.ascx";
            case "3":
            case "remember":
                return "\\modul\\default\\remember.ascx";
            case "4":
            case "account":
                return "\\modul\\default\\account.ascx";
            case "5":
            case "myaccount":
                return "\\modul\\hesap\\hesap.ascx";
            default:
                return string.Empty;
        }
    }
    public static string TarihFormat(object d)
    {
        if (d != null)
        {
            TimeSpan s = DateTime.Now - BAYMYO.UI.Converts.NullToDateTime(d);
            if (s.Days > 364)
            {
                double yil = (s.Days / 365);
                if (s.Days > ((yil * 365) + 30))
                {
                    double ay = (s.Days - (yil * 365)) / 30;
                    if (ay < 12)
                        return yil + " yıl " + Math.Round(ay) + " ay önce";
                    else
                        return (yil + 1) + " yıl önce";
                }
                else
                    return yil + " yıl önce";
            }
            else if (s.Days > 180)
                return (s.Days / 30) + " ay önce";
            else if (s.Days > 0)
                return s.Days + " gün önce";
            else if (s.Hours > 0)
                return s.Hours + " saat önce";
            else if (s.Minutes > 0)
                return s.Minutes + " dk. önce";
            else
                return s.Seconds + " sn. önce";
        }

        return "<b>Çok Yeni</b>";
    }

    public static System.Data.DataTable SiraNumaralari()
    {
        System.Data.DataTable dt = new System.Data.DataTable("SiraNumaralari");
        dt.Columns.Add("Key", typeof(byte));
        dt.Columns.Add("Value", typeof(string));
        System.Data.DataRow dr = dt.NewRow();
        for (int i = 0; i < 25; i++)
        {
            dr = dt.NewRow();
            dr[0] = i.ToString();
            dr[1] = (i + 1) + ". Sıra";
            dt.Rows.Add(dr);
        }
        return dt;
    }
    public static System.Data.DataTable ReklamTipleri()
    {
        System.Data.DataTable dt = new System.Data.DataTable("ReklamTipleri");
        dt.Columns.Add("Key", typeof(string));
        dt.Columns.Add("Value", typeof(string));
        System.Data.DataRow dr = dt.NewRow();
        dr[0] = "728x90";
        dr[1] = "728x90 Orta Blok";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "650x80";
        dr[1] = "650x80 Orta Blok";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "468x60";
        dr[1] = "468x60 Orta Blok";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "300x250";
        dr[1] = "300x250 İçerik Arası";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "250x250";
        dr[1] = "250x250 Sol Blok";
        dt.Rows.Add(dr);

        return dt;
    }
    public static System.Data.DataTable DilSecenekleri()
    {
        System.Data.DataTable dt = new System.Data.DataTable("Diller");
        dt.Columns.Add("Key", typeof(string));
        dt.Columns.Add("Value", typeof(string));
        System.Data.DataRow dr = dt.NewRow();
        dr[0] = "tr-TR";
        dr[1] = "Türkçe";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "en-US";
        dr[1] = "İngilizce";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "de-DE";
        dr[1] = "Almanca";
        dt.Rows.Add(dr);

        return dt;
    }
    public static string SayfaTipi(byte id)
    {
        switch (id)
        {
            case 1:
                return "Üst Menü";
            case 2:
                return "Alt Menü";
            case 3:
                return "Sol Menü";
            case 4:
                return "Sağ Menü";
            default:
                return "Seçiniz";
        }
    }
    public static System.Data.DataTable SayfaTipleri()
    {
        System.Data.DataTable dt = new System.Data.DataTable("SayfaTipleri");
        dt.Columns.Add("Key", typeof(byte));
        dt.Columns.Add("Value", typeof(string));
        System.Data.DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "<Seçiniz>";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = 1;
        dr[1] = "Üst Menü";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = 2;
        dr[1] = "Alt Menü";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = 3;
        dr[1] = "Sol Menü";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = 4;
        dr[1] = "Sağ Menü";
        dt.Rows.Add(dr);

        return dt;
    }
    public static Lib.HesapCinsiyet HesapCinsiyeti(byte id)
    {
        switch (id)
        {
            case 1:
                return Lib.HesapCinsiyet.Erkek;
            case 2:
                return Lib.HesapCinsiyet.Bayan;
            default:
                return Lib.HesapCinsiyet.Belirtilmedi;
        }
    }
    public static System.Data.DataTable HesapCinsiyetleri()
    {
        System.Data.DataTable dt = new System.Data.DataTable("HesapCinsiyetleri");
        dt.Columns.Add("Key", typeof(byte));
        dt.Columns.Add("Value", typeof(string));
        System.Data.DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Belirtilmedi";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = 1;
        dr[1] = "Erkek";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = 2;
        dr[1] = "Bayan";
        dt.Rows.Add(dr);

        return dt;
    }
    public static Lib.HesapTuru HesapTipi(byte id)
    {
        switch (id)
        {
            case 1:
                return Lib.HesapTuru.Admin;
            case 2:
                return Lib.HesapTuru.Moderator;
            case 3:
                return Lib.HesapTuru.Editor;
            case 4:
                return Lib.HesapTuru.Standart;
            default:
                return Lib.HesapTuru.None;
        }
    }
    public static System.Data.DataTable HesapTipleri()
    {
        System.Data.DataTable dt = new System.Data.DataTable("HesapTipleri");
        dt.Columns.Add("Key", typeof(byte));
        dt.Columns.Add("Value", typeof(string));
        System.Data.DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "<Seçiniz>";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = 1;
        dr[1] = "Admin";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = 2;
        dr[1] = "Moderator";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = 3;
        dr[1] = "Editor";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = 4;
        dr[1] = "Standart";
        dt.Rows.Add(dr);

        return dt;
    }
    public static System.Data.DataTable MakaleDurumlari()
    {
        System.Data.DataTable dt = new System.Data.DataTable("MakaleDurumlari");
        dt.Columns.Add("Key", typeof(byte));
        dt.Columns.Add("Value", typeof(string));
        System.Data.DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Standart Makale";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = 1;
        dr[1] = "Editörün Seçimi";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = 2;
        dr[1] = "Haftanın Konusu";
        dt.Rows.Add(dr);

        return dt;
    }
    public static System.Data.DataTable MesajDurumlari()
    {
        System.Data.DataTable dt = new System.Data.DataTable("MesajDurumlari");
        dt.Columns.Add("Key", typeof(byte));
        dt.Columns.Add("Value", typeof(string));
        System.Data.DataRow dr = dt.NewRow();
        dr[0] = 1;
        dr[1] = "Okunmadı olarak işaretlendi!";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = 2;
        dr[1] = "Okundu olarak işaretlendi.";
        dt.Rows.Add(dr);

        return dt;
    }
    public static System.Data.DataTable YayimlamaDurumlari()
    {
        System.Data.DataTable dt = new System.Data.DataTable("YayimlamaDurumlari");
        dt.Columns.Add("Key", typeof(bool));
        dt.Columns.Add("Value", typeof(string));
        System.Data.DataRow dr = dt.NewRow();
        dr[0] = 0;
        dr[1] = "Sadece Ben ve Hasta";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = 1;
        dr[1] = "Herkese Açık!";
        dt.Rows.Add(dr);

        return dt;
    }
    public static System.Data.DataTable RandevuDurumlari()
    {
        System.Data.DataTable dt = new System.Data.DataTable("RandevuDurumlari");
        dt.Columns.Add("Key", typeof(byte));
        dt.Columns.Add("Value", typeof(string));
        System.Data.DataRow dr = dt.NewRow();
        dr[0] = 1;
        dr[1] = "Randevu Onay Bekliyor!";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = 2;
        dr[1] = "Randevunuz Onaylandı.";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = 3;
        dr[1] = "Randevu İptal Edildi.";
        dt.Rows.Add(dr);

        return dt;
    }

    //Admin
    public static string AktivasyonDurum(object obj)
    {
        switch (obj.ToString())
        {
            case "1":
            case "True":
            case "true":
                return "<img class=\"toolTip\" title=\"Mail ile aktivasyon işlemi gerçekleştirildi.\" src=\"" + Settings.IconsPath + "admin/address.png\"/>";
            default:
                return "<img class=\"toolTip\" title=\"Mail ile aktivasyon bekleniyor!\" src=\"" + Settings.IconsPath + "admin/address-lock.png\"/>";
        }
    }
    public static string AbonelikDurum(object obj)
    {
        switch (obj.ToString())
        {
            case "1":
            case "True":
            case "true":
                return "<img class=\"toolTip\" title=\"Abonelik durumu aktif.\" src=\"" + Settings.IconsPath + "admin/email.png\"/>";
            default:
                return "<img class=\"toolTip\" title=\"Abonelik durumu pasif!\" src=\"" + Settings.IconsPath + "admin/email-lock.png\"/>";
        }
    }
    public static string YorumDurum(object obj)
    {
        switch (obj.ToString())
        {
            case "1":
            case "True":
            case "true":
                return "<img class=\"toolTip\" title=\"Yorum yazabilir\" src=\"" + Settings.IconsPath + "admin/comment.png\"/>";
            default:
                return "<img class=\"toolTip\" title=\"Kilitlendi!\" src=\"" + Settings.IconsPath + "admin/lock.png\"/>";
        }
    }
    public static string IcerikDurum(object obj)
    {
        switch (obj.ToString())
        {
            case "1":
            case "True":
            case "true":
                return "<img class=\"toolTip\" title=\"Aktif\" src=\"" + Settings.IconsPath + "admin/check.png\"/>";
            default:
                return "<img class=\"toolTip\" title=\"Kilitlendi!\" src=\"" + Settings.IconsPath + "admin/lock.png\"/>";
        }
    }
    public static string YoneticiDurum(object obj)
    {
        switch (obj.ToString())
        {
            case "1":
            case "True":
            case "true":
                return "<img class=\"toolTip\" title=\"Yönetici Onaylı İçerik.\" src=\"" + Settings.IconsPath + "admin/onay.png\"/>";
            default:
                return "<img class=\"toolTip\" title=\"Yönetici Onayı Bekliyor!\" src=\"" + Settings.IconsPath + "admin/onay-lock.png\"/>";
        }
    }
    public static string VideoDurum(object obj)
    {
        switch (obj.ToString())
        {
            case "1":
            case "True":
            case "true":
                return "<img class=\"toolTip\" title=\"Videolu İçerik.\" src=\"" + Settings.IconsPath + "admin/video.png\"/>";
            default:
                return "<img class=\"toolTip\" title=\"Video Yok!\" src=\"" + Settings.IconsPath + "admin/video-lock.png\"/>";
        }
    }
    public static string RandevuDurum(object obj)
    {
        switch (obj.ToString())
        {
            case "2":
                return "<img class=\"toolTip\" title=\"Randevu Onaylandı.\" src=\"" + Settings.IconsPath + "admin/date.png\"/>";
            case "3":
                return "<img class=\"toolTip\" title=\"Randevu İptal Edildi.\" src=\"" + Settings.IconsPath + "admin/remove.png\"/>";
            default:
                return "<img class=\"toolTip\" title=\"Randevu Onay Bekliyor!\" src=\"" + Settings.IconsPath + "admin/date-lock.png\"/>";
        }
    }

    //Users
    public static string YorumGosterim(object obj)
    {
        switch (obj.ToString())
        {
            case "1":
            case "True":
            case "true":
                return "<img class=\"toolTip\" title=\"Herkese Açık!\" src=\"" + Settings.IconsPath + "12/global.png\"/>";
            default:
                return "<img class=\"toolTip\" title=\"Kilitlendi!\" src=\"" + Settings.IconsPath + "12/content-0.png\"/>";
        }
    }
    public static string YoneticiDurum(object obj, byte px)
    {
        switch (obj.ToString())
        {
            case "1":
            case "True":
            case "true":
                return "<img class=\"toolTip\" title=\"Yönetici Onaylı İçerik.\" src=\"" + Settings.IconsPath + px + "/editor.png\"/>";
            default:
                return "<img class=\"toolTip\" title=\"Yönetici Onayı Bekliyor!\" src=\"" + Settings.IconsPath + px + "/editor-lock.png\"/>";
        }
    }
    public static string RandevuKabul(object obj)
    {
        switch (obj.ToString())
        {
            case "2":
                return "<img class=\"toolTip images left\" title=\"Randevu Onaylandı.\" src=\"" + Settings.IconsPath + "64/ok.png\"/>";
            case "3":
                return "<img class=\"toolTip images left\" title=\"Randevu İptal Edildi.\" src=\"" + Settings.IconsPath + "64/cancel.png\"/>";
            default:
                return "<img class=\"toolTip images left\" title=\"Randevu Onay Bekliyor!\" src=\"" + Settings.IconsPath + "64/wait.png\"/>";
        }
    }
    public static string RandevuAl(string baslik, string link, bool aktif)
    {
        switch (aktif)
        {
            case true:
                return "<a href=\"" + link + "\"><img class=\"toolTip\" title=\"'" + baslik + "' isimli ofisimden Randevu Talep Etmek için tıklayın!\" src=\"" + Settings.ImagesPath + "randevu-al.png\"/></a>";
            default:
                return "";
        }
    }
    public static string RandevuAl(object baslik, string link, object aktif)
    {
        switch (aktif.ToString())
        {
            case "1":
            case "True":
            case "true":
                return "<a href=\"" + link + "\"><img class=\"toolTip right\" title=\"'" + baslik + "' isimli ofisimden Randevu Talep Etmek için tıklayın!\" src=\"" + Settings.ImagesPath + "randevu-al-liste.png\"/></a>";
            default:
                return "";
        }
    }
    public static string RandevuAlListe(string baslik, string link, object aktif)
    {
        switch (aktif.ToString())
        {
            case "1":
            case "True":
            case "true":
                return "<a href=\"" + link + "\"><span class=\"toolTip\" title=\"Randevu talep etmek için tıklayın!\"><img class=\"toolTip right\" title=\"Randevu talep etmek için tıklayın!\" src=\"" + Settings.ImagesPath + "randevu-al-liste-big.png\"/></span></a>";
            default:
                return "";
        }
    }
    #endregion

    #region --- Other Method ---
    public static void ClearControls(SortedDictionary<string, System.Web.UI.Control> controls)
    {
        foreach (string item in controls.Keys)
        {
            if (controls[item] is System.Web.UI.WebControls.TextBox)
                ((System.Web.UI.WebControls.TextBox)controls[item]).Text = string.Empty;
            else if (controls[item] is System.Web.UI.WebControls.DropDownList)
                if (((System.Web.UI.WebControls.DropDownList)controls[item]).Items.Count > 0)
                    ((System.Web.UI.WebControls.DropDownList)controls[item]).SelectedIndex = 0;
        }
    }

    public static bool CreateJSonData(string fileName, object data)
    {
        try
        {
            if (data != null)
            {
                System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string jsondata = javaScriptSerializer.Serialize(data);
                if (!string.IsNullOrEmpty(jsondata))
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath(Settings.JSonPath + fileName + ".js")))
                    {
                        sw.Write(jsondata);
                        sw.Close();
                        return true;
                    }
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public static bool CreateJSonData(string subPathName, string fileName, object data)
    {
        try
        {
            if (data != null)
            {
                System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                string jsondata = javaScriptSerializer.Serialize(data);
                if (!string.IsNullOrEmpty(jsondata))
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath(Settings.JSonPath + subPathName + "/" + fileName + ".js")))
                    {
                        sw.Write(jsondata);
                        sw.Close();
                        return true;
                    }
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static string CreateLink(string modulName, object icerikID, object baslik)
    {
        switch (modulName.ToString())
        {
            case "makale":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "a", icerikID, BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            case "makaleguncelle":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "aw", icerikID.ToString(), BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            case "makalekategori":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "ac", icerikID, BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            case "makaledoktor":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "ad", icerikID.ToString().Replace('-', ','), BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            case "haber":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "n", icerikID, BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            case "haberkategori":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "nc", icerikID, BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            case "video":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "v", icerikID, BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            case "videoguncelle":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "vu", icerikID, BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            case "videokategori":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "vc", icerikID, BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            case "mesaj":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "msg", icerikID, BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            case "mesajyanitla":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "answer", icerikID.ToString(), BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            case "mesajliste":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "qs", icerikID.ToString().Replace('-', ','), BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            case "sayfa":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "page", icerikID, BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            case "profil":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "profil", icerikID, BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            case "yazarhakkinda":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "about", icerikID, BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            case "iletisim":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "contact", icerikID, BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            case "randevu":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "randevu", icerikID.ToString().Replace('-', ','), BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            case "randevutalebi":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "rt", icerikID.ToString().Replace('-', ','), BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            case "calismaalani":
                return string.Format("{0}{3}-{1}-{2}.aspx", Settings.VirtualPath, "work", icerikID.ToString().Replace('-', ','), BAYMYO.UI.Commons.ClearInvalidCharacter(baslik.ToString().ToLower()));
            default:
                return "#";
        }
    }
    public static bool DeleteLinks(string modulName, string icerikID)
    {
        using (Lib.Manset m = Lib.MansetMethods.GetManset(modulName, icerikID))
        {
            if (m != null)
            {
                BAYMYO.UI.FileIO.Remove(HttpContext.Current.Server.MapPath(Settings.ImagesPath + "manset/" + m.ModulID + "/" + m.ResimUrl));
                Lib.MansetMethods.Delete(m);
            }
        }
        Lib.YorumMethods.Delete(modulName, icerikID);
        return true;
    }

    public static void ProccesList(string modulName, System.Web.UI.WebControls.DropDownList ddl)
    {
        int index = 0;
        ddl.Items.Clear();
        ddl.Items.Insert(index++, "İşlem Seçiniz!");
        switch (modulName)
        {
            case "hesap":
                ddl.Items.Insert(index++, "Yorum yazma aktif");
                ddl.Items.Insert(index++, "Yorum yazma kililtle");
                ddl.Items.Insert(index++, "Abonelik aktif olsun");
                ddl.Items.Insert(index++, "Abonelik pasif olsun");
                ddl.Items.Insert(index++, "Aktivasyon onayı aktif");
                ddl.Items.Insert(index++, "Aktivasyon onayı kililtle");
                ddl.Items.Insert(index++, "Hesab(ları) aktif et");
                ddl.Items.Insert(index++, "Hesab(ları) kililtle");
                break;
            case "mesaj":
                ddl.Items.Insert(index++, "Mesaj(ları) Onayla!");
                ddl.Items.Insert(index++, "Mesaj(ları) Kilitle!");
                ddl.Items.Insert(index++, "Mesaj(ları) aktif et");
                ddl.Items.Insert(index++, "Mesaj(ları) pasif et");
                break;
            case "sayfa":
                ddl.Items.Insert(index++, "Sayfa(ları) üst menü'e al");
                ddl.Items.Insert(index++, "Sayfa(ları) alt menü'e al");
                ddl.Items.Insert(index++, "Sayfa(ları) sol menü'e al");
                ddl.Items.Insert(index++, "Sayfa(ları) sağ menü'e al");
                ddl.Items.Insert(index++, "Sayfa(ları) aktif et");
                ddl.Items.Insert(index++, "Sayfa(ları) pasif et");
                break;
            case "yorum":
                ddl.Items.Insert(index++, "Yorum(ları) Onayla!");
                ddl.Items.Insert(index++, "Yorum(ları) Kilitle!");
                ddl.Items.Insert(index++, "Yorum(ları) aktif et");
                ddl.Items.Insert(index++, "Yorum(ları) pasif et");
                ddl.Items.Insert(index++, "Yorum(ları) Sil (X)");
                break;
            case "randevu":
                ddl.Items.Insert(index++, "Randevu(ları) Onayla!");
                ddl.Items.Insert(index++, "Randevu(ları) Kilitle!");
                ddl.Items.Insert(index++, "Randevu(ları) Sil (X)");
                break;
            case "makale":
                ddl.Items.Insert(index++, "Makale(leri) aktif et");
                ddl.Items.Insert(index++, "Makale(leri) pasif et");
                break;
            case "haber":
                ddl.Items.Insert(index++, "Haber(leri) aktif et");
                ddl.Items.Insert(index++, "Haber(leri) pasif et");
                break;
            case "video":
                ddl.Items.Insert(index++, "Video(lari) aktif et");
                ddl.Items.Insert(index++, "Video(lari) pasif et");
                break;
            case "reklam":
                ddl.Items.Insert(index++, "Reklam(ları) aktif et");
                ddl.Items.Insert(index++, "Reklam(ları) pasif et");
                break;
        }
    }
    public static int ProccesApply(string tableName, string columnName, object id, object value)
    {
        int rowsAffected = 0;
        using (BAYMYO.MultiSQLClient.MConnection cnn = new BAYMYO.MultiSQLClient.MConnection(BAYMYO.MultiSQLClient.MClientProvider.MSSQL))
        {
            switch (cnn.State)
            {
                case System.Data.ConnectionState.Closed:
                    cnn.Open();
                    break;
            }
            using (BAYMYO.MultiSQLClient.MCommand cmd = new BAYMYO.MultiSQLClient.MCommand(System.Data.CommandType.Text, string.Format("UPDATE {0} SET {1}=@Value WHERE ID=@ID", tableName, columnName), cnn))
            {
                cmd.Parameters.Add("ID", id);
                cmd.Parameters.Add("Value", value);
                rowsAffected = BAYMYO.MultiSQLClient.MConvert.NullToInt(cmd.ExecuteNonQuery());
            }
            switch (cnn.State)
            {
                case System.Data.ConnectionState.Open:
                    cnn.Close();
                    break;
            }
        }
        return rowsAffected;
    }

    public static bool SendMail(string receiveMail, string receiveName, string subject, string bodyText, bool isBodyHtml)
    {
        bool Sended = false;
        try
        {
            Sended = BAYMYO.UI.Mail.Send(receiveMail, receiveName, Settings.ContactMail, Settings.ContactName, subject, bodyText, isBodyHtml);
        }
        catch (Exception ex)
        {
            Sended = false;
            throw ex;
        }
        return Sended;
    }
    public static bool SendMail(string receiveMail, string receiveName, string sendingMail, string sendingName, string subject, string bodyText, bool isBodyHtml)
    {
        bool Sended = false;
        try
        {
            Sended = BAYMYO.UI.Mail.Send(receiveMail, receiveName, sendingMail, sendingName, subject, bodyText, isBodyHtml);
        }
        catch (Exception ex)
        {
            Sended = false;
            throw ex;
        }
        return Sended;
    }
    #endregion
}