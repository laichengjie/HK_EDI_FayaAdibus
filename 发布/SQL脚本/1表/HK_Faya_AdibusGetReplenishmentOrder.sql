-- =============================================================
-- FayaAdibus 获取补货单（MW FRS Replenishment order）：数据表
-- 字段与 GetReplenishmentOrderBaseBODY 保持一致（AddDataMethod 按属性名反射插入）
-- CurrentPage / TotalPage 为分页字段，不入库
-- =============================================================
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HK_Faya_AdibusGetReplenishmentOrder]') AND type = N'U')
BEGIN
    CREATE TABLE [dbo].[HK_Faya_AdibusGetReplenishmentOrder](
        [Id]                [int] IDENTITY(1,1) NOT NULL,
        [DocType]           [nvarchar](20)  NULL,
        [DocNo]             [nvarchar](100) NULL,
        [Customer]          [nvarchar](100) NULL,
        [Branch]            [nvarchar](100) NULL,
        [DeparturePOSCode]  [nvarchar](100) NULL,
        [DestinationPOSCode][nvarchar](100) NULL,
        [Article]           [nvarchar](100) NULL,
        [Size]              [nvarchar](50)  NULL,
        [BarCode]           [nvarchar](100) NULL,
        [ProposedQTY]       [nvarchar](50)  NULL,
        [BackupField1]      [nvarchar](200) NULL,
        [BackupField2]      [nvarchar](200) NULL,
        [BackupField3]      [nvarchar](200) NULL,
        [BackupField4]      [nvarchar](200) NULL,
        [BackupField5]      [nvarchar](200) NULL,
        [UploadState]       [int] NOT NULL CONSTRAINT [DF_HK_Faya_AdibusGetReplenishmentOrder_UploadState] DEFAULT ((0)),
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
