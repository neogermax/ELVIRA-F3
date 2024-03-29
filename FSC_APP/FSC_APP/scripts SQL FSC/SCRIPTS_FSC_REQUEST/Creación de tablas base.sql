GO
/****** Object:  Table [dbo].[RequestType]    Script Date: 05/21/2014 16:42:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RequestType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](100) NOT NULL,
 CONSTRAINT [PK_RequestType_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RequestSubtype]    Script Date: 05/21/2014 16:42:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RequestSubtype](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Subtype] [varchar](100) NULL,
 CONSTRAINT [PK_RequestSubtype] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Request]    Script Date: 05/21/2014 16:42:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Request](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdProject] [int] NOT NULL,
	[NoUniqueRequest] [int] NULL,
	[Code] [varchar](max) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Objective] [varchar](max) NULL,
	[Antecedent] [varchar](max) NULL,
	[Justification] [varchar](max) NULL,
	[BeginDate] [datetime] NULL,
	[Duration] [varchar](max) NULL,
	[ZoneDescription] [varchar](max) NULL,
	[Population] [varchar](max) NOT NULL,
	[StrategicDescription] [varchar](max) NULL,
	[Results] [varchar](max) NULL,
	[Source] [varchar](max) NULL,
	[Purpose] [varchar](max) NULL,
	[TotalCost] [numeric](30, 0) NULL,
	[FSCContribution] [numeric](30, 3) NULL,
	[CounterpartValue] [numeric](30, 3) NULL,
	[EffectiveBudget] [int] NOT NULL,
	[Attachment] [varchar](max) NULL,
	[IdPhase] [int] NOT NULL,
	[Enabled] [bit] NOT NULL,
	[IdUser] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[idKey] [int] NULL,
	[isLastVersion] [bit] NULL,
	[IdProcessInstance] [int] NULL,
	[IdActivityInstance] [int] NULL,
	[Typeapproval] [varchar](50) NULL,
	[editablemoney] [char](1) NULL,
	[editabletime] [char](1) NULL,
	[editableresults] [char](1) NULL,
	[completiondate] [datetime] NULL,
	[ResultsKnowledgeManagement] [varchar](max) NULL,
	[ResultsInstalledCapacity] [varchar](max) NULL,
	[Mother] [int] NULL,
	[OtherResults] [varchar](max) NULL,
	[obligationsoftheparties] [varchar](max) NULL,
	[BudgetRoute] [varchar](max) NULL,
	[RisksIdentified] [varchar](max) NULL,
	[RiskMitigation] [varchar](max) NULL,
	[ideaappliesIVA] [int] NULL,
	[days] [varchar](max) NULL,
	[Project_derivados] [int] NULL,
 CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ThirdByRequest]    Script Date: 05/21/2014 16:42:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ThirdByRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdRequest] [int] NULL,
	[IdThird] [int] NULL,
	[Type] [nvarchar](250) NULL,
	[Vrmoney] [varchar](50) NULL,
	[VrSpecies] [varchar](50) NULL,
	[FSCorCounterpartContribution] [varchar](50) NULL,
	[Name] [varchar](255) NULL,
	[Contact] [varchar](255) NULL,
	[Documents] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[Email] [varchar](500) NULL,
	[CreateDate] [datetime] NULL,
	[generatesflow] [varchar](50) NULL,
 CONSTRAINT [PK_ThirdByRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Paymentflow_Request]    Script Date: 05/21/2014 16:42:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Paymentflow_Request](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdRequest] [int] NULL,
	[Date] [datetime] NULL,
	[Percentaje] [varchar](5) NULL,
	[Deliverable] [text] NULL,
	[Partialvalue] [numeric](18, 0) NULL,
	[Totalvalue] [numeric](18, 0) NULL,
	[Idideaaproval] [int] NULL,
	[N_pagos] [varchar](max) NULL,
	[Mother] [int] NULL,
 CONSTRAINT [PK_PaymentflowRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Request_RequestType]    Script Date: 05/21/2014 16:42:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Request_RequestType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdRequest] [int] NOT NULL,
	[IdRequestType] [int] NOT NULL,
	[IdRequestSubtype] [int] NULL,
 CONSTRAINT [PK_Request_RequestType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_Request_NoUniqueRequest]    Script Date: 05/21/2014 16:42:42 ******/
ALTER TABLE [dbo].[Request] ADD  CONSTRAINT [DF_Request_NoUniqueRequest]  DEFAULT ((0)) FOR [NoUniqueRequest]
GO
/****** Object:  ForeignKey [FK_Paymentflow_Request_Request]    Script Date: 05/21/2014 16:42:42 ******/
ALTER TABLE [dbo].[Paymentflow_Request]  WITH CHECK ADD  CONSTRAINT [FK_Paymentflow_Request_Request] FOREIGN KEY([IdRequest])
REFERENCES [dbo].[Request] ([Id])
GO
ALTER TABLE [dbo].[Paymentflow_Request] CHECK CONSTRAINT [FK_Paymentflow_Request_Request]
GO
/****** Object:  ForeignKey [FK_Request_Project]    Script Date: 05/21/2014 16:42:42 ******/
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Project] FOREIGN KEY([IdProject])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Project]
GO
/****** Object:  ForeignKey [FK_Request_RequestType_Request]    Script Date: 05/21/2014 16:42:42 ******/
ALTER TABLE [dbo].[Request_RequestType]  WITH CHECK ADD  CONSTRAINT [FK_Request_RequestType_Request] FOREIGN KEY([IdRequest])
REFERENCES [dbo].[Request] ([Id])
GO
ALTER TABLE [dbo].[Request_RequestType] CHECK CONSTRAINT [FK_Request_RequestType_Request]
GO
/****** Object:  ForeignKey [FK_Request_RequestType_RequestSubtype]    Script Date: 05/21/2014 16:42:42 ******/
ALTER TABLE [dbo].[Request_RequestType]  WITH CHECK ADD  CONSTRAINT [FK_Request_RequestType_RequestSubtype] FOREIGN KEY([IdRequestSubtype])
REFERENCES [dbo].[RequestSubtype] ([Id])
GO
ALTER TABLE [dbo].[Request_RequestType] CHECK CONSTRAINT [FK_Request_RequestType_RequestSubtype]
GO
/****** Object:  ForeignKey [FK_Request_RequestType_RequestType]    Script Date: 05/21/2014 16:42:42 ******/
ALTER TABLE [dbo].[Request_RequestType]  WITH CHECK ADD  CONSTRAINT [FK_Request_RequestType_RequestType] FOREIGN KEY([IdRequestType])
REFERENCES [dbo].[RequestType] ([Id])
GO
ALTER TABLE [dbo].[Request_RequestType] CHECK CONSTRAINT [FK_Request_RequestType_RequestType]
GO
/****** Object:  ForeignKey [FK_ThirdByRequest_Request1]    Script Date: 05/21/2014 16:42:42 ******/
ALTER TABLE [dbo].[ThirdByRequest]  WITH CHECK ADD  CONSTRAINT [FK_ThirdByRequest_Request1] FOREIGN KEY([IdRequest])
REFERENCES [dbo].[Request] ([Id])
GO
ALTER TABLE [dbo].[ThirdByRequest] CHECK CONSTRAINT [FK_ThirdByRequest_Request1]
GO
/****** Object:  ForeignKey [FK_ThirdByRequest_Third]    Script Date: 05/21/2014 16:42:42 ******/
ALTER TABLE [dbo].[ThirdByRequest]  WITH CHECK ADD  CONSTRAINT [FK_ThirdByRequest_Third] FOREIGN KEY([IdThird])
REFERENCES [dbo].[Third] ([Id])
GO
ALTER TABLE [dbo].[ThirdByRequest] CHECK CONSTRAINT [FK_ThirdByRequest_Third]
GO
