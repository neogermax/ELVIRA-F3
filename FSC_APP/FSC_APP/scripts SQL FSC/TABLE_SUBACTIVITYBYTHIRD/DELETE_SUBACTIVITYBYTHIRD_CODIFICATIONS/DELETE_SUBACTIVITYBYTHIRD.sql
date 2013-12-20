USE [FSC_eProject]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SubactivityByThird_SubActivity]') AND parent_object_id = OBJECT_ID(N'[dbo].[SubactivityByThird]'))
ALTER TABLE [dbo].[SubactivityByThird] DROP CONSTRAINT [FK_SubactivityByThird_SubActivity]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SubactivityByThird_Third]') AND parent_object_id = OBJECT_ID(N'[dbo].[SubactivityByThird]'))
ALTER TABLE [dbo].[SubactivityByThird] DROP CONSTRAINT [FK_SubactivityByThird_Third]
GO

USE [FSC_eProject]
GO

/****** Object:  Table [dbo].[SubactivityByThird]    Script Date: 07/28/2013 13:41:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SubactivityByThird]') AND type in (N'U'))
DROP TABLE [dbo].[SubactivityByThird]
GO

