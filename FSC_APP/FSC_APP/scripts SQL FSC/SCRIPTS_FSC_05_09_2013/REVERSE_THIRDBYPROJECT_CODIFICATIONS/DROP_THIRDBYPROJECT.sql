USE [FSC_eProject]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ThirdByProject_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[ThirdByProject]'))
ALTER TABLE [dbo].[ThirdByProject] DROP CONSTRAINT [FK_ThirdByProject_Project]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ThirdByProject_Third]') AND parent_object_id = OBJECT_ID(N'[dbo].[ThirdByProject]'))
ALTER TABLE [dbo].[ThirdByProject] DROP CONSTRAINT [FK_ThirdByProject_Third]
GO

USE [FSC_eProject]
GO

/****** Object:  Table [dbo].[ThirdByProject]    Script Date: 07/28/2013 12:00:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ThirdByProject]') AND type in (N'U'))
DROP TABLE [dbo].[ThirdByProject]
GO

