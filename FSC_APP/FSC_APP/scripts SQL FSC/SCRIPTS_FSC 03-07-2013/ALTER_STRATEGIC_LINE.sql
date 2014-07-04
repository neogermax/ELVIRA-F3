-- agregar nuevo campoen linea estrategica
alter TABLE [dbo].[StrategicLine] add EnableStrategicLines bit NULL

--actualizacion de campo para filtrar los combos
update StrategicLine set EnableStrategicLines = 0 where id < 36
update StrategicLine set EnableStrategicLines = 1 where id > 36

