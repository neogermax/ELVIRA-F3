print ''
print '***** Eliminando campos de la tabla Contratacion... *****'

USE [FSC_eProject]

/****** Objeto:  Tabla [dbo].[ContractRequest]    Fecha: 09/03/2013 11:31:32 ******/

ALTER TABLE [dbo].[ContractRequest]
DROP COLUMN [Finished];
GO

	
print '***** Los campos se han eliminado correctamente de la tabla Contratacion!! *****'
print ''