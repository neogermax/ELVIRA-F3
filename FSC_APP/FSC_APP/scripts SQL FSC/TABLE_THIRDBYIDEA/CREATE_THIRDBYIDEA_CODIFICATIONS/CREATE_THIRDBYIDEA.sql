USE [FSC_eProject]
GO

/****** Object:  Table [dbo].[ThirdByIdea]    Script Date: 07/28/2013 11:34:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ThirdByIdea](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdIdea] [int] NULL,
	[IdThird] [int] NULL,
	[Type] [nvarchar](250) NULL,
	[Vrmoney] [varchar](50) NULL,
	[VrSpecies] [varchar](50) NULL,
	[FSCorCounterpartContribution] [varchar](50) NULL,
 CONSTRAINT [PK_ThirdByIdea] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

SET IDENTITY_INSERT [dbo].[ThirdByIdea] ON
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (6,13,34, N'Operador',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (8,94,38, N'Operador',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (25,119,39, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (45,135,40, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (48,121,41, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (49,121,42, N'Operador',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (50,134,43, N'Operador',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (68,137,44, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (69,137,45, N'Operador',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (73,139,44, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (74,139,45, N'Operador',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (75,139,44, N'Operador',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (77,140,16, N'Operador',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (86,152,47, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (87,152,48, N'Operador',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (93,145,46, N'Operador',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (98,193,49, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (106,198,50, N'Operador',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (107,161,51, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (108,161,52, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (109,188,58, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (110,188,37, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (111,197,53, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (112,197,54, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (113,197,55, N'Operador',NULL,NULL,NULL)
INSERT [dbo].[ThirdByIdea] ([Id], [IdIdea], [IdThird], [Type],[Vrmoney],[VrSpecies],[FSCorCounterpartContribution] )   VALUES (116,212,56, N'Operador',NULL,NULL,NULL)
SET IDENTITY_INSERT [dbo].[ThirdByIdea] OFF

ALTER TABLE [dbo].[ThirdByIdea]  WITH CHECK ADD  CONSTRAINT [FK_ThirdByIdea_Idea] FOREIGN KEY([IdIdea])
REFERENCES [dbo].[Idea] ([Id])
GO

ALTER TABLE [dbo].[ThirdByIdea] CHECK CONSTRAINT [FK_ThirdByIdea_Idea]
GO

ALTER TABLE [dbo].[ThirdByIdea]  WITH CHECK ADD  CONSTRAINT [FK_ThirdByIdea_Third] FOREIGN KEY([IdThird])
REFERENCES [dbo].[Third] ([Id])
GO

ALTER TABLE [dbo].[ThirdByIdea] CHECK CONSTRAINT [FK_ThirdByIdea_Third]
GO

