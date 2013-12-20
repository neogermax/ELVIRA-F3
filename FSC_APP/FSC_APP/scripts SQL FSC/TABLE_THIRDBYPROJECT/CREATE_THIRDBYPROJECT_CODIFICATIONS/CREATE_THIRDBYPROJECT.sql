USE [FSC_eProject]
GO

/****** Object:  Table [dbo].[ThirdByProject]    Script Date: 07/28/2013 11:58:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ThirdByProject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdProject] [int] NOT NULL,
	[IdThird] [int] NULL,
	[Type] [nvarchar](250) NOT NULL,
	[Vrmoney] [varchar](50) NULL,
	[VrSpecies] [varchar](50) NULL,
	[FSCorCounterpartContribution] [varchar](50) NULL,
 CONSTRAINT [PK_ThirdByProject] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

SET IDENTITY_INSERT [dbo].[ThirdByProject] ON
INSERT [dbo].[ThirdByProject] ([Id], [IdProject], [IdThird], [Type], [Vrmoney], [VrSpecies], [FSCorCounterpartContribution]) VALUES (17,101,34, N'Operador',NULL,NULL,NULL)
INSERT [dbo].[ThirdByProject] ([Id], [IdProject], [IdThird], [Type], [Vrmoney], [VrSpecies], [FSCorCounterpartContribution]) VALUES (18,152,35, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByProject] ([Id], [IdProject], [IdThird], [Type], [Vrmoney], [VrSpecies], [FSCorCounterpartContribution]) VALUES (25,481,38, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByProject] ([Id], [IdProject], [IdThird], [Type], [Vrmoney], [VrSpecies], [FSCorCounterpartContribution]) VALUES (27,605,12, N'Operador',NULL,NULL,NULL)
INSERT [dbo].[ThirdByProject] ([Id], [IdProject], [IdThird], [Type], [Vrmoney], [VrSpecies], [FSCorCounterpartContribution]) VALUES (30,624,36, N'Operador',NULL,NULL,NULL)
INSERT [dbo].[ThirdByProject] ([Id], [IdProject], [IdThird], [Type], [Vrmoney], [VrSpecies], [FSCorCounterpartContribution]) VALUES (35,642,37, N'Operador',NULL,NULL,NULL)
INSERT [dbo].[ThirdByProject] ([Id], [IdProject], [IdThird], [Type], [Vrmoney], [VrSpecies], [FSCorCounterpartContribution]) VALUES (40,666,57, N'Operador',NULL,NULL,NULL)
INSERT [dbo].[ThirdByProject] ([Id], [IdProject], [IdThird], [Type], [Vrmoney], [VrSpecies], [FSCorCounterpartContribution]) VALUES (41,630,46, N'Operador',NULL,NULL,NULL)
INSERT [dbo].[ThirdByProject] ([Id], [IdProject], [IdThird], [Type], [Vrmoney], [VrSpecies], [FSCorCounterpartContribution]) VALUES (45,700,49, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByProject] ([Id], [IdProject], [IdThird], [Type], [Vrmoney], [VrSpecies], [FSCorCounterpartContribution]) VALUES (46,665,51, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByProject] ([Id], [IdProject], [IdThird], [Type], [Vrmoney], [VrSpecies], [FSCorCounterpartContribution]) VALUES (47,665,52, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByProject] ([Id], [IdProject], [IdThird], [Type], [Vrmoney], [VrSpecies], [FSCorCounterpartContribution]) VALUES (48,675,59, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByProject] ([Id], [IdProject], [IdThird], [Type], [Vrmoney], [VrSpecies], [FSCorCounterpartContribution]) VALUES (49,675,52, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByProject] ([Id], [IdProject], [IdThird], [Type], [Vrmoney], [VrSpecies], [FSCorCounterpartContribution]) VALUES (52,711,58, N'Socio',NULL,NULL,NULL)
INSERT [dbo].[ThirdByProject] ([Id], [IdProject], [IdThird], [Type], [Vrmoney], [VrSpecies], [FSCorCounterpartContribution]) VALUES (53,711,37, N'Socio',NULL,NULL,NULL)
SET IDENTITY_INSERT [dbo].[ThirdByProject] OFF



ALTER TABLE [dbo].[ThirdByProject]  WITH CHECK ADD  CONSTRAINT [FK_ThirdByProject_Project] FOREIGN KEY([IdProject])
REFERENCES [dbo].[Project] ([Id])
GO

ALTER TABLE [dbo].[ThirdByProject] CHECK CONSTRAINT [FK_ThirdByProject_Project]
GO

ALTER TABLE [dbo].[ThirdByProject]  WITH CHECK ADD  CONSTRAINT [FK_ThirdByProject_Third] FOREIGN KEY([IdThird])
REFERENCES [dbo].[Third] ([Id])
GO

ALTER TABLE [dbo].[ThirdByProject] CHECK CONSTRAINT [FK_ThirdByProject_Third]
GO

