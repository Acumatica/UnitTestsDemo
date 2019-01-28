using Extensions;
using PX.Data;
using Tests.Base;

namespace Tests
{
	public class SalesPriceGraphMock : GraphMockBase<SalesPriceGraphMock>
	{
		public PXSelect<SalesPriceDocument> Header;
		public PXSelect<SalesPriceDetail> Lines;
	}

	public class SalesPriceBindingMock : SalesPriceGraph<SalesPriceGraphMock, SalesPriceDocument>
	{
		protected override DocumentMapping GetDocumentMapping()
		{
			return new DocumentMapping(typeof(SalesPriceDocument));
		}

		protected override DetailMapping GetDetailMapping()
		{
			return new DetailMapping(typeof(SalesPriceDetail));
		}
	}
}