using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace Lib
{
    public partial class HaberCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Haber this[int index]
        {
            get { return (Haber)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Haber obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Haber obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Haber obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Haber obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Haber obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Haber : IDisposable
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

        public Haber()
        {
        }

        /// <summary>
        /// Haber Nesnesi Oluþtur
        /// </summary>
        public Haber(Int64 pID, System.Guid pHesapID, string pKategoriID, string pResimUrl, string pBaslik, string pOzet, string pIcerik, string pEtiket, DateTime pKayitTarihi, bool pUye, bool pYorum, bool pAktif)
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
            this.m_Uye = pUye;
            this.m_Yorum = pYorum;
            this.m_Aktif = pAktif;
        }
    }

    public partial class HaberMethods
    {
        ///<summary>
        /// Haber Data PrimaryKey
        ///</summary>
        public static Haber GetHaber(Int64 pID)
        {
            Haber rvHaber = new Haber();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT * FROM Haber WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.BigInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvHaber = new Haber(MConvert.NullToInt64(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["KategoriID"]), MConvert.NullToString(IDR["ResimUrl"]), MConvert.NullToString(IDR["Baslik"]), MConvert.NullToString(IDR["Ozet"]), MConvert.NullToString(IDR["Icerik"]), MConvert.NullToString(IDR["Etiket"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToBool(IDR["Uye"]), MConvert.NullToBool(IDR["Yorum"]), MConvert.NullToBool(IDR["Aktif"]));
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
            return rvHaber;
        }


        /// <summary>
        /// Haber Getir
        /// </summary>
        public static Haber GetHaber(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            Haber rvHaber = new Haber();
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
                            rvHaber = new Haber(MConvert.NullToInt64(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["KategoriID"]), MConvert.NullToString(IDR["ResimUrl"]), MConvert.NullToString(IDR["Baslik"]), MConvert.NullToString(IDR["Ozet"]), MConvert.NullToString(IDR["Icerik"]), MConvert.NullToString(IDR["Etiket"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToBool(IDR["Uye"]), MConvert.NullToBool(IDR["Yorum"]), MConvert.NullToBool(IDR["Aktif"]));
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
            return rvHaber;
        }

        /// <summary>
        /// Haber Liste Getir
        /// </summary>
        public static HaberCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            HaberCollection rvHaber = new HaberCollection();
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
                            rvHaber.Add(new Haber(MConvert.NullToInt64(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["KategoriID"]), MConvert.NullToString(IDR["ResimUrl"]), MConvert.NullToString(IDR["Baslik"]), MConvert.NullToString(IDR["Ozet"]), MConvert.NullToString(IDR["Icerik"]), MConvert.NullToString(IDR["Etiket"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToBool(IDR["Uye"]), MConvert.NullToBool(IDR["Yorum"]), MConvert.NullToBool(IDR["Aktif"])));
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
            return rvHaber;
        }

        ///<summary>
        /// Haber Data Select
        ///</summary>
        public static HaberCollection GetSelect(System.Guid pHesapID)
        {
            HaberCollection rvHaber = new HaberCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT * FROM Haber WHERE HesapID=@HesapID", conneciton))
                {
                    cmd.Parameters.Add("HesapID", pHesapID, MSqlDbType.UniqueIdentifier);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvHaber.Add(new Haber(MConvert.NullToInt64(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["KategoriID"]), MConvert.NullToString(IDR["ResimUrl"]), MConvert.NullToString(IDR["Baslik"]), MConvert.NullToString(IDR["Ozet"]), MConvert.NullToString(IDR["Icerik"]), MConvert.NullToString(IDR["Etiket"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToBool(IDR["Uye"]), MConvert.NullToBool(IDR["Yorum"]), MConvert.NullToBool(IDR["Aktif"])));
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
            return rvHaber;
        }


        ///<summary>
        /// Haber Data Insert
        ///</summary>
        public static int Insert(System.Guid pHesapID, string pKategoriID, string pResimUrl, string pBaslik, string pOzet, string pIcerik, string pEtiket, DateTime pKayitTarihi, bool pUye, bool pYorum, bool pAktif)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "INSERT INTO Haber (HesapID,KategoriID,ResimUrl,Baslik,Ozet,Icerik,Etiket,KayitTarihi,Uye,Yorum,Aktif) VALUES(@HesapID,@KategoriID,@ResimUrl,@Baslik,@Ozet,@Icerik,@Etiket,@KayitTarihi,@Uye,@Yorum,@Aktif)", conneciton))
                {
                    cmd.Parameters.Add("HesapID", pHesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("KategoriID", pKategoriID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ResimUrl", pResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Baslik", pBaslik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Ozet", pOzet, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Icerik", pIcerik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Etiket", pEtiket, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("KayitTarihi", pKayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("Uye", pUye, MSqlDbType.Bit);
                    cmd.Parameters.Add("Yorum", pYorum, MSqlDbType.Bit);
                    cmd.Parameters.Add("Aktif", pAktif, MSqlDbType.Bit);
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
        /// Haber Data Insert
        ///</summary>
        public static int Insert(Haber p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "INSERT INTO Haber (HesapID,KategoriID,ResimUrl,Baslik,Ozet,Icerik,Etiket,KayitTarihi,Uye,Yorum,Aktif) VALUES(@HesapID,@KategoriID,@ResimUrl,@Baslik,@Ozet,@Icerik,@Etiket,@KayitTarihi,@Uye,@Yorum,@Aktif)", conneciton))
                {
                    cmd.Parameters.Add("HesapID", p.HesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("KategoriID", p.KategoriID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ResimUrl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Baslik", p.Baslik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Ozet", p.Ozet, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Icerik", p.Icerik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Etiket", p.Etiket, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("KayitTarihi", p.KayitTarihi, MSqlDbType.DateTime);
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
        /// Haber Data Update
        ///</summary>
        public static int Update(Int64 pID, System.Guid pHesapID, string pKategoriID, string pResimUrl, string pBaslik, string pOzet, string pIcerik, string pEtiket, DateTime pKayitTarihi, bool pUye, bool pYorum, bool pAktif)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "UPDATE Haber SET HesapID=@HesapID,KategoriID=@KategoriID,ResimUrl=@ResimUrl,Baslik=@Baslik,Ozet=@Ozet,Icerik=@Icerik,Etiket=@Etiket,KayitTarihi=@KayitTarihi,Uye=@Uye,Yorum=@Yorum,Aktif=@Aktif WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("HesapID", pHesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("KategoriID", pKategoriID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ResimUrl", pResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Baslik", pBaslik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Ozet", pOzet, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Icerik", pIcerik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Etiket", pEtiket, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("KayitTarihi", pKayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("Uye", pUye, MSqlDbType.Bit);
                    cmd.Parameters.Add("Yorum", pYorum, MSqlDbType.Bit);
                    cmd.Parameters.Add("Aktif", pAktif, MSqlDbType.Bit);
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
        /// Haber Data Update
        ///</summary>
        public static int Update(Haber p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "UPDATE Haber SET HesapID=@HesapID,KategoriID=@KategoriID,ResimUrl=@ResimUrl,Baslik=@Baslik,Ozet=@Ozet,Icerik=@Icerik,Etiket=@Etiket,KayitTarihi=@KayitTarihi,Uye=@Uye,Yorum=@Yorum,Aktif=@Aktif WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("HesapID", p.HesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("KategoriID", p.KategoriID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ResimUrl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Baslik", p.Baslik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Ozet", p.Ozet, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Icerik", p.Icerik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Etiket", p.Etiket, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("KayitTarihi", p.KayitTarihi, MSqlDbType.DateTime);
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
        /// Haber Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Haber WHERE ID=@ID", conneciton))
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
        /// Haber Data Delete
        ///</summary>
        public static int Delete(Haber p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Haber WHERE ID=@ID", conneciton))
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
