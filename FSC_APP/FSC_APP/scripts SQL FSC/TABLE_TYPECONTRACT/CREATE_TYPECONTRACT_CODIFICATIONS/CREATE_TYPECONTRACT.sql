-----------------NUEVA TABLA PARA EL COMBO TIPO DE CONTRATACION


USE [FSC_eProject]
GO
/****** Object:  Table [dbo].[TypeContract]    Script Date: 07/20/2013 18:56:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TypeContract](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Contract] [varchar](200) NULL,
 CONSTRAINT [PK_TypeContract] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[TypeContract] ON
INSERT [dbo].[TypeContract] ([id], [contract]) VALUES (1, N'Selecccione....')
INSERT [dbo].[TypeContract] ([id], [contract]) VALUES (2, N'Contrato')
INSERT [dbo].[TypeContract] ([id], [contract]) VALUES (3, N'Convenio')
INSERT [dbo].[TypeContract] ([id], [contract]) VALUES (4, N'OPS')
SET IDENTITY_INSERT [dbo].[TypeContract] OFF

GO

