USE [FSC_eProject]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ThirdByIdea_Idea]') AND parent_object_id = OBJECT_ID(N'[dbo].[ThirdByIdea]'))
ALTER TABLE [dbo].[ThirdByIdea] DROP CONSTRAINT [FK_ThirdByIdea_Idea]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ThirdByIdea_Third]') AND parent_object_id = OBJECT_ID(N'[dbo].[ThirdByIdea]'))
ALTER TABLE [dbo].[ThirdByIdea] DROP CONSTRAINT [FK_ThirdByIdea_Third]
GO

USE [FSC_eProject]
GO

/****** Object:  Table [dbo].[ThirdByIdea]    Script Date: 07/28/2013 11:34:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ThirdByIdea]') AND type in (N'U'))
DROP TABLE [dbo].[ThirdByIdea]
GO

