using PX.Data;

namespace RB.RapidByte
{
    public class SupplierMaint : PXGraph<SupplierMaint, Supplier>
    {
        public PXSelect<Supplier> Suppliers;
        public PXSelect<Supplier,
                   Where<Supplier.supplierID, Equal<Current<Supplier.supplierID>>>> SelectedSupplier;
        public PXSelectJoin<SupplierProduct,
                   LeftJoin<Product, On<Product.productID, Equal<SupplierProduct.productID>>>,
                   Where<SupplierProduct.supplierID, Equal<Current<Supplier.supplierID>>>> SupplierProducts;
    }
}