using System.Collections;
using System.Collections.Generic;
using PX.Data;

namespace RB.RapidByte
{
	public class ReceiptEntry : PXGraph<ReceiptEntry, Document>
	{
		public PXSelect<Document,
				   Where<Document.docType, Equal<Optional<Document.docType>>>> Receipts;
		public PXSelect<DocTransaction,
				   Where<DocTransaction.docType, Equal<Current<Document.docType>>,
					   And<DocTransaction.docNbr, Equal<Current<Document.docNbr>>>>,
				   OrderBy<Asc<DocTransaction.lineNbr>>> ReceiptTransactions;

		public PXSetup<Setup> AutoNumSetup;

		public PXSelect<SupplierData> SupplierRecords;
		public PXSelect<ProductQty> Stock;

		public ReceiptEntry()
		{
			Setup setup = AutoNumSetup.Current;
		}

		protected virtual void DocTransaction_ProductID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			DocTransaction line = (DocTransaction)e.Row;
			Product product = null;
			SupplierProduct supplierData = null;

			if (line.ProductID != null)
			{
				product = PXSelectorAttribute.Select<DocTransaction.productID>(sender, line) as Product;
				if (product != null)
				{
					line.Description = product.ProductName;
					line.StockUnit = product.StockUnit;

					supplierData = PXSelect<SupplierProduct,
									   Where<SupplierProduct.supplierID, Equal<Current<Document.supplierID>>,
										   And<SupplierProduct.productID, Equal<Required<Product.productID>>>>>
								   .Select(this, product.ProductID);
					if (supplierData != null)
					{
						line.Unit = supplierData.SupplierUnit;
						line.ConversionFactor = supplierData.ConversionFactor;
						line.UnitPrice = supplierData.SupplierPrice;
					}
				}
			}

			if (product == null)
			{
				sender.SetDefaultExt<DocTransaction.tranQty>(line);
				line.Description = null;
				line.StockUnit = null;
			}
			if (supplierData == null)
			{
				line.Unit = null;
				sender.SetDefaultExt<DocTransaction.conversionFactor>(line);
				line.UnitPrice = null;
			}
		}

		protected virtual void Document_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
		{
			Document doc = (Document)e.Row;
			if (sender.GetStatus(doc) == PXEntryStatus.Inserted)
			{
				AutoNumberAttribute.SetLastNumberField<Document.docNbr>(
					sender, doc,
					doc.DocType == DocTypes.Recpt ?
						typeof(Setup.receiptLastDocNbr) :
						typeof(Setup.returnLastDocNbr));
				AutoNumberAttribute.SetPrefix<Document.docNbr>(
					sender, doc,
					doc.DocType == DocTypes.Retrn ? "RET" : null);
			}
		}

		protected virtual void Document_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			Document doc = (Document)e.Row;
			if (doc == null) return;

			bool editable = doc.Released != true;

			Receipts.Cache.AllowUpdate = editable;
			Receipts.Cache.AllowDelete = editable;
			PXUIFieldAttribute.SetEnabled(sender, doc, editable);
			ReceiptTransactions.Cache.AllowDelete = editable;
			ReceiptTransactions.Cache.AllowInsert = editable;
			ReceiptTransactions.Cache.AllowUpdate = editable;
		}

		public static void ReleaseDocs(List<Document> list)
		{
			ReceiptEntry graph = PXGraph.CreateInstance<ReceiptEntry>();
			foreach (Document doc in list)
			{
				graph.Clear();
				graph.Receipts.Current = graph.Receipts.Search<Document.docNbr>(doc.DocNbr, doc.DocType);
				foreach (DocTransaction line in graph.ReceiptTransactions.Select())
				{
					ProductQty productQty = new ProductQty();
					productQty.ProductID = line.ProductID;
					if (doc.DocType == DocTypes.Recpt)
					{
						productQty.AvailQty = line.TranQty * line.ConversionFactor;
						SupplierData suppData = new SupplierData();
						suppData.SupplierID = doc.SupplierID;
						suppData.ProductID = line.ProductID;
						suppData.LastPurchaseDate = doc.DocDate;
						suppData.LastSupplierPrice = line.UnitPrice;
						suppData.ConversionFactor = line.ConversionFactor;
						suppData.SupplierUnit = line.Unit;
						graph.SupplierRecords.Insert(suppData);
					}
					else
					{
						productQty.AvailQty = -(line.TranQty * line.ConversionFactor);
					}
					graph.Stock.Insert(productQty);
				}
				doc.Released = true;
				graph.Receipts.Update(doc);
				graph.Persist();
			}
		}

		public PXAction<Document> Release;
		[PXProcessButton]
		[PXUIField(DisplayName = "Release")]
		protected virtual IEnumerable release(PXAdapter adapter)
		{
			List<Document> list = new List<Document>();
			foreach (Document doc in adapter.Get())
			{
				list.Add(doc);
			}
			Actions.PressSave();
			PXLongOperation.StartOperation(this, delegate()
			{
				ReleaseDocs(list);
			});
			return list;
		}
	}
}