using System;
using PX.Data;

namespace Extensions
{
	public abstract class SalesPriceGraph<TGraph, TPrimary> : PXGraphExtension<TGraph>
		where TGraph : PXGraph
		where TPrimary : class, IBqlTable, new()
	{
		protected class DocumentMapping : IBqlMapping
		{
			protected Type _table;
			public Type Table => _table;
			public Type Extension => typeof(Document);

			public DocumentMapping(Type table)
			{
				_table = table;
			}

			public Type LinesTotal => typeof(Document.linesTotal);
		}

		protected class DetailMapping : IBqlMapping
		{
			protected Type _table;
			public Type Table => _table;
			public Type Extension => typeof(Detail);

			public DetailMapping(Type table)
			{
				_table = table;
			}

			public Type LinePrice => typeof(Detail.linePrice);
			public Type DiscPct => typeof(Detail.discPct);
			public Type UnitPrice => typeof(Detail.unitPrice);
			public Type OrderQty => typeof(Detail.orderQty);
		}

		protected abstract DocumentMapping GetDocumentMapping();
		protected abstract DetailMapping GetDetailMapping();

		public PXSelectExtension<Document> Documents;
		public PXSelectExtension<Detail> Details;

		protected decimal? CalcLinePrice(decimal? unitPrice, decimal? qty, decimal? discount)
		{
			return unitPrice * qty * (1 - discount / 100);
		}

		protected virtual void _(Events.FieldUpdated<Detail, Detail.unitPrice> e)
		{
			var newPrice = CalcLinePrice(e.Row.UnitPrice, e.Row.OrderQty, e.Row.DiscPct);
			e.Cache.SetValue<Detail.linePrice>(e.Row, newPrice);
		}

		protected virtual void _(Events.FieldUpdated<Detail, Detail.orderQty> e)
		{
			var newPrice = CalcLinePrice(e.Row.UnitPrice, e.Row.OrderQty, e.Row.DiscPct);
			e.Cache.SetValue<Detail.linePrice>(e.Row, newPrice);
		}

		protected virtual void _(Events.FieldUpdated<Detail, Detail.discPct> e)
		{
			var newPrice = CalcLinePrice(e.Row.UnitPrice, e.Row.OrderQty, e.Row.DiscPct);
			e.Cache.SetValue<Detail.linePrice>(e.Row, newPrice);
		}

		protected virtual void _(Events.RowInserted<Detail> e)
		{
			Detail line = e.Row;
			Document order = Documents.Current;
			
			if (line.LinePrice != null)
			{
				var newTotal = order.LinesTotal + line.LinePrice;
				Documents.Cache.SetValue<Document.linesTotal>(order, newTotal);
			}
		}

		protected virtual void _(Events.RowUpdated<Detail> e)
		{
			Detail newLine = e.Row;
			Detail oldLine = e.OldRow;
			Document order = Documents.Current;
			
			if (!e.Cache.ObjectsEqual<Detail.linePrice>(newLine, oldLine))
			{
				var newTotal = order.LinesTotal;
				if (oldLine.LinePrice != null)
				{
					newTotal -= oldLine.LinePrice;
				}
				if (newLine.LinePrice != null)
				{
					newTotal += newLine.LinePrice;
				}
				Documents.Cache.SetValue<Document.linesTotal>(order, newTotal);
			}
		}

		protected virtual void _(Events.RowDeleted<Detail> e)
		{
			Detail line = e.Row;
			Document order = Documents.Current;
			PXEntryStatus orderStatus = Documents.Cache.GetStatus(order);
			bool isDeleted = orderStatus == PXEntryStatus.InsertedDeleted ||
							 orderStatus == PXEntryStatus.Deleted;
			if (isDeleted) return;

			if (line.LinePrice != null)
			{
				var newTotal = order.LinesTotal - line.LinePrice;
				Documents.Cache.SetValue<Document.linesTotal>(order, newTotal);
			}
		}
	}
}
