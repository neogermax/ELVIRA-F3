USE [FSC_eProject]
GO

/****** Object:  StoredProcedure [dbo].[maximeidaprovalidea]    Script Date: 07/28/2013 12:57:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[maximeidaprovalidea]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[maximeidaprovalidea]
GO

