
Delete From [SD_Adibus_Interface]  ;
 Go 
  INSERT [dbo].[SD_Adibus_Interface] ([CompanyID], [OSInterfaceID], [OSInterfaceCode], [OSInterfaceName],  [CooperationSys], [CSysAccreditID], [JobFrequency],  [ServerviceURL],  [Remark], [AllowUsed], [ModifyDTM] ) 
 VALUES (N'00000000', N'AD0000001', N'GetEstimatedShipment', N'恒康-Faya获取Adibus预计发货单服务', N'GetEstimatedShipmentService', N'', NULL,  N'',NULL, 1, CAST(N'2017-09-26T00:00:00.000' AS DateTime))
  
  INSERT [dbo].[SD_Adibus_Interface] ([CompanyID], [OSInterfaceID], [OSInterfaceCode], [OSInterfaceName],  [CooperationSys], [CSysAccreditID], [JobFrequency],  [ServerviceURL],  [Remark], [AllowUsed], [ModifyDTM] ) 
 VALUES (N'00000000', N'AD0000002', N'CreateCGOrder', N'恒康-FayaAdibus生成采购订单服务', N'CreateCGOrderService', N'', NULL,  N'',NULL, 1, CAST(N'2017-09-26T00:00:00.000' AS DateTime))

  INSERT [dbo].[SD_Adibus_Interface] ([CompanyID], [OSInterfaceID], [OSInterfaceCode], [OSInterfaceName],  [CooperationSys], [CSysAccreditID], [JobFrequency],  [ServerviceURL],  [Remark], [AllowUsed], [ModifyDTM] ) 
 VALUES (N'00000000', N'AD0000003', N'GetPackingList', N'恒康-Faya获取Adibus装箱单服务', N'GetPackingListService', N'', NULL,  N'',NULL, 1, CAST(N'2017-09-26T00:00:00.000' AS DateTime))

  INSERT [dbo].[SD_Adibus_Interface] ([CompanyID], [OSInterfaceID], [OSInterfaceCode], [OSInterfaceName],  [CooperationSys], [CSysAccreditID], [JobFrequency],  [ServerviceURL],  [Remark], [AllowUsed], [ModifyDTM] ) 
 VALUES (N'00000000', N'AD0000004', N'CreateReceipt', N'恒康-FayaAdibus生成采购到货服务', N'CreateReceiptService', N'', NULL,  N'',NULL, 1, CAST(N'2017-09-26T00:00:00.000' AS DateTime))

 GO


 Delete From [HK_Adibus_ScheduleTask] ;
 Go
Insert Into [HK_Adibus_ScheduleTask] ([Name],[Seconds],[Type],[Enabled],[StopOnError],[LastStartUtc],[LastEndUtc],[LastSuccessUtc],[CompanyID],[OSInterfaceID]) 
VALUES (N'恒康-Faya获取Adibus预计发货单服务',5,N'HK_EDI.Web.Tasks.GetEstimatedShipmentTask,HK_EDI.Web',0,0,'2018-05-21 10:25','2018-05-21 10:25','2018-05-21 10:25',N'00000000', N'AD0000001');
Insert Into [HK_Adibus_ScheduleTask] ([Name],[Seconds],[Type],[Enabled],[StopOnError],[LastStartUtc],[LastEndUtc],[LastSuccessUtc],[CompanyID],[OSInterfaceID])
 Values (N'恒康-FayaAdibus预计发货单生成期货采购订单服务',5,N'HK_EDI.Web.Tasks.CreateCGOrderTask,HK_EDI.Web',0,0,'2018-05-21 10:25','2018-05-21 10:25','2018-05-21 10:25',N'00000000', N'AD0000002');
Insert Into [HK_Adibus_ScheduleTask] ([Name],[Seconds],[Type],[Enabled],[StopOnError],[LastStartUtc],[LastEndUtc],[LastSuccessUtc],[CompanyID],[OSInterfaceID])
 Values (N'恒康-Faya获取Adibus装箱单服务',5,N'HK_EDI.Web.Tasks.GetPackingListTask,HK_EDI.Web',0,0,'2018-05-21 10:25','2018-05-21 10:25','2018-05-21 10:25',N'00000000', N'AD0000003');
Insert Into [HK_Adibus_ScheduleTask] ([Name],[Seconds],[Type],[Enabled],[StopOnError],[LastStartUtc],[LastEndUtc],[LastSuccessUtc],[CompanyID],[OSInterfaceID]) 
VALUES (N'恒康-FayaAdibus装箱单生成采购到货服务',5,N'HK_EDI.Web.Tasks.CreateReceiptTask,HK_EDI.Web',0,0,'2018-05-21 10:25','2018-05-21 10:25','2018-05-21 10:25',N'00000000', N'AD0000004');

GO