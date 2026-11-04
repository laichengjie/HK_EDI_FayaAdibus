--法雅Adibus装箱单数据
  IF NOT EXISTS ( SELECT  *
                FROM    sys.objects
                WHERE   object_id = OBJECT_ID(N'[dbo].[HK_Faya_AdibusGetPackingList]')
                        AND type IN ( N'U' ) ) 
    BEGIN

CREATE TABLE [dbo].[HK_Faya_AdibusGetPackingList](
    Id INT IDENTITY(1, 1) NOT NULL ,
	TelNo varchar(100) NULL,
	HU_No NVARCHAR(200) NULL,
	EAN varchar(100) NULL,
	ShipmentNo varchar(100) NULL,
	Address varchar(1000) NULL,
	CartonNo INT NULL,
	ETA DATETIME NULL,
	Vender varchar(200) NULL,
	CartonWeight [T_Numeric6] NULL,
	ItemSize varchar(100) NULL,
	ShipTo varchar(100) NULL,
	Division varchar(100) NULL,
	Brand varchar(100) NULL,
	Contacts varchar(100) NULL,
	SoldTo varchar(100) NULL,
	PNNo varchar(100) NULL,
	ETD DATETIME NULL,
	SONo varchar(100) NULL,
	ArticleNo varchar(100) NULL,
	Quantity [T_Numeric6]  NULL,
	UploadState INT  NOT NULL Default 0, --操作状态
	InsertData DATETIME NOT NULL DEFAULT GETDATE(), --数据插入时间
	ModifyDTM  DATETIME NOT NULL DEFAULT GETDATE(), --修改时间
	RequestData NVARCHAR(MAX) NULL
 CONSTRAINT [PK_HK_Faya_AdibusGetPackingList] PRIMARY KEY CLUSTERED 
(
	Id ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END 
