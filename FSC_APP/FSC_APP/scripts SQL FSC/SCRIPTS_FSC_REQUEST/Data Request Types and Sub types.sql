SET IDENTITY_INSERT [dbo].[RequestSubtype] ON;

INSERT INTO [dbo].[RequestSubtype] ([Id], [Subtype])
VALUES (1, 'Adici�n');

INSERT INTO [dbo].[RequestSubtype] ([Id], [Subtype])
VALUES (2, 'Prorroga');

INSERT INTO [dbo].[RequestSubtype] ([Id], [Subtype])
VALUES (3, 'Entregables');

INSERT INTO [dbo].[RequestSubtype] ([Id], [Subtype])
VALUES (4, 'Fecha de los desembolsos');

SET IDENTITY_INSERT [dbo].[RequestSubtype] OFF;




SET IDENTITY_INSERT [dbo].[RequestType] ON;

INSERT INTO [dbo].[RequestType] ([Id], [type])
VALUES (1, 'Adici�n, Pr�rroga, Entregables');

INSERT INTO [dbo].[RequestType] ([Id], [type])
VALUES (2, 'Suspensi�n');

INSERT INTO [dbo].[RequestType] ([Id], [type])
VALUES (3, 'Alcance');

INSERT INTO [dbo].[RequestType] ([Id], [type])
VALUES (4, 'Cesi�n');

INSERT INTO [dbo].[RequestType] ([Id], [type])
VALUES (5, 'Otros');

SET IDENTITY_INSERT [dbo].[RequestType] OFF;