USE [FSC_eProject]
GO

/****** Object:  Table [dbo].[Compromise]    Script Date: 09/24/2013 08:37:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Compromise](
	[id] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[idproject] [numeric](18, 0) NOT NULL,
	[liabilities] [nvarchar](max) NOT NULL,
	[responsible] [nvarchar](50) NULL,
	[tracingdate] [datetime] NULL,
	[email] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

