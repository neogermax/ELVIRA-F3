USE [FSC_eProject]
GO
/****** Object:  StoredProcedure [dbo].[delete_load_charge_masive]    Script Date: 09/10/2013 09:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		German Rodriguez
-- Create date: 08/29/2013 
-- Description:	ejecutar delete en carga masiva
-- =============================================
ALTER PROCEDURE [dbo].[delete_load_charge_masive](@project_id int)
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
-- decalramos variables
declare @component_id int;
declare @objective_id int;

					
--------------------------------------------TABLE SUBACTIVITYBYTHIRD------------------------------------------------------------

--consulta para eliminar las subactividadesbythird 

delete SubactivityByThird where IdSubActivity in (SELECT sat.IdSubActivity FROM SubactivityByThird sat  
INNER JOIN SubActivity SA on sa.Id = sat.IdSubActivity 
INNER JOIN  Activity A ON A.Id=SA.IdActivity 
where IdProyect = @project_id )
					
--------------------------------------------TABLE SUBACTIVITY--------------------------------------------------------------------

--consulta para eliminar las subactividades

 delete subactivity where Id in (select sa.Id from subactivity sa 
        inner join Activity a on sa.IdActivity = a.Id  
        where IdProyect = @project_id )

--------------------------------------------TABLE ACTIVITY------------------------------------------------------------------------

--consulta para eliminar las Actividades

delete Activity where IdProyect = @project_id

--------------------------------------------TABLE COMPONENT------------------------------------------------------------------------

--consulta para eliminar los componentes

delete Component where Id in(select c.id from Component c
inner join Objective o on o.Id=c.IdObjective
where o.IdProject = @project_id)

--------------------------------------------TABLE OBJECTIVE------------------------------------------------------------------------

--consulta para eliminar los objetivos
delete Objective where IdProject = @project_id


END
