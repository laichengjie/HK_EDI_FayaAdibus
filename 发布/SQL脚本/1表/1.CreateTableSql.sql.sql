
IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_Adibus_ActivityLogType]')
                        AND type IN ( N'U' ) ) 
    BEGIN
        CREATE TABLE [dbo].[HK_Adibus_ActivityLogType]
            (
              [Id] [int] IDENTITY(1, 1)
                         NOT NULL ,
              [SystemKeyword] [nvarchar](100) NOT NULL ,
              [Name] [nvarchar](200) NOT NULL ,
              [Enabled] [bit] NOT NULL ,
              PRIMARY KEY CLUSTERED ( [Id] ASC )
                WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
                       IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                       ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
            )
        ON  [PRIMARY]

    END 


IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_Adibus_ActivityLog]')
                        AND type IN ( N'U' ) ) 
    BEGIN

        CREATE TABLE [dbo].[HK_Adibus_ActivityLog]
            (
              [Id] [int] IDENTITY(1, 1)
                         NOT NULL ,
              [ActivityLogTypeId] [int] NOT NULL ,
              [CustomerId] [int] NOT NULL ,
              [Comment] [nvarchar](MAX) NOT NULL ,
              [CreatedOnUtc] [datetime] NOT NULL ,
              PRIMARY KEY CLUSTERED ( [Id] ASC )
                WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
                       IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                       ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
            )
        ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]



        ALTER TABLE [dbo].[HK_Adibus_ActivityLog]  WITH CHECK ADD  CONSTRAINT [AdibusActivityLog_ActivityLogType] FOREIGN KEY([ActivityLogTypeId])
        REFERENCES [dbo].[HK_Adibus_ActivityLogType] ([Id])
        ON DELETE CASCADE


        ALTER TABLE [dbo].[HK_Adibus_ActivityLog] CHECK CONSTRAINT [AdibusActivityLog_ActivityLogType]

    END 


IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_Adibus_Customer]')
                        AND type IN ( N'U' ) ) 
    BEGIN

        CREATE TABLE [dbo].[HK_Adibus_Customer]
            (
              [Id] [int] IDENTITY(1, 1)
                         NOT NULL ,
              [CustomerGuid] [uniqueidentifier] NOT NULL ,
              [Username] [nvarchar](1000) NULL ,
              [Email] [nvarchar](1000) NULL ,
              [Password] [nvarchar](MAX) NULL ,
              [PasswordSalt] [nvarchar](MAX) NULL ,
              [AdminComment] [nvarchar](MAX) NULL ,
              [Active] [bit] NOT NULL ,
              [Deleted] [bit] NOT NULL ,
              [SystemName] [nvarchar](MAX) NULL ,
              [LastIpAddress] [nvarchar](MAX) NULL ,
              [CreatedOnUtc] [datetime] NOT NULL ,
              [LastLoginDateUtc] [datetime] NULL ,
              [LastActivityDateUtc] [datetime] NOT NULL ,
              [BillingAddress_Id] [int] NULL ,
              [ShippingAddress_Id] [int] NULL ,
              PRIMARY KEY CLUSTERED ( [Id] ASC )
                WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
                       IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                       ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
            )
        ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]

    END 


IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_Adibus_CustomerRole]')
                        AND type IN ( N'U' ) ) 
    BEGIN

        CREATE TABLE [dbo].[HK_Adibus_CustomerRole]
            (
              [Id] [int] IDENTITY(1, 1)
                         NOT NULL ,
              [Name] [nvarchar](255) NOT NULL ,
              [FreeShipping] [bit] NOT NULL ,
              [TaxExempt] [bit] NOT NULL ,
              [Active] [bit] NOT NULL ,
              [IsSystemRole] [bit] NOT NULL ,
              [SystemName] [nvarchar](255) NULL ,
              [PurchasedWithProductId] [int] NOT NULL ,
              CONSTRAINT [PK__AdibusCustomer__3214EC074F7CD00D] PRIMARY KEY CLUSTERED
                ( [Id] ASC )
                WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
                       IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                       ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
            )
        ON  [PRIMARY]

    END 


IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_Adibus_PermissionRecord]')
                        AND type IN ( N'U' ) ) 
    BEGIN
        CREATE TABLE [dbo].[HK_Adibus_PermissionRecord]
            (
              [Id] [int] IDENTITY(1, 1)
                         NOT NULL ,
              [Name] [nvarchar](MAX) NOT NULL ,
              [SystemName] [nvarchar](255) NOT NULL ,
              [Category] [nvarchar](255) NOT NULL ,
              CONSTRAINT [PK__AdibusPermissi__3214EC074D5F7D71] PRIMARY KEY CLUSTERED
                ( [Id] ASC )
                WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
                       IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                       ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
            )
        ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    END 


IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_Adibus_Customer_CustomerRole_Mapping]')
                        AND type IN ( N'U' ) ) 
    BEGIN 
        CREATE TABLE [dbo].[HK_Adibus_Customer_CustomerRole_Mapping]
            (
              [Customer_Id] [int] NOT NULL ,
              [CustomerRole_Id] [int] NOT NULL ,
              CONSTRAINT [PK__AdibusCustomer__ABACF0F74BAC3F29] PRIMARY KEY CLUSTERED
                ( [Customer_Id] ASC, [CustomerRole_Id] ASC )
                WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
                       IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                       ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
            )
        ON  [PRIMARY]
    END 


IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_Adibus_PermissionRecord_Role_Mapping]')
                        AND type IN ( N'U' ) ) 
    BEGIN 

        CREATE TABLE [dbo].[HK_Adibus_PermissionRecord_Role_Mapping]
            (
              [PermissionRecord_Id] [int] NOT NULL ,
              [CustomerRole_Id] [int] NOT NULL ,
              CONSTRAINT [PK__AdibusPermissi__4804FB2651300E55] PRIMARY KEY CLUSTERED
                ( [PermissionRecord_Id] ASC, [CustomerRole_Id] ASC )
                WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
                       IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                       ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
            )
        ON  [PRIMARY]

    END 


IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_Adibus_Log]')
                        AND type IN ( N'U' ) ) 
    BEGIN 



CREATE TABLE [dbo].[HK_Adibus_Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LogLevelId] [int] NOT NULL,
	[ShortMessage] [nvarchar](max) NOT NULL,
	[FullMessage] [nvarchar](max) NULL,
	[IpAddress] [nvarchar](200) NULL,
	[CustomerId] [int] NULL,
	[PageUrl] [nvarchar](max) NULL,
	[ReferrerUrl] [nvarchar](max) NULL,
	[CreatedOnUtc] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END 


IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_Adibus_TaskRunTime]')
                        AND type IN ( N'U' ) ) 
    BEGIN 

CREATE TABLE [dbo].[HK_Adibus_TaskRunTime](
	[Key] [nvarchar](500) NOT NULL,
	[LastTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END 



IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_Adibus_Setting]')
                        AND type IN ( N'U' ) ) 
    BEGIN 

CREATE TABLE [dbo].[HK_Adibus_Setting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Value] [nvarchar](2000) NOT NULL,
	[StoreId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END


IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_Adibus_PostSkuErrorRecords]')
                        AND type IN ( N'U' ) ) 
    BEGIN 


CREATE TABLE [dbo].[HK_Adibus_PostSkuErrorRecords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SKUCode] [nvarchar](50) NULL,
	[CreateTime] [datetime] NOT NULL,
	[InterfaceType] [int] NOT NULL,
	[IsSuccess] BIT NULL  ,
	[Qty] [int]  NULL,
	[LastQueryTime] datetime NULL,
	[OSInterfaceID] [nvarchar](50) NULL,
 CONSTRAINT [PK_HK_Adibus_PostSkuErrorRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


END

IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_Adibus_PostSkuRecordsDetail]')
                        AND type IN ( N'U' ) ) 
    BEGIN
  
  CREATE TABLE [dbo].[HK_Adibus_PostSkuRecordsDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PId] [int] NULL,
	[Contents] [nvarchar](500) NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_HK_AdibusERP_PostSkuRecordsDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


  
  END   


  IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_Adibus_TransDetail_Diff]')
                        AND type IN ( N'U' ) ) 
    BEGIN
  CREATE TABLE [dbo].[HK_Adibus_TransDetail_Diff](
	[CompanyID] [dbo].[T_ComCode] NOT NULL,
	[BillNo] [dbo].[T_BillNo] NOT NULL,
	[MaterialID] [dbo].[T_ID] NOT NULL,
	[SizeID] [dbo].[T_ID] NOT NULL,
	[Qty] [dbo].[T_Numeric6] NULL,
	[WMSQty] [dbo].[T_Numeric6] NULL,
	[BarCode] [dbo].[T_Code_L] NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_HK_Adibus_TransDetail_Diff_1] PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC,
	[BillNo] ASC,
	[MaterialID] ASC,
	[SizeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
  END 



  IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_Adibus_JobOrderTrace]')
                        AND type IN ( N'U' ) ) 
    BEGIN 

	CREATE TABLE [dbo].[HK_Adibus_JobOrderTrace](
	[Id] [int] IDENTITY(10000,1) NOT NULL,
	[CompanyID] [nvarchar](10) NOT NULL,
	[OSInterfaceID] [nvarchar](20) NOT NULL,
	[SignCode] [nvarchar](50) NOT NULL,
	[NoticeBillNo] [nvarchar](40) NULL,
	[BillTypeID] [nvarchar](20) NULL,
	[OpResultStatus] [int] NULL,
	[OrderType] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ReadDate] [datetime] NULL,
	[CancelDate] [datetime] NULL,
	[ShopID] [nvarchar](40) NULL,
	[BillDate] [datetime] NULL,
	[InsertedDate] [datetime] NULL
 CONSTRAINT [PK_HK_Adibus_JobOrderTrace] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END 

IF COL_LENGTH('HK_Adibus_JobOrderTrace','StockID') IS NULL
BEGIN
ALTER TABLE dbo.HK_Adibus_JobOrderTrace ADD StockID NVARCHAR(40)
END

IF COL_LENGTH('HK_Adibus_JobOrderTrace','CgbillNo') IS NULL
BEGIN
ALTER TABLE dbo.HK_Adibus_JobOrderTrace ADD CgbillNo NVARCHAR(2000)
END
IF COL_LENGTH('HK_Adibus_JobOrderTrace','CustomerCode') IS NULL
BEGIN
ALTER TABLE dbo.HK_Adibus_JobOrderTrace ADD CustomerCode NVARCHAR(200)
END


IF EXISTS ( 
 SELECT 1 FROM SYSOBJECTS T1 
 INNER JOIN SYSCOLUMNS T2 ON T1.ID=T2.ID 
 WHERE T1.NAME='HK_Adibus_JobOrderTrace' AND T2.NAME='NoticeBillNo'
 ) 
 BEGIN
   ALTER table HK_Adibus_JobOrderTrace drop column NoticeBillNo   
 END
  
  ALTER table HK_Adibus_JobOrderTrace add NoticeBillNo nvarchar(40) 
	 


IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_Adibus_JobOrderTraceDetail]')
                        AND type IN ( N'U' ) ) 
    BEGIN 

CREATE TABLE [dbo].[HK_Adibus_JobOrderTraceDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PId] [int] NULL,
	[CompanyID] [nvarchar](10) NOT NULL,
	[BillNo] [nvarchar](40)  NULL,
	[SignCode] [nvarchar](50) NOT NULL,
	[ContentNode] [nvarchar](max) NULL,
	[ContentNodeRequest] [nvarchar](max) NULL,
	[ContentType] [int] NULL,
	[IsEffective] [bit] NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyTime] [datetime] NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_HK_Adibus_JobOrderTraceDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END 


IF EXISTS ( 
 SELECT 1 FROM SYSOBJECTS T1 
 INNER JOIN SYSCOLUMNS T2 ON T1.ID=T2.ID 
 WHERE T1.NAME='HK_Adibus_JobOrderTraceDetail' AND T2.NAME='BillNo'
 ) 
 BEGIN
   ALTER table HK_Adibus_JobOrderTraceDetail drop column BillNo  
 END
  
  ALTER table HK_Adibus_JobOrderTraceDetail add BillNo nvarchar(40) 
	 
--自动任务表
IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_Adibus_ScheduleTask]')
                        AND type IN ( N'U' ) ) 
    BEGIN 

CREATE TABLE [dbo].[HK_Adibus_ScheduleTask](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Seconds] [int] NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[StopOnError] [bit] NOT NULL,
	[LastStartUtc] [datetime] NULL,
	[LastEndUtc] [datetime] NULL,
	[LastSuccessUtc] [datetime] NULL,
	[CompanyID] [dbo].[T_ComCode] NULL,
	[OSInterfaceID] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


END 



  IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[SD_Adibus_Interface]')
                        AND type IN ( N'U' ) ) 
    BEGIN

CREATE TABLE [dbo].[SD_Adibus_Interface](
	[CompanyID] [dbo].[T_ComCode] NOT NULL,
	[OSInterfaceID] [nvarchar](20) NOT NULL,
	[OSInterfaceCode] [nvarchar](40) NULL,
	[OSInterfaceName] [nvarchar](40) NULL,
	[CooperationSys] [nvarchar](50) NULL,
	[CSysAccreditID] [nvarchar](20) NULL,
	[JobFrequency] [varchar](20) NULL,
	[ServerviceURL] [nvarchar](250) NULL,
	[Remark] [dbo].[T_Explain_L] NULL,
	[AllowUsed] [dbo].[T_Boolean] NOT NULL,
	[ModifyDTM] [datetime] NOT NULL,
	
 CONSTRAINT [PK_SD_Adibus_Interface] PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC,
	[OSInterfaceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END 



--


/****** Object:  Table [dbo].[SD_Adibus_InterfaceDetail]    Script Date: 2017/9/26 18:01:43 ******/
  IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[SD_Adibus_InterfaceDetail]')
                        AND type IN ( N'U' ) ) 
    BEGIN
CREATE TABLE [dbo].[SD_Adibus_InterfaceDetail](
	[CompanyID] [dbo].[T_ComCode] NOT NULL,
	[OSInterfaceID] [nvarchar](20) NOT NULL,
	[StockID] [varchar](20) NOT NULL,
	[BelongsCompanyID] [varchar](20) NULL,
 CONSTRAINT [PK_SD_Adibus_InterfaceDetail] PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC,
	[OSInterfaceID] ASC,
	[StockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END 
