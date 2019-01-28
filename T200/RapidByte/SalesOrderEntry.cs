using System;
using System.Collections;
using PX.Data;

namespace RB.RapidByte
{
	public class SalesOrderEntry : PXGraph<SalesOrderEntry, SalesOrder>
	{
		public PXSelect<SalesOrder> Orders;
		public PXSelect<OrderLine,
				   Where<OrderLine.orderNbr,
					   Equal<Current<SalesOrder.orderNbr>>>> OrderDetails;

		public PXSetup<Setup> AutoNumSetup;
					   
		public PXSelect<ProductQty> Stock;

		public SalesOrderEntry()
		{
			Setup setup = AutoNumSetup.Current;
		}
		
		protected virtual void SalesOrder_Hold_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			SalesOrder order = (SalesOrder)e.Row;
			if (order.Hold == true)
			{
				order.Status = OrderStatus.Hold;
			}
			else
			{
				order.Status = OrderStatus.Open;
			}
		}

		protected virtual void OrderLine_ProductID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			OrderLine line = (OrderLine)e.Row;
			Product product = PXSelectorAttribute.Select<OrderLine.productID>(sender, line) as Product;
			if (product != null)
			{
				line.UnitPrice = product.UnitPrice;
				line.StockUnit = product.StockUnit;
			}
			else
			{
				line.UnitPrice = null;
				line.StockUnit = null;
			}
		}

		protected virtual void OrderLine_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
		{
			OrderLine line = (OrderLine)e.Row;
			SalesOrder order = Orders.Current;
			bool isUpdated = false;

			if (line.TaxAmt != null)
			{
				order.TaxTotal += line.TaxAmt;
				isUpdated = true;
			}

			if (isUpdated)
				Orders.Update(order);
		}

		protected virtual void OrderLine_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			OrderLine newLine = (OrderLine)e.Row;
			OrderLine oldLine = (OrderLine)e.OldRow;
			SalesOrder order = Orders.Current;
			bool isUpdated = false;

			if (!sender.ObjectsEqual<OrderLine.taxAmt>(newLine, oldLine))
			{
				if (oldLine.TaxAmt != null)
				{
					order.TaxTotal -= oldLine.TaxAmt;
				}
				if (newLine.TaxAmt != null)
				{
					order.TaxTotal += newLine.TaxAmt;
				}
				isUpdated = true;
			}

			if (isUpdated)
				Orders.Update(order);
		}

		protected virtual void OrderLine_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
		{
			OrderLine line = (OrderLine)e.Row;
			SalesOrder order = Orders.Current;
			PXEntryStatus orderStatus = Orders.Cache.GetStatus(order);
			bool isDeleted = orderStatus == PXEntryStatus.InsertedDeleted ||
							 orderStatus == PXEntryStatus.Deleted;
			if (isDeleted) return;

			bool isUpdated = false;

			if (line.TaxAmt != null)
			{
				order.TaxTotal -= line.TaxAmt;
				isUpdated = true;
			}

			if (isUpdated)
				Orders.Update(order);
		}

		protected virtual void SalesOrder_LinesTotal_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			SalesOrder order = (SalesOrder)e.Row;
			order.OrderTotal = order.LinesTotal + order.TaxTotal;
		}

		protected virtual void SalesOrder_TaxTotal_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			SalesOrder order = (SalesOrder)e.Row;
			order.OrderTotal = order.LinesTotal + order.TaxTotal;
		}

		public void ApproveOrder(SalesOrder order, bool isMassProcess = false)
		{
			Orders.Current = order;
			if (order.Status == OrderStatus.Hold)
			{
				throw new PXException(String.Format("Order {0} is On Hold and cannot be approved.", order.OrderNbr));
			}
			else if (order.Status != OrderStatus.Open)
			{
				throw new PXException(String.Format("Order {0} is already approved.", order.OrderNbr));
			}
			order.Status = OrderStatus.Approved;
			Orders.Update(order);
			Persist();
			if (isMassProcess)
			{
				PXProcessing.SetInfo(String.Format("Order {0} has been successfully approved.", order.OrderNbr));
			}
		}

		public PXAction<SalesOrder> Approve;
		[PXProcessButton]
		[PXUIField(DisplayName = "Approve")]
		protected virtual IEnumerable approve(PXAdapter adapter)
		{
			foreach (SalesOrder order in adapter.Get())
			{
				Actions.PressSave();
				PXLongOperation.StartOperation(this, delegate()
				{
					SalesOrderEntry graph = PXGraph.CreateInstance<SalesOrderEntry>();
					graph.ApproveOrder(order);
				});
				yield return order;
			}
		}

		protected virtual void SalesOrder_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			SalesOrder order = (SalesOrder)e.Row;
			if (order == null) return;

			bool editable = order.Status != OrderStatus.Approved && order.Status != OrderStatus.Completed;

			Orders.Cache.AllowUpdate = editable;
			Orders.Cache.AllowDelete = editable;
			PXUIFieldAttribute.SetEnabled(sender, order, editable);
			OrderDetails.Cache.AllowDelete = editable;
			OrderDetails.Cache.AllowInsert = editable;
			OrderDetails.Cache.AllowUpdate = editable;
			Approve.SetEnabled(editable && order.Hold != true);
			Release.SetEnabled(
				order.Status == OrderStatus.Approved &&
				order.Status != OrderStatus.Completed);
		}

		public static void ReleaseOrder(SalesOrder order)
		{
			SalesOrderEntry graph = PXGraph.CreateInstance<SalesOrderEntry>();
			graph.Orders.Current = order;

			foreach (OrderLine line in graph.OrderDetails.Select())
			{
				ProductQty productQty = new ProductQty();
				productQty.ProductID = line.ProductID;
				productQty.AvailQty = -line.OrderQty;
				graph.Stock.Insert(productQty);
			}
			order.ShippedDate = graph.Accessinfo.BusinessDate;
			order.Status = OrderStatus.Completed;
			graph.Orders.Update(order);

			graph.Persist();
		}

		public PXAction<SalesOrder> Release;
		[PXProcessButton]
		[PXUIField(DisplayName = "Release")]
		protected virtual IEnumerable release(PXAdapter adapter)
		{
			SalesOrder order = Orders.Current;
			PXLongOperation.StartOperation(this, delegate()
			{
				ReleaseOrder(order);
			});
			return adapter.Get();
		}
	}
}