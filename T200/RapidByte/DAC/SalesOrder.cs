﻿namespace RB.RapidByte
 {
	 using System;
	 using PX.Data;

	 [System.SerializableAttribute()]
	 public class SalesOrder : PX.Data.IBqlTable
	 {
		 #region Selected
		 public abstract class selected : IBqlField
		 {
		 }
		 [PXBool]
		 [PXUIField(DisplayName = "Selected")]
		 public virtual bool? Selected { get; set; }
		 #endregion
		 #region OrderNbr
		 public abstract class orderNbr : PX.Data.IBqlField
		 {
		 }
		 protected string _OrderNbr;
		 [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
		 [PXDefault()]
		 [PXUIField(DisplayName = "Order Nbr.")]
		 [PXSelector(
			 typeof(Search<SalesOrder.orderNbr>),
			 typeof(SalesOrder.orderNbr),
			 typeof(SalesOrder.orderDate),
			 typeof(SalesOrder.status),
			 typeof(SalesOrder.customerID))]
		 [AutoNumber(typeof(Setup.autoNumbering), typeof(Setup.salesOrderLastNbr))]
		 public virtual string OrderNbr
		 {
			 get
			 {
				 return this._OrderNbr;
			 }
			 set
			 {
				 this._OrderNbr = value;
			 }
		 }
		 #endregion
		 #region OrderDate
		 public abstract class orderDate : PX.Data.IBqlField
		 {
		 }
		 protected DateTime? _OrderDate;
		 [PXDBDate()]
		 [PXDefault(typeof(AccessInfo.businessDate))]
		 [PXUIField(DisplayName = "Order Date")]
		 public virtual DateTime? OrderDate
		 {
			 get
			 {
				 return this._OrderDate;
			 }
			 set
			 {
				 this._OrderDate = value;
			 }
		 }
		 #endregion
		 #region Status
		 public abstract class status : PX.Data.IBqlField
		 {
		 }
		 protected string _Status;
		 [PXDBString(1, IsFixed = true)]
		 [PXDefault(OrderStatus.Open)]
		 [PXUIField(DisplayName = "Status")]
		 [PXStringList(
			 new string[]
			 {
				 OrderStatus.Open, 
				 OrderStatus.Hold, 
				 OrderStatus.Approved, 
				 OrderStatus.Completed
			 },
			 new string[]
			 {
				 OrderStatus.UI.Open, 
				 OrderStatus.UI.Hold, 
				 OrderStatus.UI.Approved, 
				 OrderStatus.UI.Completed
			 })]
		 public virtual string Status
		 {
			 get
			 {
				 return this._Status;
			 }
			 set
			 {
				 this._Status = value;
			 }
		 }
		 #endregion
		 #region Hold
		 public abstract class hold : PX.Data.IBqlField
		 {
		 }
		 protected bool? _Hold;
		 [PXDBBool()]
		 [PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		 [PXUIField(DisplayName = "Hold")]
		 public virtual bool? Hold
		 {
			 get
			 {
				 return this._Hold;
			 }
			 set
			 {
				 this._Hold = value;
			 }
		 }
		 #endregion
		 #region CustomerID
		 public abstract class customerID : PX.Data.IBqlField
		 {
		 }
		 protected int? _CustomerID;
		 [PXDBInt()]
		 [PXDefault]
		 [PXUIField(DisplayName = "Customer ID")]
		 [PXSelector(
			 typeof(Customer.customerID),
			 typeof(Customer.customerCD),
			 typeof(Customer.companyName),
			 SubstituteKey = typeof(Customer.customerCD))]
		 public virtual int? CustomerID
		 {
			 get
			 {
				 return this._CustomerID;
			 }
			 set
			 {
				 this._CustomerID = value;
			 }
		 }
		 #endregion
		 #region RequiredDate
		 public abstract class requiredDate : PX.Data.IBqlField
		 {
		 }
		 protected DateTime? _RequiredDate;
		 [PXDBDate()]
		 [PXDefault(
			 typeof(AccessInfo.businessDate),
			 PersistingCheck = PXPersistingCheck.Nothing)]
		 [PXUIField(DisplayName = "Required Date")]
		 public virtual DateTime? RequiredDate
		 {
			 get
			 {
				 return this._RequiredDate;
			 }
			 set
			 {
				 this._RequiredDate = value;
			 }
		 }
		 #endregion
		 #region ShippedDate
		 public abstract class shippedDate : PX.Data.IBqlField
		 {
		 }
		 protected DateTime? _ShippedDate;
		 [PXDBDate()]
		 [PXUIField(DisplayName = "Shipped Date")]
		 public virtual DateTime? ShippedDate
		 {
			 get
			 {
				 return this._ShippedDate;
			 }
			 set
			 {
				 this._ShippedDate = value;
			 }
		 }
		 #endregion
		 #region Description
		 public abstract class description : PX.Data.IBqlField
		 {
		 }
		 protected string _Description;
		 [PXDBString(50, IsUnicode = true)]
		 [PXUIField(DisplayName = "Description")]
		 public virtual string Description
		 {
			 get
			 {
				 return this._Description;
			 }
			 set
			 {
				 this._Description = value;
			 }
		 }
		 #endregion
		 #region LinesTotal
		 public abstract class linesTotal : PX.Data.IBqlField
		 {
		 }
		 protected decimal? _LinesTotal;
		 [PXDBDecimal(2)]
		 [PXDefault(TypeCode.Decimal, "0.0")]
		 [PXUIField(DisplayName = "Lines Total")]
		 public virtual decimal? LinesTotal
		 {
			 get
			 {
				 return this._LinesTotal;
			 }
			 set
			 {
				 this._LinesTotal = value;
			 }
		 }
		 #endregion
		 #region TaxTotal
		 public abstract class taxTotal : PX.Data.IBqlField
		 {
		 }
		 protected decimal? _TaxTotal;
		 [PXDBDecimal(2)]
		 [PXDefault(TypeCode.Decimal, "0.0")]
		 [PXUIField(DisplayName = "Tax Total")]
		 public virtual decimal? TaxTotal
		 {
			 get
			 {
				 return this._TaxTotal;
			 }
			 set
			 {
				 this._TaxTotal = value;
			 }
		 }
		 #endregion
		 #region OrderTotal
		 public abstract class orderTotal : PX.Data.IBqlField
		 {
		 }
		 protected decimal? _OrderTotal;
		 [PXDBDecimal(2)]
		 [PXDefault(TypeCode.Decimal, "0.0")]
		 [PXUIField(DisplayName = "Order Total")]
		 public virtual decimal? OrderTotal
		 {
			 get
			 {
				 return this._OrderTotal;
			 }
			 set
			 {
				 this._OrderTotal = value;
			 }
		 }
		 #endregion
		 #region TStamp
		 public abstract class tStamp : PX.Data.IBqlField
		 {
		 }
		 protected byte[] _TStamp;
		 [PXDBTimestamp()]
		 public virtual byte[] TStamp
		 {
			 get
			 {
				 return this._TStamp;
			 }
			 set
			 {
				 this._TStamp = value;
			 }
		 }
		 #endregion
	 }

	 public class OrderStatus
	 {
		 public const string Open = "O";
		 public const string Hold = "H";
		 public const string Approved = "A";
		 public const string Completed = "C";

		 public class UI
		 {
			 public const string Open = "Open";
			 public const string Hold = "On Hold";
			 public const string Approved = "Approved";
			 public const string Completed = "Completed";
		 }
	 }
 }