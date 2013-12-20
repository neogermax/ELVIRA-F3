print ''
print '***** Eliminando campo Persona Natural a la tabla Terceros... *****'

USE [FSC_eProject]
	
	
	/****** Objecto:  Tabla [dbo].[Third]    Fecha: 08/02/2013 15:15:34 ******/

ALTER TABLE [dbo].[Third]
	DROP COLUMN [PersonaNatural]

	
print '***** El campo Persona Natural se ha eliminado correctamente de la tabla Terceros!! *****'
print ''