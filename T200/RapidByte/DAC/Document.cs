﻿namespace RB.RapidByte
 {
	 using System;
	 using PX.Data;

	 [System.SerializableAttribute()]
	 public class Document : PX.Data.IBqlTable
	 {
		 #region DocType
		 public abstract class docType : PX.Data.IBqlField
		 {
		 }
		 protected string _DocType;
		 [PXDBString(1, IsKey = true, IsFixed = true)]
		 [PXDefault(typeof(DocTypes.recpt))]
		 [PXUIField(DisplayName = "Type")]
		 [PXStringList(
			 new string[]
			 {
				 DocTypes.Recpt, 
				 DocTypes.Retrn
			 },
			 new string[]
			 {
				 "Receipt", 
				 "Return"
			 })]
		 public virtual string DocType
		 {
			 get
			 {
				 return this._DocType;
			 }
			 set
			 {
				 this._DocType = value;
			 }
		 }
		 #endregion
		 #region DocNbr
		 public abstract class docNbr : PX.Data.IBqlField
		 {
		 }
		 protected string _DocNbr;
		 [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		 [PXDefault()]
		 [PXUIField(DisplayName = "Reference Nbr.")]
		 [PXSelector(
			 typeof(Search<Document.docNbr,
						Where<Document.docType, Equal<Current<Document.docType>>>>),
			 typeof(Document.docType),
			 typeof(Document.docNbr),
			 typeof(Document.docDate),
			 typeof(Document.supplierID))]
		 [AutoNumber(typeof(Setup.autoNumbering))]
		 public virtual string DocNbr
		 {
			 get
			 {
				 return this._DocNbr;
			 }
			 set
			 {
				 this._DocNbr = value;
			 }
		 }
		 #endregion
		 #region DocDate
		 public abstract class docDate : PX.Data.IBqlField
		 {
		 }
		 protected DateTime? _DocDate;
		 [PXDBDate()]
		 [PXDefault(typeof(AccessInfo.businessDate))]
		 [PXUIField(DisplayName = "Date")]
		 public virtual DateTime? DocDate
		 {
			 get
			 {
				 return this._DocDate;
			 }
			 set
			 {
				 this._DocDate = value;
			 }
		 }
		 #endregion
		 #region ExtDocNbr
		 public abstract class extDocNbr : PX.Data.IBqlField
		 {
		 }
		 protected string _ExtDocNbr;
		 [PXDBString(15, IsUnicode = true)]
		 [PXUIField(DisplayName = "External Ref.")]
		 public virtual string ExtDocNbr
		 {
			 get
			 {
				 return this._ExtDocNbr;
			 }
			 set
			 {
				 this._ExtDocNbr = value;
			 }
		 }
		 #endregion
		 #region SupplierID
		 public abstract class supplierID : PX.Data.IBqlField
		 {
		 }
		 protected int? _SupplierID;
		 [PXDBInt()]
		 [PXDefault]
		 [PXUIField(DisplayName = "Supplier ID")]
		 [PXSelector(
			 typeof(Supplier.supplierID),
			 typeof(Supplier.supplierCD),
			 typeof(Supplier.companyName),
			 SubstituteKey = typeof(Supplier.supplierCD))]
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
		 #region Description
		 public abstract class description : PX.Data.IBqlField
		 {
		 }
		 protected string _Description;
		 [PXDBString(50, IsUnicode = true)]
		 [PXUIField(DisplayName = "Description")]
		 public virtual string Description
		 {
			 get
			 {
				 return this._Description;
			 }
			 set
			 {
				 this._Description = value;
			 }
		 }
		 #endregion
		 #region TotalQty
		 public abstract class totalQty : PX.Data.IBqlField
		 {
		 }
		 protected decimal? _TotalQty;
		 [PXDBDecimal(2)]
		 [PXDefault(TypeCode.Decimal, "0.0")]
		 [PXUIField(DisplayName = "Total Qty.")]
		 public virtual decimal? TotalQty
		 {
			 get
			 {
				 return this._TotalQty;
			 }
			 set
			 {
				 this._TotalQty = value;
			 }
		 }
		 #endregion
		 #region TotalCost
		 public abstract class totalCost : PX.Data.IBqlField
		 {
		 }
		 protected decimal? _TotalCost;
		 [PXDBDecimal(2)]
		 [PXDefault(TypeCode.Decimal, "0.0")]
		 [PXUIField(DisplayName = "Total Cost")]
		 public virtual decimal? TotalCost
		 {
			 get
			 {
				 return this._TotalCost;
			 }
			 set
			 {
				 this._TotalCost = value;
			 }
		 }
		 #endregion
		 #region LastLineNbr
		 public abstract class lastLineNbr : PX.Data.IBqlField
		 {
		 }
		 protected int? _LastLineNbr;
		 [PXDBInt()]
		 [PXDefault(0)]
		 public virtual int? LastLineNbr
		 {
			 get
			 {
				 return this._LastLineNbr;
			 }
			 set
			 {
				 this._LastLineNbr = value;
			 }
		 }
		 #endregion
		 #region Released
		 public abstract class released : PX.Data.IBqlField
		 {
		 }
		 protected bool? _Released;
		 [PXDBBool()]
		 [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		 [PXUIField(DisplayName = "Released")]
		 public virtual bool? Released
		 {
			 get
			 {
				 return this._Released;
			 }
			 set
			 {
				 this._Released = value;
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
		 [PXUIField(DisplayName = "Owner")]
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
		 [PXUIField(DisplayName = "Last Activity Date")]
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

	 public static class DocTypes
	 {
		 public const string Recpt = "R";
		 public class recpt : Constant<String>
		 {
			 public recpt()
				 : base(Recpt)
			 {
			 }
		 }

		 public const string Retrn = "N";
		 public class retrn : Constant<String>
		 {
			 public retrn()
				 : base(Retrn)
			 {
			 }
		 }
	 }
 }