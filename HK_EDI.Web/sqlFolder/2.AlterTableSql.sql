IF COL_LENGTH('HK_EDI_JobOrderTrace', 'OrderType') IS NULL 
BEGIN

ALTER TABLE dbo.HK_EDI_JobOrderTrace ADD OrderType INT NULL 
end 

IF COL_LENGTH('HK_EDI_JobOrderTrace', 'SignCode') IS NULL 
BEGIN
   ALTER TABLE dbo.HK_EDI_JobOrderTrace ADD SignCode VARCHAR(50)
END


IF COL_LENGTH('HK_EDI_JobOrderTraceDetail', 'PId') IS NULL 
BEGIN
   ALTER TABLE dbo.HK_EDI_JobOrderTraceDetail ADD PId VARCHAR(50)
END

IF COL_LENGTH('HK_EDI_PostSkuErrorRecords', 'IsSuccess') IS NULL 
BEGIN
ALTER  TABLE HK_EDI_PostSkuErrorRecords ADD   IsSuccess bit
END

IF COL_LENGTH('HK_EDI_PostSkuErrorRecords', 'Qty') IS NULL 
BEGIN
ALTER TABLE dbo.HK_EDI_PostSkuErrorRecords ADD Qty INT 
END

IF COL_LENGTH('HK_EDI_PostSkuErrorRecords', 'LastQueryTime') IS NULL 
BEGIN
ALTER TABLE dbo.HK_EDI_PostSkuErrorRecords ADD LastQueryTime datetime 
END



IF COL_LENGTH('HK_EDI_PostSkuErrorRecords', 'OSStockID') IS NULL 
BEGIN
ALTER TABLE dbo.HK_EDI_PostSkuErrorRecords ADD OSStockID NVARCHAR(50)
END

IF COL_LENGTH('HK_EDI_JobOrderTraceDetail', 'SignCode') IS NULL 
BEGIN
ALTER TABLE dbo.HK_EDI_JobOrderTraceDetail ALTER COLUMN SignCode   NVARCHAR(50) NULL 
END

IF COL_LENGTH('HK_EDI_JobOrderTraceDetail', 'PId') IS NULL 
BEGIN
ALTER TABLE dbo.HK_EDI_JobOrderTraceDetail ALTER COLUMN PId   int NULL
END

IF COL_LENGTH('HK_EDI_JobOrderTraceDetail', 'ContentNode') IS NULL 
BEGIN
ALTER TABLE HK_EDI_PostSkuRecordsDetail ADD ContentNode NVARCHAR(MAX) DEFAULT ''
END


