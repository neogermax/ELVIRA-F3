USE [FSC_eProject]
GO

/****** Object:  StoredProcedure [dbo].[ValueValueIdea]    Script Date: 07/28/2013 13:00:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ValueValueIdea]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ValueValueIdea]
GO

