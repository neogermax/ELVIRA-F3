/*
 * Object:
 * --------------------------------------------------------------
 * Created By: Pedro Cruz
 * Created At: 2014/05/19 16:20:07
 * Comments: Creación campo contrato externo en ContractRequest
 *
 * --------------------------------------------------------------
 */
ALTER TABLE [dbo].[ContractRequest]
ADD [ExternalContract] [bit] NULL;
GO