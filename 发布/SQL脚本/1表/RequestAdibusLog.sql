
--添加法雅Adibus接口日志表
If Not Exists (Select 1 From  Sysobjects where  id = object_id('RequestAdibusLog') and type = 'U')
BEGIN
CREATE TABLE [dbo].[RequestAdibusLog]
(
 ID [bigint] NOT NULL IDENTITY(1, 1),
 InterfaceType [varchar] (20)  NOT NULL,
 RequestData NVARCHAR(Max)   NULL,
 ResponseData NVARCHAR(Max)  NULL,
 InsertData DATETIME NOT NULL DEFAULT GETDATE(), --数据插入时间
 ModifyDTM  DATETIME NOT NULL DEFAULT GETDATE(), --修改时间
) ON [PRIMARY]

ALTER TABLE [dbo].[RequestAdibusLog] ADD CONSTRAINT [PK_RequestAdibusLog] PRIMARY KEY CLUSTERED  ([ID]) ON [PRIMARY]
END
Go
