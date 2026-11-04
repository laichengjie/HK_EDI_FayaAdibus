IF COL_LENGTH('SD_Pur_ReceiptMaster','OtherBillNo') IS NULL
BEGIN
	ALTER TABLE  dbo.SD_Pur_ReceiptMaster ADD OtherBillNo VARCHAR(100) NULL
END
GO