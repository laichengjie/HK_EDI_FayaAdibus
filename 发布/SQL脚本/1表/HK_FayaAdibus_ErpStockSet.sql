SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

--法雅零触平台erp仓库设置类型

if not exists (select 1 from  sysobjects where  id = object_id('HK_FayaAdibus_ErpStockSet') and type = 'U')
Begin
	CREATE TABLE [dbo].[HK_FayaAdibus_ErpStockSet](
		[ID] BIGINT NOT NULL IDENTITY,
		[ErpStockID] NVARCHAR(100) NOT NULL, --仓库
		[IsDemorderForType] [bit] NOT NULL DEFAULT 0,--是否按类型拆单
		[ModifyDTM] DATETIME NOT NULL DEFAULT GETDATE(),  --修改时间
	 CONSTRAINT [PK_HK_FayaAdibus_ErpStockSet] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
End
GO




