﻿namespace RB.RapidByte
{
	using System;
	using PX.Data;
	
	[System.SerializableAttribute()]
	public class ShipmentLine : PX.Data.IBqlTable
	{
		public const string GiftCard = "CARD";
		public class giftCard : Constant<string>
		{
			public giftCard()
				: base(GiftCard)
			{
			}
		}

		#region ShipmentNbr
		public abstract class shipmentNbr : PX.Data.IBqlField
		{
		}
		protected string _ShipmentNbr;
		[PXDBString(10, IsKey = true, IsUnicode = true)]
		[PXDBDefault(typeof(Shipment.shipmentNbr))]
		[PXParent(typeof(Select<Shipment,
							 Where<Shipment.shipmentNbr,
								 Equal<Current<ShipmentLine.shipmentNbr>>>>))]
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
		#region ProductID
		public abstract class productID : PX.Data.IBqlField
		{
		}
		protected int? _ProductID;
		[PXDBInt(IsKey = true)]
		[PXDefault()]
		[PXUIField(DisplayName = "Product ID")]
		[PXSelector(
			typeof(Product.productID),
			typeof(Product.productCD),
			typeof(Product.productName),
			typeof(Product.unitPrice),
			SubstituteKey = typeof(Product.productCD))]
		public virtual int? ProductID
		{
			get
			{
				return this._ProductID;
			}
			set
			{
				this._ProductID = value;
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
		#region LineQty
		public abstract class lineQty : PX.Data.IBqlField
		{
		}
		protected decimal? _LineQty;
		[PXDBDecimal(6)]
		[PXDefault(TypeCode.Decimal,"0.0")]
		[PXUIField(DisplayName = "Item Qty.")]
		public virtual decimal? LineQty
		{
			get
			{
				return this._LineQty;
			}
			set
			{
				this._LineQty = value;
			}
		}
		#endregion
		#region Cancelled
		public abstract class cancelled : PX.Data.IBqlField
		{
		}
		protected bool? _Cancelled;
		[PXDBBool()]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = "Cancelled")]
		public virtual bool? Cancelled
		{
			get
			{
				return this._Cancelled;
			}
			set
			{
				this._Cancelled = value;
			}
		}
		#endregion
		#region ShipmentDate
		public abstract class shipmentDate : PX.Data.IBqlField
		{
		}
		protected DateTime? _ShipmentDate;
		[PXDBDate()]
		[PXUIField(DisplayName = "Delivery Date")]
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
		#region ShipmentTime
		public abstract class shipmentTime : PX.Data.IBqlField
		{
		}
		protected int? _ShipmentTime;
		[PXDBTimeSpan(DisplayMask = "t", InputMask = "t")]
		[PXUIField(DisplayName = "Delivery Time")]
		public virtual int? ShipmentTime
		{
			get
			{
				return this._ShipmentTime;
			}
			set
			{
				this._ShipmentTime = value;
			}
		}
		#endregion
		#region ShipmentMinTime
		public abstract class shipmentMinTime : PX.Data.IBqlField
		{
		}
		protected int? _ShipmentMinTime;
		[PXDBTimeSpan(DisplayMask = "t", InputMask = "t")]
		[PXUIField(DisplayName = "Deliver After")]
		public virtual int? ShipmentMinTime
		{
			get
			{
				return this._ShipmentMinTime;
			}
			set
			{
				this._ShipmentMinTime = value;
			}
		}
		#endregion
		#region ShipmentMaxTime
		public abstract class shipmentMaxTime : PX.Data.IBqlField
		{
		}
		protected int? _ShipmentMaxTime;
		[PXDBTimeSpan(DisplayMask = "t", InputMask = "t")]
		[PXUIField(DisplayName = "Deliver Before")]
		public virtual int? ShipmentMaxTime
		{
			get
			{
				return this._ShipmentMaxTime;
			}
			set
			{
				this._ShipmentMaxTime = value;
			}
		}
		#endregion
		#region Gift
		public abstract class gift : PX.Data.IBqlField
		{
		}
		protected bool? _Gift;
		[PXDBBool()]
		[PXDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
		public virtual bool? Gift
		{
			get
			{
				return this._Gift;
			}
			set
			{
				this._Gift = value;
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
}