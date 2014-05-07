/*
 * Object:
 * --------------------------------------------------------------
 * Created By: Pedro Cruz
 * Created At: 2014/05/07 10:38:07
 * Comments: Columna para fecha de liquidación en contratación
 *
 * --------------------------------------------------------------
 */

ALTER TABLE [dbo].[ContractRequest]
ADD [LiquidationDate] [datetime] NULL;
GO