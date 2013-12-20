print ''
print '***** Creando campo Poliza ID en tabla de Contratos... *****'

USE [FSC_eProject]
/****** Objeto:  Tabla [dbo].[ContractRequest]    Fecha: 07/31/2013 14:48:21 ******/

ALTER TABLE [dbo].[ContractRequest]
	[PolizaId] [int] NULL

ALTER TABLE [dbo].[ContractRequest]  WITH CHECK ADD  CONSTRAINT [FK_ContractRequest_Poliza] FOREIGN KEY([PolizaId])
REFERENCES [dbo].[Poliza] ([id])

ALTER TABLE [dbo].[ContractRequest] CHECK CONSTRAINT [FK_ContractRequest_Poliza]

print '***** Creación de campo PolizaID en la tabla Contratos Finalizada!! *****'
print ''