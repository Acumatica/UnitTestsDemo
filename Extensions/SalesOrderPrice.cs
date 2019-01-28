using RB.RapidByte;

namespace Extensions
{
	public class SalesOrderPrice : SalesPriceGraph<SalesOrderEntry, SalesOrder>
	{
		protected override DocumentMapping GetDocumentMapping()
		{
			return new DocumentMapping(typeof(SalesOrder));
		}

		protected override DetailMapping GetDetailMapping()
		{
			return new DetailMapping(typeof(OrderLine));
		}
	}
}