print ''
print '***** Creando tabla Poliza... *****'

USE [FSC_eProject]
/****** Objeto:  Tabla [dbo].[Poliza]    Fecha de creación: 07/31/2013 12:36:20 ******/
CREATE TABLE [dbo].[Poliza](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[numero_poliza] [varchar](50) NOT NULL,
	[consecutivo] [int] NOT NULL,
	[contrato_id] [int] NOT NULL,
 CONSTRAINT [PK_Poliza] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

print '***** Creación de la tabla Poliza Finalizada!! *****'
print ''