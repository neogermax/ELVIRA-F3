print ''
print '***** Modificando campos de la tabla Grupos... *****'

USE [FSC_eSecurity]

/****** Objeto:  Tabla [dbo].[UserGroup]    Fecha: 09/13/2013 11:31:32 ******/

INSERT INTO [dbo].[UserGroup] ([Code], [Name], [Enabled])
VALUES ('Lider', 'Lider de Proyecto', 'T');

INSERT INTO [dbo].[UserGroup] ([Code], [Name], [Enabled])
VALUES ('Juridica', 'Juridica', 'T');

INSERT INTO [dbo].[UserGroup] ([Code], [Name], [Enabled])
VALUES ('Consulta', 'Consulta', 'T');
GO

print '***** Los campos se han modificando correctamente en la tabla Grupos!! *****'
print ''