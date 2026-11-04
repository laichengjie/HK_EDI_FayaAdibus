IF EXISTS(SELECT 1 from sys.views WHERE name='vmHK_Adibus_JobOrderTraceErrDetail')
BEGIN 
	DROP VIEW vmHK_Adibus_JobOrderTraceErrDetail
END 
GO


Create View [dbo].[vmHK_Adibus_JobOrderTraceErrDetail]
As
SELECT DISTINCT  a.Id AS Id, 
        a.CompanyID AS CompanyID, 
        a.SignCode AS SignCode, 
        a.NoticeBillNo AS NoticeBillNo, 
		a.CustomerCode AS CustomerCode, 
        a.OSInterfaceID AS OSInterfaceID, 
        a.BillTypeID AS BillTypeID, 
        a.OpResultStatus AS OpResultStatus, 
        a.OrderType AS OrderType, 
        a.CreateDate AS CreateDate, 
        a.ReadDate AS ReadDate, 
        a.CancelDate AS CancelDate, 
        a.ShopID AS ShopID, 
        a.StockID AS StockID, 
        a.BillDate AS BillDate, 
        a.InsertedDate AS InsertedDate ,ISNULL(b.ContentNode,'') OrderTraceDetail
		,ISNULL(c.RequestData,'') RequestData
 FROM  HK_Adibus_JobOrderTrace a 
 LEFT JOIN HK_Adibus_JobOrderTraceDetail b ON b.CompanyID = a.CompanyID AND b.SignCode = a.SignCode AND b.CreateTime=(select max(CreateTime) from HK_Adibus_JobOrderTraceDetail where SignCode=b.SignCode)
 LEFT JOIN HK_Faya_AdibusEstimatedShipment c ON a.NoticeBillNo = c.DeliveryNo
 WHERE a.OpResultStatus <>-99  AND a.OrderType =2

 UNION ALL
 SELECT DISTINCT a.Id AS Id, 
        a.CompanyID AS CompanyID, 
        a.SignCode AS SignCode, 
        a.NoticeBillNo AS NoticeBillNo, 
		a.CustomerCode AS CustomerCode, 
        a.OSInterfaceID AS OSInterfaceID, 
        a.BillTypeID AS BillTypeID, 
        a.OpResultStatus AS OpResultStatus, 
        a.OrderType AS OrderType, 
        a.CreateDate AS CreateDate, 
        a.ReadDate AS ReadDate, 
        a.CancelDate AS CancelDate, 
        a.ShopID AS ShopID, 
        a.StockID AS StockID, 
        a.BillDate AS BillDate, 
        a.InsertedDate AS InsertedDate ,ISNULL(b.ContentNode,'') OrderTraceDetail
		,ISNULL(c.RequestData,'') RequestData
 FROM  HK_Adibus_JobOrderTrace a 
 LEFT JOIN HK_Adibus_JobOrderTraceDetail b ON b.CompanyID = a.CompanyID AND b.SignCode = a.SignCode AND b.CreateTime=(select max(CreateTime) from HK_Adibus_JobOrderTraceDetail where SignCode=b.SignCode)
 LEFT JOIN HK_Faya_AdibusGetPackingList c ON a.NoticeBillNo = c.ShipmentNo
 WHERE a.OpResultStatus <>-99   AND a.OrderType =4
GO 




