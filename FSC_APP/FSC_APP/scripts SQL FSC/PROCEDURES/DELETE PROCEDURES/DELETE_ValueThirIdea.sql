USE [FSC_eProject]
GO

/****** Object:  StoredProcedure [dbo].[ValueThirIdea]    Script Date: 07/28/2013 12:59:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ValueThirIdea]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ValueThirIdea]
GO

