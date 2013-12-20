USE [FSC_eProject]
GO

/****** Object:  StoredProcedure [dbo].[Maxime_NIT_Default]    Script Date: 07/28/2013 12:56:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Maxime_NIT_Default]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Maxime_NIT_Default]
GO

