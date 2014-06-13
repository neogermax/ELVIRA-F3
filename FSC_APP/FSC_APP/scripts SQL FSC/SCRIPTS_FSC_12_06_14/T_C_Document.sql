CREATE TABLE [dbo].[Document](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Document] ON
INSERT [dbo].[Document] ([Id], [descripcion]) VALUES (0, N'No Aplica')
INSERT [dbo].[Document] ([Id], [descripcion]) VALUES (1, N'Cedula de ciudadania')
INSERT [dbo].[Document] ([Id], [descripcion]) VALUES (2, N'Cedula extranjera')
INSERT [dbo].[Document] ([Id], [descripcion]) VALUES (3, N'Pasaporte')
SET IDENTITY_INSERT [dbo].[Document] OFF
