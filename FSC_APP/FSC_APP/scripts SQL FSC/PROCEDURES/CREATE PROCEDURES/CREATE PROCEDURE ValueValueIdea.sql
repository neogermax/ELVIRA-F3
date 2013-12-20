
USE [FSC_eProject]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		German Rodriguez
-- Create date: 05/06/2013 
-- Description:	Validacion de campos de VALORES
-- =============================================
CREATE PROCEDURE ValueValueIdea
	-- Add the parameters for the stored procedure here
	@ValueCodeIdea int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
 
 if exists (select  * from VIdeaValidatevalues where code = @ValueCodeIdea )
      select  1 ;
   else
      select  0 ;
	
END
GO
