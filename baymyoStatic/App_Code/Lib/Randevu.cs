using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace Lib
{
    public partial class RandevuCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Randevu this[int index]
        {
            get { return (Randevu)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Randevu obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Randevu obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Randevu obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Randevu obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Randevu obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Randevu : IDisposable
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
        private System.Guid m_HesapID;
        public System.Guid HesapID
        {
            get { return m_HesapID; }
            set { m_HesapID = value; }
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
        private string m_Telefon;
        public string Telefon
        {
            get { return m_Telefon; }
            set { m_Telefon = value; }
        }
        private string m_GSM;
        public string GSM
        {
            get { return m_GSM; }
            set { m_GSM = value; }
        }
        private string m_Icerik;
        public string Icerik
        {
            get { return m_Icerik; }
            set { m_Icerik = value; }
        }
        private DateTime m_TarihSaat;
        public DateTime TarihSaat
        {
            get { return m_TarihSaat; }
            set { m_TarihSaat = value; }
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

        #endregion

        public Randevu()
        {
        }

        /// <summary>
        /// Randevu Nesnesi Oluþtur
        /// </summary>
        public Randevu(System.Guid pID, System.Guid pHesapID, string pModulID, string pIcerikID, string pAdi, string pMail, string pTelefon, string pGSM, string pIcerik, DateTime pTarihSaat, byte pDurum, bool pYoneticiOnay)
        {
            this.m_ID = pID;
            this.m_HesapID = pHesapID;
            this.m_ModulID = pModulID;
            this.m_IcerikID = pIcerikID;
            this.m_Adi = pAdi;
            this.m_Mail = pMail;
            this.m_Telefon = pTelefon;
            this.m_GSM = pGSM;
            this.m_Icerik = pIcerik;
            this.m_TarihSaat = pTarihSaat;
            this.m_Durum = pDurum;
            this.m_YoneticiOnay = pYoneticiOnay;
        }
    }

    public partial class RandevuMethods
    {
        ///<summary>
        /// Randevu Data PrimaryKey
        ///</summary>
        public static Randevu GetRandevu(System.Guid pID)
        {
            Randevu rvRandevu = new Randevu();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT TOP(1) * FROM Randevu WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.UniqueIdentifier);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvRandevu = new Randevu(MConvert.NullToGuid(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["ModulID"]), MConvert.NullToString(IDR["IcerikID"]), MConvert.NullToString(IDR["Adi"]), MConvert.NullToString(IDR["Mail"]), MConvert.NullToString(IDR["Telefon"]), MConvert.NullToString(IDR["GSM"]), MConvert.NullToString(IDR["Icerik"]), MConvert.NullToDateTime(IDR["TarihSaat"]), MConvert.NullToByte(IDR["Durum"]), MConvert.NullToBool(IDR["YoneticiOnay"]));
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
            return rvRandevu;
        }

        public static Int64 GetCount(string pMail, byte pDurum)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT COUNT(ID) AS TotalCount FROM Randevu WHERE Mail=@Mail AND Durum=@Durum", conneciton))
                {
                    cmd.Parameters.Add("Mail", pMail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Durum", pDurum, MSqlDbType.TinyInt);
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
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT COUNT(ID) AS TotalCount FROM Randevu WHERE YoneticiOnay=@YoneticiOnay AND Durum=@Durum", conneciton))
                {
                    cmd.Parameters.Add("Durum", pDurum, MSqlDbType.TinyInt);
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
        public static Int64 GetCount(Guid pHesapID, byte pDurum, bool pYoneticiOnay)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT COUNT(ID) AS TotalCount FROM Randevu WHERE HesapID=@HesapID AND YoneticiOnay=@YoneticiOnay AND Durum=@Durum", conneciton))
                {
                    cmd.Parameters.Add("HesapID", pHesapID, BAYMYO.MultiSQLClient.MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("Durum", pDurum, MSqlDbType.TinyInt);
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

        ///<summary>
        /// Randevu Data Insert
        ///</summary>
        public static int Insert(Randevu p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "INSERT INTO Randevu (HesapID,ModulID,IcerikID,Adi,Mail,Telefon,GSM,Icerik,TarihSaat,Durum,YoneticiOnay) VALUES(@HesapID,@ModulID,@IcerikID,@Adi,@Mail,@Telefon,@GSM,@Icerik,@TarihSaat,@Durum,@YoneticiOnay)", conneciton))
                {
                    cmd.Parameters.Add("HesapID", p.HesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("ModulID", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("IcerikID", p.IcerikID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Adi", p.Adi, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Mail", p.Mail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Telefon", p.Telefon, MSqlDbType.VarChar);
                    cmd.Parameters.Add("GSM", p.GSM, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Icerik", p.Icerik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("TarihSaat", p.TarihSaat, MSqlDbType.DateTime);
                    cmd.Parameters.Add("Durum", p.Durum, MSqlDbType.TinyInt);
                    cmd.Parameters.Add("YoneticiOnay", p.YoneticiOnay, MSqlDbType.Bit);
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
        /// Randevu Data Update
        ///</summary>
        public static int Update(Randevu p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "UPDATE Randevu SET HesapID=@HesapID,ModulID=@ModulID,IcerikID=@IcerikID,Adi=@Adi,Mail=@Mail,Telefon=@Telefon,GSM=@GSM,Icerik=@Icerik,TarihSaat=@TarihSaat,Durum=@Durum,YoneticiOnay=@YoneticiOnay WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("HesapID", p.HesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("ModulID", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("IcerikID", p.IcerikID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Adi", p.Adi, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Mail", p.Mail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Telefon", p.Telefon, MSqlDbType.VarChar);
                    cmd.Parameters.Add("GSM", p.GSM, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Icerik", p.Icerik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("TarihSaat", p.TarihSaat, MSqlDbType.DateTime);
                    cmd.Parameters.Add("Durum", p.Durum, MSqlDbType.TinyInt);
                    cmd.Parameters.Add("YoneticiOnay", p.YoneticiOnay, MSqlDbType.Bit);
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
        /// Randevu Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Randevu WHERE ID=@ID", conneciton))
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
        /// Randevu Data Delete
        ///</summary>
        public static int Delete(Randevu p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Randevu WHERE ID=@ID", conneciton))
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Randevu WHERE ModulID=@ModulID AND IcerikID=@IcerikID", conneciton))
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
