USE [FSC_eSecurity]
SET IDENTITY_INSERT [dbo].[Menu] ON
INSERT [dbo].[Menu] ([Id], [TextField], [URL], [Enabled], [SortOrder], [Level]) VALUES (385, N'Reporte de Actas', N'/FSC/Report/Engagement/ReportContractRequestactas.aspx', N'T', 99, 3)
SET IDENTITY_INSERT [dbo].[Menu] OFF
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(1,385)
insert into MenusByUserGroup (IdUserGroup,IdMenu) values(28,385)