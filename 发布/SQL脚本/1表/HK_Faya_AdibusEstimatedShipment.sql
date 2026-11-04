--法雅Adibus预计发货单数据
  IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_Faya_AdibusEstimatedShipment]')
                        AND type IN ( N'U' ) ) 
    BEGIN

CREATE TABLE [dbo].[HK_Faya_AdibusEstimatedShipment](
    Id INT IDENTITY(1, 1) NOT NULL ,
	DeliveryNo varchar(100) NULL,
	CreateDate VARCHAR(100) NULL,
	CustomerCode VARCHAR(100) NULL,
	STC varchar(1000) NULL,
	SalesOrder VARCHAR(100) NULL,
	OrderType VARCHAR(100) NULL,
	Status VARCHAR(100) NULL,
	ArticleNo VARCHAR(100) NULL,
	Size VARCHAR(100) NULL,
	QTY INT NULL,
	NetValue [T_Numeric6] NULL,
	NetValueWithVAT [T_Numeric6] NULL,
	Plant VARCHAR(100) NULL,
	EAN VARCHAR(100) NULL,
	OTB_Season VARCHAR(100) NULL,
	UploadState INT  NOT NULL Default 0, --操作状态
	InsertData DATETIME NOT NULL DEFAULT GETDATE(), --数据插入时间
	ModifyDTM  DATETIME NOT NULL DEFAULT GETDATE(), --修改时间
	RequestData NVARCHAR(MAX) NULL
 CONSTRAINT [PK_HK_Faya_AdibusEstimatedShipment] PRIMARY KEY CLUSTERED 
(
	Id ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END 
