USE [FSC_eProject]
GO
/****** Object:  Table [dbo].[TypeActas]    Script Date: 09/16/2013 19:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TypeActas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ActaName] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[TypeActas] ON
INSERT [dbo].[TypeActas] ([id], [ActaName]) VALUES (1, N'Acta de inicio')
INSERT [dbo].[TypeActas] ([id], [ActaName]) VALUES (2, N'Acta de seguimiento')
INSERT [dbo].[TypeActas] ([id], [ActaName]) VALUES (3, N'Acta de cierre')
SET IDENTITY_INSERT [dbo].[TypeActas] OFF
