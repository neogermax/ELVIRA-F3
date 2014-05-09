/*
 * Object: SupervisorbyContractReq
 * --------------------------------------------------------------
 * Created By: Pedro Cruz
 * Created At: 2014/05/09 15:35:13
 * Comments: Creacion de tabla de Supervisores por contratacion
 *
 * --------------------------------------------------------------
 */

CREATE TABLE [dbo].[SupervisorbyContractReq] (
  [id] [int] IDENTITY (1, 1) NOT NULL,
  [Third_Id] [int] NULL,
  [ContractRequest_Id] [int] NULL,
) ON [PRIMARY];
GO

ALTER TABLE [dbo].[SupervisorbyContractReq]  WITH CHECK ADD  CONSTRAINT [FK_SupervisorbyContractReq_ContractRequest] FOREIGN KEY([ContractRequest_Id])
REFERENCES [dbo].[ContractRequest] ([RequestNumber])
GO

ALTER TABLE [dbo].[SupervisorbyContractReq] CHECK CONSTRAINT [FK_SupervisorbyContractReq_ContractRequest]
GO