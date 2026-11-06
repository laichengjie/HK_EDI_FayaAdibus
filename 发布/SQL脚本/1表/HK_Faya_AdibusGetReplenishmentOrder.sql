-- =============================================================
-- FayaAdibus 获取补货单（MW FRS Replenishment order）：数据表
-- 字段与 GetReplenishmentOrderBaseBODY 保持一致（AddDataMethod 按属性名反射插入）
-- CurrentPage / TotalPage 为分页字段，不入库
-- =============================================================
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HK_Faya_AdibusGetReplenishmentOrder]') AND type = N'U')
BEGIN
    CREATE TABLE [dbo].[HK_Faya_AdibusGetReplenishmentOrder](
        [Id]                [int] IDENTITY(1,1) NOT NULL,
        [DocType]           [nvarchar](20)  NULL,--单据类型
        [DocNo]             [nvarchar](100) NULL,--单据号
        [Customer]          [nvarchar](100) NULL,--客户
        [Branch]            [nvarchar](100) NULL,--分公司
        [DeparturePOSCode]  [nvarchar](100) NULL,--发货仓编号
        [DestinationPOSCode][nvarchar](100) NULL,--收货店铺编号
        [Article]           [nvarchar](100) NULL,--货号
        [Size]              [nvarchar](50)  NULL,--尺码
        [BarCode]           [nvarchar](100) NULL,--条码
        [ProposedQTY]       [nvarchar](50)  NULL,--备用字段
        [BackupField1]      [nvarchar](200) NULL,--备用字段
        [BackupField2]      [nvarchar](200) NULL,--备用字段
        [BackupField3]      [nvarchar](200) NULL,--备用字段
        [BackupField4]      [nvarchar](200) NULL,--备用字段
        [BackupField5]      [nvarchar](200) NULL,--备用字段
		[CurrentPage]       [INT] NULL,--当前页码
		[TotalPage]         [INT] NULL,--总页码
		[ChkNums]   [INT]   DEFAULT (0) NULL,--总页码
		[OutStockID] VARCHAR(20) NULL ,
        [InStockID] VARCHAR(20) NULL ,
		[BillNo] [VARCHAR](40) NULL ,--ERP调拨申请单单号
        [UploadState]       [int] NOT NULL
		 CONSTRAINT [DF_HK_Faya_AdibusGetReplenishmentOrder_UploadState] DEFAULT ((0)),
        CONSTRAINT [PK_HK_Faya_AdibusGetReplenishmentOrder] PRIMARY KEY CLUSTERED ([Id] ASC)
    ) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_HK_Faya_AdibusGetReplenishmentOrder_DocNo' AND object_id = OBJECT_ID(N'[dbo].[HK_Faya_AdibusGetReplenishmentOrder]'))
BEGIN
    CREATE INDEX [IX_HK_Faya_AdibusGetReplenishmentOrder_DocNo]
        ON [dbo].[HK_Faya_AdibusGetReplenishmentOrder]([DocNo])
END
GO



