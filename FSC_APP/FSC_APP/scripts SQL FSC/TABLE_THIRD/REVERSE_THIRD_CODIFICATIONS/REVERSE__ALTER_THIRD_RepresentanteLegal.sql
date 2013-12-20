print ''
print '***** Eliminando campo representante legal a la tabla Terceros... *****'

USE [FSC_eProject]
	
	
	/****** Objecto:  Tabla [dbo].[Third]    Fecha: 08/02/2013 15:15:34 ******/

ALTER TABLE [dbo].[Third]
	DROP COLUMN [RepresentanteLegal]

	
print '***** El campo representante legal se ha eliminado correctamente de la tabla Terceros!! *****'
print ''