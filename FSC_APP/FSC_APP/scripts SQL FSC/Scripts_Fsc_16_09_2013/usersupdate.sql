use FSC_eSecurity

SET IDENTITY_INSERT [dbo].[ApplicationUser] ON
INSERT [dbo].[ApplicationUser] ([ID], [Code], [Name], [Password], [FirstName], [MiddleName], [LastName], [Edited], [LastLogin], [LastLogout], [IDLocality], [PositionName], [Identification], [EMail], [expiration], [Enabled], [Active], [ImageUser], [ChangePassword]) VALUES (161, N'nvalencia', N'NATALIA VALENCIA LÓPEZ', N'45183B6BFEDC9EEE0E6108E632785047', N'NATALIA', N'EUGENIA', N'VALENCIA LÓPEZ', CAST(0x0000A1E000855719 AS DateTime), CAST(0x0000A20D00C6375A AS DateTime), CAST(0x0000A1E000846E18 AS DateTime), 1, N'Gestión del Conocimiento', N'43814828', N'nvalencia@saldarriagaconcha.org', 9999, N'T', N'T', N'NoImage.gif', N'F')
INSERT [dbo].[ApplicationUser] ([ID], [Code], [Name], [Password], [FirstName], [MiddleName], [LastName], [Edited], [LastLogin], [LastLogout], [IDLocality], [PositionName], [Identification], [EMail], [expiration], [Enabled], [Active], [ImageUser], [ChangePassword]) VALUES (162, N'Asalleg', N'Ana Salleg Barrera', N'0B704C050029F463F01AED50FEEDAD55', N'Ana', N'Carolina', N'Salleg Barrera', CAST(0x0000A1B2007F1D27 AS DateTime), CAST(0x0000A20000FEAD9F AS DateTime), CAST(0x0000A1E700FC0FF6 AS DateTime), 1, N'Analista de Seguimiento y Evaluación', N'1064977797', N'asalleg@saldarriagaconcha.org', 9999, N'T', N'T', N'NoImage.gif', N'F')
SET IDENTITY_INSERT [dbo].[ApplicationUser] OFF

delete from UsersByGroup where IDApplicationUser = 8
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(8,34)
delete from UsersByGroup where IDApplicationUser = 64
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(64,34)
delete from UsersByGroup where IDApplicationUser = 10
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(10,34)
delete from UsersByGroup where IDApplicationUser = 81
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(81,34)
delete from UsersByGroup where IDApplicationUser = 3
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(3,34)
delete from UsersByGroup where IDApplicationUser = 9
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(9,34)
delete from UsersByGroup where IDApplicationUser = 26
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(26,34)
delete from UsersByGroup where IDApplicationUser = 7
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(7,1)
delete from UsersByGroup where IDApplicationUser = 126
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(126,1)
delete from UsersByGroup where IDApplicationUser = 120
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(120,28)
delete from UsersByGroup where IDApplicationUser = 73
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(73,35)
delete from UsersByGroup where IDApplicationUser = 11
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(11,36)
delete from UsersByGroup where IDApplicationUser = 19
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(19,36)
delete from UsersByGroup where IDApplicationUser = 12
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(12,36)
delete from UsersByGroup where IDApplicationUser = 17
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(17,34)
delete from UsersByGroup where IDApplicationUser = 161
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(161,34)
delete from UsersByGroup where IDApplicationUser = 125
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(125,34)
delete from UsersByGroup where IDApplicationUser = 162
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(162,28)
delete from UsersByGroup where IDApplicationUser = 14
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(14,36)

