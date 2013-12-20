print ''
print '***** Creando tabla Detalles de Poliza... *****'

USE [FSC_eProject]
/****** Objeto:  Tabla [dbo].[PolizaDetails]    Fecha: 07/31/2013 13:00:31 ******/

CREATE TABLE [dbo].[PolizaDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Poliza] [int] NOT NULL,
	[Concepto] [varchar](50) NULL,
	[Consecutivo] [varchar](50) NULL,
	[Fecha_Ini] [datetime] NOT NULL,
	[Fecha_Fin] [datetime] NULL,
 CONSTRAINT [PK_PolizaDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[PolizaDetails]  WITH CHECK ADD  CONSTRAINT [FK_PolizaDetails_Poliza] FOREIGN KEY([Id_Poliza])
REFERENCES [dbo].[Poliza] ([id])

ALTER TABLE [dbo].[PolizaDetails] CHECK CONSTRAINT [FK_PolizaDetails_Poliza]

print '***** Creación de la tabla Detalles de Poliza Finalizada!! *****'
print ''