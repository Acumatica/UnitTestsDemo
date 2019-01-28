﻿namespace RB.RapidByte
{
	using System;
	using PX.Data;
	
	[System.SerializableAttribute()]
	public class Product : PX.Data.IBqlTable
	{
		#region ProductID
		public abstract class productID : PX.Data.IBqlField
		{
		}
		protected int? _ProductID;
		[PXDBIdentity]
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
		#region ProductCD
		public abstract class productCD : PX.Data.IBqlField
		{
		}
		protected string _ProductCD;
		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Product ID")]
		public virtual string ProductCD
		{
			get
			{
				return this._ProductCD;
			}
			set
			{
				this._ProductCD = value;
			}
		}
		#endregion
		#region ProductName
		public abstract class productName : PX.Data.IBqlField
		{
		}
		protected string _ProductName;
		[PXDBString(50, IsUnicode = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Product Name")]
		public virtual string ProductName
		{
			get
			{
				return this._ProductName;
			}
			set
			{
				this._ProductName = value;
			}
		}
		#endregion
		#region Active
		public abstract class active : PX.Data.IBqlField
		{
		}
		protected bool? _Active;
		[PXDBBool()]
		[PXDefault(true)]
		[PXUIField(DisplayName = "Active")]
		public virtual bool? Active
		{
			get
			{
				return this._Active;
			}
			set
			{
				this._Active = value;
			}
		}
		#endregion
		#region StockUnit
		public abstract class stockUnit : PX.Data.IBqlField
		{
		}
		protected string _StockUnit;
		[PXDBString(20, IsUnicode = true)]
		[PXDefault]
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
		#region MinAvailQty
		public abstract class minAvailQty : PX.Data.IBqlField
		{
		}
		protected decimal? _MinAvailQty;
		[PXDBDecimal(2)]
		[PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Min. Avail. Qty")]
		public virtual decimal? MinAvailQty
		{
			get
			{
				return this._MinAvailQty;
			}
			set
			{
				this._MinAvailQty = value;
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
        #region AvailQty
        public abstract class availQty : PX.Data.IBqlField
        {
        }
        [PXDecimal(2)]
        [PXUIField(DisplayName = "Avail. Qty", Enabled = false)]
        [PXDBScalar(
           typeof(Search<ProductQty.availQty, 
                      Where<ProductQty.productID, Equal<Product.productID>>>))]
        public virtual decimal? AvailQty { get; set; }
        #endregion
	}

    public class decimal_0 : Constant<decimal>
    {
        public decimal_0()
            : base(0)
        {
        }
    }

    [Serializable]
    public class ProductReorder : Product
    {
        #region Selected
        public abstract class selected : PX.Data.IBqlField
        {
        }
        [PXBool]
        [PXUIField(DisplayName = "Selected")]
        public virtual bool? Selected { get; set; }
        #endregion
        #region ProductID
        public new abstract class productID : PX.Data.IBqlField
        {
        }
        #endregion
        #region Active
        public new abstract class active : PX.Data.IBqlField
        {
        }
        #endregion
        #region MinAvailQty
        public new abstract class minAvailQty : PX.Data.IBqlField
        {
        }
        #endregion
        #region AvailQty
        public new abstract class availQty : PX.Data.IBqlField
        {
        }
        [PXDecimal(2)]
        [PXUIField(DisplayName = "Avail. Qty")]
        [PXDBScalar(typeof(Search<ProductQty.availQty,
                               Where<ProductQty.productID, Equal<ProductReorder.productID>>>))]
        public override decimal? AvailQty { get; set; }
        #endregion
        #region Discrepancy
        public abstract class discrepancy : PX.Data.IBqlField
        {
        }
        [PXDecimal(2)]
        [PXUIField(DisplayName = "Discrepancy")]
        [PXDBCalced(
            typeof(Minus<Sub<IsNull<ProductReorder.availQty, decimal_0>, ProductReorder.minAvailQty>>),
            typeof(Decimal))]
        public virtual decimal? Discrepancy { get; set; }
        #endregion
        #region SupplierID
        public abstract class supplierID : PX.Data.IBqlField
        {
        }
        [PXInt]
        [PXUIField(DisplayName = "Supplier ID")]
        [PXDBScalar(typeof(Search<SupplierProduct.supplierID,
                               Where<SupplierProduct.productID, Equal<ProductReorder.productID>>,
                               OrderBy<Asc<SupplierProduct.supplierPrice>>>))]
        [PXSelector(
            typeof(Search2<Supplier.supplierID,
                       InnerJoin<SupplierProduct, On<SupplierProduct.supplierID, Equal<Supplier.supplierID>>>,
                       Where<SupplierProduct.productID, Equal<Current<ProductReorder.productID>>>>),
            typeof(Supplier.supplierCD),
            typeof(Supplier.companyName),
            typeof(SupplierProduct.supplierUnit),
            typeof(SupplierProduct.supplierPrice),
            SubstituteKey = typeof(Supplier.supplierCD))]
        public virtual int? SupplierID { get; set; }
        #endregion
    }
}