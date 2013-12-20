print ''
print '***** Deshaciendo cambios de la tabla Contratacion... *****'

USE [FSC_eProject]

/****** Objeto:  Tabla [dbo].[PolizaDetails]    Fecha: 09/05/2013 11:31:32 ******/

ALTER TABLE [dbo].[PolizaDetails]
DROP COLUMN [aseguradora] [varchar] (200) NULL;
GO
	
print '***** Los campos se han modificando correctamente en la tabla Contratacion!! *****'
print ''