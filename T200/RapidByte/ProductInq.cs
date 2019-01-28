using System;
using System.Collections;
using System.Collections.Generic;
using PX.SM;
using PX.Data;


namespace RB.RapidByte
{
    public class ProductInq : PXGraph<ProductInq>
    {
        //used as PrimaryView
        public PXFilter<ProductFilter> Filter;
        public PXCancel<ProductFilter> Cancel;
        // Default query
        // Filtering parameters are specified in BQL
        public PXSelectReadonly2<Account,
                                 LeftJoin<SalesOrder, On<SalesOrder.customerAccountID, Equal<Account.accountID>>,
                                 LeftJoin<OrderDetail, On<SalesOrder.orderNbr, Equal<OrderDetail.orderNbr>>,
                                 LeftJoin<Product, On<Product.productID, Equal<OrderDetail.productID>>>>>,
                                 Where2<
                                     Where<Current<ProductFilter.customerID>, IsNull,
                                           Or<SalesOrder.customerAccountID, Equal<Current<ProductFilter.customerID>>>>,
                                     And2<Where<Current<ProductFilter.categoryCD>, IsNull,
                                                Or<Product.categoryCD, Equal<Current<ProductFilter.categoryCD>>>>,
                                     And<Account.companyType, Equal<CompanyTypes.customer>>>>,
                                 OrderBy<Asc<Account.accountCD, Asc<Product.categoryCD>>>> ProductRecords;

        protected virtual IEnumerable productRecords()
        {
            ProductFilter filter = Filter.Current as ProductFilter;
            switch (filter.Aggregated)
            {
                case 1: // Sum Ext Price
                    // Aggregation specified in BQL
                    PXSelectBase<Account> query = 
                        new PXSelectJoinGroupBy<Account,
                                                LeftJoin<SalesOrder, On<SalesOrder.customerAccountID, Equal<Account.accountID>>,
                                                LeftJoin<OrderDetail, On<SalesOrder.orderNbr, Equal<OrderDetail.orderNbr>>,
                                                LeftJoin<Product, On<Product.productID, Equal<OrderDetail.productID>>>>>,
                                                Where<Account.companyType, Equal<CompanyTypes.customer>>,
                                                Aggregate<GroupBy<Account.accountID, 
                                                          GroupBy<Product.categoryCD, 
                                                          Sum<OrderDetail.extPrice>>>>>(this);
                    // Filtering parameters added dynamically
                    if (filter.CustomerID != null)
                        query.WhereAnd<Where<SalesOrder.customerAccountID, Equal<Current<ProductFilter.customerID>>>>();
                    if (filter.CategoryCD != null)
                        query.WhereAnd<Where<Product.categoryCD, Equal<Current<ProductFilter.categoryCD>>>>();
                    // Constructing each PXResult<> from the retrieved aggregated data
                    PXResultset<Account, OrderDetail, Product> res = new PXResultset<Account, OrderDetail, Product>();
                    // We need Product, so we have to list all classes that go before Account in PXSelect
                    // Including SalesOrder, even if we don't use it within foreach
                    foreach (PXResult<Account, SalesOrder, OrderDetail, Product> p in query.Select())
                    {
                        Product p1 = (Product)p;
                        OrderDetail d1 = (OrderDetail)p;
                        Account a1 = (Account)p;
                        // Create new objects to have empty values in fields that aren't aggregated
                        Product resultProd = new Product();
                        OrderDetail resultDetail = new OrderDetail();
                        resultProd.CategoryCD = p1.CategoryCD;
                        resultDetail.ExtPrice = d1.ExtPrice;
                        // a1 passed directly since we need three fields of Account (key fields) 
                        // and we don't need empty values in Account
                        res.Add(new PXResult<Account, OrderDetail, Product>(a1, resultDetail,resultProd));
                    }
                    return res;
                case 2: // Calculate and sum up amounts of order details
                    query = 
                        new PXSelectJoin<Account,
                                         LeftJoin<SalesOrder, On<SalesOrder.customerAccountID, Equal<Account.accountID>>,
                                         LeftJoin<OrderDetail, On<SalesOrder.orderNbr, Equal<OrderDetail.orderNbr>>,
                                         LeftJoin<Product, On<Product.productID, Equal<OrderDetail.productID>>>>>,
                                         Where<Account.companyType, Equal<CompanyTypes.customer>>,
                                         // Sorting is applied to retrieve records here;
                                         // in the UI the records are sorted as specified in the data view
                                         OrderBy<Asc<SalesOrder.customerAccountID, Asc<Product.categoryCD>>>>(this);
                    if (filter.CustomerID != null)
                        query.WhereAnd<Where<SalesOrder.customerAccountID, Equal<Current<ProductFilter.customerID>>>>();
                    if (filter.CategoryCD != null)
                        query.WhereAnd<Where<Product.categoryCD, Equal<Current<ProductFilter.categoryCD>>>>();
                    // Constructing a new PXResultset with calculated aggregates
                    res = new PXResultset<Account, OrderDetail, Product>();
                    Product pendingProd = null;
                    Account pendingAccount = null;
                    decimal? amtSum = 0;
                    foreach (PXResult<Account, SalesOrder, OrderDetail, Product> p in query.Select())
                    {
                        Product p1 = (Product)p;
                        OrderDetail od1 = (OrderDetail)p;
                        SalesOrder o1 = (SalesOrder)p;
                        Account a1 = (Account)p;
                        if (pendingProd != null && pendingAccount != null && (p1.CategoryCD != pendingProd.CategoryCD ||
                            o1.CustomerAccountID != pendingAccount.AccountID))
                        {
                            // Create new objects to have empty values in fields that aren't aggregated
                            Product resultProd = new Product();
                            resultProd.CategoryCD = pendingProd.CategoryCD;
                            OrderDetail resultDetail = new OrderDetail();
                            resultDetail.ExtPrice = amtSum;
                            res.Add(new PXResult<Account, OrderDetail, Product>(a1, resultDetail, resultProd));
                            amtSum = 0;
                        }
                        pendingProd = p1;
                        pendingAccount = a1;
                        amtSum += od1.UnitPrice * od1.OrderDetailQty;
                    }
                    if (pendingProd != null && pendingAccount != null)
                    {
                        Product resultProd = new Product();
                        resultProd.CategoryCD = pendingProd.CategoryCD;
                        OrderDetail resultDetail = new OrderDetail();
                        resultDetail.ExtPrice = amtSum;
                        res.Add(new PXResult<Account, OrderDetail, Product>(pendingAccount, resultDetail, resultProd));
                    }
                    return res;
                default: // 0
                    return null;
            }
          
        }

        public PXAction<ProductFilter> ViewCustomer;
        [PXButton]
        [PXUIField(DisplayName = "Customer Details")]
        protected virtual void viewCustomer()
        {
            Account row = ProductRecords.Current;
            CustomerMaint graph = PXGraph.CreateInstance<CustomerMaint>();
            graph.Customers.Current = graph.Customers.Search<Account.accountID>(row.AccountID);
            if (graph.Customers.Current != null)
            {
                throw new PXRedirectRequiredException(graph, true, null);
            }

        }
    }

    // A filter is an ordinary DAC that consists of unbound data fields
    // List filter parameters as fields of this class
    [Serializable]
    public class ProductFilter : PX.Data.IBqlTable
    {
        #region CategoryCD
        public abstract class categoryCD : PX.Data.IBqlField
        {
        }
        [PXString(15, IsUnicode = true)]
        [PXUIField(DisplayName = " Product Category")]
        [PXSelector(typeof(Category.categoryCD))]
        public virtual string CategoryCD { get; set; }
        #endregion

        #region CustomerID
        public abstract class customerID : PX.Data.IBqlField
        {
        }
        [PXInt]
        [PXUIField(DisplayName = "Customer ID")]
        [PXSelector(typeof(Search<Account.accountID,
                               Where<Account.companyType, Equal<CompanyTypes.customer>>>),
                    DescriptionField = typeof(Account.accountCD),
                    SelectorMode = PXSelectorMode.DisplayModeText)]
        public virtual int? CustomerID { get; set; }
        #endregion

        #region Aggregated
        public abstract class aggregated : PX.Data.IBqlField
        {
        }
        [PXInt]
        [PXIntList(new int[] {0, 1, 2}, 
                   new string[] {"View All", "Sum Ext. Price", "Sum Amount"})]
        [PXDefault(0)]
        [PXUIField(DisplayName = "Aggregated")]
        public virtual int? Aggregated { get; set; }
        #endregion
    }
}