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

		 //protected override bool PrepareInsert(PXCache sender, object row, PXAccumulatorCollection columns)
		 //{
		 //    if (!base.PrepareInsert(sender, row, columns))
		 //    {
		 //        return false;
		 //    }
		 //    ProductQty newQty = (ProductQty)row;
		 //    if (newQty.AvailQty < 0m)
		 //    {
		 //        columns.Restrict<ProductQty.availQty>(PXComp.GE, -newQty.AvailQty);
		 //    }
		 //    columns.Update<ProductQty.availQty>(newQty.AvailQty, PXDataFieldAssign.AssignBehavior.Summarize);
		 //    return true;
		 //}

		 //public override bool PersistInserted(PXCache sender, object row)
		 //{
		 //    try
		 //    {
		 //        return base.PersistInserted(sender, row);
		 //    }
		 //    catch (PXLockViolationException)
		 //    {
		 //        ProductQty newQty = (ProductQty)row;
		 //        ProductQty oldQty = PXSelectReadonly<ProductQty,
		 //                                Where<ProductQty.productID, Equal<Required<ProductQty.productID>>>>
		 //                            .Select(sender.Graph, newQty.ProductID);
		 //        if (newQty.AvailQty < 0m)
		 //        {
		 //            ProductQty resultQty = new ProductQty();
		 //            resultQty.ProductID = oldQty.ProductID;
		 //            resultQty.AvailQty = oldQty.AvailQty + newQty.AvailQty;
		 //            if (resultQty.AvailQty < 0m)
		 //            {
		 //                Product product = PXSelectReadonly<Product,
		 //                                      Where<Product.productID, Equal<Current<ProductQty.productID>>>>
		 //                                  .SelectSingleBound(sender.Graph, new object[] { row });
		 //                throw new PXRowPersistingException(
		 //                    "Avail. Qty",
		 //                    product != null ? product.ProductCD : string.Empty,
		 //                    "Updating product quantity in stock will lead to a negative value.");
		 //            }
		 //        }
		 //        throw;
		 //    }
		 //}

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
