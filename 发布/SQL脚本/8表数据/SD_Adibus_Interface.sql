-- =============================================
-- SD_Adibus_Interface：不存在才新增
-- =============================================
IF NOT EXISTS (
    SELECT 1 FROM [dbo].[SD_Adibus_Interface]
    WHERE [CompanyID] = N'00000000' AND [OSInterfaceID] = N'AD0000001'
)
INSERT [dbo].[SD_Adibus_Interface]
([CompanyID], [OSInterfaceID], [OSInterfaceCode], [OSInterfaceName], [CooperationSys], [CSysAccreditID], [JobFrequency], [ServerviceURL], [Remark], [AllowUsed], [ModifyDTM])
VALUES
(N'00000000', N'AD0000001', N'GetEstimatedShipment', N'恒康-Faya获取Adibus预计发货单服务', N'GetEstimatedShipmentService', N'', NULL, N'', NULL, 1, CAST(N'2017-09-26T00:00:00.000' AS DateTime));
GO

IF NOT EXISTS (
    SELECT 1 FROM [dbo].[SD_Adibus_Interface]
    WHERE [CompanyID] = N'00000000' AND [OSInterfaceID] = N'AD0000002'
)
INSERT [dbo].[SD_Adibus_Interface]
([CompanyID], [OSInterfaceID], [OSInterfaceCode], [OSInterfaceName], [CooperationSys], [CSysAccreditID], [JobFrequency], [ServerviceURL], [Remark], [AllowUsed], [ModifyDTM])
VALUES
(N'00000000', N'AD0000002', N'CreateCGOrder', N'恒康-FayaAdibus生成采购订单服务', N'CreateCGOrderService', N'', NULL, N'', NULL, 1, CAST(N'2017-09-26T00:00:00.000' AS DateTime));
GO

IF NOT EXISTS (
    SELECT 1 FROM [dbo].[SD_Adibus_Interface]
    WHERE [CompanyID] = N'00000000' AND [OSInterfaceID] = N'AD0000003'
)
INSERT [dbo].[SD_Adibus_Interface]
([CompanyID], [OSInterfaceID], [OSInterfaceCode], [OSInterfaceName], [CooperationSys], [CSysAccreditID], [JobFrequency], [ServerviceURL], [Remark], [AllowUsed], [ModifyDTM])
VALUES
(N'00000000', N'AD0000003', N'GetPackingList', N'恒康-Faya获取Adibus装箱单服务', N'GetPackingListService', N'', NULL, N'', NULL, 1, CAST(N'2017-09-26T00:00:00.000' AS DateTime));
GO

IF NOT EXISTS (
    SELECT 1 FROM [dbo].[SD_Adibus_Interface]
    WHERE [CompanyID] = N'00000000' AND [OSInterfaceID] = N'AD0000004'
)
INSERT [dbo].[SD_Adibus_Interface]
([CompanyID], [OSInterfaceID], [OSInterfaceCode], [OSInterfaceName], [CooperationSys], [CSysAccreditID], [JobFrequency], [ServerviceURL], [Remark], [AllowUsed], [ModifyDTM])
VALUES
(N'00000000', N'AD0000004', N'CreateReceipt', N'恒康-FayaAdibus生成采购到货服务', N'CreateReceiptService', N'', NULL, N'', NULL, 1, CAST(N'2017-09-26T00:00:00.000' AS DateTime));
GO


-- =============================================
-- HK_Adibus_ScheduleTask：不存在才新增
-- =============================================
IF NOT EXISTS (
    SELECT 1 FROM [HK_Adibus_ScheduleTask]
    WHERE [CompanyID] = N'00000000' AND [OSInterfaceID] = N'AD0000001'
)
INSERT INTO [HK_Adibus_ScheduleTask]
([Name], [Seconds], [Type], [Enabled], [StopOnError], [LastStartUtc], [LastEndUtc], [LastSuccessUtc], [CompanyID], [OSInterfaceID])
VALUES
(N'恒康-Faya获取Adibus预计发货单服务', 5, N'HK_EDI.Web.Tasks.GetEstimatedShipmentTask,HK_EDI.Web', 0, 0, '2018-05-21 10:25', '2018-05-21 10:25', '2018-05-21 10:25', N'00000000', N'AD0000001');
GO

IF NOT EXISTS (
    SELECT 1 FROM [HK_Adibus_ScheduleTask]
    WHERE [CompanyID] = N'00000000' AND [OSInterfaceID] = N'AD0000002'
)
INSERT INTO [HK_Adibus_ScheduleTask]
([Name], [Seconds], [Type], [Enabled], [StopOnError], [LastStartUtc], [LastEndUtc], [LastSuccessUtc], [CompanyID], [OSInterfaceID])
VALUES
(N'恒康-FayaAdibus预计发货单生成期货采购订单服务', 5, N'HK_EDI.Web.Tasks.CreateCGOrderTask,HK_EDI.Web', 0, 0, '2018-05-21 10:25', '2018-05-21 10:25', '2018-05-21 10:25', N'00000000', N'AD0000002');
GO

IF NOT EXISTS (
    SELECT 1 FROM [HK_Adibus_ScheduleTask]
    WHERE [CompanyID] = N'00000000' AND [OSInterfaceID] = N'AD0000003'
)
INSERT INTO [HK_Adibus_ScheduleTask]
([Name], [Seconds], [Type], [Enabled], [StopOnError], [LastStartUtc], [LastEndUtc], [LastSuccessUtc], [CompanyID], [OSInterfaceID])
VALUES
(N'恒康-Faya获取Adibus装箱单服务', 5, N'HK_EDI.Web.Tasks.GetPackingListTask,HK_EDI.Web', 0, 0, '2018-05-21 10:25', '2018-05-21 10:25', '2018-05-21 10:25', N'00000000', N'AD0000003');
GO

IF NOT EXISTS (
    SELECT 1 FROM [HK_Adibus_ScheduleTask]
    WHERE [CompanyID] = N'00000000' AND [OSInterfaceID] = N'AD0000004'
)
INSERT INTO [HK_Adibus_ScheduleTask]
([Name], [Seconds], [Type], [Enabled], [StopOnError], [LastStartUtc], [LastEndUtc], [LastSuccessUtc], [CompanyID], [OSInterfaceID])
VALUES
(N'恒康-FayaAdibus装箱单生成采购到货服务', 5, N'HK_EDI.Web.Tasks.CreateReceiptTask,HK_EDI.Web', 0, 0, '2018-05-21 10:25', '2018-05-21 10:25', '2018-05-21 10:25', N'00000000', N'AD0000004');
GO

-- =============================================
-- FayaAdibus 补货单：获取补货单 / 生成补货单
-- =============================================
IF NOT EXISTS (
    SELECT 1 FROM [dbo].[SD_Adibus_Interface]
    WHERE [CompanyID] = N'00000000' AND [OSInterfaceID] = N'AD0000005'
)
INSERT [dbo].[SD_Adibus_Interface]
([CompanyID], [OSInterfaceID], [OSInterfaceCode], [OSInterfaceName], [CooperationSys], [CSysAccreditID], [JobFrequency], [ServerviceURL], [Remark], [AllowUsed], [ModifyDTM])
VALUES
(N'00000000', N'AD0000005', N'GetReplenishmentOrder', N'恒康-Faya获取Adibus补货单服务', N'GetReplenishmentOrderService', N'', NULL, N'', NULL, 1, CAST(N'2026-11-04T00:00:00.000' AS DateTime));
GO

IF NOT EXISTS (
    SELECT 1 FROM [dbo].[SD_Adibus_Interface]
    WHERE [CompanyID] = N'00000000' AND [OSInterfaceID] = N'AD0000006'
)
INSERT [dbo].[SD_Adibus_Interface]
([CompanyID], [OSInterfaceID], [OSInterfaceCode], [OSInterfaceName], [CooperationSys], [CSysAccreditID], [JobFrequency], [ServerviceURL], [Remark], [AllowUsed], [ModifyDTM])
VALUES
(N'00000000', N'AD0000006', N'CreateReplenishmentOrder', N'恒康-FayaAdibus生成补货单(调拨申请单)服务', N'CreateReplenishmentOrderService', N'', NULL, N'', NULL, 1, CAST(N'2026-11-04T00:00:00.000' AS DateTime));
GO

IF NOT EXISTS (
    SELECT 1 FROM [HK_Adibus_ScheduleTask]
    WHERE [CompanyID] = N'00000000' AND [OSInterfaceID] = N'AD0000005'
)
INSERT INTO [HK_Adibus_ScheduleTask]
([Name], [Seconds], [Type], [Enabled], [StopOnError], [LastStartUtc], [LastEndUtc], [LastSuccessUtc], [CompanyID], [OSInterfaceID])
VALUES
(N'恒康-Faya获取Adibus补货单服务', 5, N'HK_EDI.Web.Tasks.GetReplenishmentOrderTask,HK_EDI.Web', 0, 0, '2018-05-21 10:25', '2018-05-21 10:25', '2018-05-21 10:25', N'00000000', N'AD0000005');
GO

IF NOT EXISTS (
    SELECT 1 FROM [HK_Adibus_ScheduleTask]
    WHERE [CompanyID] = N'00000000' AND [OSInterfaceID] = N'AD0000006'
)
INSERT INTO [HK_Adibus_ScheduleTask]
([Name], [Seconds], [Type], [Enabled], [StopOnError], [LastStartUtc], [LastEndUtc], [LastSuccessUtc], [CompanyID], [OSInterfaceID])
VALUES
(N'恒康-FayaAdibus生成补货单(调拨申请单)服务', 5, N'HK_EDI.Web.Tasks.CreateReplenishmentOrderTask,HK_EDI.Web', 0, 0, '2018-05-21 10:25', '2018-05-21 10:25', '2018-05-21 10:25', N'00000000', N'AD0000006');
GO


IF NOT EXISTS (
    SELECT 1 FROM [dbo].[SD_Adibus_Interface]
    WHERE [CompanyID] = N'00000000' AND [OSInterfaceID] = N'AD0000007'
)
INSERT [dbo].[SD_Adibus_Interface]
([CompanyID], [OSInterfaceID], [OSInterfaceCode], [OSInterfaceName], [CooperationSys], [CSysAccreditID], [JobFrequency], [ServerviceURL], [Remark], [AllowUsed], [ModifyDTM])
VALUES
(N'00000000', N'AD0000007', N'PushReplenishmentOrderConfirm', N'恒康-FayaAdibus生成补货单(调拨申请单)确认补货单数量服务', N'PushReplenishmentOrderConfirmService', N'', NULL, N'', NULL, 1, CAST(N'2026-11-04T00:00:00.000' AS DateTime));
GO
IF NOT EXISTS (
    SELECT 1 FROM [HK_Adibus_ScheduleTask]
    WHERE [CompanyID] = N'00000000' AND [OSInterfaceID] = N'AD0000007'
)
INSERT INTO [HK_Adibus_ScheduleTask]
([Name], [Seconds], [Type], [Enabled], [StopOnError], [LastStartUtc], [LastEndUtc], [LastSuccessUtc], [CompanyID], [OSInterfaceID])
VALUES
(N'恒康-FayaAdibus生成补货单(调拨申请单)确认补货单数量服务', 0, N'HK_EDI.Web.Tasks.PushReplenishmentOrderConfirmTask,HK_EDI.Web', 0, 0, '2018-05-21 10:25', '2018-05-21 10:25', '2018-05-21 10:25', N'00000000', N'AD0000007');
GO

