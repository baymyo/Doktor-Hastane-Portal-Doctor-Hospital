using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace Lib
{
    public partial class BegenCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Begen this[int index]
        {
            get { return (Begen)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Begen obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Begen obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Begen obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Begen obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Begen obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Begen : IDisposable
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
        private bool m_Durum;
        public bool Durum
        {
            get { return m_Durum; }
            set { m_Durum = value; }
        }
        private DateTime m_KayitTarihi;
        public DateTime KayitTarihi
        {
            get { return m_KayitTarihi; }
            set { m_KayitTarihi = value; }
        }

        #endregion

        public Begen()
        {
        }

        /// <summary>
        /// Begen Nesnesi Oluþtur
        /// </summary>
        public Begen(System.Guid pID, System.Guid pHesapID, string pIP, string pModulID, string pIcerikID, bool pDurum, DateTime pKayitTarihi)
        {
            this.m_ID = pID;
            this.m_HesapID = pHesapID;
            this.m_IP = pIP;
            this.m_ModulID = pModulID;
            this.m_IcerikID = pIcerikID;
            this.m_Durum = pDurum;
            this.m_KayitTarihi = pKayitTarihi;
        }
    }

    public partial class BegenMethods
    {
        ///<summary>
        /// Begen Data PrimaryKey
        ///</summary>
        public static Begen GetBegen(System.Guid pID)
        {
            Begen rvBegen = new Begen();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT * FROM Begen WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.UniqueIdentifier);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvBegen = new Begen(MConvert.NullToGuid(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["IP"]), MConvert.NullToString(IDR["ModulID"]), MConvert.NullToString(IDR["IcerikID"]), MConvert.NullToBool(IDR["Durum"]), MConvert.NullToDateTime(IDR["KayitTarihi"]));
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
            return rvBegen;
        }

        ///<summary>
        /// Begen Data Insert
        ///</summary>
        public static int Insert(Begen p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "INSERT INTO Begen (HesapID,IP,ModulID,IcerikID,Durum,KayitTarihi) VALUES(@HesapID,@IP,@ModulID,@IcerikID,@Durum,@KayitTarihi)", conneciton))
                {
                    cmd.Parameters.Add("HesapID", p.HesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("IP", p.IP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ModulID", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("IcerikID", p.IcerikID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Durum", p.Durum, MSqlDbType.Bit);
                    cmd.Parameters.Add("KayitTarihi", p.KayitTarihi, MSqlDbType.DateTime);
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
        /// Begen Data Update
        ///</summary>
        public static int Update(Begen p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "UPDATE Begen SET HesapID=@HesapID,IP=@IP,ModulID=@ModulID,IcerikID=@IcerikID,Durum=@Durum,KayitTarihi=@KayitTarihi WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("HesapID", p.HesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("IP", p.IP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ModulID", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("IcerikID", p.IcerikID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Durum", p.Durum, MSqlDbType.Bit);
                    cmd.Parameters.Add("KayitTarihi", p.KayitTarihi, MSqlDbType.DateTime);
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
        /// Begen Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Begen WHERE ID=@ID", conneciton))
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
        /// Begen Data Delete
        ///</summary>
        public static int Delete(Begen p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Begen WHERE ID=@ID", conneciton))
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
    }
}
