using System;
using PX.Data;
using Tests.Base;

namespace Tests
{
	public class SalesPriceDetail : DetailMockBase
	{
		#region UnitPrice
		public abstract class unitPrice : PX.Data.IBqlField
		{
		}
		protected decimal? _UnitPrice;
		[PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal, "0.0")]
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
		#region DiscPct
		public abstract class discPct : PX.Data.IBqlField
		{
		}
		protected decimal? _DiscPct;
		[PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal, "0.0")]
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
		#region OrderQty
		public abstract class orderQty : PX.Data.IBqlField
		{
		}
		protected decimal? _OrderQty;
		[PXDBDecimal(2)]
		[PXDefault(TypeCode.Decimal, "0.0")]
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
	}
}