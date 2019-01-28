﻿namespace RB.RapidByte
{
	using System;
	using PX.Data;
	
	[System.SerializableAttribute()]
	public class SupplierProduct : PX.Data.IBqlTable
	{
		#region SupplierID
		public abstract class supplierID : PX.Data.IBqlField
		{
		}
		protected int? _SupplierID;
		[PXDBInt(IsKey = true)]
		[PXDBDefault(typeof(Supplier.supplierID))]
		[PXParent(
			typeof(Select<Supplier,
					   Where<Supplier.supplierID, 
						   Equal<Current<SupplierProduct.supplierID>>>>))]
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
		#region ProductID
		public abstract class productID : PX.Data.IBqlField
		{
		}
		protected int? _ProductID;
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Product ID")]
		[PXSelector(
			typeof(Search<Product.productID>),
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
		#region SupplierUnit
		public abstract class supplierUnit : PX.Data.IBqlField
		{
		}
		protected string _SupplierUnit;
		[PXDBString(20, IsUnicode = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Supplier Unit")]
		public virtual string SupplierUnit
		{
			get
			{
				return this._SupplierUnit;
			}
			set
			{
				this._SupplierUnit = value;
			}
		}
		#endregion
		#region ConversionFactor
		public abstract class conversionFactor : PX.Data.IBqlField
		{
		}
		protected decimal? _ConversionFactor;
		[PXDBDecimal(1)]
		[PXDefault(TypeCode.Decimal,"1.0")]
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
		#region SupplierPrice
		public abstract class supplierPrice : PX.Data.IBqlField
		{
		}
		protected decimal? _SupplierPrice;
		[PXDBDecimal(2)]
		[PXDefault(TypeCode.Decimal,"0.0")]
		[PXUIField(DisplayName = "Supplier Price")]
		public virtual decimal? SupplierPrice
		{
			get
			{
				return this._SupplierPrice;
			}
			set
			{
				this._SupplierPrice = value;
			}
		}
		#endregion
		#region LastSupplierPrice
		public abstract class lastSupplierPrice : PX.Data.IBqlField
		{
		}
		protected decimal? _LastSupplierPrice;
		[PXDBDecimal(2)]
		[PXUIField(DisplayName = "Last Supplier Price", Enabled = false)]
		public virtual decimal? LastSupplierPrice
		{
			get
			{
				return this._LastSupplierPrice;
			}
			set
			{
				this._LastSupplierPrice = value;
			}
		}
		#endregion
		#region LastPurchaseDate
		public abstract class lastPurchaseDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _LastPurchaseDate;
		[PXDBDate()]
		[PXUIField(DisplayName = "Last Purchase Date", Enabled = false)]
		public virtual DateTime? LastPurchaseDate
		{
			get
			{
				return this._LastPurchaseDate;
			}
			set
			{
				this._LastPurchaseDate = value;
			}
		}
		#endregion
		#region MinOrderQty
		public abstract class minOrderQty : PX.Data.IBqlField
		{
		}
		protected decimal? _MinOrderQty;
		[PXDBDecimal(2)]
		[PXDefault(TypeCode.Decimal,"1.0")]
		[PXUIField(DisplayName = "Min. Order Qty")]
		public virtual decimal? MinOrderQty
		{
			get
			{
				return this._MinOrderQty;
			}
			set
			{
				this._MinOrderQty = value;
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

	[SupplierDataAccumulator]
	public class SupplierData : SupplierProduct
	{
	}

	public class SupplierDataAccumulatorAttribute : PXAccumulatorAttribute
	{
		public SupplierDataAccumulatorAttribute()
		{
			_SingleRecord = true;
		}

		protected override bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
		{
			if (!base.PrepareInsert(sender, row, columns)) return false;

			SupplierData supplierData = (SupplierData)row;
			columns.Update<SupplierData.supplierPrice>(supplierData.SupplierPrice, PXDataFieldAssign.AssignBehavior.Initialize);
			columns.Update<SupplierData.supplierUnit>(supplierData.SupplierUnit, PXDataFieldAssign.AssignBehavior.Initialize);
			columns.Update<SupplierData.conversionFactor>(supplierData.ConversionFactor, PXDataFieldAssign.AssignBehavior.Initialize);
			columns.Update<SupplierData.lastSupplierPrice>(supplierData.LastSupplierPrice, PXDataFieldAssign.AssignBehavior.Replace);
			columns.Update<SupplierData.lastPurchaseDate>(supplierData.LastPurchaseDate, PXDataFieldAssign.AssignBehavior.Replace);
			return true;
		}
	}
}