---------------------------------------creacion de la tabla temporal para carga masiva-------------------------------------------


/****** Object:  Table [dbo].[TemporaryActivities]    Script Date: 07/24/2013 10:06:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

USE [FSC_eProject]
GO
CREATE TABLE [dbo].[TemporaryActivities](
	[Cod_project] [varchar](50) NULL,
	[Cod_activity] [varchar](50) NULL,
	[Activity] [varchar](50) NULL,
	[Cod_subactivity] [varchar](50) NULL,
	[subactivity] [varchar](50) NULL,
	[Subactivity_previous] [varchar](50) NULL,
	[Nit_Actors] [varchar](50) NULL,
	[Actors] [varchar](50) NULL,
	[responsible] [varchar](50) NULL,
	[Star_date] [varchar](50) NULL,
	[End_date] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
