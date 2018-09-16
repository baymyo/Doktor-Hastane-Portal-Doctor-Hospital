using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace Lib
{
    public partial class CalismaAlaniCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public CalismaAlani this[int index]
        {
            get { return (CalismaAlani)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(CalismaAlani obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, CalismaAlani obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(CalismaAlani obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(CalismaAlani obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(CalismaAlani obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class CalismaAlani : IDisposable
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
        private string m_Kurum;
        public string Kurum
        {
            get { return m_Kurum; }
            set { m_Kurum = value; }
        }
        private string m_Telefon;
        public string Telefon
        {
            get { return m_Telefon; }
            set { m_Telefon = value; }
        }
        private string m_Adres;
        public string Adres
        {
            get { return m_Adres; }
            set { m_Adres = value; }
        }
        private string m_Semt;
        public string Semt
        {
            get { return m_Semt; }
            set { m_Semt = value; }
        }
        private string m_Sehir;
        public string Sehir
        {
            get { return m_Sehir; }
            set { m_Sehir = value; }
        }
        private string m_WebSitesi;
        public string WebSitesi
        {
            get { return m_WebSitesi; }
            set { m_WebSitesi = value; }
        }
        private bool m_Randevu;
        public bool Randevu
        {
            get { return m_Randevu; }
            set { m_Randevu = value; }
        }
        private bool m_Varsayilan;
        public bool Varsayilan
        {
            get { return m_Varsayilan; }
            set { m_Varsayilan = value; }
        }
        private bool m_Aktif;
        public bool Aktif
        {
            get { return m_Aktif; }
            set { m_Aktif = value; }
        }
        #endregion

        public CalismaAlani()
        {
        }

        /// <summary>
        /// CalismaAlani Nesnesi Oluþtur
        /// </summary>
        public CalismaAlani(System.Guid pID, System.Guid pHesapID, string pKurum, string pTelefon, string pAdres, string pSemt, string pSehir, string pWebSitesi, bool pRandevu, bool pVarsayilan, bool pAktif)
        {
            this.m_ID = pID;
            this.m_HesapID = pHesapID;
            this.m_Kurum = pKurum;
            this.m_Telefon = pTelefon;
            this.m_Adres = pAdres;
            this.m_Semt = pSemt;
            this.m_Sehir = pSehir;
            this.m_WebSitesi = pWebSitesi;
            this.m_Randevu = pRandevu;
            this.m_Varsayilan = pVarsayilan;
            this.m_Aktif = pAktif;
        }
    }

    public partial class CalismaAlaniMethods
    {
        ///<summary>
        /// CalismaAlani Data PrimaryKey
        ///</summary>
        public static CalismaAlani GetCalismaAlani(System.Guid pID)
        {
            CalismaAlani rvCalismaAlani = new CalismaAlani();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT TOP(1) * FROM CalismaAlani WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.UniqueIdentifier);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvCalismaAlani = new CalismaAlani(MConvert.NullToGuid(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["Kurum"]), MConvert.NullToString(IDR["Telefon"]), MConvert.NullToString(IDR["Adres"]), MConvert.NullToString(IDR["Semt"]), MConvert.NullToString(IDR["Sehir"]), MConvert.NullToString(IDR["WebSitesi"]), MConvert.NullToBool(IDR["Randevu"]), MConvert.NullToBool(IDR["Varsayilan"]), MConvert.NullToBool(IDR["Aktif"]));
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
            return rvCalismaAlani;
        }

        public static CalismaAlani GetDefault(System.Guid pHesapID)
        {
            CalismaAlani rvCalismaAlani = new CalismaAlani();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT TOP(1) * FROM CalismaAlani WHERE HesapID=@HesapID AND Varsayilan=1 AND Aktif=1", conneciton))
                {
                    cmd.Parameters.Add("HesapID", pHesapID, MSqlDbType.UniqueIdentifier);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvCalismaAlani = new CalismaAlani(MConvert.NullToGuid(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["Kurum"]), MConvert.NullToString(IDR["Telefon"]), MConvert.NullToString(IDR["Adres"]), MConvert.NullToString(IDR["Semt"]), MConvert.NullToString(IDR["Sehir"]), MConvert.NullToString(IDR["WebSitesi"]), MConvert.NullToBool(IDR["Randevu"]), MConvert.NullToBool(IDR["Varsayilan"]), MConvert.NullToBool(IDR["Aktif"]));
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
            return rvCalismaAlani;
        }

        ///<summary>
        /// CalismaAlani Data Select
        ///</summary>
        public static CalismaAlaniCollection GetSelect(System.Guid pHesapID)
        {
            CalismaAlaniCollection rvCalismaAlani = new CalismaAlaniCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT * FROM CalismaAlani WHERE HesapID=@HesapID", conneciton))
                {
                    cmd.Parameters.Add("HesapID", pHesapID, MSqlDbType.UniqueIdentifier);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvCalismaAlani.Add(new CalismaAlani(MConvert.NullToGuid(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["Kurum"]), MConvert.NullToString(IDR["Telefon"]), MConvert.NullToString(IDR["Adres"]), MConvert.NullToString(IDR["Semt"]), MConvert.NullToString(IDR["Sehir"]), MConvert.NullToString(IDR["WebSitesi"]), MConvert.NullToBool(IDR["Randevu"]), MConvert.NullToBool(IDR["Varsayilan"]), MConvert.NullToBool(IDR["Aktif"])));
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
            return rvCalismaAlani;
        }

        ///<summary>
        /// CalismaAlani Data Insert
        ///</summary>
        public static int Insert(CalismaAlani p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "if (@Varsayilan=1) begin UPDATE CalismaAlani SET Varsayilan=0 WHERE HesapID=@HesapID AND Varsayilan=1 end INSERT INTO CalismaAlani (HesapID,Kurum,Telefon,Adres,Semt,Sehir,WebSitesi,Randevu,Varsayilan,Aktif) VALUES(@HesapID,@Kurum,@Telefon,@Adres,@Semt,@Sehir,@WebSitesi,@Randevu,@Varsayilan,@Aktif)", conneciton))
                {
                    cmd.Parameters.Add("HesapID", p.HesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("Kurum", System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(p.Kurum.ToLower().Trim()), MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Telefon", p.Telefon, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Adres", p.Adres, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Semt", System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(p.Semt.ToLower().Trim()), MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Sehir", p.Sehir.ToUpper().Trim(), MSqlDbType.NVarChar);
                    cmd.Parameters.Add("WebSitesi", p.WebSitesi.Trim(), MSqlDbType.VarChar);
                    cmd.Parameters.Add("Randevu", p.Randevu, MSqlDbType.Bit);
                    cmd.Parameters.Add("Varsayilan", p.Varsayilan, MSqlDbType.Bit);
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
        /// CalismaAlani Data Update
        ///</summary>
        public static int Update(CalismaAlani p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "if (@Varsayilan=1) begin UPDATE CalismaAlani SET Varsayilan=0 WHERE HesapID=@HesapID AND Varsayilan=1 end UPDATE CalismaAlani SET HesapID=@HesapID,Kurum=@Kurum,Telefon=@Telefon,Adres=@Adres,Semt=@Semt,Sehir=@Sehir,WebSitesi=@WebSitesi,Randevu=@Randevu,Varsayilan=@Varsayilan,Aktif=@Aktif WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("HesapID", p.HesapID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("Kurum", System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(p.Kurum.ToLower().Trim()), MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Telefon", p.Telefon, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Adres", p.Adres, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Semt", System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(p.Semt.ToLower().Trim()), MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Sehir", p.Sehir.ToUpper().Trim(), MSqlDbType.NVarChar);
                    cmd.Parameters.Add("WebSitesi", p.WebSitesi.Trim(), MSqlDbType.VarChar);
                    cmd.Parameters.Add("Randevu", p.Randevu, MSqlDbType.Bit);
                    cmd.Parameters.Add("Varsayilan", p.Varsayilan, MSqlDbType.Bit);
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
        /// CalismaAlani Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM CalismaAlani WHERE ID=@ID", conneciton))
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
        /// CalismaAlani Data Delete
        ///</summary>
        public static int Delete(CalismaAlani p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM CalismaAlani WHERE ID=@ID", conneciton))
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
