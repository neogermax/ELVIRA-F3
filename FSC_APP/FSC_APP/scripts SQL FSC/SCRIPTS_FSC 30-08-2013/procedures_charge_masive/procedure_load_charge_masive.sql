USE [FSC_eProject]
GO
/****** Object:  StoredProcedure [dbo].[load_data_validation]    Script Date: 08/31/2013 09:42:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Juan Camilo Martinez Morales
-- Create date: 29-08-2013
-- Description:	Realiza el proceso de carga masiva para actividades y subactividades
-- =============================================
ALTER PROCEDURE [dbo].[load_data_validation](@project_id int)
AS
BEGIN
	-- 1. Cargar Tablas
declare @existenCodigosProyectoNulos int;
declare @existeCodigoProyectoUnico int;
declare @existenCamposObligatoriosVacios int;
declare @mensaje int;

-- 2. Validar Proyecto sea solo uno
set @existenCamposObligatoriosVacios = (select COUNT(1) from TemporaryActivities where  TemporaryActivities.Cod_project is null or TemporaryActivities.Cod_activity is null or TemporaryActivities.Activity is null or  TemporaryActivities.Cod_subactivity is null or  TemporaryActivities.subactivity is null or TemporaryActivities.Nit_Actors is null or  TemporaryActivities.Actors is null or TemporaryActivities.Star_date is null or TemporaryActivities.End_date is null)
	if @existenCamposObligatoriosVacios > 0
	begin
		set @mensaje =  1; -- el archivo tiene campos vacios por favor verifique campos no obligatorios (actividad predesesora y responsable)
		update resultsmensaje set valuesresult=@mensaje where id=1

	end
	else
	begin
				-- 3. Codigo de proyecto unico
	set @existeCodigoProyectoUnico = (select count(distinct(Cod_project)) from TemporaryActivities);
	if @existeCodigoProyectoUnico > 1
	begin
		set @mensaje =  2; -- solo puede ingresar un Id_proyecto
		update resultsmensaje set valuesresult=@mensaje where id=1
	end
	else
	begin
		set @existeCodigoProyectoUnico = (select distinct(Cod_project) from TemporaryActivities);
		if @existeCodigoProyectoUnico <> @project_id
		begin
			set @mensaje =  3; -- el id_proyecto no coincide con el que esta ingresando
			update resultsmensaje set valuesresult=@mensaje where id=1
		end
		else
		begin
			-- 4. Validar campos obligatorios
			--set @existenCamposObligatoriosVacios = (select COUNT(1) from TemporaryActivities where  TemporaryActivities.Cod_project is null or TemporaryActivities.Cod_activity is null or TemporaryActivities.Activity is null or  TemporaryActivities.Cod_subactivity is null or  TemporaryActivities.subactivity is null or TemporaryActivities.Nit_Actors is null or  TemporaryActivities.Actors is null or TemporaryActivities.Star_date is null or TemporaryActivities.End_date is null)
			--if @existenCamposObligatoriosVacios > 0
			--begin
			--	set @mensaje =  4; -- el archivo tiene campos vacios por favor verifique campos no obligatorios (actividad predesesora y responsable)
			--end
			--else
			--begin
				begin try
					begin transaction;
					-- 5. Se realiza el proceso de carga masiva a las tablas correspondientes
					-- 5.1 Se inserta registro requerido por las tablas hijas de objetivo
					insert into Objective(Code,Name,IdProject,Enabled,IdUser,CreateDate,idKey,isLastVersion,IdPhase) values ('default','default',CAST(@project_id as varchar),'1','149',GETDATE(),'145','1','1');
					-- 5.2 Actualiza idKey con id de la misma tabla
					declare @objetive_id int;
					set @objetive_id = (select MAX(ID) FROM Objective WHERE IdProject = CAST(@project_id as varchar));
					update Objective set idKey=id where id = @objetive_id;
					-- 5.3 Se inserta registro requerido por las tablas hijas de componente
					insert into Component(Code, Name, IdObjective, Enabled, IdUser,CreateDate,idKey,isLastVersion,IdPhase)values ('default','default',cast(@objetive_id as varchar),'True','149',GETDATE(),'145','True','1');
					-- 5.4 Actualiza idKey con id de la misma tabla
					declare @component_id int;
					set @component_id = (select MAX(ID) FROM Component WHERE IdObjective = CAST(@objetive_id as varchar));
					update Component set idKey=id where id = CAST(@component_id as varchar);
					-- 5.5 Realiza proceso de carga para Actividades
					INSERT into activity(number,title,idcomponent,description,enabled,iduser,createDate,idkey,islastversion,idphase, idproyect) select distinct Cod_activity as number, Activity as title, CAST(@component_id as varchar) as idcomponent, '' as description, '1' as enabled, '149' as iduser, GETDATE() as createDate, '350' as idkey, '1' as islastversion, '1' as idphase, cod_project as idproyect from TemporaryActivities;
					-- 5.6 Actualiza idKey con id de la misma tabla
					update Activity set idkey = id  where  IdProyect = CAST(@project_id as varchar);
					-- 5.7 Se realiza el proceso de carga para subactividades
					INSERT into SubActivity(idActivity, Type,Number, Name, Description, IdResponsible, BeginDate, EndDate, TotalCost, Duration, FSCContribution, OFContribution, Attachment, CriticalPath,RequiresApproval, Enabled, IdUser, CreateDate, idKey,isLastVersion, IdPhase,reponsible ) select a.id as idActivity, '1' as type, a.Number, ta.subactivity, '' as description, '149' as IDreponsable, ta.Star_date, ta.End_date, '0' as totalcost, '0' as duration, '0' as FSCcotribution, '0' as OFCcontribution, '' attachment, '0' as criticalpath, '0' as requiresapproval, '1' as enabled, '149' as iduser, GETDATE() as createDate,'350' as idkey, '1' as islastversion, '1' as idphase, responsible from TemporaryActivities ta  join Activity a on a.Number =ta.Cod_activity  join Project p on p.id =a.IdProyect;
					-- 5.8 Se realiza el procedimiento de actualizacion para subactividades
					update SubActivity set idkey = id  where SubActivity.Id IN (select SA.ID from SubActivity sa  join Activity a on a.ID = SA.IdActivity where  IdProyect=CAST(@project_id as varchar));
					-- 5.9 Se realiza el proceso de insercion para los terceros que no existen en la base de datos
					insert into third (Code, Name,contact,documents,phone,email,Actions,Experiences, Enabled, IdUser, CreateDate) SELECT distinct  ta.Nit_Actors, ta.Actors,'' as contact,'' as documents,'' as phone,'' as email,'' as actions,'' as experiences,1 as enabled,'149' as iduser,GETDATE() as createdate   FROM TemporaryActivities ta left join  Third t on ta.nit_actors = t.code where ta.Nit_Actors IS not NULL and t.Id is null
					-- 5.10 Actualiza tabla muchos a muchos entre actores y subactividades
					insert into SubactivityByThird (IdSubActivity,IdThird) select distinct SA.Id,t.Id from TemporaryActivities TA join  Activity A ON TA.Cod_project= A.IdProyect join SubActivity SA on sa.IdActivity = a.Id join Third t on t.Code =ta.Nit_Actors;
					set @mensaje =  4; -- registro la carga del cronograma exitosamente
					update resultsmensaje set valuesresult=@mensaje where id=1

					commit;
				end try
				begin catch
					set @mensaje =  5; --no cargo la carga 
					update resultsmensaje set valuesresult=@mensaje where id=1
					rollback;
				end catch
			--end
		end
	end
end
END
