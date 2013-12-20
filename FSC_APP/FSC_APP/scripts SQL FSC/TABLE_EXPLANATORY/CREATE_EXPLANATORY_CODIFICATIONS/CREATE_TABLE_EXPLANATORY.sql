USE [FSC_eProject]
GO

/****** Object:  Table [dbo].[Explanatory]    Script Date: 08/01/2013 20:07:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Explanatory](
	[id] [int] NOT NULL,
	[observation] [text] NULL,
	[fecha] [date] NOT NULL,
	[idproject] [int] NOT NULL,
 CONSTRAINT [PK_Explanatory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Explanatory]  WITH CHECK ADD  CONSTRAINT [FK_Explanatory_Project] FOREIGN KEY([idproject])
REFERENCES [dbo].[Project] ([Id])
GO

ALTER TABLE [dbo].[Explanatory] CHECK CONSTRAINT [FK_Explanatory_Project]
GO

