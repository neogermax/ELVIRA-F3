/*
 * --------------------------------------------------------------
 * Created By: PEDRO CRUZ
 * Created At: 2014/05/06 15:46:16
 * Comments:Crea las columnas fecha inicio y fin de vigencia de conceptos de poliza
 * --------------------------------------------------------------
 */

ALTER TABLE [dbo].[PolizaDetails]
ADD [inivig] [date] NULL;
GO
ALTER TABLE [dbo].[PolizaDetails]
ADD [finvig] [date] NULL;
GO
