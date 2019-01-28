using System;
using PX.Data;

namespace Tests.Base
{
	public class DocumentMockBase : IBqlTable
	{
		public abstract class documentMockID : PX.Data.IBqlField { }
		[PXDBIdentity(IsKey = true)]
		public virtual Int32? DocumentMockID { get; set; }
	}
}