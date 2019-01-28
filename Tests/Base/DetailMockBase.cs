using System;
using PX.Data;

namespace Tests.Base
{
	public class DetailMockBase : IBqlTable
	{
		public abstract class documentMockID : PX.Data.IBqlField { }
		[PXDBInt(IsKey = true)]
		[PXDBDefault(typeof(DocumentMockBase.documentMockID))]
		public virtual Int32? DocumentMockID { get; set; }
		public abstract class detailMockID : PX.Data.IBqlField { }
		[PXLineNbr(typeof(DocumentMockBase))]
		[PXDBInt(IsKey = true)]
		public virtual Int32? DetailMockID { get; set; }
	}
}