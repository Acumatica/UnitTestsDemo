﻿namespace RB.RapidByte
{
	using System;
	using PX.Data;
	
	[System.SerializableAttribute()]
	public class OrderLine : PX.Data.IBqlTable
	{
		#region OrderNbr
		public abstract class orderNbr : PX.Data.IBqlField
		{
		}
		protected string _OrderNbr;
        [PXDBString(15, IsKey = true, IsUnicode = true)]
        [PXDBDefault(typeof(SalesOrder.orderNbr))]
        [PXParent(typeof(Select<SalesOrder,
                             Where<SalesOrder.orderNbr,
                                 Equal<Current<OrderLine.orderNbr>>>>))]
		public virtual string OrderNbr
		{
			get
			{
				return this._OrderNbr;
			}
			set
			{
				this._OrderNbr = value;
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
		#region UnitPrice
		public abstract class unitPrice : PX.Data.IBqlField
		{
		}
		protected decimal? _UnitPrice;
		[PXDBDecimal(6)]
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
		#region OrderQty
		public abstract class orderQty : PX.Data.IBqlField
		{
		}
		protected decimal? _OrderQty;
        [PXDBDecimal(2)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Quantity")]
        public virtual decimal? OrderQty
		{
			get
			{
				return this._OrderQty;
			}
			set
			{
				this._OrderQty = value;
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
		#region TaxAmt
		public abstract class taxAmt : PX.Data.IBqlField
		{
		}
		protected decimal? _TaxAmt;
        [PXDBDecimal(2)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Tax Amount")]
		public virtual decimal? TaxAmt
		{
			get
			{
				return this._TaxAmt;
			}
			set
			{
				this._TaxAmt = value;
			}
		}
		#endregion
		#region DiscPct
		public abstract class discPct : PX.Data.IBqlField
		{
		}
		protected decimal? _DiscPct;
        [PXDBDecimal(6)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Discount")]
		public virtual decimal? DiscPct
		{
			get
			{
				return this._DiscPct;
			}
			set
			{
				this._DiscPct = value;
			}
		}
		#endregion
		#region LinePrice
		public abstract class linePrice : PX.Data.IBqlField
		{
		}
		protected decimal? _LinePrice;
        [PXDBDecimal(2)]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Ext. Price", Enabled = false)]
		public virtual decimal? LinePrice
		{
			get
			{
				return this._LinePrice;
			}
			set
			{
				this._LinePrice = value;
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