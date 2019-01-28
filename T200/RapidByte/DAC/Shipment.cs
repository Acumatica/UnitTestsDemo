﻿namespace RB.RapidByte
{
	using System;
	using PX.Data;
	
	[System.SerializableAttribute()]
	public class Shipment : PX.Data.IBqlTable
	{
		#region ShipmentNbr
		public abstract class shipmentNbr : PX.Data.IBqlField
		{
		}
		protected string _ShipmentNbr;
		[PXDBString(10, IsKey = true, IsUnicode = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Shipment Nbr.")]
		[PXSelector(
			typeof(Search<Shipment.shipmentNbr>),
			typeof(Shipment.shipmentNbr),
			typeof(Shipment.customerID),
			typeof(Shipment.status))]
		public virtual string ShipmentNbr
		{
			get
			{
				return this._ShipmentNbr;
			}
			set
			{
				this._ShipmentNbr = value;
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
		#region ShipmentType
		public abstract class shipmentType : PX.Data.IBqlField
		{
		}
		protected string _ShipmentType;
		[PXDBString(1, IsFixed = true)]
		[PXDefault(ShipmentTypes.Single)]
		[PXUIField(DisplayName = "Delivery Type")]
		[PXStringList(
			new string[] 
			{
				ShipmentTypes.Single, 
				ShipmentTypes.Multiple
			},
			new string[]
			{
				"Single", 
				"Multiple"
			})]
		public virtual string ShipmentType
		{
			get
			{
				return this._ShipmentType;
			}
			set
			{
				this._ShipmentType = value;
			}
		}
		#endregion
		#region ShipmentDate
		public abstract class shipmentDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _ShipmentDate;
		[PXDBDate()]
		[PXDefault(typeof(AccessInfo.businessDate))]
		[PXUIField(DisplayName = "Shipment Date")]
		public virtual DateTime? ShipmentDate
		{
			get
			{
				return this._ShipmentDate;
			}
			set
			{
				this._ShipmentDate = value;
			}
		}
		#endregion
		#region Status
		public abstract class status : PX.Data.IBqlField
		{
		}
		protected string _Status;
		[PXDBString(1, IsFixed = true)]
		[PXDefault(ShipmentStatus.OnHold)]
		[PXUIField(DisplayName = "Status")]
		[PXStringList(
			new string[]
			{
				ShipmentStatus.OnHold, 
				ShipmentStatus.Shipping, 
				ShipmentStatus.Cancelled, 
				ShipmentStatus.Delivered
			},
			new string[]
			{
				"On Hold", 
				"Shipping", 
				"Canceled", 
				"Delivered"
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
		#region DeliveryMaxDate
		public abstract class deliveryMaxDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _DeliveryMaxDate;
		[PXDBDate()]
		[PXUIField(DisplayName = "Delivery Before")]
		public virtual DateTime? DeliveryMaxDate
		{
			get
			{
				return this._DeliveryMaxDate;
			}
			set
			{
				this._DeliveryMaxDate = value;
			}
		}
		#endregion
		#region DeliveryDate
		public abstract class deliveryDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _DeliveryDate;
		[PXDBDate()]
		[PXUIField(DisplayName = "Delivered On")]
		public virtual DateTime? DeliveryDate
		{
			get
			{
				return this._DeliveryDate;
			}
			set
			{
				this._DeliveryDate = value;
			}
		}
		#endregion
		#region TotalQty
		public abstract class totalQty : PX.Data.IBqlField
		{
		}
		protected decimal? _TotalQty;
		[PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Total Qty.")]
		public virtual decimal? TotalQty
		{
			get
			{
				return this._TotalQty;
			}
			set
			{
				this._TotalQty = value;
			}
		}
		#endregion
		#region ShippedQty
		public abstract class shippedQty : PX.Data.IBqlField
		{
		}
		protected decimal? _ShippedQty;
		[PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Shipped Qty.")]
		public virtual decimal? ShippedQty
		{
			get
			{
				return this._ShippedQty;
			}
			set
			{
				this._ShippedQty = value;
			}
		}
		#endregion
		#region PendingQty
		public abstract class pendingQty : PX.Data.IBqlField
		{
		}
		protected decimal? _PendingQty;
		[PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal, "0.0")]
		[PXUIField(DisplayName = "Pending Qty.")]
		public virtual decimal? PendingQty
		{
			get
			{
				return this._PendingQty;
			}
			set
			{
				this._PendingQty = value;
			}
		}
		#endregion
		#region Description
		public abstract class description : PX.Data.IBqlField
		{
		}
		protected string _Description;
		[PXDBString(255, IsUnicode = true)]
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
		#region CreatedByID
		public abstract class createdByID : PX.Data.IBqlField
		{
		}
		protected Guid? _CreatedByID;
		[PXDBCreatedByID()]
		public virtual Guid? CreatedByID
		{
			get
			{
				return this._CreatedByID;
			}
			set
			{
				this._CreatedByID = value;
			}
		}
		#endregion
		#region CreatedByScreenID
		public abstract class createdByScreenID : PX.Data.IBqlField
		{
		}
		protected string _CreatedByScreenID;
		[PXDBCreatedByScreenID()]
		public virtual string CreatedByScreenID
		{
			get
			{
				return this._CreatedByScreenID;
			}
			set
			{
				this._CreatedByScreenID = value;
			}
		}
		#endregion
		#region CreatedDateTime
		public abstract class createdDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _CreatedDateTime;
		[PXDBCreatedDateTime()]
		public virtual DateTime? CreatedDateTime
		{
			get
			{
				return this._CreatedDateTime;
			}
			set
			{
				this._CreatedDateTime = value;
			}
		}
		#endregion
		#region LastModifiedByID
		public abstract class lastModifiedByID : PX.Data.IBqlField
		{
		}
		protected Guid? _LastModifiedByID;
		[PXDBLastModifiedByID()]
		public virtual Guid? LastModifiedByID
		{
			get
			{
				return this._LastModifiedByID;
			}
			set
			{
				this._LastModifiedByID = value;
			}
		}
		#endregion
		#region LastModifiedByScreenID
		public abstract class lastModifiedByScreenID : PX.Data.IBqlField
		{
		}
		protected string _LastModifiedByScreenID;
		[PXDBLastModifiedByScreenID()]
		public virtual string LastModifiedByScreenID
		{
			get
			{
				return this._LastModifiedByScreenID;
			}
			set
			{
				this._LastModifiedByScreenID = value;
			}
		}
		#endregion
		#region LastModifiedDateTime
		public abstract class lastModifiedDateTime : PX.Data.IBqlField
		{
		}
		protected DateTime? _LastModifiedDateTime;
		[PXDBLastModifiedDateTime()]
		public virtual DateTime? LastModifiedDateTime
		{
			get
			{
				return this._LastModifiedDateTime;
			}
			set
			{
				this._LastModifiedDateTime = value;
			}
		}
		#endregion
	}

	public static class ShipmentTypes
	{
		public const string Single = "S";
		public const string Multiple = "M";
	}

	public static class ShipmentStatus
	{
		public const string OnHold = "H";
		public const string Shipping = "S";
		public const string Cancelled = "C";
		public const string Delivered = "D";
	}
}
																																																																																																																																																																																																																																																																																																																																																																																																																	   