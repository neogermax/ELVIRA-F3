
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		German Rodriguez
-- Create date: 29/08/2013 
-- Description:	Validacion de idea aprobada
-- =============================================
USE [FSC_eProject]
GO

create PROCEDURE [dbo].[Value_exists_activity]
	-- Add the parameters for the stored procedure here
	@ValueIdproject int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
   if exists (select a.IdProyect from Activity a where IdProyect = @ValueIdproject)
     select  1 ;
   else
     select  0 ;
 
END
