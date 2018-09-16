using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace Lib
{
    public partial class VideoCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Video this[int index]
        {
            get { return (Video)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Video obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Video obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Video obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Video obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Video obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Video : IDisposable
    {
        #region ---IDisposable Members---
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ---Properties/Field - Özellikler Alanlar---
        private Int64 m_ID;
        public Int64 ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        private System.Guid m_HesapID;
        public System.Guid HesapID
        {
            get { return m_HesapID; }
            set { m_HesapID = value; }
        }
        private string m_KategoriID;
        public string KategoriID
        {
            get { return m_KategoriID; }
            set { m_KategoriID = value; }
        }
        private string m_ResimUrl;
        public string ResimUrl
        {
            get { return m_ResimUrl; }
            set { m_ResimUrl = value; }
        }
        private string m_Baslik;
        public string Baslik
        {
            get { return m_Baslik; }
            set { m_Baslik = value; }
        }
        private string m_Embed;
        public string Embed
        {
            get { return m_Embed; }
            set { m_Embed = value; }
        }
        private string m_Etiket;
        public string Etiket
        {
            get { return m_Etiket; }
            set { m_Etiket = value; }
        }
        private DateTime m_KayitTarihi;
        public DateTime KayitTarihi
        {
            get { return m_KayitTarihi; }
            set { m_KayitTarihi = value; }
        }
        private bool m_Yorum;
        public bool Yorum
        {
            get { return m_Yorum; }
            set { m_Yorum = value; }
        }
        private bool m_Aktif;
        public bool Aktif
        {
            get { return m_Aktif; }
            set { m_Aktif = value; }
        }
        #endregion

        public Video()
        {
        }

        /// <summary>
        /// Video Nesnesi Oluþtur
        /// </summary>
        public Video(Int64 pID, System.Guid pHesapID, string pKategoriID, string pResimUrl, string pBaslik, string pEmbed, string pEtiket, DateTime pKayitTarihi, bool pYorum, bool pAktif)
        {
            this.m_ID = pID;
            this.m_HesapID = pHesapID;
            this.m_KategoriID = pKategoriID;
            this.m_ResimUrl = pResimUrl;
            this.m_Baslik = pBaslik;
            this.m_Embed = pEmbed;
            this.m_Etiket = pEtiket;
            this.m_KayitTarihi = pKayitTarihi;
            this.m_Yorum = pYorum;
            this.m_Aktif = pAktif;
        }
    }

    public partial class VideoJSON : IDisposable
    {
        #region ---IDisposable Members---
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ---Properties/Field - Özellikler Alanlar---
        private string m_Link;
        public string Link
        {
            get { return m_Link; }
            set { m_Link = value; }
        }
        private string m_ResimUrl;
        public string ResimUrl
        {
            get { return m_ResimUrl; }
            set { m_ResimUrl = value; }
        }
        private string m_Baslik;
        public string Baslik
        {
            get { return m_Baslik; }
            set { m_Baslik = value; }
        }
        private string m_Url;
        public string Url
        {
            get { return m_Url; }
            set { m_Url = value; }
        }
        private string m_AdiSoyadi;
        public string AdiSoyadi
        {
            get { return m_AdiSoyadi; }
            set { m_AdiSoyadi = value; }
        }
        private string m_KayitTarihi;
        public string KayitTarihi
        {
            get { return m_KayitTarihi; }
            set { m_KayitTarihi = value; }
        }
        #endregion

        public VideoJSON()
        {
        }

        /// <summary>
        /// Video Nesnesi Oluþtur
        /// </summary>
        public VideoJSON(Int64 pID, string pResimUrl, string pBaslik, string pUrl, string pAdiSoyadi, DateTime pKayitTarihi)
        {
            this.m_Link = Settings.CreateLink("video", pID, pBaslik);
            this.m_ResimUrl = pResimUrl;
            this.m_Baslik = pBaslik;
            this.m_Url = pUrl;
            this.m_AdiSoyadi = pAdiSoyadi;
            this.m_KayitTarihi = Settings.TarihFormat(pKayitTarihi);
        }
    }

    public partial class VideoMethods
    {
        ///<summary>
        /// Video Data PrimaryKey
        ///</summary>
        public static Video GetVideo(Int64 pID)
        {
            Video rvVideo = new Video();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT TOP(1) * FROM Video WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.BigInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvVideo = new Video(MConvert.NullToInt64(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["KategoriID"]), MConvert.NullToString(IDR["ResimUrl"]), MConvert.NullToString(IDR["Baslik"]), MConvert.NullToString(IDR["Embed"]), MConvert.NullToString(IDR["Etiket"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToBool(IDR["Yorum"]), MConvert.NullToBool(IDR["Aktif"]));
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
            return rvVideo;
        }

        ///<summary>
        /// Video Data Select
        ///</summary>
        public static List<VideoJSON> GetJSON(QueryType sorguTipi, int top)
        {
            List<VideoJSON> rvVideo = new List<VideoJSON>();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                string query = "";
                switch (sorguTipi)
                {
                    case QueryType.Date:
                        query = "SELECT TOP(" + top + ") Video.ID, Video.ResimUrl, Video.Baslik, Video.KayitTarihi, dbo.GetHesapUrl(Video.HesapID) AS Url, (Hesap.Adi +' '+Hesap.Soyadi) AS AdiSoyadi FROM Video INNER JOIN Hesap ON Video.HesapID = Hesap.ID WHERE Video.KayitTarihi=(SELECT MAX(Video.KayitTarihi) FROM Video WHERE Video.HesapID=Hesap.ID) AND Video.Aktif=1";
                        break;
                    case QueryType.Populer:
                        query = "SELECT * FROM (SELECT TOP(" + top + ") Video.ID, Video.ResimUrl, Video.Baslik, Video.KayitTarihi, dbo.GetHesapUrl(Video.HesapID) AS Url, (Hesap.Adi +' '+Hesap.Soyadi) AS AdiSoyadi, dbo.GetGosterimOran('video',Video.ID) AS Oran FROM Video INNER JOIN Hesap ON Video.HesapID = Hesap.ID WHERE Video.KayitTarihi=(SELECT MAX(Video.KayitTarihi) FROM Video WHERE Video.HesapID=Hesap.ID) AND Video.Aktif=1 ORDER BY Oran DESC) VideoListe WHERE Oran >= 1";
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, query, conneciton))
                {
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvVideo.Add(new VideoJSON(MConvert.NullToInt64(IDR["ID"]), MConvert.NullToString(IDR["ResimUrl"]), MConvert.NullToString(IDR["Baslik"]), MConvert.NullToString(IDR["Url"]), MConvert.NullToString(IDR["AdiSoyadi"]), MConvert.NullToDateTime(IDR["KayitTarihi"])));
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
            return rvVideo;
        }
        public static VideoCollection GetSelect(System.Guid pHesapID)
        {
            VideoCollection rvVideo = new VideoCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT * FROM Video WHERE HesapID=@HesapID", conneciton))
                {
                    cmd.Parameters.Add("HesapID", pHesapID, MSqlDbType.UniqueIdentifier);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvVideo.Add(new Video(MConvert.NullToInt64(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["KategoriID"]), MConvert.NullToString(IDR["ResimUrl"]), MConvert.NullToString(IDR["Baslik"]), MConvert.NullToString(IDR["Embed"]), MConvert.NullToString(IDR["Etiket"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToBool(IDR["Yorum"]), MConvert.NullToBool(IDR["Aktif"])));
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
            return rvVideo;
        }

        ///<summary>
        /// Video Data Insert
        ///</summary>
        public static int Insert(Video p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "INSERT INTO Video (HesapID,KategoriID,ResimUrl,Baslik,Embed,Etiket,KayitTarihi,Yorum,Aktif) VALUES(@HesapID,@KategoriID,@ResimUrl,@Baslik,@Embed,@Etiket,@KayitTarihi,@Yorum,@Aktif)", conneciton))
                {
                    cmd.Parameters.Add("HesapID", p.HesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("KategoriID", p.KategoriID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ResimUrl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Baslik", p.Baslik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Embed", p.Embed, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Etiket", p.Etiket, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("KayitTarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("Yorum", p.Yorum, MSqlDbType.Bit);
                    cmd.Parameters.Add("Aktif", p.Aktif, MSqlDbType.Bit);
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
        /// Video Data Update
        ///</summary>
        public static int Update(Video p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "UPDATE Video SET HesapID=@HesapID,KategoriID=@KategoriID,ResimUrl=@ResimUrl,Baslik=@Baslik,Embed=@Embed,Etiket=@Etiket,KayitTarihi=@KayitTarihi,Yorum=@Yorum,Aktif=@Aktif WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("HesapID", p.HesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("KategoriID", p.KategoriID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ResimUrl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Baslik", p.Baslik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Embed", p.Embed, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Etiket", p.Etiket, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("KayitTarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("Yorum", p.Yorum, MSqlDbType.Bit);
                    cmd.Parameters.Add("Aktif", p.Aktif, MSqlDbType.Bit);
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
        /// Video Data Delete
        ///</summary>
        public static int Delete(Int64 pID)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Video WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.BigInt);
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
        /// Video Data Delete
        ///</summary>
        public static int Delete(Video p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Video WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.BigInt);
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
