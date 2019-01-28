using PX.Data;

namespace RB.RapidByte
{
	public class ProductMaint : PXGraph<ProductMaint, Product>
	{
		public PXSelect<Product> Products;

		[PXDBString(15, IsUnicode = true, IsKey = true)]
		[PXDefault]
		[PXUIField(DisplayName = "Product ID")]
		[PXSelector(
			typeof(Search<Product.productCD>),
			typeof(Product.productCD),
			typeof(Product.productName),
			typeof(Product.stockUnit),
			typeof(Product.unitPrice))]
		protected void Product_ProductCD_CacheAttached(PXCache sender)
		{
		}
	}
}