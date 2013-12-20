print ''
print '***** Modificando campos de la tabla Contratacion... *****'

USE [FSC_eProject]

/****** Objeto:  Tabla [dbo].[Poliza]    Fecha: 09/05/2013 11:31:32 ******/

ALTER TABLE [dbo].[Poliza]
ADD [aseguradora] [varchar] (255) NULL;
GO
ALTER TABLE [dbo].[Poliza]
ALTER COLUMN [consecutivo] [int] NULL;
GO
	
print '***** Los campos se han modificando correctamente en la tabla Contratacion!! *****'
print ''