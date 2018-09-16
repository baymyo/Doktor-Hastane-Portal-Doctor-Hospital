using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace Lib
{
    public partial class MansetCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Manset this[int index]
        {
            get { return (Manset)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Manset obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Manset obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Manset obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Manset obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Manset obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Manset : IDisposable
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
        private string m_ResimKucuk;
        public string ResimKucuk
        {
            get { return m_ResimKucuk; }
            set { m_ResimKucuk = value; }
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
        private string m_Aciklama;
        public string Aciklama
        {
            get { return m_Aciklama; }
            set { m_Aciklama = value; }
        }
        private DateTime m_KayitTarihi;
        public DateTime KayitTarihi
        {
            get { return m_KayitTarihi; }
            set { m_KayitTarihi = value; }
        }
        private bool m_Aktif;
        public bool Aktif
        {
            get { return m_Aktif; }
            set { m_Aktif = value; }
        }

        #endregion

        public Manset()
        {
        }

        /// <summary>
        /// Manset Nesnesi Oluþtur
        /// </summary>
        public Manset(System.Guid pID, string pModulID, string pIcerikID, string pResimKucuk, string pResimUrl, string pBaslik, string pAciklama, DateTime pKayitTarihi, bool pAktif)
        {
            this.m_ID = pID;
            this.m_ModulID = pModulID;
            this.m_IcerikID = pIcerikID;
            this.m_ResimKucuk = pResimKucuk;
            this.m_ResimUrl = pResimUrl;
            this.m_Baslik = pBaslik;
            this.m_Aciklama = pAciklama;
            this.m_KayitTarihi = pKayitTarihi;
            this.m_Aktif = pAktif;
        }
    }

    public partial class MansetJSON : IDisposable
    {
        #region ---IDisposable Members---
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ---Properties/Field - Özellikler Alanlar---
        private string m_ModulID;
        public string ModulID
        {
            get { return m_ModulID; }
            set { m_ModulID = value; }
        }
        private string m_Link;
        public string Link
        {
            get { return m_Link; }
            set { m_Link = value; }
        }
        private string m_ResimKucuk;
        public string ResimKucuk
        {
            get { return m_ResimKucuk; }
            set { m_ResimKucuk = value; }
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
        private string m_Aciklama;
        public string Aciklama
        {
            get { return m_Aciklama; }
            set { m_Aciklama = value; }
        }
        #endregion

        public MansetJSON()
        {
        }

        /// <summary>
        /// MansetJSON Nesnesi Oluþtur
        /// </summary>
        public MansetJSON(string pModulID, string pIcerikID, string pResimKucuk, string pResimUrl, string pBaslik, string pAciklama)
        {
            this.m_ModulID = pModulID;
            this.m_Link = Settings.CreateLink(pModulID, pIcerikID, pBaslik);
            this.m_ResimKucuk = pResimKucuk;
            this.m_ResimUrl = pResimUrl;
            this.m_Baslik = pBaslik;
            this.m_Aciklama = pAciklama;
        }
    }

    public partial class MansetMethods
    {
        ///<summary>
        /// Manset Data PrimaryKey
        ///</summary>
        public static Manset GetManset(System.Guid pID)
        {
            Manset rvManset = new Manset();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT * FROM Manset WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.UniqueIdentifier);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvManset = new Manset(MConvert.NullToGuid(IDR["ID"]), MConvert.NullToString(IDR["ModulID"]), MConvert.NullToString(IDR["IcerikID"]), MConvert.NullToString(IDR["ResimKucuk"]), MConvert.NullToString(IDR["ResimUrl"]), MConvert.NullToString(IDR["Baslik"]), MConvert.NullToString(IDR["Aciklama"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToBool(IDR["Aktif"]));
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
            return rvManset;
        }
        public static Manset GetManset(string pModulID, string pIcerikID)
        {
            Manset rvManset = new Manset();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT TOP(1) * FROM Manset WHERE ModulID=@ModulID AND IcerikID=@IcerikID", conneciton))
                {
                    cmd.Parameters.Add("ModulID", pModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("IcerikID", pIcerikID, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvManset = new Manset(MConvert.NullToGuid(IDR["ID"]), MConvert.NullToString(IDR["ModulID"]), MConvert.NullToString(IDR["IcerikID"]), MConvert.NullToString(IDR["ResimKucuk"]), MConvert.NullToString(IDR["ResimUrl"]), MConvert.NullToString(IDR["Baslik"]), MConvert.NullToString(IDR["Aciklama"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToBool(IDR["Aktif"]));
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
            return rvManset;
        }

        ///<summary>
        /// Manset Data Select
        ///</summary>
        public static List<MansetJSON> GetJSON(int top)
        {
            List<MansetJSON> rvManset = new List<MansetJSON>();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT TOP(" + top + ") * FROM Manset WHERE Aktif=1 ORDER BY KayitTarihi DESC", conneciton))
                {
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvManset.Add(new MansetJSON(MConvert.NullToString(IDR["ModulID"]), MConvert.NullToString(IDR["IcerikID"]), MConvert.NullToString(IDR["ResimKucuk"]), MConvert.NullToString(IDR["ResimUrl"]), MConvert.NullToString(IDR["Baslik"]), MConvert.NullToString(IDR["Aciklama"])));
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
            return rvManset;
        }

        ///<summary>
        /// Manset Data Insert
        ///</summary>
        public static int Insert(Manset p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "INSERT INTO Manset (ModulID,IcerikID,ResimKucuk,ResimUrl,Baslik,Aciklama,KayitTarihi,Aktif) VALUES(@ModulID,@IcerikID,@ResimKucuk,@ResimUrl,@Baslik,@Aciklama,@KayitTarihi,@Aktif)", conneciton))
                {
                    cmd.Parameters.Add("ModulID", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("IcerikID", p.IcerikID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ResimKucuk", p.ResimKucuk, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ResimUrl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Baslik", p.Baslik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Aciklama", p.Aciklama, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("KayitTarihi", p.KayitTarihi, MSqlDbType.DateTime);
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
        /// Manset Data Update
        ///</summary>
        public static int Update(Manset p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "UPDATE Manset SET ModulID=@ModulID,IcerikID=@IcerikID,ResimKucuk=@ResimKucuk,ResimUrl=@ResimUrl,Baslik=@Baslik,Aciklama=@Aciklama,KayitTarihi=@KayitTarihi,Aktif=@Aktif WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("ModulID", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("IcerikID", p.IcerikID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ResimKucuk", p.ResimKucuk, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ResimUrl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Baslik", p.Baslik, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Aciklama", p.Aciklama, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("KayitTarihi", p.KayitTarihi, MSqlDbType.DateTime);
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
        /// Manset Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Manset WHERE ID=@ID", conneciton))
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
        /// Manset Data Delete
        ///</summary>
        public static int Delete(Manset p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Manset WHERE ID=@ID", conneciton))
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Manset WHERE ModulID=@ModulID AND IcerikID=@IcerikID", conneciton))
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
