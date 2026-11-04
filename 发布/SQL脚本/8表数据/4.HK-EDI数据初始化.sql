
	--Delete HK_Adibus_ScheduleTask where [Type]='HK_EDI.Web.Tasks.KeepAliveTask,HK_EDI.Web'
	--Go
	--Insert Into [HK_Adibus_ScheduleTask] ([Name],[Seconds],[Type],[Enabled],[StopOnError],[LastStartUtc],[LastEndUtc],[LastSuccessUtc],[CompanyID],[OSInterfaceID]) Values (N'保持网站运行状态',300,N'HK_EDI.Web.Tasks.KeepAliveTask,HK_EDI.Web',1,0,'08 22 2015  5:34PM','08 22 2015  5:34PM','08 22 2015  5:34PM','',N'')
	--go

	--Delete HK_Adibus_ScheduleTask where [Type]='HK_EDI.Web.Tasks.AutoCancelOrdersTask,HK_EDI.Web'
	--Go
	--Insert Into [HK_Adibus_ScheduleTask] ([Name],[Seconds],[Type],[Enabled],[StopOnError],[LastStartUtc],[LastEndUtc],[LastSuccessUtc],[CompanyID],[OSInterfaceID]) Values (N'自动撤单',1000,N'HK_EDI.Web.Tasks.AutoCancelOrdersTask,HK_EDI.Web',1,0,NULL,NULL,NULL,'',N'')
	--go

	--HK_Adibus_CustomerRole
	Delete HK_Adibus_CustomerRole where SystemName in('Administrators','Registered','Guests','WebServiceManager')
	Go
	Insert Into [HK_Adibus_CustomerRole] ([Name],[FreeShipping],[TaxExempt],[Active],[IsSystemRole],[SystemName],[PurchasedWithProductId]) Values (N'系统管理员',0,0,1,1,N'Administrators',0)
	Insert Into [HK_Adibus_CustomerRole] ([Name],[FreeShipping],[TaxExempt],[Active],[IsSystemRole],[SystemName],[PurchasedWithProductId]) Values (N'注册用户',0,0,1,1,N'Registered',0)
	Insert Into [HK_Adibus_CustomerRole] ([Name],[FreeShipping],[TaxExempt],[Active],[IsSystemRole],[SystemName],[PurchasedWithProductId]) Values (N'访客',0,0,1,1,N'Guests',0)
	Insert Into [HK_Adibus_CustomerRole] ([Name],[FreeShipping],[TaxExempt],[Active],[IsSystemRole],[SystemName],[PurchasedWithProductId]) Values (N'Web API Service 管理',0,0,1,1,N'WebServiceManager',0)
	Go

	--HK_Adibus_Customer
	Delete HK_Adibus_Customer where UserName in('admin@hengkangit.com','webapi@hengkangit.com')
	Go
	Insert Into [HK_Adibus_Customer] ([CustomerGuid],[Username],[Email],[Password],[PasswordSalt],[AdminComment],[Active],[Deleted],[SystemName],[LastIpAddress],[CreatedOnUtc],[LastLoginDateUtc],[LastActivityDateUtc],[BillingAddress_Id],[ShippingAddress_Id]) Values (NEWID(),N'admin@hengkangit.com',N'admin@hengkangit.com',N'23A1A164745D8B14A2C6A6257061499DE612D6DD',N'BWcvcOg=',NULL,1,0,NULL,N'192.168.99.9','05  6 2015  3:43AM','08 22 2015  3:00PM','08 18 2015  1:19AM',411,411)
	Insert Into [HK_Adibus_Customer] ([CustomerGuid],[Username],[Email],[Password],[PasswordSalt],[AdminComment],[Active],[Deleted],[SystemName],[LastIpAddress],[CreatedOnUtc],[LastLoginDateUtc],[LastActivityDateUtc],[BillingAddress_Id],[ShippingAddress_Id]) Values (NEWID(),N'webapi@hengkangit.com',N'webapi@hengkangit.com',N'681E2A29C57B3D607733B0006CC98823A06FA719',N'PWgju6A=',NULL,1,0,NULL,NULL,'08 18 2015  1:57PM','08 18 2015  3:05PM','08 18 2015  1:57PM',NULL,NULL)
	Go

	--HK_Adibus_Customer_CustomerRole_Mapping
	INSERT into dbo.HK_Adibus_Customer_CustomerRole_Mapping
			( Customer_Id, CustomerRole_Id )
	SELECT a.Id AS Customer_Id, b.Id AS CustomerRole_Id
	FROM dbo.HK_Adibus_Customer a
	FULL JOIN dbo.HK_Adibus_CustomerRole  b ON  b.SystemName <> 'Guests'
	WHERE UserName in('admin@hengkangit.com','webapi@hengkangit.com')
		AND NOT EXISTS(
		SELECT * FROM HK_Adibus_Customer_CustomerRole_Mapping 
		WHERE Customer_Id=a.Id AND CustomerRole_Id=b.Id)

	--HK_Adibus_PermissionRecord
	Delete HK_Adibus_PermissionRecord 
	 Go
	Insert Into [HK_Adibus_PermissionRecord] ([Name],[SystemName],[Category]) Values (N'访问后台',N'AccessAdminPanel',N'Standard')
	Insert Into [HK_Adibus_PermissionRecord] ([Name],[SystemName],[Category]) Values (N'管理用户',N'ManageCustomers',N'Customers')
	Insert Into [HK_Adibus_PermissionRecord] ([Name],[SystemName],[Category]) Values (N'管理订单',N'ManageOrders',N'Orders')
	Insert Into [HK_Adibus_PermissionRecord] ([Name],[SystemName],[Category]) Values (N'管理配置',N'ManageSettings',N'Configuration')
	Insert Into [HK_Adibus_PermissionRecord] ([Name],[SystemName],[Category]) Values (N'查看系统日志',N'ManageSystemLog',N'Configuration')
	Insert Into [HK_Adibus_PermissionRecord] ([Name],[SystemName],[Category]) Values (N'管理任务计划',N'ManageScheduleTasks',N'Configuration')
	Insert Into [HK_Adibus_PermissionRecord] ([Name],[SystemName],[Category]) Values (N'访问Web API',N'AccessWebApiService',N'Plugin')
	Insert Into [HK_Adibus_PermissionRecord] ([Name],[SystemName],[Category]) Values (N'管理用户访问权限',N'ManageACL',N'Configuration')
	 Go

	--HK_Adibus_PermissionRecord_Role_Mapping
	INSERT INTO dbo.HK_Adibus_PermissionRecord_Role_Mapping
				 ( PermissionRecord_Id ,
				   CustomerRole_Id
				 )
	SELECT a.ID AS PermissionRecord_Id, b.Id AS CustomerRole_Id
	FROM dbo.HK_Adibus_PermissionRecord a
	FULL JOIN dbo.HK_Adibus_CustomerRole b ON 1=1
	WHERE b.SystemName ='Administrators'
		AND NOT EXISTS(SELECT * FROM HK_Adibus_PermissionRecord_Role_Mapping 
		WHERE PermissionRecord_Id = a.id AND CustomerRole_Id=b.Id)

