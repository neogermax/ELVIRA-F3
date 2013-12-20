USE [FSC_eProject]
GO

/****** Object:  Table [dbo].[SubactivityByThird]    Script Date: 07/28/2013 13:37:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SubactivityByThird](
	[IdThird] [int] NULL,
	[IdSubActivity] [int] NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SubactivityByThird]  WITH CHECK ADD  CONSTRAINT [FK_SubactivityByThird_SubActivity] FOREIGN KEY([IdSubActivity])
REFERENCES [dbo].[SubActivity] ([Id])
GO

ALTER TABLE [dbo].[SubactivityByThird] CHECK CONSTRAINT [FK_SubactivityByThird_SubActivity]
GO

ALTER TABLE [dbo].[SubactivityByThird]  WITH CHECK ADD  CONSTRAINT [FK_SubactivityByThird_Third] FOREIGN KEY([IdThird])
REFERENCES [dbo].[Third] ([Id])
GO

ALTER TABLE [dbo].[SubactivityByThird] CHECK CONSTRAINT [FK_SubactivityByThird_Third]
GO

