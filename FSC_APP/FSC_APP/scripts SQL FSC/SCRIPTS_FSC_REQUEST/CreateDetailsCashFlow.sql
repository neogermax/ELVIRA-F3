GO

/****** Object:  Table [dbo].[DetailedCashFlowsRequest]    Script Date: 06/06/2014 10:43:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DetailedCashFlowsRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdRequest] [int] NULL,
	[N_pago] [int] NULL,
	[IdAportante] [int] NULL,
	[Aportante] [varchar](250) NULL,
	[Desembolso] [varchar](100) NULL,
	[Mother] [int] NULL,
 CONSTRAINT [PK_DetailedCashFlowsRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[DetailedCashFlowsRequest]  WITH CHECK ADD  CONSTRAINT [FK_DetailedCashFlowsRequest_Request] FOREIGN KEY([IdRequest])
REFERENCES [dbo].[Request] ([Id])
GO

ALTER TABLE [dbo].[DetailedCashFlowsRequest] CHECK CONSTRAINT [FK_DetailedCashFlowsRequest_Request]
GO


