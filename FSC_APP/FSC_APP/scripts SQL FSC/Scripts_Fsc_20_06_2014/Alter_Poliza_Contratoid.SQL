/*
 * Object: Poliza
 * --------------------------------------------------------------
 * Created By: Pedro Cruz
 * Created At: 2014/06/20 09:19:04
 * Comments: Script para cambiar el tipo de datos de numero de poliza y contrato id por varchar 255
 *
 * --------------------------------------------------------------
 */


ALTER TABLE [dbo].[Poliza]
ALTER COLUMN [numero_poliza] [varchar] (255) NOT NULL;
GO

ALTER TABLE [dbo].[Poliza]
ALTER COLUMN [contrato_id] [varchar] (255) NOT NULL;
GO
