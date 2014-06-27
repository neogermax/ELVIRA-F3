/*
 * Object: ContractNature
 * --------------------------------------------------------------
 * Created By: Pedro Cruz
 * Created At: 2014/06/26 17:09:25
 * Comments: Script para crear tabla Naturaleza del contrato.
 *
 * --------------------------------------------------------------
 */

CREATE TABLE [dbo].[ContractNature] (
  [id] [int] IDENTITY (1, 1) NOT NULL,
  [ContractNature_id] [int] NULL,
  [Nature_Name] [nvarchar] (200) NULL,
) ON [PRIMARY];
GO