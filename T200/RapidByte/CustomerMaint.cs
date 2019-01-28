using PX.Data;

namespace RB.RapidByte
{
    public class CustomerMaint : PXGraph<CustomerMaint, Customer>
    {
        public PXSelect<Customer> Customers;

        protected void Customer_CountryCD_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
        {
            Customer row = (Customer)e.Row;
            row.Region = null;
        }
    }
}