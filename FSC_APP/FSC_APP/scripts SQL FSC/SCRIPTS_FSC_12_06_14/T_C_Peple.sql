CREATE TABLE [dbo].[People](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[People] ON
INSERT [dbo].[People] ([Id], [descripcion]) VALUES (0, N'No Aplica')
INSERT [dbo].[People] ([Id], [descripcion]) VALUES (1, N'Persona Natural')
INSERT [dbo].[People] ([Id], [descripcion]) VALUES (2, N'Persona Juridica')
SET IDENTITY_INSERT [dbo].[People] OFF