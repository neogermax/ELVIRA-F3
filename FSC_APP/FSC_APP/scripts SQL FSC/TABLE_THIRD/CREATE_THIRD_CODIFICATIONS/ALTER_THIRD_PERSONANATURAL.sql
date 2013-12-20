print ''
print '***** Agregando campo Persona Natural a la tabla Terceros... *****'

USE [FSC_eProject]

/****** Objeto:  Tabla [dbo].[Third]    Fecha: 08/01/2013 11:31:32 ******/

ALTER TABLE [dbo].[Third]
ADD PersonaNatural bit NULL
	
print '***** El campo se ha agregado correctamente a la tabla Terceros!! *****'
print ''