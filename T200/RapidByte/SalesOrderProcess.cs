using PX.Data;

namespace RB.RapidByte
{
    public class SalesOrderProcess : PXGraph<SalesOrderProcess>
    {
        public PXCancel<SalesOrder> Cancel;

        public PXProcessing<SalesOrder> Orders;

        public SalesOrderProcess()
        {
            Orders.SetProcessCaption("Approve");
            Orders.SetProcessAllCaption("Approve All");
            Orders.SetProcessDelegate<SalesOrderEntry>(delegate(SalesOrderEntry graph, SalesOrder order)
            {
                graph.Clear();
                graph.ApproveOrder(order, true);
            });
        }
    }
}