USE [FSC_eProject]
GO

/****** Object:  Table [dbo].[TemporaryActivities]    Script Date: 07/28/2013 13:57:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TemporaryActivities]') AND type in (N'U'))
DROP TABLE [dbo].[TemporaryActivities]
GO

