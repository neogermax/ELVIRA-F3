USE [FSC_eProject]
GO

/****** Object:  Table [dbo].[Paymentflow]    Script Date: 08/03/2013 16:37:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Paymentflow](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idproject] [int] NOT NULL,
	[fecha] [datetime] NULL,
	[porcentaje] [decimal](18, 1) NULL,
	[entregable] [text] NULL,
	[ididea] [int] NOT NULL,
	[valorparcial] [int] NULL,
 CONSTRAINT [PK_Paymentflow] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Paymentflow]  WITH CHECK ADD  CONSTRAINT [FK_Paymentflow_Project] FOREIGN KEY([idproject])
REFERENCES [dbo].[Project] ([Id])
GO

ALTER TABLE [dbo].[Paymentflow] CHECK CONSTRAINT [FK_Paymentflow_Project]
GO

