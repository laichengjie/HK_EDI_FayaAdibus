
IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_EDI_ActivityLogType]')
                        AND type IN ( N'U' ) ) 
    BEGIN
        CREATE TABLE [dbo].[HK_EDI_ActivityLogType]
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
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_EDI_ActivityLog]')
                        AND type IN ( N'U' ) ) 
    BEGIN

        CREATE TABLE [dbo].[HK_EDI_ActivityLog]
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



        ALTER TABLE [dbo].[HK_EDI_ActivityLog]  WITH CHECK ADD  CONSTRAINT [ActivityLog_ActivityLogType] FOREIGN KEY([ActivityLogTypeId])
        REFERENCES [dbo].[HK_EDI_ActivityLogType] ([Id])
        ON DELETE CASCADE


        ALTER TABLE [dbo].[HK_EDI_ActivityLog] CHECK CONSTRAINT [ActivityLog_ActivityLogType]

    END 


IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_EDI_Customer]')
                        AND type IN ( N'U' ) ) 
    BEGIN

        CREATE TABLE [dbo].[HK_EDI_Customer]
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
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_EDI_CustomerRole]')
                        AND type IN ( N'U' ) ) 
    BEGIN

        CREATE TABLE [dbo].[HK_EDI_CustomerRole]
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
              CONSTRAINT [PK__Customer__3214EC074F7CD00D] PRIMARY KEY CLUSTERED
                ( [Id] ASC )
                WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
                       IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                       ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
            )
        ON  [PRIMARY]

    END 


IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_EDI_PermissionRecord]')
                        AND type IN ( N'U' ) ) 
    BEGIN
        CREATE TABLE [dbo].[HK_EDI_PermissionRecord]
            (
              [Id] [int] IDENTITY(1, 1)
                         NOT NULL ,
              [Name] [nvarchar](MAX) NOT NULL ,
              [SystemName] [nvarchar](255) NOT NULL ,
              [Category] [nvarchar](255) NOT NULL ,
              CONSTRAINT [PK__Permissi__3214EC074D5F7D71] PRIMARY KEY CLUSTERED
                ( [Id] ASC )
                WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
                       IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                       ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
            )
        ON  [PRIMARY] TEXTIMAGE_ON [PRIMARY]
    END 


IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_EDI_Customer_CustomerRole_Mapping]')
                        AND type IN ( N'U' ) ) 
    BEGIN 
        CREATE TABLE [dbo].[HK_EDI_Customer_CustomerRole_Mapping]
            (
              [Customer_Id] [int] NOT NULL ,
              [CustomerRole_Id] [int] NOT NULL ,
              CONSTRAINT [PK__Customer__ABACF0F74BAC3F29] PRIMARY KEY CLUSTERED
                ( [Customer_Id] ASC, [CustomerRole_Id] ASC )
                WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
                       IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                       ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
            )
        ON  [PRIMARY]
    END 


IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_EDI_PermissionRecord_Role_Mapping]')
                        AND type IN ( N'U' ) ) 
    BEGIN 

        CREATE TABLE [dbo].[HK_EDI_PermissionRecord_Role_Mapping]
            (
              [PermissionRecord_Id] [int] NOT NULL ,
              [CustomerRole_Id] [int] NOT NULL ,
              CONSTRAINT [PK__Permissi__4804FB2651300E55] PRIMARY KEY CLUSTERED
                ( [PermissionRecord_Id] ASC, [CustomerRole_Id] ASC )
                WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
                       IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
                       ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
            )
        ON  [PRIMARY]

    END 


IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_EDI_JobOrderTrace]')
                        AND type IN ( N'U' ) ) 
    BEGIN 

	CREATE TABLE [dbo].[HK_EDI_JobOrderTrace](
	[Id] [int] IDENTITY(10000,1) NOT NULL,
	[CompanyID] [nvarchar](10) NOT NULL,
	[NoticeBillNo] [nvarchar](40) NOT NULL,
	[OpDirection] [nvarchar](10) NOT NULL,
	[OSStockID] [nvarchar](20) NOT NULL,
	[OSStockCode] [nvarchar](40) NULL,
	[BillTypeID] [nvarchar](20) NULL,
	[ReadDate] [datetime] NULL,
	[CancelDate] [datetime] NULL,
	[InsertedDate] [datetime] NULL,
	[JoinOrderOpStatus] [int] NULL,
	[OpResultStatus] [int] NULL,
	[TransBillNo] [nvarchar](40) NULL,
	[IsHandling] [nvarchar](10) NULL,
	[Remark] [nvarchar](1024) NULL,
	[CreateDate] [datetime] NULL,
	[BoundQty] [numeric](24, 6) NULL,
 CONSTRAINT [PK_HK_EDI_JobOrderTrace_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END 


IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_EDI_JobOrderTraceDetail]')
                        AND type IN ( N'U' ) ) 
    BEGIN 

CREATE TABLE [dbo].[HK_EDI_JobOrderTraceDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyID] [nvarchar](10) NOT NULL,
	[BillNo] [nvarchar](40) NOT NULL,
	[SignCode] [nvarchar](50) NOT NULL,
	[ContentNode] [nvarchar](max) NULL,
	[ContentNodeRequest] [nvarchar](max) NULL,
	[ContentType] [int] NULL,
	[IsEffective] [bit] NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyTime] [datetime] NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_HK_EDI_JobOrderTraceDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END 


IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_EDI_Log]')
                        AND type IN ( N'U' ) ) 
    BEGIN 



CREATE TABLE [dbo].[HK_EDI_Log](
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
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_EDI_ScheduleTask]')
                        AND type IN ( N'U' ) ) 
    BEGIN 

CREATE TABLE [dbo].[HK_EDI_ScheduleTask](
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
	[OSStockID] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


END 

IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_EDI_TaskRunTime]')
                        AND type IN ( N'U' ) ) 
    BEGIN 

CREATE TABLE [dbo].[HK_EDI_TaskRunTime](
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
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_EDI_Setting]')
                        AND type IN ( N'U' ) ) 
    BEGIN 

CREATE TABLE [dbo].[HK_EDI_Setting](
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
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_EDI_PostSkuErrorRecords]')
                        AND type IN ( N'U' ) ) 
    BEGIN 


CREATE TABLE [dbo].[HK_EDI_PostSkuErrorRecords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SKUCode] [nvarchar](50) NULL,
	[CreateTime] [datetime] NOT NULL,
	[InterfaceType] [int] NOT NULL,
 CONSTRAINT [PK_HK_EDI_PostSkuErrorRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


END

IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_EDI_PostSkuRecordsDetail]')
                        AND type IN ( N'U' ) ) 
    BEGIN
  
  CREATE TABLE [dbo].[HK_EDI_PostSkuRecordsDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PId] [int] NULL,
	[Contents] [nvarchar](500) NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_HK_ERP_PostSkuRecordsDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


  
  END   


  IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_EDI_TransDetail_Diff]')
                        AND type IN ( N'U' ) ) 
    BEGIN
  CREATE TABLE [dbo].[HK_EDI_TransDetail_Diff](
	[CompanyID] [dbo].[T_ComCode] NOT NULL,
	[BillNo] [dbo].[T_BillNo] NOT NULL,
	[MaterialID] [dbo].[T_ID] NOT NULL,
	[SizeID] [dbo].[T_ID] NOT NULL,
	[Qty] [dbo].[T_Numeric6] NULL,
	[WMSQty] [dbo].[T_Numeric6] NULL,
	[BarCode] [dbo].[T_Code_L] NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_HK_EDI_TransDetail_Diff_1] PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC,
	[BillNo] ASC,
	[MaterialID] ASC,
	[SizeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
  END 






