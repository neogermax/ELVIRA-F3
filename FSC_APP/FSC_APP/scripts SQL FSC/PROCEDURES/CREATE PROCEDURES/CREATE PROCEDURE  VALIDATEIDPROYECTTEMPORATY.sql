
--------------------------------------------procedimiento almacenado para tabla temporal de proyecto actividades autor: german rodriguez 

USE [FSC_eProject]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		German Rodriguez
-- Create date: 25/07/2013 
-- Description:	Validacion de proyecto temporal
-- =============================================
CREATE PROCEDURE  VALIDATEIDPROYECTTEMPORATY
	-- Add the parameters for the stored procedure here
	@ValueCodeIdea int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
  
 SELECT COUNT(Cod_project)FROM TemporaryActivities 
where Cod_project <>  @ValueCodeIdea 
   	  
END

