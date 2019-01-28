using System;
using PX.Data;

namespace RB.RapidByte
{
	public class ShipmentEntry : PXGraph<ShipmentEntry, Shipment>
	{
		public PXSelect<Shipment> Shipments;
		public PXSelect<ShipmentLine,
				   Where<ShipmentLine.shipmentNbr, Equal<Current<Shipment.shipmentNbr>>>,
				   OrderBy<Desc<ShipmentLine.gift>>> ShipmentLines;

		public PXSelect<Product,
				   Where<Product.productCD, Equal<ShipmentLine.giftCard>>> GiftCard;

		// <px:PXDropDown ID="ShipmentType" runat="server" DataField="ShipmentType" CommitChanges="True"> ////
		//protected virtual void Shipment_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		//{
		//    Shipment row = (Shipment)e.Row;
		//    if (row == null) return;

		//    PXUIFieldAttribute.SetEnabled<Shipment.deliveryDate>(sender, row, row.ShipmentType == ShipmentTypes.Single);
		//    PXUIFieldAttribute.SetVisible<Shipment.deliveryMaxDate>(sender, row, row.ShipmentType == ShipmentTypes.Single);
		//    PXUIFieldAttribute.SetVisible<Shipment.pendingQty>(sender, row, row.ShipmentType != ShipmentTypes.Single);

		//    PXUIFieldAttribute.SetVisible<ShipmentLine.shipmentDate>(ShipmentLines.Cache, null, row.ShipmentType != ShipmentTypes.Single);
		//    PXUIFieldAttribute.SetVisible<ShipmentLine.shipmentTime>(ShipmentLines.Cache, null, row.ShipmentType != ShipmentTypes.Single);
		//    PXUIFieldAttribute.SetVisible<ShipmentLine.shipmentMinTime>(ShipmentLines.Cache, null, row.ShipmentType != ShipmentTypes.Single);
		//    PXUIFieldAttribute.SetVisible<ShipmentLine.shipmentMaxTime>(ShipmentLines.Cache, null, row.ShipmentType != ShipmentTypes.Single);
		//}

		////// 1st option
		//protected virtual void Shipment_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
		//{
		//    if (ShipmentLines.Select().Count == 0)
		//    {
		//        bool oldDirty = ShipmentLines.Cache.IsDirty;
		//        Product card = GiftCard.Select();
		//        if (card != null)
		//        {
		//            ShipmentLines.Insert();
		//            ShipmentLines.Cache.IsDirty = oldDirty;
		//        }
		//    }
		//}

		//protected virtual void ShipmentLine_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
		//{
		//    ShipmentLine line = (ShipmentLine)e.Row;
		//    if (ShipmentLines.Select().Count == 0)
		//    {
		//        Product card = GiftCard.Select();
		//        if (card != null)
		//        {
		//            line.ProductID = card.ProductID;
		//            line.Description = card.ProductName;
		//            line.LineQty = card.MinAvailQty;
		//            line.Gift = true;
		//        }
		//    }
		//}
		////// 1st option

		//// 2nd option
		protected virtual void Shipment_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
		{
			if (ShipmentLines.Select().Count == 0)
			{
				bool oldDirty = ShipmentLines.Cache.IsDirty;
				Product card = GiftCard.Select();
				if (card != null)
				{
					ShipmentLine line = new ShipmentLine();
					line.ProductID = card.ProductID;
					line.LineQty = card.MinAvailQty;
					ShipmentLines.Insert(line);

					ShipmentLines.Cache.IsDirty = oldDirty;
				}
			}
		}

		//// <px:PXGridColumn DataField="ProductID" Width="140px" CommitChanges="True"> ////
		protected virtual void ShipmentLine_ProductID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			ShipmentLine line = (ShipmentLine)e.Row;
			line.Description = string.Empty;
			if (line.ProductID != null)
			{
				Product product =
					PXSelectorAttribute.Select<ShipmentLine.productID>(
						sender, line) as Product;
				if (product != null)
				{
					line.Description = product.ProductName;
				}
			}
		}

		protected virtual void ShipmentLine_Gift_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			ShipmentLine line = (ShipmentLine)e.Row;
			if (line == null) return;

			Product card = GiftCard.Select();
			if (card != null && line.ProductID == card.ProductID)
			{
				e.NewValue = true;
				e.Cancel = true;
			}
		}
		//// 2nd option

		//protected virtual void ShipmentLine_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		//{
		//    ShipmentLine line = (ShipmentLine)e.Row;
		//    if (line == null) return;

		//    PXUIFieldAttribute.SetEnabled(sender, line, line.Gift != true);
		//}

		//// <px:PXGridColumn DataField="LineQty" Width="100px" CommitChanges="True"> ////
		protected virtual void ShipmentLine_LineQty_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			ShipmentLine line = (ShipmentLine)e.Row;
			if (e.NewValue == null) return;

			if ((decimal)e.NewValue < 0)
			{
				throw new PXSetPropertyException("Item Qty. cannot be negative.");
			}

			Product product = PXSelect<Product,
								  Where<Product.productID, Equal<Required<Product.productID>>>>
							  .Select(this, line.ProductID);
			if (product != null && (decimal)e.NewValue < product.MinAvailQty)
			{
				e.NewValue = product.MinAvailQty;
				sender.RaiseExceptionHandling<ShipmentLine.lineQty>(line, e.NewValue,
					new PXSetPropertyException("Item Qty. was too small for the selected product.", PXErrorLevel.Warning));
			}
		}

		protected virtual void ShipmentLine_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
		{
			ShipmentLine line = (ShipmentLine)e.Row;
			if (!e.ExternalCall) return;

			if (line.Gift == true)
			{
				throw new PXException("Product {0} cannot be deleted", ShipmentLine.GiftCard);
			}
			else if (sender.GetStatus(line) != PXEntryStatus.InsertedDeleted)
			{
				if (ShipmentLines.Ask("Confirm Delete", "Are you sure?", MessageButtons.YesNo) != WebDialogResult.Yes)
				{
					e.Cancel = true;
				}
			}
		}

		//// Aggregates
		protected virtual void ShipmentLine_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
		{
			ShipmentLine line = (ShipmentLine)e.Row;
			Shipment row = Shipments.Current;
			row.TotalQty += line.LineQty;
			Shipments.Update(row);
		}

		//protected virtual void ShipmentLine_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		//{
		//    ShipmentLine newLine = (ShipmentLine)e.Row;
		//    ShipmentLine oldLine = (ShipmentLine)e.OldRow;
		//    Shipment row = Shipments.Current;
		//    bool rowUpdated = false;

		//    if (!sender.ObjectsEqual<ShipmentLine.lineQty>(newLine, oldLine) && newLine.Cancelled != true)
		//    {
		//        row.TotalQty -= oldLine.LineQty;
		//        row.TotalQty += newLine.LineQty;
		//        rowUpdated = true;
		//    }
		//    if (!sender.ObjectsEqual<ShipmentLine.cancelled>(newLine, oldLine))
		//    {
		//        if (newLine.Cancelled == true)
		//        {
		//            row.TotalQty -= oldLine.LineQty;
		//        }
		//        else if (oldLine.Cancelled == true)
		//        {
		//            row.TotalQty += newLine.LineQty;
		//        }
		//        rowUpdated = true;
		//    }

		//    if (rowUpdated == true)
		//    {
		//        Shipments.Update(row);
		//    }
		//}

		protected virtual void ShipmentLine_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
		{
			ShipmentLine line = (ShipmentLine)e.Row;
			Shipment row = Shipments.Current;
			if (line.Cancelled != true &&
				Shipments.Cache.GetStatus(row) != PXEntryStatus.InsertedDeleted &&
				Shipments.Cache.GetStatus(row) != PXEntryStatus.Deleted)
			{
				row.TotalQty -= line.LineQty;
				Shipments.Update(row);
			}
		}
		//// Aggregates

		//// Status
		//// <px:PXDropDown ID="Status" runat="server" DataField="Status" CommitChanges="True"> ////
		//protected virtual void Shipment_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		//{
		//    Shipment row = (Shipment)e.Row;
		//    if (row == null) return;

		//    PXUIFieldAttribute.SetEnabled<Shipment.deliveryDate>(sender, row, row.ShipmentType == ShipmentTypes.Single);
		//    PXUIFieldAttribute.SetEnabled<Shipment.shipmentDate>(sender, row, row.Status == ShipmentStatus.Shipping);
		//    PXUIFieldAttribute.SetVisible<Shipment.deliveryMaxDate>(sender, row, row.ShipmentType == ShipmentTypes.Single);
		//    PXUIFieldAttribute.SetVisible<Shipment.pendingQty>(sender, row, row.ShipmentType != ShipmentTypes.Single);

		//    ShipmentLines.Cache.AllowInsert = row.Status != ShipmentStatus.Shipping;
		//    ShipmentLines.Cache.AllowDelete = row.Status != ShipmentStatus.Shipping;

		//    PXUIFieldAttribute.SetVisible<ShipmentLine.shipmentDate>(ShipmentLines.Cache, null, row.ShipmentType != ShipmentTypes.Single);
		//    PXUIFieldAttribute.SetVisible<ShipmentLine.shipmentTime>(ShipmentLines.Cache, null, row.ShipmentType != ShipmentTypes.Single);
		//    PXUIFieldAttribute.SetVisible<ShipmentLine.shipmentMinTime>(ShipmentLines.Cache, null, row.ShipmentType != ShipmentTypes.Single);
		//    PXUIFieldAttribute.SetVisible<ShipmentLine.shipmentMaxTime>(ShipmentLines.Cache, null, row.ShipmentType != ShipmentTypes.Single);
		//}

		//protected virtual void ShipmentLine_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		//{
		//    ShipmentLine line = (ShipmentLine)e.Row;
		//    Shipment row = Shipments.Current;
		//    if (line == null || row == null) return;

		//    PXUIFieldAttribute.SetEnabled(sender, line, line.Gift != true);
		//    PXUIFieldAttribute.SetEnabled<ShipmentLine.lineQty>(sender, line, line.Gift != true && row.Status != ShipmentStatus.Shipping);
		//    PXUIFieldAttribute.SetEnabled<ShipmentLine.cancelled>(sender, line, line.Gift != true && row.Status != ShipmentStatus.Shipping);
		//}

		protected virtual void Shipment_Status_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			if (e.NewValue == null) return;

			bool errorOccured = false;
			if ((string)e.NewValue == ShipmentStatus.Shipping)
			{
				foreach (ShipmentLine line in ShipmentLines.Select())
				{
					if (line.LineQty == 0)
					{
						ShipmentLines.Cache.RaiseExceptionHandling<ShipmentLine.lineQty>(line, line.LineQty,
							new PXSetPropertyException("Item Qty. is not specified."));
						errorOccured = true;
					}
				}
			}
			if (errorOccured)
			{
				e.NewValue = sender.GetValue<Shipment.status>(e.Row);
				throw new PXSetPropertyException("Product quantities have not been specified.");
			}
		}
		//// Status

		//// Actions
		public PXAction<Shipment> CancelShipment;
		[PXButton(CommitChanges = true)]
		[PXUIField(DisplayName = "Cancel Shipment")]
		protected virtual void cancelShipment()
		{
			Shipment row = Shipments.Current;
			row.Status = ShipmentStatus.Cancelled;
			Shipments.Update(row);
			Actions.PressSave();
		}

		protected virtual void Shipment_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
		{
			Shipment row = (Shipment)e.NewRow;
			Shipment originalRow = (Shipment)e.Row;
			if (row.ShipmentType == ShipmentTypes.Single &&
				!sender.ObjectsEqual<Shipment.deliveryMaxDate>(row, originalRow))
			{
				if (row.DeliveryDate != null && row.DeliveryMaxDate != null &&
					row.DeliveryMaxDate < row.DeliveryDate)
				{
					row.DeliveryMaxDate = row.DeliveryDate;
					sender.RaiseExceptionHandling<Shipment.deliveryMaxDate>(row, row.DeliveryMaxDate,
						new PXSetPropertyException("Specified value was too early.", PXErrorLevel.Warning));
				}
			}
			else if (row.ShipmentType == ShipmentTypes.Single &&
					 !sender.ObjectsEqual<Shipment.deliveryDate,
										  Shipment.deliveryMaxDate>(row, originalRow))
			{
				if (row.DeliveryDate != null && row.DeliveryMaxDate != null &&
					row.DeliveryDate > row.DeliveryMaxDate)
				{
					sender.RaiseExceptionHandling<Shipment.deliveryDate>(row, row.DeliveryDate,
						new PXSetPropertyException("Delivery time expired."));
					e.Cancel = true;
				}
			}
		}

		protected virtual void Shipment_DeliveryDate_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			Shipment row = (Shipment)e.Row;
			if (e.NewValue == null) return;

			if (row.ShipmentDate != null && row.ShipmentDate > (DateTime)e.NewValue)
			{
				e.NewValue = row.ShipmentDate;
				throw new PXSetPropertyException<Shipment.shipmentDate>("Shipment Date cannot be later then Delivery Date");
			}
		}

		//public PXAction<Shipment> DeliverShipment;
		//[PXButton(CommitChanges = true)]
		//[PXUIField(DisplayName = "Deliver Shipment")]
		//protected virtual void deliverShipment()
		//{
		//    Shipment row = Shipments.Current;
		//    PXCache cache = Shipments.Cache;
		//    bool errorOccured = false;

		//    if (row.DeliveryDate == null)
		//    {
		//        cache.RaiseExceptionHandling<Shipment.deliveryDate>(row, row.DeliveryDate,
		//            new PXSetPropertyException("Delivery date may not be empty."));
		//        errorOccured = true;
		//    }
		//    if (row.DeliveryMaxDate == null)
		//    {
		//        cache.RaiseExceptionHandling<Shipment.deliveryMaxDate>(row, row.DeliveryMaxDate,
		//            new PXSetPropertyException("Delivery Before date may not be empty."));
		//        errorOccured = true;
		//    }
		//    if (errorOccured)
		//    {
		//        throw new PXException("Shipment '{0}' can not be delivered.", row.ShipmentNbr);
		//    }

		//    foreach (ShipmentLine line in ShipmentLines.Select())
		//    {
		//        if (line.Cancelled != true)
		//        {
		//            row.ShippedQty += line.LineQty;
		//        }
		//    }

		//    row.Status = ShipmentStatus.Delivered;
		//    Shipments.Update(row);
		//    Actions.PressSave();
		//}

		protected virtual void Shipment_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			Shipment row = (Shipment)e.Row;
			if (row == null) return;

			PXUIFieldAttribute.SetVisible<Shipment.deliveryMaxDate>(sender, row, row.ShipmentType == ShipmentTypes.Single);
			PXUIFieldAttribute.SetVisible<Shipment.pendingQty>(sender, row, row.ShipmentType != ShipmentTypes.Single);

			PXUIFieldAttribute.SetVisible<ShipmentLine.shipmentDate>(ShipmentLines.Cache, null, row.ShipmentType != ShipmentTypes.Single);
			PXUIFieldAttribute.SetVisible<ShipmentLine.shipmentTime>(ShipmentLines.Cache, null, row.ShipmentType != ShipmentTypes.Single);
			PXUIFieldAttribute.SetVisible<ShipmentLine.shipmentMinTime>(ShipmentLines.Cache, null, row.ShipmentType != ShipmentTypes.Single);
			PXUIFieldAttribute.SetVisible<ShipmentLine.shipmentMaxTime>(ShipmentLines.Cache, null, row.ShipmentType != ShipmentTypes.Single);

			if (row.Status != ShipmentStatus.Cancelled && row.Status != ShipmentStatus.Delivered)
			{
				Shipments.Cache.AllowDelete = row.Status != ShipmentStatus.Shipping && row.ShippedQty == 0;
				PXUIFieldAttribute.SetEnabled(sender, row, true);

				PXUIFieldAttribute.SetEnabled<Shipment.deliveryDate>(sender, row, row.ShipmentType == ShipmentTypes.Single);
				PXUIFieldAttribute.SetEnabled<Shipment.shipmentDate>(sender, row, row.Status == ShipmentStatus.Shipping);
				PXStringListAttribute.SetList<Shipment.status>(
					sender, row,
					new string[]
					{
						ShipmentStatus.OnHold,
						ShipmentStatus.Shipping,
					},
					new string[]
					{
						"On Hold",
						"Shipping",
					});

				ShipmentLines.Cache.AllowInsert = row.Status != ShipmentStatus.Shipping;
				ShipmentLines.Cache.AllowUpdate = true;
				ShipmentLines.Cache.AllowDelete = row.Status != ShipmentStatus.Shipping;

				CancelShipment.SetEnabled(row.Status == ShipmentStatus.Shipping && Shipments.Cache.GetStatus(row) != PXEntryStatus.Inserted);
				DeliverShipment.SetEnabled(row.Status == ShipmentStatus.Shipping && Shipments.Cache.GetStatus(row) != PXEntryStatus.Inserted);
			}
			else
			{
				Shipments.Cache.AllowDelete = false;
				PXUIFieldAttribute.SetEnabled(sender, row, false);
				PXUIFieldAttribute.SetEnabled<Shipment.shipmentNbr>(sender, row, true);
				PXStringListAttribute.SetList<Shipment.status>(
					sender, row,
					new string[]
					{
						ShipmentStatus.Cancelled, 
						ShipmentStatus.Delivered
					},
					new string[]
					{
						"Cancelled", 
						"Delivered"
					});

				ShipmentLines.Cache.AllowInsert = false;
				ShipmentLines.Cache.AllowUpdate = false;
				ShipmentLines.Cache.AllowDelete = false;

				CancelShipment.SetEnabled(false);
				DeliverShipment.SetEnabled(false);
			}
		}
		//// Actions

		//// ShipmentType
		protected virtual void ShipmentLine_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
		{
			ShipmentLine line = (ShipmentLine)e.Row;
			Shipment row = Shipments.Current;
			if (line == null || row == null) return;

			PXUIFieldAttribute.SetEnabled(sender, line, line.Gift != true);
			PXUIFieldAttribute.SetEnabled<ShipmentLine.lineQty>(sender, line, line.Gift != true && row.Status != ShipmentStatus.Shipping);
			PXUIFieldAttribute.SetEnabled<ShipmentLine.cancelled>(sender, line, line.Gift != true && row.Status != ShipmentStatus.Shipping);
			if (row.ShipmentType != ShipmentTypes.Single)
			{
				PXUIFieldAttribute.SetEnabled<ShipmentLine.cancelled>(sender, line,
					line.Gift != true && row.Status != ShipmentStatus.Shipping && line.ShipmentDate == null);
			}
			else
			{
				PXUIFieldAttribute.SetEnabled<ShipmentLine.cancelled>(sender, line, line.Gift != true && row.Status != ShipmentStatus.Shipping);
			}
			PXUIFieldAttribute.SetEnabled<ShipmentLine.shipmentDate>(sender, line, row.Status == ShipmentStatus.Shipping && line.Cancelled != true);
			PXUIFieldAttribute.SetEnabled<ShipmentLine.shipmentTime>(sender, line, row.Status == ShipmentStatus.Shipping && line.Cancelled != true);
			PXUIFieldAttribute.SetEnabled<ShipmentLine.shipmentMinTime>(sender, line, row.Status != ShipmentStatus.Shipping && line.Cancelled != true);
			PXUIFieldAttribute.SetEnabled<ShipmentLine.shipmentMaxTime>(sender, line, row.Status != ShipmentStatus.Shipping && line.Cancelled != true);
		}

		protected virtual void ShipmentLine_ShipmentMinTime_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			Shipment row = Shipments.Current;
			if (row != null && row.ShipmentType != ShipmentTypes.Single)
			{
				e.NewValue = "9:00 AM";
			}
			else
			{
				e.NewValue = null;
			}
		}

		protected virtual void ShipmentLine_ShipmentMaxTime_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
		{
			Shipment row = Shipments.Current;
			if (row != null && row.ShipmentType != ShipmentTypes.Single)
			{
				e.NewValue = "7:00 PM";
			}
			else
			{
				e.NewValue = null;
			}
		}

		protected virtual void Shipment_ShipmentType_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			foreach (ShipmentLine line in ShipmentLines.Select())
			{
				line.ShipmentDate = null;
				ShipmentLines.Cache.SetDefaultExt<ShipmentLine.shipmentMinTime>(line);
				ShipmentLines.Cache.SetDefaultExt<ShipmentLine.shipmentMaxTime>(line);
				ShipmentLines.Update(line);
			}
		}
		//// ShipmentType

		//// Aggregates
		protected virtual void ShipmentLine_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
		{
			ShipmentLine newLine = (ShipmentLine)e.Row;
			ShipmentLine oldLine = (ShipmentLine)e.OldRow;
			Shipment row = Shipments.Current;
			bool rowUpdated = false;

			if (!sender.ObjectsEqual<ShipmentLine.lineQty>(newLine, oldLine) && newLine.Cancelled != true)
			{
				row.TotalQty -= oldLine.LineQty;
				row.TotalQty += newLine.LineQty;
				rowUpdated = true;
			}
			if (!sender.ObjectsEqual<ShipmentLine.cancelled>(newLine, oldLine))
			{
				if (newLine.Cancelled == true)
				{
					row.TotalQty -= oldLine.LineQty;
				}
				else if (oldLine.Cancelled == true)
				{
					row.TotalQty += newLine.LineQty;
				}
				rowUpdated = true;
			}

			if (row.ShipmentType != ShipmentTypes.Single)
			{
				if (!sender.ObjectsEqual<ShipmentLine.shipmentDate, ShipmentLine.shipmentTime>(newLine, oldLine))
				{
					if (newLine.ShipmentDate != null && newLine.ShipmentTime != null)
					{
						row.ShippedQty += newLine.LineQty;
						rowUpdated = true;
					}
					if (oldLine.ShipmentDate != null && oldLine.ShipmentTime != null)
					{
						row.ShippedQty -= oldLine.LineQty;
						rowUpdated = true;
					}
				}
			}

			if (rowUpdated == true)
			{
				Shipments.Update(row);
			}
		}
		// Aggregates

		//// Shipment
		protected virtual void Shipment_ShipmentType_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
		{
			Shipment row = (Shipment)e.Row;
			if (e.NewValue == null) return;

			if ((string)e.NewValue == ShipmentTypes.Single && row.ShippedQty > 0)
			{
				throw new PXSetPropertyException("Delivery Type can not be changed when a shipment is partially delivered.");
			}
		}

		protected virtual void Shipment_TotalQty_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			Shipment row = (Shipment)e.Row;
			row.PendingQty += row.TotalQty;
			row.PendingQty -= (decimal)e.OldValue;
		}

		protected virtual void Shipment_ShippedQty_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			Shipment row = (Shipment)e.Row;
			row.PendingQty -= row.ShippedQty;
			row.PendingQty += (decimal)e.OldValue;
		}
		//// Shipment

		//// ShipmentLine
		//// <px:PXGridColumn DataField="ShipmentDate" Width="120px" CommitChanges="True"> ////
		protected virtual void ShipmentLine_ShipmentDate_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
		{
			ShipmentLine line = (ShipmentLine)e.Row;
			line.ShipmentTime = null;
		}

		protected virtual void ShipmentLine_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
		{
			ShipmentLine line = (ShipmentLine)e.NewRow;
			ShipmentLine originalLine = (ShipmentLine)e.Row;
			Shipment row = Shipments.Current;
			if (row.ShipmentType != ShipmentTypes.Single &&
				!sender.ObjectsEqual<ShipmentLine.shipmentTime,
									 ShipmentLine.shipmentMinTime,
									 ShipmentLine.shipmentMaxTime>(line, originalLine))
			{
				if (line.ShipmentTime != null && line.ShipmentMinTime != null &&
					line.ShipmentTime < line.ShipmentMinTime)
				{
					sender.RaiseExceptionHandling<ShipmentLine.shipmentTime>(line, line.ShipmentTime,
						new PXSetPropertyException("Delivery Time is too early."));
					e.Cancel = true;
				}
				if (line.ShipmentTime != null && line.ShipmentMaxTime != null &&
					line.ShipmentTime > line.ShipmentMaxTime)
				{
					line.ShipmentTime = line.ShipmentMaxTime;
					sender.RaiseExceptionHandling<ShipmentLine.shipmentTime>(line, line.ShipmentTime,
						new PXSetPropertyException("Specified Delivery Time was too late.", PXErrorLevel.Warning));
				}
			}
		}
		//// ShipmentLine

		public PXAction<Shipment> DeliverShipment;
		[PXButton(CommitChanges = true)]
		[PXUIField(DisplayName = "Deliver Shipment")]
		protected virtual void deliverShipment()
		{
			Shipment row = Shipments.Current;
			PXCache cache = Shipments.Cache;
			bool errorOccured = false;

			if (row.ShipmentType != ShipmentTypes.Single)
			{
				if (row.PendingQty > 0)
				{
					cache.RaiseExceptionHandling<Shipment.pendingQty>(row, row.PendingQty,
						new PXSetPropertyException("Products have not been completely delivered yet."));
					errorOccured = true;
				}
			}
			else
			{
				if (row.DeliveryDate == null)
				{
					cache.RaiseExceptionHandling<Shipment.deliveryDate>(row, row.DeliveryDate,
						new PXSetPropertyException("Delivery date may not be empty."));
					errorOccured = true;
				}
				if (row.DeliveryMaxDate == null)
				{
					cache.RaiseExceptionHandling<Shipment.deliveryMaxDate>(row, row.DeliveryMaxDate,
						new PXSetPropertyException("Delivery Before date may not be empty."));
					errorOccured = true;
				}
			}
			if (errorOccured)
			{
				throw new PXException("Shipment '{0}' can not be delivered.", row.ShipmentNbr);
			}

			if (row.ShipmentType != ShipmentTypes.Single)
			{
				row.DeliveryDate = Accessinfo.BusinessDate;
			}
			else
			{
				foreach (ShipmentLine line in ShipmentLines.Select())
				{
					if (line.Cancelled != true)
					{
						row.ShippedQty += line.LineQty;
					}
				}
			}

			row.Status = ShipmentStatus.Delivered;
			Shipments.Update(row);
			Actions.PressSave();
		}
	}
}