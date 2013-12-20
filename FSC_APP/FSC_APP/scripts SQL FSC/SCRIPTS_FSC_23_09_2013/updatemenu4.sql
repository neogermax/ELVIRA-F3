USE [FSC_eSecurity]

SET IDENTITY_INSERT [dbo].[MenusByUserGroup] ON
INSERT [dbo].[MenusByUserGroup] ([Id], [IdUserGroup], [IdMenu]) VALUES (698, 1, 322)
INSERT [dbo].[MenusByUserGroup] ([Id], [IdUserGroup], [IdMenu]) VALUES (699, 1, 324)
SET IDENTITY_INSERT [dbo].[MenusByUserGroup] OFF

update menu set textfield = 'Aprendizaje Logros y Ajustes' where ID = 355
update menu set textfield = 'Registrar testimonios ajustes y aprendizajes' where ID = 319
update menu set textfield = 'Busqueda (incluida busqueda por toda la clasificacion)' where ID = 324