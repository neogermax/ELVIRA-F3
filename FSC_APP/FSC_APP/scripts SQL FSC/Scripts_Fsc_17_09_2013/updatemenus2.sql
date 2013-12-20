USE [FSC_eSecurity]
SET IDENTITY_INSERT [dbo].[Menu] ON
INSERT [dbo].[Menu] ([Id], [TextField], [URL], [Enabled], [SortOrder], [Level]) VALUES (374, N'Consulta general de proyectos', N'/FSC/Report/generalConsultingProjects.aspx', N'T', 99, 3)
INSERT [dbo].[Menu] ([Id], [TextField], [URL], [Enabled], [SortOrder], [Level]) VALUES (375, N'Componente', NULL, N'T', 99, 2)
INSERT [dbo].[Menu] ([Id], [TextField], [URL], [Enabled], [SortOrder], [Level]) VALUES (376, N'Nuevo Componente', N'/FSC/FormulationAndAdoption/addComponent.aspx?op=add', N'T', 99, 3)
INSERT [dbo].[Menu] ([Id], [TextField], [URL], [Enabled], [SortOrder], [Level]) VALUES (377, N'Buscar Componente', N'/FSC/FormulationAndAdoption/searchcomponent.aspx', N'T', 99, 3)
INSERT [dbo].[Menu] ([Id], [TextField], [URL], [Enabled], [SortOrder], [Level]) VALUES (378, N'Objetivo', NULL, N'T', 99, 3)
INSERT [dbo].[Menu] ([Id], [TextField], [URL], [Enabled], [SortOrder], [Level]) VALUES (379, N'Nuevo Objetivo', N'/FSC/FormulationAndAdoption/addObjective.aspx?op=add', N'T', 99, 4)
INSERT [dbo].[Menu] ([Id], [TextField], [URL], [Enabled], [SortOrder], [Level]) VALUES (380, N'Buscar Objetivo', N'/FSC/FormulationAndAdoption/searchObjective.aspx', N'T', 99, 4)
INSERT [dbo].[Menu] ([Id], [TextField], [URL], [Enabled], [SortOrder], [Level]) VALUES (381, N'Mitigación', NULL, N'T', 99, 3)
INSERT [dbo].[Menu] ([Id], [TextField], [URL], [Enabled], [SortOrder], [Level]) VALUES (382, N'Nueva Mitigación', N'/FSC/FormulationAndAdoption/addMitigation.aspx?op=add', N'T', 99, 4)
INSERT [dbo].[Menu] ([Id], [TextField], [URL], [Enabled], [SortOrder], [Level]) VALUES (383, N'Buscar Mitigación', N'/FSC/FormulationAndAdoption/searchMitigation.aspx', N'T', 99, 4)
SET IDENTITY_INSERT [dbo].[Menu] OFF

insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,374)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,215)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,194)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,267)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,272)
update Menu set level = 2 where id = 213
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,213)
update Menu set level = 3 where id = 229
update Menu set level = 3 where id = 230
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,229)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,230)
update Menu set TextField = 'Panel General de Proyectos' where id = 194
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,194)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,375)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,376)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,377)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,378)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,379)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,380)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,381)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,382)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,383)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(28,378)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(28,379)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(28,380)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(28,381)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(28,382)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(28,383)