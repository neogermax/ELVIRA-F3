/*
 * --------------------------------------------------------------
 * Created By: PEDRO CRUZ
 * Created At: 2014/05/06 15:46:16
 * Comments:Elimina las columnas fecha inicio y fin de vigencia de conceptos de poliza
 * --------------------------------------------------------------
 */

ALTER TABLE [dbo].[PolizaDetails]
DROP COLUMN [inivig];
GO
ALTER TABLE [dbo].[PolizaDetails]
DROP COLUMN [finvig];
GO
