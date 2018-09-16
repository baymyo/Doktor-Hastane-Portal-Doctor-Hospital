using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace Lib
{
    public partial class KategoriCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Kategori this[int index]
        {
            get { return (Kategori)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Kategori obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Kategori obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Kategori obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Kategori obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Kategori obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Kategori : IDisposable
    {
        #region ---IDisposable Members---
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ---Properties/Field - Özellikler Alanlar---
        private string m_ID;
        public string ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        private string m_ParentID;
        public string ParentID
        {
            get { return m_ParentID; }
            set { m_ParentID = value; }
        }
        private string m_ModulID;
        public string ModulID
        {
            get { return m_ModulID; }
            set { m_ModulID = value; }
        }
        private string m_Adi;
        public string Adi
        {
            get { return m_Adi; }
            set { m_Adi = value; }
        }
        private string m_Dil;
        public string Dil
        {
            get { return m_Dil; }
            set { m_Dil = value; }
        }
        private byte m_Sira;
        public byte Sira
        {
            get { return m_Sira; }
            set { m_Sira = value; }
        }
        private bool m_Tab;
        public bool Tab
        {
            get { return m_Tab; }
            set { m_Tab = value; }
        }
        private bool m_Menu;
        public bool Menu
        {
            get { return m_Menu; }
            set { m_Menu = value; }
        }
        private bool m_Aktif;
        public bool Aktif
        {
            get { return m_Aktif; }
            set { m_Aktif = value; }
        }

        #endregion

        public Kategori()
        {
        }

        /// <summary>
        /// Kategori Nesnesi Oluþtur
        /// </summary>
        public Kategori(string pID, string pParentID, string pModulID, string pAdi, string pDil, byte pSira, bool pTab, bool pMenu, bool pAktif)
        {
            this.m_ID = pID;
            this.m_ParentID = pParentID;
            this.m_ModulID = pModulID;
            this.m_Adi = pAdi;
            this.m_Dil = pDil;
            this.m_Sira = pSira;
            this.m_Tab = pTab;
            this.m_Menu = pMenu;
            this.m_Aktif = pAktif;
        }
    }

    [System.ComponentModel.DataObject(true)]
    public partial class KategoriMethods
    {
        ///<summary>
        /// Kategori Data PrimaryKey
        ///</summary>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static Kategori GetKategori(string modulID, string id)
        {
            Kategori rvKategori = new Kategori();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT TOP(1) * FROM Kategori WHERE ModulID=@ModulID AND ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ModulID", modulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ID", id, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvKategori = new Kategori(MConvert.NullToString(IDR["ID"]), MConvert.NullToString(IDR["ParentID"]), MConvert.NullToString(IDR["ModulID"]), MConvert.NullToString(IDR["Adi"]), MConvert.NullToString(IDR["Dil"]), MConvert.NullToByte(IDR["Sira"]), MConvert.NullToBool(IDR["Tab"]), MConvert.NullToBool(IDR["Menu"]), MConvert.NullToBool(IDR["Aktif"]));
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
            return rvKategori;
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static List<Kategori> GetMenu(string modulID, bool rootNode)
        {
            List<Kategori> rvKategori = new List<Kategori>();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT * FROM Kategori WHERE ModulID=@ModulID AND Menu=1 AND Aktif=1 ORDER BY ID ASC", conneciton))
                {
                    cmd.Parameters.Add("ModulID", modulID, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        if (rootNode)
                            rvKategori.Add(new Kategori("0", "", modulID, "<Seçiniz>", "", 0, false, false, false));
                        while (IDR.Read())
                            rvKategori.Add(new Kategori(MConvert.NullToString(IDR["ID"]), MConvert.NullToString(IDR["ParentID"]), MConvert.NullToString(IDR["ModulID"]), MConvert.NullToString(IDR["Adi"]), MConvert.NullToString(IDR["Dil"]), MConvert.NullToByte(IDR["Sira"]), MConvert.NullToBool(IDR["Tab"]), MConvert.NullToBool(IDR["Menu"]), MConvert.NullToBool(IDR["Aktif"])));
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
            return rvKategori;
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static BAYMYO.UI.Data.HierarchicalCollection GetHierarchical(string modulID, bool rootNode)
        {
            BAYMYO.UI.Data.HierarchicalCollection rvKategori = new BAYMYO.UI.Data.HierarchicalCollection();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT * FROM Kategori WHERE ModulID=@ModulID ORDER BY ID ASC", conneciton))
                {
                    cmd.Parameters.Add("ModulID", modulID, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        if (rootNode)
                            rvKategori.Add(new BAYMYO.UI.Data.Hierarchical("0", "", "Yeni Kategori"));
                        while (IDR.Read())
                            rvKategori.Add(new BAYMYO.UI.Data.Hierarchical(MConvert.NullToString(IDR["ID"]), MConvert.NullToString(IDR["ParentID"]), MConvert.NullToString(IDR["Adi"])));
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
            return rvKategori;
        }

        ///<summary>
        /// Kategori Data Insert
        ///</summary>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Insert)]
        public static int Insert(Kategori p)
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
                using (MCommand cmd = new MCommand(CommandType.StoredProcedure, "Kategori_Insert", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ParentID", p.ParentID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ModulID", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Adi", p.Adi, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Dil", p.Dil, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Sira", p.Sira, MSqlDbType.TinyInt);
                    cmd.Parameters.Add("Tab", p.Tab, MSqlDbType.Bit);
                    cmd.Parameters.Add("Menu", p.Menu, MSqlDbType.Bit);
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
        /// Kategori Data Update
        ///</summary>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Update)]
        public static int Update(Kategori p)
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
                using (MCommand cmd = new MCommand(CommandType.StoredProcedure, "Kategori_Update", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ParentID", p.ParentID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ModulID", p.ModulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Adi", p.Adi, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Dil", p.Dil, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Sira", p.Sira, MSqlDbType.TinyInt);
                    cmd.Parameters.Add("Tab", p.Tab, MSqlDbType.Bit);
                    cmd.Parameters.Add("Menu", p.Menu, MSqlDbType.Bit);
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
        /// Kategori Data Delete
        ///</summary>
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Delete)]
        public static int Delete(string modulID, string id)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Kategori WHERE ModulID=@ModulID AND ID LIKE @ID", conneciton))
                {
                    cmd.Parameters.Add("ModulID", modulID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ID", id + "%", MSqlDbType.VarChar);
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
