USE [FSC_eProject]
GO
/****** Object:  Table [dbo].[Type_aproval_project]    Script Date: 04/30/2014 12:15:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type_aproval_project](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[estados] [nvarchar](200) NOT NULL,
	[aplica_idea][nvarchar](1) NOT NULL,
 CONSTRAINT [PK_Type_aproval_project] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Type_aproval_project] ON
INSERT [dbo].[Type_aproval_project] ([id], [estados], [aplica_idea]) VALUES (1, N'Aprobado', N's')
INSERT [dbo].[Type_aproval_project] ([id], [estados], [aplica_idea]) VALUES (2, N'No Aprobado', N's')
INSERT [dbo].[Type_aproval_project] ([id], [estados], [aplica_idea]) VALUES (3, N'Pendiente de aprobación', N's')
INSERT [dbo].[Type_aproval_project] ([id], [estados], [aplica_idea]) VALUES (4, N'Cerrado', N'n')
INSERT [dbo].[Type_aproval_project] ([id], [estados], [aplica_idea]) VALUES (5, N'Liquidado', N'n')
INSERT [dbo].[Type_aproval_project] ([id], [estados], [aplica_idea]) VALUES (6, N'Suspendido', N'n')
INSERT [dbo].[Type_aproval_project] ([id], [estados], [aplica_idea]) VALUES (7, N'Contratado', N'n')
INSERT [dbo].[Type_aproval_project] ([id], [estados], [aplica_idea]) VALUES (8, N' En Ejecución', N'n')
SET IDENTITY_INSERT [dbo].[Type_aproval_project] OFF
