using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace Lib
{
    public partial class GosterimCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Gosterim this[int index]
        {
            get { return (Gosterim)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Gosterim obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Gosterim obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Gosterim obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Gosterim obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Gosterim obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Gosterim : IDisposable
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
        private DateTime m_KayitTarihi;
        public DateTime KayitTarihi
        {
            get { return m_KayitTarihi; }
            set { m_KayitTarihi = value; }
        }
        #endregion

        public Gosterim()
        {
        }

        /// <summary>
        /// Gosterim Nesnesi Oluþtur
        /// </summary>
        public Gosterim(System.Guid pID, System.Guid pHesapID, string pIP, string pModulID, string pIcerikID, DateTime pKayitTarihi)
        {
            this.m_ID = pID;
            this.m_HesapID = pHesapID;
            this.m_IP = pIP;
            this.m_ModulID = pModulID;
            this.m_IcerikID = pIcerikID;
            this.m_KayitTarihi = pKayitTarihi;
        }
    }

    public partial class GosterimMethods
    {
        ///<summary>
        /// Gosterim Data PrimaryKey
        ///</summary>
        public static Gosterim GetGosterim(System.Guid pID)
        {
            Gosterim rvGosterim = new Gosterim();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT * FROM Gosterim WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.UniqueIdentifier);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvGosterim = new Gosterim(MConvert.NullToGuid(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["IP"]), MConvert.NullToString(IDR["ModulID"]), MConvert.NullToString(IDR["IcerikID"]), MConvert.NullToDateTime(IDR["KayitTarihi"]));
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
            return rvGosterim;
        }

        ///<summary>
        /// Gosterim Data Count
        ///</summary>
        public static Int64 GetCount(string modulID, string id)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT COUNT(DISTINCT IP) AS TotalView FROM Gosterim WHERE ModulID=@ModulID AND IcerikID=@IcerikID", conneciton))
                {
                    cmd.Parameters.Add("ModulID", modulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("IcerikID", id, MSqlDbType.VarChar);
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
        /// Gosterim Data Insert
        ///</summary>
        public static int Insert(Gosterim p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "INSERT INTO Gosterim (HesapID,IP,ModulID,IcerikID,KayitTarihi) VALUES(@HesapID,@IP,@ModulID,@IcerikID,@KayitTarihi)", conneciton))
                {
                    cmd.Parameters.Add("HesapID", p.HesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("IP", p.IP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ModulID", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("IcerikID", p.IcerikID, MSqlDbType.VarChar);
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
        /// Gosterim Data Update
        ///</summary>
        public static int Update(Gosterim p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "UPDATE Gosterim SET HesapID=@HesapID,IP=@IP,ModulID=@ModulID,IcerikID=@IcerikID,KayitTarihi=@KayitTarihi WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("HesapID", p.HesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("IP", p.IP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ModulID", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("IcerikID", p.IcerikID, MSqlDbType.VarChar);
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
        /// Gosterim Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Gosterim WHERE ID=@ID", conneciton))
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
        /// Gosterim Data Delete
        ///</summary>
        public static int Delete(Gosterim p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Gosterim WHERE ID=@ID", conneciton))
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