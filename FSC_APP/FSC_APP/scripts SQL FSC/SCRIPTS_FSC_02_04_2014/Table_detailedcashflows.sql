USE [FSC_eProject]
GO

/****** Object:  Table [dbo].[Detailedcashflows]    Script Date: 04/02/2014 12:36:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Detailedcashflows](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdIdea] [int] NULL,
	[IdProject] [int] NULL,
	[N_pago] [int] NULL,
	[IdAportante] [int] NULL,
	[Aportante] [varchar](250) NULL,
	[Desembolso] [varchar](100) NULL,
 CONSTRAINT [PK_Detailedcashflows] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

