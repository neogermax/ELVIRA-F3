USE [FSC_eProject]
GO

/****** Object:  StoredProcedure [dbo].[ValueLocationIdea]    Script Date: 07/28/2013 12:58:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ValueLocationIdea]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ValueLocationIdea]
GO

