﻿namespace RB.RapidByte
 {
	 using System;
	 using PX.Data;

	 [ProductQtyAccumulator]
	 [System.SerializableAttribute()]
	 public class ProductQty : PX.Data.IBqlTable
	 {
		 #region ProductID
		 public abstract class productID : PX.Data.IBqlField
		 {
		 }
		 protected int? _ProductID;
		 [PXDBInt(IsKey = true)]
		 [PXDefault()]
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
		 #region AvailQty
		 public abstract class availQty : PX.Data.IBqlField
		 {
		 }
		 protected decimal? _AvailQty;
		 [PXDBDecimal(2)]
		 [PXDefault(TypeCode.Decimal, "0.0")]
		 [PXUIField(DisplayName = "Avail. Qty")]
		 public virtual decimal? AvailQty
		 {
			 get
			 {
				 return this._AvailQty;
			 }
			 set
			 {
				 this._AvailQty = value;
			 }
		 }
		 #endregion
	 }

	 public class ProductQtyAccumulatorAttribute : PXAccumulatorAttribute
	 {
		 public ProductQtyAccumulatorAttribute()
			 : base()
		 {
			 _SingleRecord = true;
		 }

		 protected override bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
		 {
			 if (!base.PrepareInsert(sender, row, columns))
			 {
				 return false;
			 }
			 ProductQty newQty = (ProductQty)row;
			 if (newQty.AvailQty < 0m)
			 {
				 columns.AppendException("Updating product quantity in stock will lead to a negative value.",
					 new PXAccumulatorRestriction<ProductQty.availQty>(PXComp.GE, 0m));
			 }
			 columns.Update<ProductQty.availQty>(newQty.AvailQty, PXDataFieldAssign.AssignBehavior.Summarize);
			 return true;
		 }
	 }
 }
