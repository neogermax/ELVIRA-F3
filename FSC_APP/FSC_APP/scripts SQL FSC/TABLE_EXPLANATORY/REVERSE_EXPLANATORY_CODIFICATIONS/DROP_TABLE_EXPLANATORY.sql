USE [FSC_eProject]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Explanatory_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[Explanatory]'))
ALTER TABLE [dbo].[Explanatory] DROP CONSTRAINT [FK_Explanatory_Project]
GO

USE [FSC_eProject]
GO

/****** Object:  Table [dbo].[Explanatory]    Script Date: 08/09/2013 08:34:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Explanatory]') AND type in (N'U'))
DROP TABLE [dbo].[Explanatory]
GO

