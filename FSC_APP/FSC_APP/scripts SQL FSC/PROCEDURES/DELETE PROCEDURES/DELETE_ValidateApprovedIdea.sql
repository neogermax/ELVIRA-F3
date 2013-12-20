USE [FSC_eProject]
GO

/****** Object:  StoredProcedure [dbo].[ValidateApprovedIdea]    Script Date: 07/28/2013 12:58:10 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ValidateApprovedIdea]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ValidateApprovedIdea]
GO

