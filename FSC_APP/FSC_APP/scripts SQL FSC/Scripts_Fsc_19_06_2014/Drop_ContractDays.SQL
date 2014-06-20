/*
 * Object:Contract Request
 * --------------------------------------------------------------
 * Created By: Pedro Cruz
 * Created At: 2014/06/19 15:43:47
 * Comments:Eliminar días de duración del contrato.
 *
 * --------------------------------------------------------------
 */

ALTER TABLE [dbo].[ContractRequest]
DROP COLUMN [ContractDays];
GO
