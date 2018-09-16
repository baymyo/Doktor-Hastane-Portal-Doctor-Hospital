using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BAYMYO.MultiSQLClient;

namespace Lib
{
    public partial class SayfaCollection : CollectionBase, IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Sayfa this[int index]
        {
            get { return (Sayfa)this.List[index]; }
            set { this.List[index] = value; }
        }

		public object SyncRoot { get { return this.List.SyncRoot; } }

		public int Add(Sayfa obj)
		{
			return this.List.Add(obj);
		}

		public void Insert(int index, Sayfa obj)
		{
			this.List.Insert(index, obj);
		}

		public bool Contains(Sayfa obj)
		{
			return this.List.Contains(obj);
		}

		public int IndexOf(Sayfa obj)
		{
			return this.List.IndexOf(obj);
		}

		public void Remove(Sayfa obj)
		{
			this.List.Remove(obj);
		}
    }

    public partial class Sayfa : IDisposable
    {
		#region ---IDisposable Members---
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}
		#endregion

		#region ---Properties/Field - Özellikler Alanlar---
		private Int16 m_ID;
		public Int16 ID
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
		private string m_Baslik;
		public string Baslik
		{
			get { return m_Baslik; }
			set { m_Baslik = value; }
		}
		private string m_Icerik;
		public string Icerik
		{
			get { return m_Icerik; }
			set { m_Icerik = value; }
		}
		private string m_Dil;
		public string Dil
		{
			get { return m_Dil; }
			set { m_Dil = value; }
		}
		private DateTime m_KayitTarihi;
		public DateTime KayitTarihi
		{
			get { return m_KayitTarihi; }
			set { m_KayitTarihi = value; }
		}
		private byte m_Tipi;
		public byte Tipi
		{
			get { return m_Tipi; }
			set { m_Tipi = value; }
		}
		private bool m_Aktif;
		public bool Aktif
		{
			get { return m_Aktif; }
			set { m_Aktif = value; }
		}

		#endregion

		public Sayfa()
		{
		}

		/// <summary>
		/// Sayfa Nesnesi Oluþtur
		/// </summary>
		public Sayfa(Int16 pID, System.Guid pHesapID, string pBaslik, string pIcerik, string pDil, DateTime pKayitTarihi, byte pTipi, bool pAktif)
		{
			this.m_ID = pID;
			this.m_HesapID = pHesapID;
			this.m_Baslik = pBaslik;
			this.m_Icerik = pIcerik;
			this.m_Dil = pDil;
			this.m_KayitTarihi = pKayitTarihi;
			this.m_Tipi = pTipi;
			this.m_Aktif = pAktif;
		}
    }

	public partial class SayfaMethods
    {
			///<summary>
		/// Sayfa Data PrimaryKey
		///</summary>
		public static Sayfa GetSayfa(Int16 pID)
		{
			Sayfa rvSayfa = new Sayfa();
			using(MConnection conneciton = new MConnection(MClientProvider.MSSQL))
			{
				switch (conneciton.State)
				{
					case System.Data.ConnectionState.Closed:
						conneciton.Open();
						break;
				}
				using(MCommand cmd = new MCommand(CommandType.Text, "SELECT * FROM Sayfa WHERE ID=@ID", conneciton))
				{
					 cmd.Parameters.Add("ID", pID, MSqlDbType.SmallInt);
					 using (IDataReader IDR = cmd.ExecuteReader())
					 {
						 while(IDR.Read())
							rvSayfa = new Sayfa(MConvert.NullToInt16(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["Baslik"]), MConvert.NullToString(IDR["Icerik"]), MConvert.NullToString(IDR["Dil"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToByte(IDR["Tipi"]), MConvert.NullToBool(IDR["Aktif"]));
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
			return rvSayfa;
		}
		 

		/// <summary>
		/// Sayfa Getir
		/// </summary>
		public static Sayfa GetSayfa(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
		{
			Sayfa rvSayfa = new Sayfa();
			using(MConnection conneciton = new MConnection(MClientProvider.MSSQL))
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
							rvSayfa = new Sayfa( MConvert.NullToInt16(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["Baslik"]), MConvert.NullToString(IDR["Icerik"]), MConvert.NullToString(IDR["Dil"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToByte(IDR["Tipi"]), MConvert.NullToBool(IDR["Aktif"]));
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
			return rvSayfa;
		}

		/// <summary>
		/// Sayfa Liste Getir
		/// </summary>
		public static SayfaCollection GetList(CommandType cmdType, string sqlQuery, MParameterCollection parameters)
		{
			SayfaCollection rvSayfa = new SayfaCollection();
			using(MConnection conneciton = new MConnection(MClientProvider.MSSQL))
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
							rvSayfa.Add(new Sayfa( MConvert.NullToInt16(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["Baslik"]), MConvert.NullToString(IDR["Icerik"]), MConvert.NullToString(IDR["Dil"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToByte(IDR["Tipi"]), MConvert.NullToBool(IDR["Aktif"])));
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
			return rvSayfa;
		}

		///<summary>
		/// Sayfa Data Select
		///</summary>
		public static SayfaCollection GetSelect(System.Guid pHesapID)
		{
			SayfaCollection rvSayfa = new SayfaCollection();
			using(MConnection conneciton = new MConnection(MClientProvider.MSSQL))
			{
				switch (conneciton.State)
				{
					case System.Data.ConnectionState.Closed:
						conneciton.Open();
						break;
				}
				using(MCommand cmd = new MCommand(CommandType.Text, "SELECT * FROM Sayfa WHERE HesapID=@HesapID", conneciton))
				{
					 cmd.Parameters.Add("HesapID", pHesapID, MSqlDbType.UniqueIdentifier);
					 using (IDataReader IDR = cmd.ExecuteReader())
					 {
						 while(IDR.Read())
							rvSayfa.Add(new Sayfa(MConvert.NullToInt16(IDR["ID"]), MConvert.NullToGuid(IDR["HesapID"]), MConvert.NullToString(IDR["Baslik"]), MConvert.NullToString(IDR["Icerik"]), MConvert.NullToString(IDR["Dil"]), MConvert.NullToDateTime(IDR["KayitTarihi"]), MConvert.NullToByte(IDR["Tipi"]), MConvert.NullToBool(IDR["Aktif"])));
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
			return rvSayfa;
		}
		 

		///<summary>
		/// Sayfa Data Insert
		///</summary>
		public static int Insert(System.Guid pHesapID, string pBaslik, string pIcerik, string pDil, DateTime pKayitTarihi, byte pTipi, bool pAktif)
		{
			int rowsAffected = 0;
			using(MConnection conneciton = new MConnection(MClientProvider.MSSQL))
			{
				switch (conneciton.State)
				{
					case System.Data.ConnectionState.Closed:
						conneciton.Open();
						break;
				}
				using(MCommand cmd = new MCommand(CommandType.Text, "INSERT INTO Sayfa (HesapID,Baslik,Icerik,Dil,KayitTarihi,Tipi,Aktif) VALUES(@HesapID,@Baslik,@Icerik,@Dil,@KayitTarihi,@Tipi,@Aktif)", conneciton))
				{
					cmd.Parameters.Add("HesapID", pHesapID, MSqlDbType.UniqueIdentifier);
		 		cmd.Parameters.Add("Baslik", pBaslik, MSqlDbType.NVarChar);
		 		cmd.Parameters.Add("Icerik", pIcerik, MSqlDbType.NVarChar);
		 		cmd.Parameters.Add("Dil", pDil, MSqlDbType.VarChar);
		 		cmd.Parameters.Add("KayitTarihi", pKayitTarihi, MSqlDbType.DateTime);
		 		cmd.Parameters.Add("Tipi", pTipi, MSqlDbType.TinyInt);
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
		/// Sayfa Data Insert
		///</summary>
		public static int Insert(Sayfa p)
		{
			int rowsAffected = 0;
			using(MConnection conneciton = new MConnection(MClientProvider.MSSQL))
			{
				switch (conneciton.State)
				{
					case System.Data.ConnectionState.Closed:
						conneciton.Open();
						break;
				}
				using(MCommand cmd = new MCommand(CommandType.Text, "INSERT INTO Sayfa (HesapID,Baslik,Icerik,Dil,KayitTarihi,Tipi,Aktif) VALUES(@HesapID,@Baslik,@Icerik,@Dil,@KayitTarihi,@Tipi,@Aktif)", conneciton))
				{
					cmd.Parameters.Add("HesapID", p.HesapID, MSqlDbType.UniqueIdentifier);
		 		cmd.Parameters.Add("Baslik", p.Baslik, MSqlDbType.NVarChar);
		 		cmd.Parameters.Add("Icerik", p.Icerik, MSqlDbType.NVarChar);
		 		cmd.Parameters.Add("Dil", p.Dil, MSqlDbType.VarChar);
		 		cmd.Parameters.Add("KayitTarihi", p.KayitTarihi, MSqlDbType.DateTime);
		 		cmd.Parameters.Add("Tipi", p.Tipi, MSqlDbType.TinyInt);
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
		/// Sayfa Data Update
		///</summary>
		public static int Update(Int16 pID, System.Guid pHesapID, string pBaslik, string pIcerik, string pDil, DateTime pKayitTarihi, byte pTipi, bool pAktif)
		{
			int rowsAffected = 0;
			using(MConnection conneciton = new MConnection(MClientProvider.MSSQL))
			{
				switch (conneciton.State)
				{
					case System.Data.ConnectionState.Closed:
						conneciton.Open();
						break;
				}
				using(MCommand cmd = new MCommand(CommandType.Text, "UPDATE Sayfa SET HesapID=@HesapID,Baslik=@Baslik,Icerik=@Icerik,Dil=@Dil,KayitTarihi=@KayitTarihi,Tipi=@Tipi,Aktif=@Aktif WHERE ID=@ID", conneciton))
				{
					cmd.Parameters.Add("ID", pID, MSqlDbType.SmallInt);
		 		cmd.Parameters.Add("HesapID", pHesapID, MSqlDbType.UniqueIdentifier);
		 		cmd.Parameters.Add("Baslik", pBaslik, MSqlDbType.NVarChar);
		 		cmd.Parameters.Add("Icerik", pIcerik, MSqlDbType.NVarChar);
		 		cmd.Parameters.Add("Dil", pDil, MSqlDbType.VarChar);
		 		cmd.Parameters.Add("KayitTarihi", pKayitTarihi, MSqlDbType.DateTime);
		 		cmd.Parameters.Add("Tipi", pTipi, MSqlDbType.TinyInt);
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
		/// Sayfa Data Update
		///</summary>
		public static int Update(Sayfa p)
		{
			int rowsAffected = 0;
			using(MConnection conneciton = new MConnection(MClientProvider.MSSQL))
			{
				switch (conneciton.State)
				{
					case System.Data.ConnectionState.Closed:
						conneciton.Open();
						break;
				}
				using(MCommand cmd = new MCommand(CommandType.Text, "UPDATE Sayfa SET HesapID=@HesapID,Baslik=@Baslik,Icerik=@Icerik,Dil=@Dil,KayitTarihi=@KayitTarihi,Tipi=@Tipi,Aktif=@Aktif WHERE ID=@ID", conneciton))
				{
					cmd.Parameters.Add("ID", p.ID, MSqlDbType.SmallInt);
		 		cmd.Parameters.Add("HesapID", p.HesapID, MSqlDbType.UniqueIdentifier);
		 		cmd.Parameters.Add("Baslik", p.Baslik, MSqlDbType.NVarChar);
		 		cmd.Parameters.Add("Icerik", p.Icerik, MSqlDbType.NVarChar);
		 		cmd.Parameters.Add("Dil", p.Dil, MSqlDbType.VarChar);
		 		cmd.Parameters.Add("KayitTarihi", p.KayitTarihi, MSqlDbType.DateTime);
		 		cmd.Parameters.Add("Tipi", p.Tipi, MSqlDbType.TinyInt);
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
		/// Sayfa Data Delete
		///</summary>
		public static int Delete(Int16 pID)
		{
			int rowsAffected = 0;
			using(MConnection conneciton = new MConnection(MClientProvider.MSSQL))
			{
				switch (conneciton.State)
				{
					case System.Data.ConnectionState.Closed:
						conneciton.Open();
						break;
				}
				using(MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Sayfa WHERE ID=@ID", conneciton))
				{
					cmd.Parameters.Add("ID", pID, MSqlDbType.SmallInt);
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
		/// Sayfa Data Delete
		///</summary>
		public static int Delete(Sayfa p)
		{
			int rowsAffected = 0;
			using(MConnection conneciton = new MConnection(MClientProvider.MSSQL))
			{
				switch (conneciton.State)
				{
					case System.Data.ConnectionState.Closed:
						conneciton.Open();
						break;
				}
				using(MCommand cmd = new MCommand(CommandType.Text, "DELETE FROM Sayfa WHERE ID=@ID", conneciton))
				{
					cmd.Parameters.Add("ID", p.ID, MSqlDbType.SmallInt);
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
