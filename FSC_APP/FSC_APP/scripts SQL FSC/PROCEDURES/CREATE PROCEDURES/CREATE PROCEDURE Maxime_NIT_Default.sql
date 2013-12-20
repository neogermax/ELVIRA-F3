-- procedimiento almacenado de la sentencia anterior 


USE [FSC_eProject]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		German Rodriguez
-- Create date: 05/06/2013 
-- Description:	Validacion de NIT Actores 
-- =============================================
CREATE PROCEDURE Maxime_NIT_Default

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	select substring(max(code),6,15) + 1 
    from Third
    where Code like  '%NIT_D%' 

	
	END
GO
