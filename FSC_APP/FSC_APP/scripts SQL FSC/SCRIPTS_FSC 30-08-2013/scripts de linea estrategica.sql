--script para lineas estrategicas y programas

USE [FSC_eProject]
ALTER TABLE Program  alter column Code varchar(255) NULL

update StrategicLine set Name ='Educaci�n Inclusiva' where Code = 'Educaci�n'
update StrategicLine set Name ='Incidencia en pol�tica p�blica' where Code = 'pol�tica p�blica'
update StrategicLine set Name ='Gesti�n del Conocimiento' where Code = 'Movilizaci�n - Gesti�n del Conocimiento'
update StrategicLine set Name ='Fortalecimiento' where Code = 'Fortalecimiento'

update StrategicLine set Code ='Incidencia en pol�tica p�blica',Name ='Incidencia en pol�tica p�blica' where ID = 25

insert into StrategicLine(Code,Name,IdStrategicObjective,IdManagment,Enabled,IdUser,CreateDate)
values('Movilizaci�n Social','Movilizaci�n Social',17,4,1,149,2013-08-29 )

update Program set Code='Fortalecimiento Organizacional',Name ='Fortalecimiento Organizacional' where id = 24
update Program set Code ='Fortalecimiento Comunitario',Name ='Fortalecimiento Comunitario' where ID = 25
update Program set Code='Gesti�n del Conocimiento',Name ='Gesti�n del Conocimiento' where id = 27
update Program set Code='Campa�as de sensibilizaci�n�y movilizaci�n social', Name ='Campa�as de sensibilizaci�n�y movilizaci�n social'  where id = 33

update Program set Code ='Fortalecimiento Comunitario',Name ='Fortalecimiento Comunitario' where ID = 25


insert into Program(Code,Name,IdStrategicLine,Enabled,IdUser,CreateDate)
values('Pedagog�a e incidencia medi�tica','Pedagog�a e incidencia medi�tica',19,1,149,2013-08-29 )

insert into Program(Code,Name,IdStrategicLine,Enabled,IdUser,CreateDate)
values('Internacionalizaci�n','Internacionalizaci�n',26,1,149,2013-08-29)


INSERT INTO ProgramComponent(Code,Name,Description,IdProgram,IdResponsible,Enabled,IdUser,CreateDate)
VALUES('Pedagog�a e incidencia medi�tica','Pedagog�a e incidencia medi�tica','Pedagog�a e incidencia medi�tica',48,149,1,149,GETDATE())

INSERT INTO ProgramComponent(Code,Name,Description,IdProgram,IdResponsible,Enabled,IdUser,CreateDate)
VALUES('Internalizaci�n','Internalizaci�n','Internalizaci�n',49,149,1,149,GETDATE())

update Program set IdStrategicLine = 29  where Name like 'Pedagog�a e incidencia medi�tica'
update Program set IdStrategicLine = 29  where Name like 'Campa�as de sensibilizaci�n�y movilizaci�n social'