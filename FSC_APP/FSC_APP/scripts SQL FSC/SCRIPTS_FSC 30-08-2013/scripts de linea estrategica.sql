--script para lineas estrategicas y programas

USE [FSC_eProject]
ALTER TABLE Program  alter column Code varchar(255) NULL

update StrategicLine set Name ='Educación Inclusiva' where Code = 'Educación'
update StrategicLine set Name ='Incidencia en política pública' where Code = 'política pública'
update StrategicLine set Name ='Gestión del Conocimiento' where Code = 'Movilización - Gestión del Conocimiento'
update StrategicLine set Name ='Fortalecimiento' where Code = 'Fortalecimiento'

update StrategicLine set Code ='Incidencia en política pública',Name ='Incidencia en política pública' where ID = 25

insert into StrategicLine(Code,Name,IdStrategicObjective,IdManagment,Enabled,IdUser,CreateDate)
values('Movilización Social','Movilización Social',17,4,1,149,2013-08-29 )

update Program set Code='Fortalecimiento Organizacional',Name ='Fortalecimiento Organizacional' where id = 24
update Program set Code ='Fortalecimiento Comunitario',Name ='Fortalecimiento Comunitario' where ID = 25
update Program set Code='Gestión del Conocimiento',Name ='Gestión del Conocimiento' where id = 27
update Program set Code='Campañas de sensibilización y movilización social', Name ='Campañas de sensibilización y movilización social'  where id = 33

update Program set Code ='Fortalecimiento Comunitario',Name ='Fortalecimiento Comunitario' where ID = 25


insert into Program(Code,Name,IdStrategicLine,Enabled,IdUser,CreateDate)
values('Pedagogía e incidencia mediática','Pedagogía e incidencia mediática',19,1,149,2013-08-29 )

insert into Program(Code,Name,IdStrategicLine,Enabled,IdUser,CreateDate)
values('Internacionalización','Internacionalización',26,1,149,2013-08-29)


INSERT INTO ProgramComponent(Code,Name,Description,IdProgram,IdResponsible,Enabled,IdUser,CreateDate)
VALUES('Pedagogía e incidencia mediática','Pedagogía e incidencia mediática','Pedagogía e incidencia mediática',48,149,1,149,GETDATE())

INSERT INTO ProgramComponent(Code,Name,Description,IdProgram,IdResponsible,Enabled,IdUser,CreateDate)
VALUES('Internalización','Internalización','Internalización',49,149,1,149,GETDATE())

update Program set IdStrategicLine = 29  where Name like 'Pedagogía e incidencia mediática'
update Program set IdStrategicLine = 29  where Name like 'Campañas de sensibilización y movilización social'