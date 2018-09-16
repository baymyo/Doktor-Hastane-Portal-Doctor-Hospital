using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace Lib
{
    public partial class YorumCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Yorum this[int index]
        {
            get { return (Yorum)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Yorum obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Yorum obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Yorum obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Yorum obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Yorum obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Yorum : IDisposable
    {
        #region ---IDisposable Members---
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ---Properties/Field - Özellikler Alanlar---
        private System.Guid m_ID;
        public System.Guid ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        private string m_IP;
        public string IP
        {
            get { return m_IP; }
            set { m_IP = value; }
        }
        private string m_ModulID;
        public string ModulID
        {
            get { return m_ModulID; }
            set { m_ModulID = value; }
        }
        private string m_IcerikID;
        public string IcerikID
        {
            get { return m_IcerikID; }
            set { m_IcerikID = value; }
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
        private string m_Icerik;
        public string Icerik
        {
            get { return m_Icerik; }
            set { m_Icerik = value; }
        }
        private DateTime m_KayitTarihi;
        public DateTime KayitTarihi
        {
            get { return m_KayitTarihi; }
            set { m_KayitTarihi = value; }
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

        public Yorum()
        {
        }

        /// <summary>
        /// Yorum Nesnesi Oluþtur
        /// </summary>
        public Yorum(System.Guid pID, string pIP, string pModulID, string pIcerikID, string pAdi, string pMail, string pIcerik, DateTime pKayitTarihi, bool pYoneticiOnay, bool pAktif)
        {
            this.m_ID = pID;
            this.m_IP = pIP;
            this.m_ModulID = pModulID;
            this.m_IcerikID = pIcerikID;
            this.m_Adi = pAdi;
            this.m_Mail = pMail;
            this.m_Icerik = pIcerik;
            this.m_KayitTarihi = pKayitTarihi;
            this.m_YoneticiOnay = pYoneticiOnay;
            this.m_Aktif = pAktif;
        }
    }

    public partial class YorumMethods
    {
        ///<summary>
        /// Yorum Data PrimaryKey
        ///</summary>
        public static Yorum GetYorum(System.Guid pID)
        {
            Yorum rvYorum = new Yorum();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT * FROM Yorum WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.UniqueIdentifier);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvYorum = new Yorum(MConvert.NullToGuid(IDR["ID"]), MConvert.NullToString(IDR["IP"]), MConvert.NullToString(IDR["ModulID"]), MConvert.NullToString(IDR["IcerikID"]), MConvert.NullToString(IDR["Adi"]), MConvert.NullToString(IDR["Mail"]), MConvert.NullToString(IDR["Icerik"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToBool(IDR["YoneticiOnay"]), MConvert.NullToBool(IDR["Aktif"]));
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
            return rvYorum;
        }

        public static Int64 GetCount(bool pYoneticiOnay)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT COUNT(ID) AS TotalCount FROM Yorum WHERE YoneticiOnay=@YoneticiOnay", conneciton))
                {
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
        public static Int64 GetCount(string pMail, bool pAktif)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT COUNT(ID) AS TotalCount FROM Yorum WHERE Mail=@Mail AND Aktif=@Aktif", conneciton))
                {
                    cmd.Parameters.Add("Mail", pMail, MSqlDbType.VarChar);
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
        public static Int64 GetCount(Guid pHesapID, string pUrl, bool pYoneticiOnay, bool pAktif)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT COUNT(ID) AS TotalCount FROM Yorum WHERE (YoneticiOnay=@YoneticiOnay AND Aktif=@Aktif) AND (EXISTS(SELECT ID FROM Makale WHERE HesapID=@HesapID) OR EXISTS(SELECT ID FROM Mesaj WHERE HesapID=@HesapID) OR EXISTS(SELECT ID FROM Video WHERE HesapID=@HesapID) OR IcerikID=@Url)", conneciton))
                {
                    cmd.Parameters.Add("HesapID", pHesapID, BAYMYO.MultiSQLClient.MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("Url", pUrl, BAYMYO.MultiSQLClient.MSqlDbType.VarChar);
                    cmd.Parameters.Add("YoneticiOnay", pYoneticiOnay, MSqlDbType.Bit);
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
        /// Yorum Data Insert
        ///</summary>
        public static int Insert(Yorum p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "INSERT INTO Yorum (IP,ModulID,IcerikID,Adi,Mail,Icerik,KayitTarihi,YoneticiOnay,Aktif) VALUES(@IP,@ModulID,@IcerikID,@Adi,@Mail,@Icerik,@KayitTarihi,@YoneticiOnay,@Aktif)", conneciton))
                {
                    cmd.Parameters.Add("IP", p.IP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ModulID", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("IcerikID", p.IcerikID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Adi", p.Adi, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Mail", p.Mail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Icerik", p.Icerik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("KayitTarihi", p.KayitTarihi, MSqlDbType.DateTime);
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
        /// Yorum Data Update
        ///</summary>
        public static int Update(Yorum p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "UPDATE Yorum SET IP=@IP,ModulID=@ModulID,IcerikID=@IcerikID,Adi=@Adi,Mail=@Mail,Icerik=@Icerik,KayitTarihi=@KayitTarihi,YoneticiOnay=@YoneticiOnay,Aktif=@Aktif WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("IP", p.IP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ModulID", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("IcerikID", p.IcerikID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Adi", p.Adi, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Mail", p.Mail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Icerik", p.Icerik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("KayitTarihi", p.KayitTarihi, MSqlDbType.DateTime);
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
        /// Yorum Data Delete
        ///</summary>
        public static int Delete(System.Guid pID)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Yorum WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.UniqueIdentifier);
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
        /// Yorum Data Delete
        ///</summary>
        public static int Delete(Yorum p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Yorum WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.UniqueIdentifier);
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

        public static int Delete(string modulID, string icerikID)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Yorum WHERE ModulID=@ModulID AND IcerikID=@IcerikID", conneciton))
                {
                    cmd.Parameters.Add("ModulID", modulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("IcerikID", icerikID, MSqlDbType.VarChar);
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
