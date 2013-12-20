-- eliminar campo type de thirbyproject

USE [FSC_eProject]

ALTER TABLE thirdbyProject drop column Type


ALTER TABLE ThirdByProject add  Type varchar(50); --reversa campo

