print ''
print '***** Agregando campos a la tabla Contratacion... *****'

USE [FSC_eProject]

/****** Objeto:  Tabla [dbo].[ContractRequest]    Fecha: 08/01/2013 11:31:32 ******/

ALTER TABLE [dbo].[ContractRequest]
	ADD [PolizaId] [int] NULL,
	[SignedContract] [bit] NULL,
	[StartDate] [datetime] NULL,
	[SuscriptDate] [datetime] NULL,
	[Confidential] [int] NULL
	
print '***** Los campos se han agregado correctamente a la tabla Contratacion!! *****'
print ''