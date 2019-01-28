﻿namespace RB.RapidByte
{
	using System;
	using PX.Data;
	
	[System.SerializableAttribute()]
	public class Supplier : PX.Data.IBqlTable
	{
		#region SupplierID
		public abstract class supplierID : PX.Data.IBqlField
		{
		}
		protected int? _SupplierID;
		[PXDBIdentity]
		public virtual int? SupplierID
		{
			get
			{
				return this._SupplierID;
			}
			set
			{
				this._SupplierID = value;
			}
		}
		#endregion
		#region SupplierCD
		public abstract class supplierCD : PX.Data.IBqlField
		{
		}
		protected string _SupplierCD;
		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Supplier ID")]
		[PXSelector(
			typeof(Search<Supplier.supplierCD>),
			typeof(Supplier.supplierCD),
			typeof(Supplier.companyName))]
		public virtual string SupplierCD
		{
			get
			{
				return this._SupplierCD;
			}
			set
			{
				this._SupplierCD = value;
			}
		}
		#endregion
		#region CompanyName
		public abstract class companyName : PX.Data.IBqlField
		{
		}
		protected string _CompanyName;
		[PXDBString(40, IsUnicode = true)]
		[PXUIField(DisplayName = "Company Name")]
		public virtual string CompanyName
		{
			get
			{
				return this._CompanyName;
			}
			set
			{
				this._CompanyName = value;
			}
		}
		#endregion
		#region ContactName
		public abstract class contactName : PX.Data.IBqlField
		{
		}
		protected string _ContactName;
		[PXDBString(255, IsUnicode = true)]
		[PXUIField(DisplayName = "Contact Name")]
		public virtual string ContactName
		{
			get
			{
				return this._ContactName;
			}
			set
			{
				this._ContactName = value;
			}
		}
		#endregion
		#region Phone
		public abstract class phone : PX.Data.IBqlField
		{
		}
		protected string _Phone;
		[PXDBString(20, IsUnicode = true)]
		[PXUIField(DisplayName = "Phone")]
		public virtual string Phone
		{
			get
			{
				return this._Phone;
			}
			set
			{
				this._Phone = value;
			}
		}
		#endregion
		#region Fax
		public abstract class fax : PX.Data.IBqlField
		{
		}
		protected string _Fax;
		[PXDBString(20, IsUnicode = true)]
		[PXUIField(DisplayName = "Fax")]
		public virtual string Fax
		{
			get
			{
				return this._Fax;
			}
			set
			{
				this._Fax = value;
			}
		}
		#endregion
		#region Address
		public abstract class address : PX.Data.IBqlField
		{
		}
		protected string _Address;
		[PXDBString(60, IsUnicode = true)]
		[PXUIField(DisplayName = "Address")]
		public virtual string Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				this._Address = value;
			}
		}
		#endregion
		#region City
		public abstract class city : PX.Data.IBqlField
		{
		}
		protected string _City;
		[PXDBString(20, IsUnicode = true)]
		[PXUIField(DisplayName = "City")]
		public virtual string City
		{
			get
			{
				return this._City;
			}
			set
			{
				this._City = value;
			}
		}
		#endregion
		#region CountryCD
		public abstract class countryCD : PX.Data.IBqlField
		{
		}
		protected string _CountryCD;
		[PXDBString(2, IsUnicode = true)]
		[PXUIField(DisplayName = "Country ID")]
		[PXSelector(
			typeof(Search<Country.countryCD>),
			typeof(Country.countryCD),
			typeof(Country.description),
			DescriptionField = typeof(Country.description))]
		public virtual string CountryCD
		{
			get
			{
				return this._CountryCD;
			}
			set
			{
				this._CountryCD = value;
			}
		}
		#endregion
		#region Region
		public abstract class region : PX.Data.IBqlField
		{
		}
		protected string _Region;
		[PXDBString(15, IsUnicode = true)]
		[PXUIField(DisplayName = "Region")]
		public virtual string Region
		{
			get
			{
				return this._Region;
			}
			set
			{
				this._Region = value;
			}
		}
		#endregion
		#region PostalCode
		public abstract class postalCode : PX.Data.IBqlField
		{
		}
		protected string _PostalCode;
		[PXDBString(10, IsUnicode = true)]
		[PXUIField(DisplayName = "Postal Code")]
		public virtual string PostalCode
		{
			get
			{
				return this._PostalCode;
			}
			set
			{
				this._PostalCode = value;
			}
		}
		#endregion
		#region TStamp
		public abstract class tStamp : PX.Data.IBqlField
		{
		}
		protected byte[] _TStamp;
		[PXDBTimestamp()]
		public virtual byte[] TStamp
		{
			get
			{
				return this._TStamp;
			}
			set
			{
				this._TStamp = value;
			}
		}
		#endregion
		#region CreatedByID
		public abstract class createdByID : PX.Data.IBqlField
		{
		}
		protected Guid? _CreatedByID;
		[PXDBCreatedByID()]
		public virtual Guid? CreatedByID
		{
			get
			{
				return this._CreatedByID;
			}
			set
			{
				this._CreatedByID = value;
			}
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : PX.Data.IBqlField
		{
		}
		protected string _CreatedByScreenID;
		[PXDBCreatedByScreenID()]
		public virtual string CreatedByScreenID
		{
			get
			{
				return this._CreatedByScreenID;
			}
			set
			{
				this._CreatedByScreenID = value;
			}
		}
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _CreatedDateTime;
		[PXDBCreatedDateTime()]
		public virtual DateTime? CreatedDateTime
		{
			get
			{
				return this._CreatedDateTime;
			}
			set
			{
				this._CreatedDateTime = value;
			}
		}
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : PX.Data.IBqlField
		{
		}
		protected Guid? _LastModifiedByID;
		[PXDBLastModifiedByID()]
		public virtual Guid? LastModifiedByID
		{
			get
			{
				return this._LastModifiedByID;
			}
			set
			{
				this._LastModifiedByID = value;
			}
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : PX.Data.IBqlField
		{
		}
		protected string _LastModifiedByScreenID;
		[PXDBLastModifiedByScreenID()]
		public virtual string LastModifiedByScreenID
		{
			get
			{
				return this._LastModifiedByScreenID;
			}
			set
			{
				this._LastModifiedByScreenID = value;
			}
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _LastModifiedDateTime;
		[PXDBLastModifiedDateTime()]
		public virtual DateTime? LastModifiedDateTime
		{
			get
			{
				return this._LastModifiedDateTime;
			}
			set
			{
				this._LastModifiedDateTime = value;
			}
		}
		#endregion
	}
}