using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace Lib
{
    public partial class ReklamCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Reklam this[int index]
        {
            get { return (Reklam)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Reklam obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Reklam obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Reklam obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Reklam obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Reklam obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Reklam : IDisposable
    {
        #region ---IDisposable Members---
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ---Properties/Field - Özellikler Alanlar---
        private int m_ID;
        public int ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        private string m_BannerName;
        public string BannerName
        {
            get { return m_BannerName; }
            set { m_BannerName = value; }
        }
        private string m_ImageUrl;
        public string ImageUrl
        {
            get { return m_ImageUrl; }
            set { m_ImageUrl = value; }
        }
        private string m_NavigateUrl;
        public string NavigateUrl
        {
            get { return m_NavigateUrl; }
            set { m_NavigateUrl = value; }
        }
        private string m_AlternateText;
        public string AlternateText
        {
            get { return m_AlternateText; }
            set { m_AlternateText = value; }
        }
        private string m_Keyword;
        public string Keyword
        {
            get { return m_Keyword; }
            set { m_Keyword = value; }
        }
        private int m_Impressions;
        public int Impressions
        {
            get { return m_Impressions; }
            set { m_Impressions = value; }
        }
        private int m_Width;
        public int Width
        {
            get { return m_Width; }
            set { m_Width = value; }
        }
        private int m_Height;
        public int Height
        {
            get { return m_Height; }
            set { m_Height = value; }
        }
        private bool m_IsActive;
        public bool IsActive
        {
            get { return m_IsActive; }
            set { m_IsActive = value; }
        }

        #endregion

        public Reklam()
        {
        }

        /// <summary>
        /// Reklam Nesnesi Oluþtur
        /// </summary>
        public Reklam(int pID, string pBannerName, string pImageUrl, string pNavigateUrl, string pAlternateText, string pKeyword, int pImpressions, int pWidth, int pHeight, bool pIsActive)
        {
            this.m_ID = pID;
            this.m_BannerName = pBannerName;
            this.m_ImageUrl = pImageUrl;
            this.m_NavigateUrl = pNavigateUrl;
            this.m_AlternateText = pAlternateText;
            this.m_Keyword = pKeyword;
            this.m_Impressions = pImpressions;
            this.m_Width = pWidth;
            this.m_Height = pHeight;
            this.m_IsActive = pIsActive;
        }
    }

    public partial class ReklamMethods
    {
        ///<summary>
        /// Reklam Data PrimaryKey
        ///</summary>
        public static Reklam GetReklam(int pID)
        {
            Reklam rvReklam = new Reklam();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT * FROM Reklam WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.Int);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvReklam = new Reklam(MConvert.NullToInt(IDR["ID"]), MConvert.NullToString(IDR["BannerName"]), MConvert.NullToString(IDR["ImageUrl"]), MConvert.NullToString(IDR["NavigateUrl"]), MConvert.NullToString(IDR["AlternateText"]), MConvert.NullToString(IDR["Keyword"]), MConvert.NullToInt(IDR["Impressions"]), MConvert.NullToInt(IDR["Width"]), MConvert.NullToInt(IDR["Height"]), MConvert.NullToBool(IDR["IsActive"]));
                        IDR.Close();
                    }
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rvReklam;
        }

        /// <summary>
        /// Reklam Getir
        /// </summary>
        public static Reklam GetReklam(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            Reklam rvReklam = new Reklam();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(cmdType, sqlQuery, conneciton))
                {
                    if (parameters != null)
                        foreach (MParameter item in parameters)
                            cmd.Parameters.Add(item);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvReklam = new Reklam(MConvert.NullToInt(IDR["ID"]), MConvert.NullToString(IDR["BannerName"]), MConvert.NullToString(IDR["ImageUrl"]), MConvert.NullToString(IDR["NavigateUrl"]), MConvert.NullToString(IDR["AlternateText"]), MConvert.NullToString(IDR["Keyword"]), MConvert.NullToInt(IDR["Impressions"]), MConvert.NullToInt(IDR["Width"]), MConvert.NullToInt(IDR["Height"]), MConvert.NullToBool(IDR["IsActive"]));
                        IDR.Close();
                    }
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rvReklam;
        }

        /// <summary>
        /// Reklam Liste Getir
        /// </summary>
        public static ReklamCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            ReklamCollection rvReklam = new ReklamCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(cmdType, sqlQuery, conneciton))
                {
                    if (parameters != null)
                        foreach (MParameter item in parameters)
                            cmd.Parameters.Add(item);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvReklam.Add(new Reklam(MConvert.NullToInt(IDR["ID"]), MConvert.NullToString(IDR["BannerName"]), MConvert.NullToString(IDR["ImageUrl"]), MConvert.NullToString(IDR["NavigateUrl"]), MConvert.NullToString(IDR["AlternateText"]), MConvert.NullToString(IDR["Keyword"]), MConvert.NullToInt(IDR["Impressions"]), MConvert.NullToInt(IDR["Width"]), MConvert.NullToInt(IDR["Height"]), MConvert.NullToBool(IDR["IsActive"])));
                        IDR.Close();
                    }
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rvReklam;
        }

        ///<summary>
        /// Reklam Data Select
        ///</summary>
        public static ReklamCollection GetSelect()
        {
            ReklamCollection rvReklam = new ReklamCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT * FROM Reklam", conneciton))
                {
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvReklam.Add(new Reklam(MConvert.NullToInt(IDR["ID"]), MConvert.NullToString(IDR["BannerName"]), MConvert.NullToString(IDR["ImageUrl"]), MConvert.NullToString(IDR["NavigateUrl"]), MConvert.NullToString(IDR["AlternateText"]), MConvert.NullToString(IDR["Keyword"]), MConvert.NullToInt(IDR["Impressions"]), MConvert.NullToInt(IDR["Width"]), MConvert.NullToInt(IDR["Height"]), MConvert.NullToBool(IDR["IsActive"])));
                        IDR.Close();
                    }
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rvReklam;
        }

        public static ReklamCollection GetSelect(int width, int height)
        {
            ReklamCollection rvReklam = new ReklamCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                       conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT TOP(10) * FROM Reklam WHERE Width=@Width AND Height=@Height AND IsActive=1", conneciton))
                {
                    cmd.Parameters.Add("Width", width, MSqlDbType.Int);
                    cmd.Parameters.Add("Height", height, MSqlDbType.Int);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvReklam.Add(new Reklam(MConvert.NullToInt(IDR["ID"]), MConvert.NullToString(IDR["BannerName"]), MConvert.NullToString(IDR["ImageUrl"]), MConvert.NullToString(IDR["NavigateUrl"]), MConvert.NullToString(IDR["AlternateText"]), MConvert.NullToString(IDR["Keyword"]), MConvert.NullToInt(IDR["Impressions"]), MConvert.NullToInt(IDR["Width"]), MConvert.NullToInt(IDR["Height"]), MConvert.NullToBool(IDR["IsActive"])));
                        IDR.Close();
                    }
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rvReklam;
        }

        ///<summary>
        /// Reklam Data Insert
        ///</summary>
        public static int Insert(string pBannerName, string pImageUrl, string pNavigateUrl, string pAlternateText, string pKeyword, int pImpressions, int pWidth, int pHeight, bool pIsActive)
        {
            int rowsAffected = 0;
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "INSERT INTO Reklam (BannerName,ImageUrl,NavigateUrl,AlternateText,Keyword,Impressions,Width,Height,IsActive) VALUES(@BannerName,@ImageUrl,@NavigateUrl,@AlternateText,@Keyword,@Impressions,@Width,@Height,@IsActive)", conneciton))
                {
                    cmd.Parameters.Add("BannerName", pBannerName, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("ImageUrl", pImageUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("NavigateUrl", pNavigateUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("AlternateText", pAlternateText, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Keyword", pKeyword, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Impressions", pImpressions, MSqlDbType.Int);
                    cmd.Parameters.Add("Width", pWidth, MSqlDbType.Int);
                    cmd.Parameters.Add("Height", pHeight, MSqlDbType.Int);
                    cmd.Parameters.Add("IsActive", pIsActive, MSqlDbType.Bit);
                    rowsAffected = MConvert.NullToInt(cmd.ExecuteNonQuery());
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rowsAffected;
        }

        ///<summary>
        /// Reklam Data Insert
        ///</summary>
        public static int Insert(Reklam p)
        {
            int rowsAffected = 0;
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "INSERT INTO Reklam (BannerName,ImageUrl,NavigateUrl,AlternateText,Keyword,Impressions,Width,Height,IsActive) VALUES(@BannerName,@ImageUrl,@NavigateUrl,@AlternateText,@Keyword,@Impressions,@Width,@Height,@IsActive)", conneciton))
                {
                    cmd.Parameters.Add("BannerName", p.BannerName, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("ImageUrl", p.ImageUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("NavigateUrl", p.NavigateUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("AlternateText", p.AlternateText, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Keyword", p.Keyword, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Impressions", p.Impressions, MSqlDbType.Int);
                    cmd.Parameters.Add("Width", p.Width, MSqlDbType.Int);
                    cmd.Parameters.Add("Height", p.Height, MSqlDbType.Int);
                    cmd.Parameters.Add("IsActive", p.IsActive, MSqlDbType.Bit);
                    rowsAffected = MConvert.NullToInt(cmd.ExecuteNonQuery());
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rowsAffected;
        }

        ///<summary>
        /// Reklam Data Update
        ///</summary>
        public static int Update(int pID, string pBannerName, string pImageUrl, string pNavigateUrl, string pAlternateText, string pKeyword, int pImpressions, int pWidth, int pHeight, bool pIsActive)
        {
            int rowsAffected = 0;
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "UPDATE Reklam SET BannerName=@BannerName,ImageUrl=@ImageUrl,NavigateUrl=@NavigateUrl,AlternateText=@AlternateText,Keyword=@Keyword,Impressions=@Impressions,Width=@Width,Height=@Height,IsActive=@IsActive WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.Int);
                    cmd.Parameters.Add("BannerName", pBannerName, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("ImageUrl", pImageUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("NavigateUrl", pNavigateUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("AlternateText", pAlternateText, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Keyword", pKeyword, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Impressions", pImpressions, MSqlDbType.Int);
                    cmd.Parameters.Add("Width", pWidth, MSqlDbType.Int);
                    cmd.Parameters.Add("Height", pHeight, MSqlDbType.Int);
                    cmd.Parameters.Add("IsActive", pIsActive, MSqlDbType.Bit);
                    rowsAffected = MConvert.NullToInt(cmd.ExecuteNonQuery());
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rowsAffected;
        }

        ///<summary>
        /// Reklam Data Update
        ///</summary>
        public static int Update(Reklam p)
        {
            int rowsAffected = 0;
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "UPDATE Reklam SET BannerName=@BannerName,ImageUrl=@ImageUrl,NavigateUrl=@NavigateUrl,AlternateText=@AlternateText,Keyword=@Keyword,Impressions=@Impressions,Width=@Width,Height=@Height,IsActive=@IsActive WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.Int);
                    cmd.Parameters.Add("BannerName", p.BannerName, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("ImageUrl", p.ImageUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("NavigateUrl", p.NavigateUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("AlternateText", p.AlternateText, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Keyword", p.Keyword, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Impressions", p.Impressions, MSqlDbType.Int);
                    cmd.Parameters.Add("Width", p.Width, MSqlDbType.Int);
                    cmd.Parameters.Add("Height", p.Height, MSqlDbType.Int);
                    cmd.Parameters.Add("IsActive", p.IsActive, MSqlDbType.Bit);
                    rowsAffected = MConvert.NullToInt(cmd.ExecuteNonQuery());
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rowsAffected;
        }

        ///<summary>
        /// Reklam Data Delete
        ///</summary>
        public static int Delete(int pID)
        {
            int rowsAffected = 0;
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Reklam WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.Int);
                    rowsAffected = MConvert.NullToInt(cmd.ExecuteNonQuery());
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rowsAffected;
        }

        ///<summary>
        /// Reklam Data Delete
        ///</summary>
        public static int Delete(Reklam p)
        {
            int rowsAffected = 0;
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Reklam WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.Int);
                    rowsAffected = MConvert.NullToInt(cmd.ExecuteNonQuery());
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rowsAffected;
        }
    }
}
