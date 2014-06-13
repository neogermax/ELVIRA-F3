--create table catalogo SEX

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sex](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Sex] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Sex] ON
INSERT [dbo].[Sex] ([Id], [descripcion]) VALUES (0, N'No Aplica')
INSERT [dbo].[Sex] ([Id], [descripcion]) VALUES (1, N'Femenino')
INSERT [dbo].[Sex] ([Id], [descripcion]) VALUES (2, N'Masculino')
SET IDENTITY_INSERT [dbo].[Sex] OFF
/****** Object:  Table [dbo].[Third_Type_People]    Script Date: 06/11/2014 11:36:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO