﻿namespace RB.RapidByte
{
	using System;
	using PX.Data;
	
	[System.SerializableAttribute()]
	public class DocTransaction : PX.Data.IBqlTable
	{
		#region DocType
		public abstract class docType : PX.Data.IBqlField
		{
		}
		protected string _DocType;
		[PXDBString(1, IsKey = true, IsFixed = true)]
        [PXDBDefault(typeof(Document.docType))]
        [PXParent(
            typeof(Select<Document,
                       Where<Document.docType, Equal<Current<DocTransaction.docType>>,
                           And<Document.docNbr, Equal<Current<DocTransaction.docNbr>>>>>))]
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
		[PXDBString(15, IsKey = true, IsUnicode = true)]
        [PXDBDefault(typeof(Document.docNbr))]
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
		#region LineNbr
		public abstract class lineNbr : PX.Data.IBqlField
		{
		}
		protected int? _LineNbr;
		[PXDBInt(IsKey = true)]
        [PXLineNbr(typeof(Document.lastLineNbr))]
		public virtual int? LineNbr
		{
			get
			{
				return this._LineNbr;
			}
			set
			{
				this._LineNbr = value;
			}
		}
		#endregion
		#region ProductID
		public abstract class productID : PX.Data.IBqlField
		{
		}
		protected int? _ProductID;
		[PXDBInt()]
        [PXDefault()]
		[PXUIField(DisplayName = "Product ID")]
        [PXSelector(
            typeof(Search<Product.productID,
                       Where<Product.active, Equal<True>>>),
            typeof(Product.productCD),
            typeof(Product.productName),
            typeof(Product.stockUnit),
            typeof(Product.unitPrice),
            SubstituteKey = typeof(Product.productCD))]
		public virtual int? ProductID
		{
			get
			{
				return this._ProductID;
			}
			set
			{
				this._ProductID = value;
			}
		}
		#endregion
		#region TranQty
		public abstract class tranQty : PX.Data.IBqlField
		{
		}
		protected decimal? _TranQty;
		[PXDBDecimal(2)]
        [PXDefault(TypeCode.Decimal,"0.0")]
        [PXUIField(DisplayName = "Tran. Qty")]
        [PXFormula(null,
                   typeof(SumCalc<Document.totalQty>))]
		public virtual decimal? TranQty
		{
			get
			{
				return this._TranQty;
			}
			set
			{
				this._TranQty = value;
			}
		}
		#endregion
		#region Unit
		public abstract class unit : PX.Data.IBqlField
		{
		}
		protected string _Unit;
		[PXDBString(20, IsUnicode = true)]
		[PXUIField(DisplayName = "Unit")]
		public virtual string Unit
		{
			get
			{
				return this._Unit;
			}
			set
			{
				this._Unit = value;
			}
		}
		#endregion
		#region ConversionFactor
		public abstract class conversionFactor : PX.Data.IBqlField
		{
		}
		protected decimal? _ConversionFactor;
		[PXDBDecimal(1)]
        [PXDefault(TypeCode.Decimal, "1.0")]
		[PXUIField(DisplayName = "Conversion Factor")]
		public virtual decimal? ConversionFactor
		{
			get
			{
				return this._ConversionFactor;
			}
			set
			{
				this._ConversionFactor = value;
			}
		}
		#endregion
		#region StockUnit
		public abstract class stockUnit : PX.Data.IBqlField
		{
		}
		protected string _StockUnit;
		[PXDBString(20, IsUnicode = true)]
		[PXUIField(DisplayName = "Stock Unit")]
		public virtual string StockUnit
		{
			get
			{
				return this._StockUnit;
			}
			set
			{
				this._StockUnit = value;
			}
		}
		#endregion
		#region UnitPrice
		public abstract class unitPrice : PX.Data.IBqlField
		{
		}
		protected decimal? _UnitPrice;
		[PXDBDecimal(2)]
        [PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Unit Price")]
		public virtual decimal? UnitPrice
		{
			get
			{
				return this._UnitPrice;
			}
			set
			{
				this._UnitPrice = value;
			}
		}
		#endregion
		#region ExtPrice
		public abstract class extPrice : PX.Data.IBqlField
		{
		}
		protected decimal? _ExtPrice;
		[PXDBDecimal(2)]
        [PXUIField(DisplayName = "Line Total", Enabled = false)]
        [PXFormula(
            typeof(Mult<DocTransaction.tranQty, DocTransaction.unitPrice>), 
            typeof(SumCalc<Document.totalCost>))]
		public virtual decimal? ExtPrice
		{
			get
			{
				return this._ExtPrice;
			}
			set
			{
				this._ExtPrice = value;
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