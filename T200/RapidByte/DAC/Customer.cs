﻿namespace RB.RapidByte
 {
	 using System;
	 using PX.Data;

	 [System.SerializableAttribute()]
	 public class Customer : PX.Data.IBqlTable
	 {
		 #region CustomerID
		 public abstract class customerID : PX.Data.IBqlField
		 {
		 }
		 protected int? _CustomerID;
		 [PXDBIdentity]
		 public virtual int? CustomerID
		 {
			 get
			 {
				 return this._CustomerID;
			 }
			 set
			 {
				 this._CustomerID = value;
			 }
		 }
		 #endregion
		 #region CustomerCD
		 public abstract class customerCD : PX.Data.IBqlField
		 {
		 }
		 protected string _CustomerCD;
		 [PXDBString(15, IsUnicode = true, IsKey = true)]
		 [PXDefault]
		 [PXUIField(DisplayName = "Customer ID")]
		 [PXSelector(
			 typeof(Search<Customer.customerCD>),
			 typeof(Customer.customerCD),
			 typeof(Customer.companyName))]
		 public virtual string CustomerCD
		 {
			 get
			 {
				 return this._CustomerCD;
			 }
			 set
			 {
				 this._CustomerCD = value;
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
	 }
 }