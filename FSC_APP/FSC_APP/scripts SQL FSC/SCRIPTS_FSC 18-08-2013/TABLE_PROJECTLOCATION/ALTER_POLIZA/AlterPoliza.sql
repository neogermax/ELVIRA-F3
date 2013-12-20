print ''
print '***** Agregando fechas a la tabla poliza... *****'

USE [FSC_eProject]

/****** Objeto:  Tabla [dbo].[Poliza]    Fecha: 08/18/2013 09:00:01 ******/
ALTER TABLE [dbo].[Poliza]
ADD [fecha_exp] [datetime] NULL;
GO
ALTER TABLE [dbo].[Poliza]
ADD [fecha_ven] [datetime] NULL;
GO
	
print '***** Los campos requeridos para poliza se han agregado correctamente!! *****'
print ''