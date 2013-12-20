print ''
print '***** Agregando campos a la tabla Contratacion... *****'

USE [FSC_eProject]

/****** Objeto:  Tabla [dbo].[ContractRequest]    Fecha: 09/03/2013 11:31:32 ******/

ALTER TABLE [dbo].[ContractRequest]
ADD [Finished] [bit] NULL;
GO
	
print '***** Los campos se han agregado correctamente a la tabla Contratacion!! *****'
print ''