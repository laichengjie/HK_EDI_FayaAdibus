-- =============================================================
-- FayaAdibus 获取补货单：数据表
-- 字段与 GetReplenishmentOrderBaseBODY 保持一致（AddDataMethod 按属性名反射插入）
-- =============================================================
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HK_Faya_AdibusGetReplenishmentOrder]') AND type = N'U')
BEGIN
    CREATE TABLE [dbo].[HK_Faya_AdibusGetReplenishmentOrder](
        [ReplenishmentNo] [nvarchar](100) NULL,
        [TelNo]           [nvarchar](100) NULL,
        [HU_No]           [nvarchar](100) NULL,
        [EAN]             [nvarchar](100) NULL,
        [Address]         [nvarchar](500) NULL,
        [CartonNo]        [nvarchar](100) NULL,
        [ETA]             [datetime]      NULL,
        [Vender]          [nvarchar](200) NULL,
        [CartonWeight]    [nvarchar](50)  NULL,
        [ItemSize]        [nvarchar](50)  NULL,
        [ShipTo]          [nvarchar](200) NULL,
        [Division]        [nvarchar](100) NULL,
        [Brand]           [nvarchar](100) NULL,
        [Contacts]        [nvarchar](100) NULL,
        [SoldTo]          [nvarchar](200) NULL,
        [PNNo]            [nvarchar](100) NULL,
        [ETD]             [datetime]      NULL,
        [SONo]            [nvarchar](100) NULL,
        [ArticleNo]       [nvarchar](100) NULL,
        [Quantity]        [nvarchar](50)  NULL
    ) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_HK_Faya_AdibusGetReplenishmentOrder_No' AND object_id = OBJECT_ID(N'[dbo].[HK_Faya_AdibusGetReplenishmentOrder]'))
BEGIN
    CREATE INDEX [IX_HK_Faya_AdibusGetReplenishmentOrder_No]
        ON [dbo].[HK_Faya_AdibusGetReplenishmentOrder]([ReplenishmentNo])
END
GO
