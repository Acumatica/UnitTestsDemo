using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;

namespace Extensions
{
	public class Detail : PXMappedCacheExtension
	{
		#region LinePrice
		public abstract class linePrice : PX.Data.IBqlField
		{
		}
		protected decimal? _LinePrice;
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
		#region DiscPct
		public abstract class discPct : PX.Data.IBqlField
		{
		}
		protected decimal? _DiscPct;
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
		#region UnitPrice
		public abstract class unitPrice : PX.Data.IBqlField
		{
		}
		protected decimal? _UnitPrice;
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
