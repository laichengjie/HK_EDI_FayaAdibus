SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

--法雅零触平台供应商设置类型

if not exists (select 1 from  sysobjects where  id = object_id('HK_FayaAdibus_VendCustSet') and type = 'U')
Begin
	CREATE TABLE [dbo].[HK_FayaAdibus_VendCustSet](
		[ID] BIGINT NOT NULL IDENTITY,
		[VendCustID] NVARCHAR(100) NOT NULL, --供应商ID
		[OperationType] NVARCHAR(20) ,--单据类型
		[BillState] NVARCHAR(10) ,--单据状态
		[ModifyDTM] DATETIME NOT NULL DEFAULT GETDATE(),  --修改时间
	 CONSTRAINT [PK_HK_FayaAdibus_VendCustSet] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
End
GO
