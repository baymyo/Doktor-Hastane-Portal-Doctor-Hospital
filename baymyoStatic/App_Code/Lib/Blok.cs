using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAYMYO.UI;
using System.IO;

namespace Lib
{
    public enum BlokTipi
    {
        Ascx,
        Html
    }

    public enum BlokSablonTipi
    {
        None,
        HtmlNoTheme,
        HtmlAndTheme,
        AscxNoTheme,
        AscxAndTheme
    }

    public static class Blok
    {
        #region ---Properties/Field - Özellikler Alanlar---
        public static string ThemePath
        {
            get { return Settings.VirtualPath + "common/master/"; }
        }
        public static string AscxPath
        {
            get { return Settings.VirtualPath + "common/ascx/"; }
        }
        public static string HtmlPath
        {
            get { return Settings.AppDataPath + "html/"; }
        }
        public static string XmlPath
        {
            get { return Settings.AppDataPath + "xml/bloklar.xml"; }
        }
        #endregion

        #region ---Methot/Fonksiyonlar---
        public static DataTable GetXmlData()
        {
            DataTable XMLData = new DataTable("Bloklar");
            try
            {
                XMLData.Columns.Add("ID", typeof(int));
                XMLData.Columns.Add("Adi", typeof(string));
                XMLData.Columns.Add("Baslik", typeof(string));
                XMLData.Columns.Add("Yer", typeof(string));
                XMLData.Columns.Add("SablonTipi", typeof(string));
                XMLData.Columns.Add("Sira", typeof(Int16));
                XMLData.Columns.Add("Dil", typeof(string));
                XMLData.Columns.Add("TumDil", typeof(bool));
                XMLData.Columns.Add("Sayfa", typeof(string));
                XMLData.Columns.Add("TumSayfa", typeof(bool));
                XMLData.Columns.Add("Tipi", typeof(string));
                XMLData.Columns.Add("Aktif", typeof(bool));

                XMLData.Columns["ID"].AllowDBNull = false;
                XMLData.Columns["ID"].AutoIncrement = true;
                XMLData.Columns["ID"].AutoIncrementStep = 1;
                XMLData.Columns["ID"].AutoIncrementSeed = 1;

                string xmlPath = HttpContext.Current.Server.MapPath(Blok.XmlPath);
                if (!System.IO.File.Exists(xmlPath))
                    XMLData.WriteXml(xmlPath);
                XMLData.ReadXml(xmlPath);
                xmlPath = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return XMLData;
        }
        public static DataTable GetDosyalar(BlokTipi blokType)
        {
            switch (blokType)
            {
                case BlokTipi.Ascx:
                    return BAYMYO.UI.FileIO.ReadDirectory(HttpContext.Current.Server.MapPath(Blok.AscxPath), "*.ascx");
                default:
                    return BAYMYO.UI.FileIO.ReadDirectory(HttpContext.Current.Server.MapPath(Blok.HtmlPath), "*.bhtml");
            }
        }
        public static DataTable GetYerlesimler()
        {
            DataTable dt = new DataTable("Yerlesimler");
            dt.Columns.Add("Key", typeof(string));
            dt.Columns.Add("Value", typeof(string));

            DataRow dr;

            #region ---Üst Blok Seçenekleri---
            dr = dt.NewRow();
            dr["Key"] = "Tum_EnUst";
            dr["Value"] = "Tüm - En Üst Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key"] = "Tum_Ust";
            dr["Value"] = "Tüm - Üst Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key"] = "Tum_UstKemer";
            dr["Value"] = "Tüm - Üst Kemer Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key"] = "Tum_Sol";
            dr["Value"] = "Tüm - Sol Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key"] = "Tum_Orta";
            dr["Value"] = "Tüm - Orta Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key"] = "Tum_Sag";
            dr["Value"] = "Tüm - Sağ Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key"] = "Tum_Alt";
            dr["Value"] = "Tüm - Alt Blok";
            dt.Rows.Add(dr);
            #endregion

            #region ---Anasayfa Blok Seçenekleri---
            dr = dt.NewRow();
            dr["Key"] = "Anasayfa_EnUst";
            dr["Value"] = "Anasayfa - En Üst Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key"] = "Anasayfa_Ust";
            dr["Value"] = "Anasayfa - Üst Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key"] = "Anasayfa_UstKemer";
            dr["Value"] = "Anasayfa - Üst Kemer Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key"] = "Anasayfa_Sol";
            dr["Value"] = "Anasayfa - Sol Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key"] = "Anasayfa_Orta";
            dr["Value"] = "Anasayfa - Orta Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key"] = "Anasayfa_Sag";
            dr["Value"] = "Anasayfa - Sağ Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key"] = "Anasayfa_Alt";
            dr["Value"] = "Anasayfa - Alt Blok";
            dt.Rows.Add(dr);
            #endregion

            #region ---Icerik Blok Seçenekleri---
            dr = dt.NewRow();
            dr["Key"] = "Icerik_EnUst";
            dr["Value"] = "Icerik - En Üst Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key"] = "Icerik_Ust";
            dr["Value"] = "Icerik - Üst Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key"] = "Icerik_UstKemer";
            dr["Value"] = "Icerik - Üst Kemer Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key"] = "Icerik_Sol";
            dr["Value"] = "Icerik - Sol Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key"] = "Icerik_Orta";
            dr["Value"] = "Icerik - Orta Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key"] = "Icerik_Sag";
            dr["Value"] = "Icerik - Sağ Blok";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Key"] = "Icerik_Alt";
            dr["Value"] = "Icerik - Alt Blok";
            dt.Rows.Add(dr);
            #endregion

            return dt;
        }
        public static DataTable GetSablonTipleri(bool isHtmlBlock)
        {
            DataTable dt = new DataTable("SablonTipleri");
            dt.Columns.Add("Key", typeof(string));
            dt.Columns.Add("Value", typeof(string));

            DataRow dr;

            #region ---Blok Seçenekleri---
            switch (isHtmlBlock)
            {
                case true:
                    dr = dt.NewRow();
                    dr["Key"] = "HtmlNoTheme";
                    dr["Value"] = "Html NoTheme";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["Key"] = "HtmlAndTheme";
                    dr["Value"] = "Html And Theme";
                    dt.Rows.Add(dr);
                    break;
                case false:
                    dr = dt.NewRow();
                    dr["Key"] = "AscxNoTheme";
                    dr["Value"] = "Ascx NoTheme";
                    dt.Rows.Add(dr);

                    dr = dt.NewRow();
                    dr["Key"] = "AscxAndTheme";
                    dr["Value"] = "Ascx And Theme";
                    dt.Rows.Add(dr);
                    break;
            }
            #endregion

            return dt;
        }
        #endregion

        static LiteralControl AddLiteral(string path)
        {
            LiteralControl getLiteral = new LiteralControl();
            try
            {
                getLiteral.Text = FileIO.ReadText(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return getLiteral;
        }

        public static void Add(ContentPlaceHolder contentPlace, string themeType, string name, string title)
        {
            try
            {
                //UserControl getTheme = null; Label getTitle = null;
                if (contentPlace != null)
                    switch (themeType)
                    {
                        case "HtmlNoTheme":
                            string fullPath = HttpContext.Current.Server.MapPath(Blok.HtmlPath + name);
                            if (File.Exists(fullPath))
                                contentPlace.Controls.Add(AddLiteral(fullPath));
                            fullPath = null;
                            return;
                        case "HtmlAndTheme":
                            //string fullPath = HttpContext.Current.Server.MapPath(Blok.HtmlPath + name);
                            //if (File.Exists(fullPath))
                            //getTheme = (UserControl)contentPlace.Page.LoadControl(ThemePath);
                            //getTitle = ((Label)getTheme.FindControl("Title"));
                            //getTitle.Text = getTitle.Text.Replace("%Title%", title);
                            //((ContentPlaceHolder)getTheme.FindControl("Content")).Controls.Add(AddLiteral(fullPath));
                            //contentPlace.Controls.Add(getTheme);
                            //fullPath = null;
                            return;
                        case "AscxNoTheme":
                            if (File.Exists(HttpContext.Current.Server.MapPath(Blok.AscxPath + name)))
                                contentPlace.Controls.Add((UserControl)contentPlace.Page.LoadControl(Blok.AscxPath + name));
                            return;
                        case "AscxAndTheme":
                            //if (File.Exists(HttpContext.Current.Server.MapPath(Blok.AscxPath + name)))
                            //getTheme = (UserControl)contentPlace.Page.LoadControl(ThemePath);
                            //getTitle = ((Label)getTheme.FindControl("Title"));
                            //getTitle.Text = getTitle.Text.Replace("%Title%", title);
                            //((ContentPlaceHolder)getTheme.FindControl("Content")).Controls.Add((UserControl)contentPlace.Page.LoadControl(Blok.AscxPath + name));
                            //contentPlace.Controls.Add(getTheme);
                            return;
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}