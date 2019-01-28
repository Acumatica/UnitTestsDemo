using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;

namespace Extensions
{
	public class Document : PXMappedCacheExtension
	{
		#region LinesTotal
		public abstract class linesTotal : PX.Data.IBqlField
		{
		}
		protected decimal? _LinesTotal;

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
