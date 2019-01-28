﻿namespace RB.RapidByte
 {
	 using System;
	 using PX.Data;

	 [System.SerializableAttribute()]
	 [PXPrimaryGraph(typeof(SetupMaint))]
	 [PXCacheName("RapidByte Preferences")]
	 public class Setup : PX.Data.IBqlTable
	 {
		 #region ReceiptLastDocNbr
		 public abstract class receiptLastDocNbr : PX.Data.IBqlField
		 {
		 }
		 protected string _ReceiptLastDocNbr;
		 [PXDBString(15, IsUnicode = true)]
		 [PXDefault("00010")]
		 [PXUIField(DisplayName = "Receipt Last Ref. Number")]
		 public virtual string ReceiptLastDocNbr
		 {
			 get
			 {
				 return this._ReceiptLastDocNbr;
			 }
			 set
			 {
				 this._ReceiptLastDocNbr = value;
			 }
		 }
		 #endregion
		 #region ReturnLastDocNbr
		 public abstract class returnLastDocNbr : PX.Data.IBqlField
		 {
		 }
		 protected string _ReturnLastDocNbr;
		 [PXDBString(15, IsUnicode = true)]
		 [PXDefault("00010")]
		 [PXUIField(DisplayName = "Return Last Ref. Number")]
		 public virtual string ReturnLastDocNbr
		 {
			 get
			 {
				 return this._ReturnLastDocNbr;
			 }
			 set
			 {
				 this._ReturnLastDocNbr = value;
			 }
		 }
		 #endregion
		 #region SalesOrderLastNbr
		 public abstract class salesOrderLastNbr : PX.Data.IBqlField
		 {
		 }
		 protected string _SalesOrderLastNbr;
		 [PXDBString(15, IsUnicode = true)]
		 [PXDefault("00010")]
		 [PXUIField(DisplayName = "Sales Order Last Number")]
		 public virtual string SalesOrderLastNbr
		 {
			 get
			 {
				 return this._SalesOrderLastNbr;
			 }
			 set
			 {
				 this._SalesOrderLastNbr = value;
			 }
		 }
		 #endregion
		 #region AutoNumbering
		 public abstract class autoNumbering : PX.Data.IBqlField
		 {
		 }
		 protected bool? _AutoNumbering;
		 [PXDBBool()]
		 [PXDefault(true, PersistingCheck = PXPersistingCheck.Nothing)]
		 [PXUIField(DisplayName = "Auto Numbering")]
		 public virtual bool? AutoNumbering
		 {
			 get
			 {
				 return this._AutoNumbering;
			 }
			 set
			 {
				 this._AutoNumbering = value;
			 }
		 }
		 #endregion
	 }
 }