USE [FSC_eProject]
GO

/****** Object:  Index [PK_Idea]    Script Date: 07/28/2013 10:33:24 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Idea]') AND name = N'PK_Idea')
ALTER TABLE [dbo].[Idea] DROP CONSTRAINT [PK_Idea]
GO

