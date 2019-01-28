using System;
using PX.Data;
using Tests.Base;

namespace Tests
{
	public class SalesPriceDocument : DocumentMockBase
	{
		#region LinesTotal
		public abstract class linesTotal : PX.Data.IBqlField
		{
		}
		protected decimal? _LinesTotal;
		[PXDBDecimal(2)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		public virtual decimal? LinesTotal
		{
			get
			{
				return this._LinesTotal;
			}
			set
			{
				this._LinesTotal = value;
			}
		}
		#endregion
	}
}