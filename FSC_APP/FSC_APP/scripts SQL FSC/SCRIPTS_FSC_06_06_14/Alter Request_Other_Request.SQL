/*
 * Object:
 * --------------------------------------------------------------
 * Created By: Pedro Cruz
 * Created At: 2014/06/06 17:01:54
 * Comments: Agregado campo para tipo de solicitud - Otro
 *
 * --------------------------------------------------------------
 */

ALTER TABLE [dbo].[Request]
ADD [Other_Request] [varchar] (MAX) NULL;
GO
