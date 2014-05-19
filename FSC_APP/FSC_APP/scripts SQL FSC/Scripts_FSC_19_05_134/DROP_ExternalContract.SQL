/*
 * Object:
 * --------------------------------------------------------------
 * Created By: Pedro Cruz
 * Created At: 2014/05/19 16:20:41
 * Comments: Eliminación campo contrato externo en ContractRequest
 *
 * --------------------------------------------------------------
 */
ALTER TABLE [dbo].[ContractRequest]
DROP COLUMN [ExternalContract];
GO
