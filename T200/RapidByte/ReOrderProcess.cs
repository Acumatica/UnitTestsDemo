using System;
using System.Collections.Generic;
using System.Linq;
using PX.Data;

namespace RB.RapidByte
{
	public class ReorderProcess : PXGraph<ReorderProcess>
	{
		[Serializable]
		public class ProductReorderFilter : IBqlTable
		{
			#region Discrepancy
			public abstract class discrepancy : PX.Data.IBqlField
			{
			}
			[PXDecimal(2)]
			[PXDefault(TypeCode.Decimal, "0.0")]
			[PXUIField(DisplayName = "Min. Discrepancy")]
			public virtual decimal? Discrepancy { get; set; }
			#endregion
			#region ActiveProducts
			public abstract class activeProducts : PX.Data.IBqlField
			{
			}
			[PXBool]
			[PXDefault(true, PersistingCheck = PXPersistingCheck.Nothing)]
			[PXUIField(DisplayName = "Active products only")]
			public virtual bool? ActiveProducts { get; set; }
			#endregion
		}

		public PXCancel<ProductReorderFilter> Cancel;

		public PXFilter<ProductReorderFilter> Filter;
		public PXFilteredProcessingJoin<ProductReorder, ProductReorderFilter,
				   LeftJoin<SupplierProduct, On<SupplierProduct.productID, Equal<ProductReorder.productID>,
					   And<SupplierProduct.supplierID, Equal<ProductReorder.supplierID>>>>,
				   Where<ProductReorder.discrepancy, Greater<decimal_0>,
					   And<ProductReorder.discrepancy, GreaterEqual<Current<ProductReorderFilter.discrepancy>>,
					   And<Where<Current<ProductReorderFilter.activeProducts>, NotEqual<True>,
						   Or<ProductReorder.active, Equal<Current<ProductReorderFilter.activeProducts>>>>>>>,
				   OrderBy<Desc<ProductReorder.discrepancy>>> Records;

		public override bool IsDirty
		{
			get
			{
				return false;
			}
		}

		public ReorderProcess()
		{
			Records.SetProcessDelegate(ReorderProducts);

			PXUIFieldAttribute.SetEnabled<ProductReorder.supplierID>(Records.Cache, null, true);
		}

		public static void ReorderProducts(List<ProductReorder> products)
		{
			ReceiptEntry graph = PXGraph.CreateInstance<ReceiptEntry>();
			bool erroroccurred = false;
			List<ProductReorder> productsToProceed = products.OrderBy(item => item.SupplierID).ToList();
			List<ProductReorder> pendingProducts = new List<ProductReorder>();
			Document doc = null;
			int? pendingSupplierID = null;

			foreach (ProductReorder product in productsToProceed)
			{
				try
				{
					pendingProducts.Add(product);

					if (product.SupplierID == null)
						throw new PXException("Supplier has not been specified.");

					if (doc == null || product.SupplierID != pendingSupplierID)
					{
						doc = new Document();
						doc.DocType = DocTypes.Recpt;
						doc.DocDate = graph.Accessinfo.BusinessDate;
						doc.SupplierID = product.SupplierID;
						doc = graph.Receipts.Insert(doc);
						pendingSupplierID = product.SupplierID;
					}
					DocTransaction tran = new DocTransaction();
					tran.DocType = doc.DocType;
					tran.DocNbr = doc.DocNbr;
					tran.ProductID = product.ProductID;
					tran.TranQty = product.Discrepancy;
					graph.ReceiptTransactions.Insert(tran);

					int nextProductIndex = productsToProceed.IndexOf(product) + 1;
					if (productsToProceed.Count == nextProductIndex ||
						productsToProceed[nextProductIndex].SupplierID != product.SupplierID)
					{
						graph.Actions.PressSave();
						foreach (ProductReorder pendingProduct in pendingProducts)
						{
							PXProcessing<ProductReorder>.SetInfo(
								products.IndexOf(pendingProduct),
								String.Format("The receipt {0} has been created", doc.DocNbr));
						}
						pendingProducts.Clear();
						graph.Clear();
					}
				}
				catch (Exception)
				{
					erroroccurred = true;
					foreach (ProductReorder pendingProduct in pendingProducts)
					{
						PXProcessing<ProductReorder>.SetError(
							products.IndexOf(pendingProduct),
							"A receipt cannot be created");
					}
					pendingProducts.Clear();
					graph.Clear();
					doc = null;
				}
			}
			if (erroroccurred)
				throw new PXException("At least one product hasn't been processed.");
		}
	}
}