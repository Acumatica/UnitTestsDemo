using FluentAssertions;
using PX.Data;
using Tests.Base;
using Xunit;

namespace Tests
{
	public class SalesOrderPriceTests : TestBase
	{
		[Fact]
		public void OrderPrice_TotalsUpdated()
		{
			var graph = PXGraph.CreateInstance<SalesPriceGraphMock>();
			var extension = graph.GetExtension<SalesPriceBindingMock>();

			var header = graph.Header.Insert(new SalesPriceDocument());
			var line = graph.Lines.Insert(new SalesPriceDetail() {UnitPrice = 10.0m, OrderQty = 5});
			line.LinePrice.Should().Be(50.0m);
			graph.Header.Current.LinesTotal.Should().Be(50.0m);

			var line2 = graph.Lines.Insert(new SalesPriceDetail() { UnitPrice = 5.0m, OrderQty = 5 });
			line2.LinePrice.Should().Be(25.0m);
			graph.Header.Current.LinesTotal.Should().Be(75.0m);

			line.UnitPrice = 20.0m;
			line.OrderQty = 10;
			line.DiscPct = 5.0m;
			line = graph.Lines.Update(line);

			line.LinePrice.Should().Be(190.0m);
			graph.Header.Current.LinesTotal.Should().Be(215.0m);

			graph.Lines.Delete(line);
			graph.Header.Current.LinesTotal.Should().Be(25.0m);
		}
	}
}