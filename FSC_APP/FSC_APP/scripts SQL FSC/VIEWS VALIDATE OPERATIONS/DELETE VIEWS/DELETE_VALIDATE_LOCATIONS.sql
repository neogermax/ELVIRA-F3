USE [FSC_eProject]
GO

/****** Object:  View [dbo].[VIdeaValidateLocation]    Script Date: 07/28/2013 12:29:18 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VIdeaValidateLocation]'))
DROP VIEW [dbo].[VIdeaValidateLocation]
GO

