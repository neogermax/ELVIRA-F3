
USE [FSC_eProject]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		German Rodriguez
-- Create date: 05/06/2013 
-- Description:	PARA AVERIGUAR EL CONSECUTIVO DE APROVACIONIDEA
-- =============================================
CREATE PROCEDURE maximeidaprovalidea 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
   select MAX(Code) from ProjectApprovalRecord  
END
GO
