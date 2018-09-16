using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace Lib
{
    public partial class MakaleCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Makale this[int index]
        {
            get { return (Makale)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Makale obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Makale obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Makale obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Makale obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Makale obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Makale : IDisposable
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
        private string m_Ozet;
        public string Ozet
        {
            get { return m_Ozet; }
            set { m_Ozet = value; }
        }
        private string m_Icerik;
        public string Icerik
        {
            get { return m_Icerik; }
            set { m_Icerik = value; }
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
        private byte m_Durum;
        public byte Durum
        {
            get { return m_Durum; }
            set { m_Durum = value; }
        }
        private bool m_Uye;
        public bool Uye
        {
            get { return m_Uye; }
            set { m_Uye = value; }
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

        public Makale()
        {
        }

        /// <summary>
        /// Makale Nesnesi Oluþtur
        /// </summary>
        public Makale(Int64 pID, System.Guid pHesapID, string pKategoriID, string pResimUrl, string pBaslik, string pOzet, string pIcerik, string pEtiket, DateTime pKayitTarihi, byte pDurum, bool pUye, bool pYorum, bool pAktif)
        {
            this.m_ID = pID;
            this.m_HesapID = pHesapID;
            this.m_KategoriID = pKategoriID;
            this.m_ResimUrl = pResimUrl;
            this.m_Baslik = pBaslik;
            this.m_Ozet = pOzet;
            this.m_Icerik = pIcerik;
            this.m_Etiket = pEtiket;
            this.m_KayitTarihi = pKayitTarihi;
            this.m_Durum = pDurum;
            this.m_Uye = pUye;
            this.m_Yorum = pYorum;
            this.m_Aktif = pAktif;
        }
    }

    public partial class MakaleJSON : IDisposable
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
        private string m_Ozet;
        public string Ozet
        {
            get { return m_Ozet; }
            set { m_Ozet = value; }
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

        public MakaleJSON()
        {
        }

        /// <summary>
        /// Makale Nesnesi Oluþtur
        /// </summary>
        public MakaleJSON(Int64 pID, string pKategoriID, string pResimUrl, string pBaslik, string pOzet, string pUrl, string pAdiSoyadi, DateTime pKayitTarihi)
        {
            this.m_Link = Settings.CreateLink("makale", pID, pBaslik);
            this.m_KategoriID = pKategoriID;
            this.m_ResimUrl = pResimUrl;
            this.m_Baslik = pBaslik;
            this.m_Ozet = BAYMYO.UI.Commons.SubStringText(pOzet, 700) + ".. .";
            this.m_Url = pUrl;
            this.m_AdiSoyadi = pAdiSoyadi;
            this.m_KayitTarihi = Settings.TarihFormat(pKayitTarihi);
        }
    }

    public partial class MakaleMethods
    {
        ///<summary>
        /// Makale Data PrimaryKey
        ///</summary>
        public static Makale GetMakale(Int64 pID)
        {
            Makale rvMakale = new Makale();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT TOP(1) * FROM Makale WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.BigInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvMakale = new Makale(MConvert.NullToInt64(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["KategoriID"]), MConvert.NullToString(IDR["ResimUrl"]), MConvert.NullToString(IDR["Baslik"]), MConvert.NullToString(IDR["Ozet"]), MConvert.NullToString(IDR["Icerik"]), MConvert.NullToString(IDR["Etiket"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToByte(IDR["Durum"]), MConvert.NullToBool(IDR["Uye"]), MConvert.NullToBool(IDR["Yorum"]), MConvert.NullToBool(IDR["Aktif"]));
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
            return rvMakale;
        }

        ///<summary>
        /// Makale Data Select
        ///</summary>
        public static List<MakaleJSON> GetJSON(QueryType sorguTipi, int top)
        {
            List<MakaleJSON> rvMakale = new List<MakaleJSON>();
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
                        query = "SELECT TOP(" + top + ") Makale.ID, Makale.KategoriID, Makale.ResimUrl, Makale.Baslik, Makale.Icerik as Ozet, Makale.KayitTarihi, dbo.GetHesapUrl(Makale.HesapID) AS Url, (Hesap.Adi +' '+Hesap.Soyadi) AS AdiSoyadi FROM Makale INNER JOIN Hesap ON Makale.HesapID = Hesap.ID WHERE Makale.KayitTarihi=(SELECT MAX(Makale.KayitTarihi) FROM Makale WHERE Makale.HesapID=Hesap.ID) AND Makale.Aktif=1";
                        break;
                    case QueryType.Populer:
                        query = "SELECT * FROM (SELECT TOP(" + top + ") Makale.ID, Makale.KategoriID, Makale.ResimUrl, Makale.Baslik, Makale.Ozet, Makale.KayitTarihi, dbo.GetHesapUrl(Makale.HesapID) AS Url, (Hesap.Adi +' '+Hesap.Soyadi) AS AdiSoyadi, dbo.GetGosterimOran('makale',Makale.ID) AS Oran FROM Makale INNER JOIN Hesap ON Makale.HesapID = Hesap.ID WHERE Makale.KayitTarihi=(SELECT MAX(Makale.KayitTarihi) FROM Makale WHERE Makale.HesapID=Hesap.ID) AND Makale.Aktif=1 ORDER BY Oran DESC) MakaleListe WHERE Oran >= 1";
                        break;
                    case QueryType.WeekOfThe:
                        query = "SELECT TOP(" + top + ") Makale.ID, Makale.KategoriID, Makale.ResimUrl, Makale.Baslik, Makale.Icerik as Ozet, Makale.KayitTarihi, dbo.GetHesapUrl(Makale.HesapID) AS Url, (Hesap.Adi +' '+Hesap.Soyadi) AS AdiSoyadi FROM Makale INNER JOIN Hesap ON Makale.HesapID = Hesap.ID WHERE Makale.KayitTarihi=(SELECT MAX(Makale.KayitTarihi) FROM Makale WHERE Makale.HesapID=Hesap.ID) AND Makale.Aktif=1 AND Makale.Durum=2";
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, query, conneciton))
                {
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvMakale.Add(new MakaleJSON(MConvert.NullToInt64(IDR["ID"]), MConvert.NullToString(IDR["KategoriID"]), MConvert.NullToString(IDR["ResimUrl"]), MConvert.NullToString(IDR["Baslik"]), MConvert.NullToString(IDR["Ozet"]), MConvert.NullToString(IDR["Url"]), MConvert.NullToString(IDR["AdiSoyadi"]), MConvert.NullToDateTime(IDR["KayitTarihi"])));
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
            return rvMakale;
        }
        public static MakaleCollection GetSelect(System.Guid pHesapID)
        {
            MakaleCollection rvMakale = new MakaleCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT * FROM Makale WHERE HesapID=@HesapID", conneciton))
                {
                    cmd.Parameters.Add("HesapID", pHesapID, MSqlDbType.UniqueIdentifier);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvMakale.Add(new Makale(MConvert.NullToInt64(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["KategoriID"]), MConvert.NullToString(IDR["ResimUrl"]), MConvert.NullToString(IDR["Baslik"]), MConvert.NullToString(IDR["Ozet"]), MConvert.NullToString(IDR["Icerik"]), MConvert.NullToString(IDR["Etiket"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToByte(IDR["Durum"]), MConvert.NullToBool(IDR["Uye"]), MConvert.NullToBool(IDR["Yorum"]), MConvert.NullToBool(IDR["Aktif"])));
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
            return rvMakale;
        }

        ///<summary>
        /// Makale Data Insert
        ///</summary>
        public static int Insert(Makale p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "INSERT INTO Makale (HesapID,KategoriID,ResimUrl,Baslik,Ozet,Icerik,Etiket,KayitTarihi,Durum,Uye,Yorum,Aktif) VALUES(@HesapID,@KategoriID,@ResimUrl,@Baslik,@Ozet,@Icerik,@Etiket,@KayitTarihi,@Durum,@Uye,@Yorum,@Aktif)", conneciton))
                {
                    cmd.Parameters.Add("HesapID", p.HesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("KategoriID", p.KategoriID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ResimUrl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Baslik", p.Baslik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Ozet", BAYMYO.UI.Commons.SubStringText(p.Ozet, 250), MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Icerik", p.Icerik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Etiket", p.Etiket, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("KayitTarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("Durum", p.Durum, MSqlDbType.Byte);
                    cmd.Parameters.Add("Uye", p.Uye, MSqlDbType.Bit);
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
        /// Makale Data Update
        ///</summary>
        public static int Update(Makale p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "UPDATE Makale SET HesapID=@HesapID,KategoriID=@KategoriID,ResimUrl=@ResimUrl,Baslik=@Baslik,Ozet=@Ozet,Icerik=@Icerik,Etiket=@Etiket,KayitTarihi=@KayitTarihi,Durum=@Durum,Uye=@Uye,Yorum=@Yorum,Aktif=@Aktif WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("HesapID", p.HesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("KategoriID", p.KategoriID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ResimUrl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Baslik", p.Baslik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Ozet", BAYMYO.UI.Commons.SubStringText(p.Ozet, 250), MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Icerik", p.Icerik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Etiket", p.Etiket, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("KayitTarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("Durum", p.Durum, MSqlDbType.Byte);
                    cmd.Parameters.Add("Uye", p.Uye, MSqlDbType.Bit);
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
        /// Makale Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Makale WHERE ID=@ID", conneciton))
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
        /// Makale Data Delete
        ///</summary>
        public static int Delete(Makale p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Makale WHERE ID=@ID", conneciton))
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
