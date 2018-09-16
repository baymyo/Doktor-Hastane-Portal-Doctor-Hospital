using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace Lib
{
    public enum HesapTuru
    {
        None = 0,
        Admin = 1,
        Moderator = 2,
        Editor = 3,
        Standart = 4
    }

    public enum HesapCinsiyet
    {
        Belirtilmedi = 0,
        Erkek = 1,
        Bayan = 2
    }

    public partial class HesapCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Hesap this[int index]
        {
            get { return (Hesap)this.List[index]; }
            set { this.List[index] = value; }
        }

        public object SyncRoot { get { return this.List.SyncRoot; } }

        public int Add(Hesap obj)
        {
            return this.List.Add(obj);
        }

        public void Insert(int index, Hesap obj)
        {
            this.List.Insert(index, obj);
        }

        public bool Contains(Hesap obj)
        {
            return this.List.Contains(obj);
        }

        public int IndexOf(Hesap obj)
        {
            return this.List.IndexOf(obj);
        }

        public void Remove(Hesap obj)
        {
            this.List.Remove(obj);
        }
    }

    public partial class Hesap : IDisposable
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
        private string m_Adi;
        public string Adi
        {
            get { return m_Adi; }
            set { m_Adi = value; }
        }
        private string m_Soyadi;
        public string Soyadi
        {
            get { return m_Soyadi; }
            set { m_Soyadi = value; }
        }
        private string m_Mail;
        public string Mail
        {
            get { return m_Mail; }
            set { m_Mail = value; }
        }
        private string m_Sifre;
        public string Sifre
        {
            get { return m_Sifre; }
            set { m_Sifre = value; }
        }
        private string m_Roller;
        public string Roller
        {
            get { return m_Roller; }
            set { m_Roller = value; }
        }
        private string m_OnayKodu;
        public string OnayKodu
        {
            get { return m_OnayKodu; }
            set { m_OnayKodu = value; }
        }
        private DateTime m_DogumTarihi;
        public DateTime DogumTarihi
        {
            get { return m_DogumTarihi; }
            set { m_DogumTarihi = value; }
        }
        private DateTime m_KayitTarihi;
        public DateTime KayitTarihi
        {
            get { return m_KayitTarihi; }
            set { m_KayitTarihi = value; }
        }
        private HesapCinsiyet m_Cinsiyet;
        public HesapCinsiyet Cinsiyet
        {
            get { return m_Cinsiyet; }
            set { m_Cinsiyet = value; }
        }
        private HesapTuru m_Tipi;
        public HesapTuru Tipi
        {
            get { return m_Tipi; }
            set { m_Tipi = value; }
        }
        private bool m_Yorum;
        public bool Yorum
        {
            get { return m_Yorum; }
            set { m_Yorum = value; }
        }
        private bool m_Abonelik;
        public bool Abonelik
        {
            get { return m_Abonelik; }
            set { m_Abonelik = value; }
        }
        private bool m_Aktivasyon;
        public bool Aktivasyon
        {
            get { return m_Aktivasyon; }
            set { m_Aktivasyon = value; }
        }
        private bool m_Aktif;
        public bool Aktif
        {
            get { return m_Aktif; }
            set { m_Aktif = value; }
        }
        private Profil m_ProfilObject;
        public Profil ProfilObject
        {
            get { return m_ProfilObject; }
            set { m_ProfilObject = value; }
        }
        #endregion

        public Hesap()
        {
        }

        /// <summary>
        /// Hesap Nesnesi Oluþtur
        /// </summary>
        public Hesap(System.Guid pID, string pIP, string pAdi, string pSoyadi, string pMail, string pSifre, string pRoller, string pOnayKodu, DateTime pDogumTarihi, DateTime pKayitTarihi, byte pCinsiyet, byte pTipi, bool pYorum, bool pAbonelik, bool pAktivasyon, bool pAktif)
        {
            this.m_ID = pID;
            this.m_IP = pIP;
            this.m_Adi = pAdi;
            this.m_Soyadi = pSoyadi;
            this.m_Mail = pMail;
            this.m_Sifre = pSifre;
            this.m_Roller = pRoller;
            this.m_OnayKodu = pOnayKodu;
            this.m_DogumTarihi = pDogumTarihi;
            this.m_KayitTarihi = pKayitTarihi;
            this.m_Cinsiyet = Settings.HesapCinsiyeti(pCinsiyet);
            this.m_Tipi = Settings.HesapTipi(pTipi);
            this.m_Yorum = pYorum;
            this.m_Abonelik = pAbonelik;
            this.m_Aktivasyon = pAktivasyon;
            this.m_Aktif = pAktif;
        }
    }

    public partial class HesapJSON : IDisposable
    {
        #region ---IDisposable Members---
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ---Properties/Field - Özellikler Alanlar---
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
        private string m_UzmanlikAlani;
        public string UzmanlikAlani
        {
            get { return m_UzmanlikAlani; }
            set { m_UzmanlikAlani = value; }
        }
        private string m_AdiSoyadi;
        public string AdiSoyadi
        {
            get { return m_AdiSoyadi; }
            set { m_AdiSoyadi = value; }
        }
        private string m_KayitTarihi;
        public string KayitTarihi
        {
            get { return m_KayitTarihi; }
            set { m_KayitTarihi = value; }
        }
        #endregion

        public HesapJSON()
        {
        }

        /// <summary>
        /// Hesap Nesnesi Oluþtur
        /// </summary>
        public HesapJSON(string pUrl, string pResimUrl, string pUzmanlikAlani, string pAdiSoyadi, DateTime pKayitTarihi)
        {
            this.m_Url = pUrl;
            this.m_ResimUrl = pResimUrl;
            this.m_UzmanlikAlani = pUzmanlikAlani;
            this.m_AdiSoyadi = pAdiSoyadi;
            this.m_KayitTarihi = pKayitTarihi.ToShortDateString();
        }
    }

    public partial class HesapMethods
    {
        ///<summary>
        /// Hesap Data PrimaryKey
        ///</summary>
        public static Hesap GetHesap(System.Guid pID)
        {
            Hesap rvHesap = new Hesap();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT * FROM Hesap WHERE ID=@ID", conneciton))
                {
                    cmd.Parameters.Add("ID", pID, MSqlDbType.UniqueIdentifier);
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvHesap = new Hesap(MConvert.NullToGuid(IDR["ID"]), MConvert.NullToString(IDR["IP"]), MConvert.NullToString(IDR["Adi"]), MConvert.NullToString(IDR["Soyadi"]), MConvert.NullToString(IDR["Mail"]), MConvert.NullToString(IDR["Sifre"]), MConvert.NullToString(IDR["Roller"]), MConvert.NullToString(IDR["OnayKodu"]), MConvert.NullToDateTime(IDR["DogumTarihi"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToByte(IDR["Cinsiyet"]), MConvert.NullToByte(IDR["Tipi"]), MConvert.NullToBool(IDR["Yorum"]), MConvert.NullToBool(IDR["Abonelik"]), MConvert.NullToBool(IDR["Aktivasyon"]), MConvert.NullToBool(IDR["Aktif"]));
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
            rvHesap.ProfilObject = ProfilMethods.GetProfil(rvHesap.ID);
            return rvHesap;
        }
        public static Hesap GetHesap(string pUrl)
        {
            Hesap rvHesap = new Hesap();
            Profil tempProfil = ProfilMethods.GetProfil(pUrl);
            if (tempProfil != null)
                using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
                {
                    switch (conneciton.State)
                    {
                        case System.Data.ConnectionState.Closed:
                            conneciton.Open();
                            break;
                    }
                    using (MCommand cmd = new MCommand(CommandType.Text, "SELECT TOP(1) * FROM Hesap WHERE ID=@ID", conneciton))
                    {
                        cmd.Parameters.Add("ID", tempProfil.ID, MSqlDbType.UniqueIdentifier);
                        using (IDataReader IDR = cmd.ExecuteReader())
                        {
                            while (IDR.Read())
                                rvHesap = new Hesap(MConvert.NullToGuid(IDR["ID"]), MConvert.NullToString(IDR["IP"]), MConvert.NullToString(IDR["Adi"]), MConvert.NullToString(IDR["Soyadi"]), MConvert.NullToString(IDR["Mail"]), MConvert.NullToString(IDR["Sifre"]), MConvert.NullToString(IDR["Roller"]), MConvert.NullToString(IDR["OnayKodu"]), MConvert.NullToDateTime(IDR["DogumTarihi"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToByte(IDR["Cinsiyet"]), MConvert.NullToByte(IDR["Tipi"]), MConvert.NullToBool(IDR["Yorum"]), MConvert.NullToBool(IDR["Abonelik"]), MConvert.NullToBool(IDR["Aktivasyon"]), MConvert.NullToBool(IDR["Aktif"]));
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
            rvHesap.ProfilObject = tempProfil;
            return rvHesap;
        }

        /// <summary>
        /// Hesap Getir
        /// </summary>
        public static Hesap GetHesap(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
        {
            Hesap rvHesap = new Hesap();
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
                            rvHesap = new Hesap(MConvert.NullToGuid(IDR["ID"]), MConvert.NullToString(IDR["IP"]), MConvert.NullToString(IDR["Adi"]), MConvert.NullToString(IDR["Soyadi"]), MConvert.NullToString(IDR["Mail"]), MConvert.NullToString(IDR["Sifre"]), MConvert.NullToString(IDR["Roller"]), MConvert.NullToString(IDR["OnayKodu"]), MConvert.NullToDateTime(IDR["DogumTarihi"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToByte(IDR["Cinsiyet"]), MConvert.NullToByte(IDR["Tipi"]), MConvert.NullToBool(IDR["Yorum"]), MConvert.NullToBool(IDR["Abonelik"]), MConvert.NullToBool(IDR["Aktivasyon"]), MConvert.NullToBool(IDR["Aktif"]));
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
            rvHesap.ProfilObject = ProfilMethods.GetProfil(rvHesap.ID);
            return rvHesap;
        }
        public static Int64 GetCount(bool pAktif)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "SELECT COUNT(ID) AS TotalCount FROM Hesap WHERE Aktivasyon=1 AND Aktif=@Aktif", conneciton))
                {
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

        public static List<HesapJSON> GetJSON(QueryType sorguTipi, int top)
        {
            List<HesapJSON> rvHesap = new List<HesapJSON>();
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                string query = "";
                switch (sorguTipi)
                {
                    case QueryType.Date:
                        query = "SELECT TOP(" + top + ") Profil.Url, Profil.ResimUrl, dbo.GetKategoriAdi('uzmanlik',Profil.UzmanlikAlaniID) AS UzmanlikAlani, (dbo.GetKategoriAdi('unvan',Profil.Unvan) +' '+ Hesap.Adi +' '+ Hesap.Soyadi) AS AdiSoyadi, Hesap.KayitTarihi FROM Hesap INNER JOIN Profil ON Hesap.ID = Profil.ID WHERE (Hesap.Aktivasyon = 1) AND (Hesap.Aktif = 1) AND (Hesap.Tipi = 3) AND (Profil.Unvan IS NOT NULL) ORDER BY Hesap.KayitTarihi DESC";
                        break;
                    case QueryType.Populer:
                        query = "SELECT * FROM (SELECT TOP(" + top + ") Profil.Url, Profil.ResimUrl, dbo.GetKategoriAdi('uzmanlik',Profil.UzmanlikAlaniID) AS UzmanlikAlani, (dbo.GetKategoriAdi('unvan',Profil.Unvan) +' '+ Hesap.Adi +' '+ Hesap.Soyadi) AS AdiSoyadi, Hesap.KayitTarihi, dbo.GetGosterimOran('profil',Hesap.ID) AS Oran FROM Hesap INNER JOIN Profil ON Hesap.ID = Profil.ID WHERE (Hesap.Aktivasyon = 1) AND (Hesap.Aktif = 1) AND (Hesap.Tipi = 3) AND (Profil.Unvan IS NOT NULL)) HesapListe ORDER BY Oran DESC";
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.Text, query, conneciton))
                {
                    using (IDataReader IDR = cmd.ExecuteReader())
                    {
                        while (IDR.Read())
                            rvHesap.Add(new HesapJSON(MConvert.NullToString(IDR["Url"]), MConvert.NullToString(IDR["ResimUrl"]), MConvert.NullToString(IDR["UzmanlikAlani"]), MConvert.NullToString(IDR["AdiSoyadi"]), MConvert.NullToDateTime(IDR["KayitTarihi"])));
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
            return rvHesap;
        }

        ///<summary>
        /// Hesap Data Insert
        ///</summary>
        public static object Insert(Hesap p)
        {
            object rowsAffected = null;
            using (MConnection conneciton = new MConnection(MClientProvider.MSSQL))
            {
                switch (conneciton.State)
                {
                    case System.Data.ConnectionState.Closed:
                        conneciton.Open();
                        break;
                }
                using (MCommand cmd = new MCommand(CommandType.StoredProcedure, "Hesap_Insert", conneciton))
                {
                    cmd.Parameters.Add("IP", p.IP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Adi", System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(p.Adi.ToLower().Trim()), MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Soyadi", p.Soyadi.ToUpper().Trim(), MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Mail", p.Mail.Trim(), MSqlDbType.VarChar);
                    cmd.Parameters.Add("Sifre", p.Sifre, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Roller", p.Roller, MSqlDbType.VarChar);
                    cmd.Parameters.Add("OnayKodu", p.OnayKodu, MSqlDbType.VarChar);
                    cmd.Parameters.Add("DogumTarihi", p.DogumTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("KayitTarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("Cinsiyet", MConvert.NullToByte(p.Cinsiyet), MSqlDbType.TinyInt);
                    cmd.Parameters.Add("Tipi", MConvert.NullToByte(p.Tipi), MSqlDbType.TinyInt);
                    cmd.Parameters.Add("Yorum", p.Yorum, MSqlDbType.Bit);
                    cmd.Parameters.Add("Abonelik", p.Abonelik, MSqlDbType.Bit);
                    cmd.Parameters.Add("Aktivasyon", p.Aktivasyon, MSqlDbType.Bit);
                    cmd.Parameters.Add("Aktif", p.Aktif, MSqlDbType.Bit);
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
        /// Hesap Data Update
        ///</summary>
        public static int Update(Hesap p)
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
                using (MCommand cmd = new MCommand(CommandType.StoredProcedure, "Hesap_Update", conneciton))
                {
                    cmd.Parameters.Add("ID", p.ID, MSqlDbType.UniqueIdentifier);
                    cmd.Parameters.Add("IP", p.IP, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Adi", System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(p.Adi.ToLower().Trim()), MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Soyadi", p.Soyadi.ToUpper().Trim(), MSqlDbType.NVarChar);
                    cmd.Parameters.Add("Mail", p.Mail.Trim(), MSqlDbType.VarChar);
                    cmd.Parameters.Add("Sifre", p.Sifre, MSqlDbType.VarChar);
                    cmd.Parameters.Add("Roller", p.Roller, MSqlDbType.VarChar);
                    cmd.Parameters.Add("OnayKodu", p.OnayKodu, MSqlDbType.VarChar);
                    cmd.Parameters.Add("DogumTarihi", p.DogumTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("KayitTarihi", p.KayitTarihi, MSqlDbType.DateTime);
                    cmd.Parameters.Add("Cinsiyet", MConvert.NullToByte(p.Cinsiyet), MSqlDbType.TinyInt);
                    cmd.Parameters.Add("Tipi", MConvert.NullToByte(p.Tipi), MSqlDbType.TinyInt);
                    cmd.Parameters.Add("Yorum", p.Yorum, MSqlDbType.Bit);
                    cmd.Parameters.Add("Abonelik", p.Abonelik, MSqlDbType.Bit);
                    cmd.Parameters.Add("Aktivasyon", p.Aktivasyon, MSqlDbType.Bit);
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
        /// Hesap Data Delete
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Hesap WHERE ID=@ID", conneciton))
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
        /// Hesap Data Delete
        ///</summary>
        public static int Delete(Hesap p)
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
                using (MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Hesap WHERE ID=@ID", conneciton))
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
