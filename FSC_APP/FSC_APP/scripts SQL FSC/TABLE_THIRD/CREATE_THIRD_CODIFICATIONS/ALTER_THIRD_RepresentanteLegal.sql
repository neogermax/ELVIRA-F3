print ''
print '***** Agregando campo representante legal a la tabla Terceros... *****'

USE [FSC_eProject]
	
	
	/****** Objecto:  Tabla [dbo].[Third]    Fecha: 08/02/2013 15:15:34 ******/

ALTER TABLE [dbo].[Third]
	ADD [RepresentanteLegal] [varchar](255) NULL

	
print '***** El campo representante legal se ha agregado correctamente a la tabla Terceros!! *****'
print ''