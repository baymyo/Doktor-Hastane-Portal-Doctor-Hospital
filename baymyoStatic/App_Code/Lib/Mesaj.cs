using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace Lib
{
    public partial class MesajCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Mesaj this[int index]
        {
            get { return (Mesaj)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Mesaj obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Mesaj obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Mesaj obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Mesaj obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Mesaj obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Mesaj : IDisposable
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
        private string m_IP;
        public string IP
        {
            get { return m_IP; }
            set { m_IP = value; }
        }
        private string m_Adi;
        public string Adi
        {
            get { return m_Adi; }
            set { m_Adi = value; }
        }
        private string m_Mail;
        public string Mail
        {
            get { return m_Mail; }
            set { m_Mail = value; }
        }
        private string m_Telefon;
        public string Telefon
        {
            get { return m_Telefon; }
            set { m_Telefon = value; }
        }
        private string m_Konu;
        public string Konu
        {
            get { return m_Konu; }
            set { m_Konu = value; }
        }
        private string m_Icerik;
        public string Icerik
        {
            get { return m_Icerik; }
            set { m_Icerik = value; }
        }
        private string m_Yanit;
        public string Yanit
        {
            get { return m_Yanit; }
            set { m_Yanit = value; }
        }
        private DateTime m_KayitTarihi;
        public DateTime KayitTarihi
        {
            get { return m_KayitTarihi; }
            set { m_KayitTarihi = value; }
        }
        private DateTime m_GuncellemeTarihi;
        public DateTime GuncellemeTarihi
        {
            get { return m_GuncellemeTarihi; }
            set { m_GuncellemeTarihi = value; }
        }
        private byte m_Durum;
        public byte Durum
        {
            get { return m_Durum; }
            set { m_Durum = value; }
        }
        private bool m_YoneticiOnay;
        public bool YoneticiOnay
        {
            get { return m_YoneticiOnay; }
            set { m_YoneticiOnay = value; }
        }
        private bool m_Aktif;
        public bool Aktif
        {
            get { return m_Aktif; }
            set { m_Aktif = value; }
        }

        #endregion

        public Mesaj()
        {
        }

        /// <summary>
        /// Mesaj Nesnesi Oluþtur
        /// </summary>
        public Mesaj(Int64 pID, System.Guid pHesapID, string pIP, string pAdi, string pMail, string pTelefon, string pKonu, string pIcerik, string pYanit, DateTime pKayitTarihi, DateTime pGuncellemeTarihi, byte pDurum, bool pYoneticiOnay, bool pAktif)
        {
            this.m_ID = pID;
            this.m_HesapID = pHesapID;
            this.m_IP = pIP;
            this.m_Adi = pAdi;
            this.m_Mail = pMail;
            this.m_Telefon = pTelefon;
            this.m_Konu = pKonu;
            this.m_Icerik = pIcerik;
            this.m_Yanit = pYanit;
            this.m_KayitTarihi = pKayitTarihi;
            this.m_GuncellemeTarihi = pGuncellemeTarihi;
            this.m_Durum = pDurum;
            this.m_YoneticiOnay = pYoneticiOnay;
            this.m_Aktif = pAktif;
        }
    }

    public partial class MesajMethods
    {
        ///<summary>
        /// Mesaj Data PrimaryKey
        ///</summary>
        public static Mesaj GetMesaj(Int64 pID)
        {
            Mesaj rvMesaj = new Mesaj();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT TOP(1) * FROM Mesaj WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.BigInt);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvMesaj = new Mesaj(MConvert.NullToInt64(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["IP"]), MConvert.NullToString(IDR["Adi"]), MConvert.NullToString(IDR["Mail"]), MConvert.NullToString(IDR["Telefon"]), MConvert.NullToString(IDR["Konu"]), MConvert.NullToString(IDR["Icerik"]), MConvert.NullToString(IDR["Yanit"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToDateTime(IDR["GuncellemeTarihi"]), MConvert.NullToByte(IDR["Durum"]), MConvert.NullToBool(IDR["YoneticiOnay"]), MConvert.NullToBool(IDR["Aktif"]));
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
            return rvMesaj;
        }

        ///<summary>
        /// Mesaj Data Select
        ///</summary>
        public static MesajCollection GetSelect(System.Guid pHesapID)
        {
            MesajCollection rvMesaj = new MesajCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT * FROM Mesaj WHERE HesapID=@HesapID", conneciton))
                {
                    cmd.Parameters.Add("HesapID", pHesapID, MSqlDbType.UniqueIdentifier);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvMesaj.Add(new Mesaj(MConvert.NullToInt64(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["IP"]), MConvert.NullToString(IDR["Adi"]), MConvert.NullToString(IDR["Mail"]), MConvert.NullToString(IDR["Telefon"]), MConvert.NullToString(IDR["Konu"]), MConvert.NullToString(IDR["Icerik"]), MConvert.NullToString(IDR["Yanit"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToDateTime(IDR["GuncellemeTarihi"]), MConvert.NullToByte(IDR["Durum"]), MConvert.NullToBool(IDR["YoneticiOnay"]), MConvert.NullToBool(IDR["Aktif"])));
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
            return rvMesaj;
        }

        public static Int64 GetCount(byte pDurum, bool pYoneticiOnay)
        {
            Int64 rvCount = 0;
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT COUNT(ID) AS TotalCount FROM Mesaj WHERE Durum=@Durum AND YoneticiOnay=@YoneticiOnay", conneciton))
                {
                    cmd.Parameters.Add("Durum", pDurum, MSqlDbType.Byte);
                    cmd.Parameters.Add("YoneticiOnay", pYoneticiOnay, MSqlDbType.Bit);
                    rvCount = MConvert.NullToInt64(cmd.ExecuteScalar());
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rvCount;
        }
        public static Int64 GetCount(Guid pHesapID, byte pDurum, bool pAktif)
        {
            Int64 rvCount = 0;
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT COUNT(ID) AS TotalCount FROM Mesaj WHERE YoneticiOnay=1 AND HesapID=@HesapID AND Durum=@Durum AND Aktif=@Aktif", conneciton))
                {
                    cmd.Parameters.Add("HesapID", pHesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("Durum", pDurum, MSqlDbType.Byte);
                    cmd.Parameters.Add("Aktif", pAktif, MSqlDbType.Bit);
                    rvCount = MConvert.NullToInt64(cmd.ExecuteScalar());
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rvCount;
        }
        public static Int64 GetCount(string pMail, byte pDurum, bool pAktif)
        {
            Int64 rvCount = 0;
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT COUNT(ID) AS TotalCount FROM Mesaj WHERE Mail=@Mail AND Durum=@Durum AND Aktif=@Aktif", conneciton))
                {
                    cmd.Parameters.Add("Mail", pMail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Durum", pDurum, MSqlDbType.Byte);
                    cmd.Parameters.Add("Aktif", pAktif, MSqlDbType.Bit);
                    rvCount = MConvert.NullToInt64(cmd.ExecuteScalar());
                }
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Open:
                        conneciton.Close();
                        break;
                }
            }
            return rvCount;
        }

        ///<summary>
        /// Mesaj Data Insert
        ///</summary>
        public static int Insert(System.Guid pHesapID, string pIP, string pAdi, string pMail, string pTelefon, string pKonu, string pIcerik, string pYanit, DateTime pKayitTarihi, DateTime pGuncellemeTarihi, byte pDurum, bool pYoneticiOnay, bool pAktif)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "INSERT INTO Mesaj (HesapID,IP,Adi,Mail,Telefon,Konu,Icerik,Yanit,KayitTarihi,GuncellemeTarihi,Durum,YoneticiOnay,Aktif) VALUES(@HesapID,@IP,@Adi,@Mail,@Telefon,@Konu,@Icerik,@Yanit,@KayitTarihi,@GuncellemeTarihi,@Durum,@YoneticiOnay,@Aktif)", conneciton))
                {
                    cmd.Parameters.Add("HesapID", pHesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("IP", pIP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Adi", pAdi, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Mail", pMail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Telefon", pTelefon, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Konu", pKonu, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Icerik", pIcerik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Yanit", pYanit, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("KayitTarihi", pKayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("GuncellemeTarihi", pGuncellemeTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("Durum", pDurum, MSqlDbType.TinyInt);
                    cmd.Parameters.Add("YoneticiOnay", pYoneticiOnay, MSqlDbType.Bit);
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
        /// Mesaj Data Insert
        ///</summary>
        public static int Insert(Mesaj p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "INSERT INTO Mesaj (HesapID,IP,Adi,Mail,Telefon,Konu,Icerik,Yanit,KayitTarihi,GuncellemeTarihi,Durum,YoneticiOnay,Aktif) VALUES(@HesapID,@IP,@Adi,@Mail,@Telefon,@Konu,@Icerik,@Yanit,@KayitTarihi,@GuncellemeTarihi,@Durum,@YoneticiOnay,@Aktif)", conneciton))
                {
                    cmd.Parameters.Add("HesapID", p.HesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("IP", p.IP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Adi", p.Adi, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Mail", p.Mail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Telefon", p.Telefon, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Konu", p.Konu, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Icerik", p.Icerik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Yanit", p.Yanit, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("KayitTarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("GuncellemeTarihi", p.GuncellemeTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("Durum", p.Durum, MSqlDbType.TinyInt);
                    cmd.Parameters.Add("YoneticiOnay", p.YoneticiOnay, MSqlDbType.Bit);
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
        /// Mesaj Data Update
        ///</summary>
        public static int Update(Int64 pID, System.Guid pHesapID, string pIP, string pAdi, string pMail, string pTelefon, string pKonu, string pIcerik, string pYanit, DateTime pKayitTarihi, DateTime pGuncellemeTarihi, byte pDurum, bool pYoneticiOnay, bool pAktif)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "UPDATE Mesaj SET HesapID=@HesapID,IP=@IP,Adi=@Adi,Mail=@Mail,Telefon=@Telefon,Konu=@Konu,Icerik=@Icerik,Yanit=@Yanit,KayitTarihi=@KayitTarihi,GuncellemeTarihi=@GuncellemeTarihi,Durum=@Durum,YoneticiOnay=@YoneticiOnay,Aktif=@Aktif WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("HesapID", pHesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("IP", pIP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Adi", pAdi, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Mail", pMail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Telefon", pTelefon, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Konu", pKonu, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Icerik", pIcerik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Yanit", pYanit, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("KayitTarihi", pKayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("GuncellemeTarihi", pGuncellemeTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("Durum", pDurum, MSqlDbType.TinyInt);
                    cmd.Parameters.Add("YoneticiOnay", pYoneticiOnay, MSqlDbType.Bit);
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
        /// Mesaj Data Update
        ///</summary>
        public static int Update(Mesaj p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "UPDATE Mesaj SET HesapID=@HesapID,IP=@IP,Adi=@Adi,Mail=@Mail,Telefon=@Telefon,Konu=@Konu,Icerik=@Icerik,Yanit=@Yanit,KayitTarihi=@KayitTarihi,GuncellemeTarihi=@GuncellemeTarihi,Durum=@Durum,YoneticiOnay=@YoneticiOnay,Aktif=@Aktif WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.BigInt);
                    cmd.Parameters.Add("HesapID", p.HesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("IP", p.IP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Adi", p.Adi, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Mail", p.Mail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Telefon", p.Telefon, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Konu", p.Konu, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Icerik", p.Icerik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Yanit", p.Yanit, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("KayitTarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("GuncellemeTarihi", p.GuncellemeTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("Durum", p.Durum, MSqlDbType.TinyInt);
                    cmd.Parameters.Add("YoneticiOnay", p.YoneticiOnay, MSqlDbType.Bit);
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
        /// Mesaj Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Mesaj WHERE ID=@ID", conneciton))
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
        /// Mesaj Data Delete
        ///</summary>
        public static int Delete(Mesaj p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Mesaj WHERE ID=@ID", conneciton))
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
