using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PX.Data;
using PX.Objects.ExtensionTests;
using RB.RapidByte;
using Tests.Base;
using Xunit;
using FluentAssertions;

namespace Tests
{
    public class SalesOrderTests : TestBase
    {
		//inserting setup tables into cache on graph initialization
		private void Entry_OnPrepare(PXGraph sender)
		{
			sender.Caches[typeof(Setup)].Current = new Setup();
		}

		[Fact]
	    public void Order_StatusUpdatedWhenHoldUpdated()
	    {
		    PXGraph.OnPrepare += Entry_OnPrepare;
		    var soe = PXGraph.CreateInstance<SalesOrderEntry>();
		    PXGraph.OnPrepare -= Entry_OnPrepare;
		    foreach (PXCache cache in soe.Caches.Values)
		    {
			    cache.Interceptor = new PXUIEmulatorAttribute();
		    }

		    SalesOrder SO1 = soe.Orders.Insert(new SalesOrder());
		    SO1.Status.Should().Be(OrderStatus.Open, "because new order should be created Open");
		    SO1.Hold.Should().Be(false, "because new order should be created not on hold");

		    SO1.Hold = true;
		    SO1 = soe.Orders.Update(SO1);
		    SO1.Status.Should().Be(OrderStatus.Hold, "because setting order On Hold should update status");

		    SO1.Hold = false;
		    SO1 = soe.Orders.Update(SO1);
		    SO1.Status.Should().Be(OrderStatus.Open, "because setting order off hold should change status to Open");
		}

		[Fact]
	    public void OrderPrice_TotalsUpdated()
	    {
		    PXGraph.OnPrepare += Entry_OnPrepare;
		    var soe = PXGraph.CreateInstance<SalesOrderEntry>();
		    PXGraph.OnPrepare -= Entry_OnPrepare;
		    foreach (PXCache cache in soe.Caches.Values)
		    {
			    cache.Interceptor = new PXUIEmulatorAttribute();
		    }

		    SalesOrder SO1 = soe.Orders.Insert(new SalesOrder());
		    OrderLine line = soe.OrderDetails.Insert(new OrderLine());
		    line.UnitPrice = 20.0m;
		    line.OrderQty = 10;
		    line.DiscPct = 5.0m;
		    line.TaxAmt = 10.0m;
		    line = soe.OrderDetails.Update(line);
			line.LinePrice.Should().Be(190.0m);
			soe.Orders.Current.LinesTotal.Should().Be(190.0m);
		    soe.Orders.Current.TaxTotal.Should().Be(10.0m);
		}

	}
}
