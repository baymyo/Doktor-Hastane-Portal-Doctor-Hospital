using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace Lib
{
    public partial class ProfilCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Profil this[int index]
        {
            get { return (Profil)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Profil obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Profil obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Profil obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Profil obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Profil obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Profil : IDisposable
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
        private string m_Url;
        public string Url
        {
            get { return m_Url; }
            set { m_Url = value; }
        }
        private string m_ResimUrl;
        public string ResimUrl
        {
            get { return m_ResimUrl; }
            set { m_ResimUrl = value; }
        }
        private string m_Unvan;
        public string Unvan
        {
            get { return m_Unvan; }
            set { m_Unvan = value; }
        }
        private string m_UzmanlikAlaniID;
        public string UzmanlikAlaniID
        {
            get { return m_UzmanlikAlaniID; }
            set { m_UzmanlikAlaniID = value; }
        }
        private string m_TCKimlikNo;
        public string TCKimlikNo
        {
            get { return m_TCKimlikNo; }
            set { m_TCKimlikNo = value; }
        }
        private string m_DiplomaNo;
        public string DiplomaNo
        {
            get { return m_DiplomaNo; }
            set { m_DiplomaNo = value; }
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
        private string m_Hakkimda;
        public string Hakkimda
        {
            get { return m_Hakkimda; }
            set { m_Hakkimda = value; }
        }
        #endregion

        public Profil()
        {
        }

        /// <summary>
        /// Profil Nesnesi Oluþtur
        /// </summary>
        public Profil(System.Guid pID, string pUrl, string pResimUrl, string pUnvan, string pUzmanlikAlaniID, string pTCKimlikNo, string pDiplomaNo, string pAdi, string pMail, string pHakkimda)
        {
            this.m_ID = pID;
            this.m_Url = pUrl;
            this.m_ResimUrl = pResimUrl;
            this.m_Unvan = pUnvan;
            this.m_UzmanlikAlaniID = pUzmanlikAlaniID;
            this.m_TCKimlikNo = pTCKimlikNo;
            this.m_DiplomaNo = pDiplomaNo;
            this.m_Adi = pAdi;
            this.m_Mail = pMail;
            this.m_Hakkimda = pHakkimda;
        }
    }

    public partial class ProfilMethods
    {
        ///<summary>
        /// Profil Data PrimaryKey
        ///</summary>
        public static Profil GetProfil(System.Guid pID)
        {
            Profil rvProfil = new Profil();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT * FROM Profil WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.UniqueIdentifier);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvProfil = new Profil(MConvert.NullToGuid(IDR["ID"]), MConvert.NullToString(IDR["Url"]), MConvert.NullToString(IDR["ResimUrl"]), MConvert.NullToString(IDR["Unvan"]), MConvert.NullToString(IDR["UzmanlikAlaniID"]), MConvert.NullToString(IDR["TCKimlikNo"]), MConvert.NullToString(IDR["DiplomaNo"]), MConvert.NullToString(IDR["Adi"]), MConvert.NullToString(IDR["Mail"]), MConvert.NullToString(IDR["Hakkimda"]));
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
            return rvProfil;
        }
        public static Profil GetProfil(string pUrl)
        {
            Profil rvProfil = new Profil();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT TOP(1) * FROM Profil WHERE Url=@Url", conneciton))
                {
                    cmd.Parameters.Add("Url", pUrl, MSqlDbType.VarChar);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvProfil = new Profil(MConvert.NullToGuid(IDR["ID"]), MConvert.NullToString(IDR["Url"]), MConvert.NullToString(IDR["ResimUrl"]), MConvert.NullToString(IDR["Unvan"]), MConvert.NullToString(IDR["UzmanlikAlaniID"]), MConvert.NullToString(IDR["TCKimlikNo"]), MConvert.NullToString(IDR["DiplomaNo"]), MConvert.NullToString(IDR["Adi"]), MConvert.NullToString(IDR["Mail"]), MConvert.NullToString(IDR["Hakkimda"]));
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
            return rvProfil;
        }

        ///<summary>
        /// Profil Data Insert
        ///</summary>
        public static object Insert(Profil p)
        {
            object rowsAffected = 0;
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.StoredProcedure, "Profil_Insert", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("Url", p.Url, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ResimUrl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Unvan", p.Unvan, MSqlDbType.VarChar);
                    cmd.Parameters.Add("UzmanlikAlaniID", p.UzmanlikAlaniID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("TCKimlikNo", p.TCKimlikNo, MSqlDbType.VarChar);
                    cmd.Parameters.Add("DiplomaNo", p.DiplomaNo, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Adi", p.Adi, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Mail", p.Mail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Hakkimda", p.Hakkimda, MSqlDbType.NVarChar);
                    rowsAffected = cmd.ExecuteScalar();
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
        /// Profil Data Update
        ///</summary>
        public static object Update(Profil p)
        {
            object rowsAffected = 0;
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.StoredProcedure, "Profil_Update", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("Url", p.Url, MSqlDbType.VarChar);
                    cmd.Parameters.Add("ResimUrl", p.ResimUrl, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Unvan", p.Unvan, MSqlDbType.VarChar);
                    cmd.Parameters.Add("UzmanlikAlaniID", p.UzmanlikAlaniID, MSqlDbType.VarChar);
                    cmd.Parameters.Add("TCKimlikNo", p.TCKimlikNo, MSqlDbType.VarChar);
                    cmd.Parameters.Add("DiplomaNo", p.DiplomaNo, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Adi", p.Adi, MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Mail", p.Mail, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Hakkimda", p.Hakkimda, MSqlDbType.NVarChar);
                    rowsAffected = cmd.ExecuteScalar();
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
        /// Profil Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Profil WHERE ID=@ID", conneciton))
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
        /// Profil Data Delete
        ///</summary>
        public static int Delete(Profil p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Profil WHERE ID=@ID", conneciton))
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
