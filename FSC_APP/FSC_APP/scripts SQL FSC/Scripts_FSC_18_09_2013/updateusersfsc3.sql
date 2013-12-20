USE [FSC_eSecurity]

update ApplicationUser set Password = 'DFE85D6B88FE509F' where ID = 8
delete from UsersByGroup where IDApplicationUser = 8
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(8,34)

update ApplicationUser set Password = 'DFE85D6B88FE509F' where ID = 64
delete from UsersByGroup where IDApplicationUser = 64
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(64,34)

update ApplicationUser set Password = '282393FA6DE539C47C99B06297A411A9' where ID = 17
delete from UsersByGroup where IDApplicationUser = 17
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(17,34)

update ApplicationUser set Password = 'DFE85D6B88FE509F' where ID = 10
delete from UsersByGroup where IDApplicationUser = 10
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(10,34)

update ApplicationUser set Password = 'DFE85D6B88FE509F' where ID = 81
delete from UsersByGroup where IDApplicationUser = 81
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(81,34)

update ApplicationUser set Password = 'DFE85D6B88FE509F' where ID = 3
delete from UsersByGroup where IDApplicationUser = 3
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(3,34)

update ApplicationUser set Password = '16673DD70B5D0133F024AB37ED53BEE5' where ID = 9
delete from UsersByGroup where IDApplicationUser = 9
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(9,34)

update ApplicationUser set Password = 'DFE85D6B88FE509F' where ID = 161
delete from UsersByGroup where IDApplicationUser = 161
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(161,34)

update ApplicationUser set Password = 'DFE85D6B88FE509F' where ID = 26
delete from UsersByGroup where IDApplicationUser = 26
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(26,34)

update ApplicationUser set Password = 'DFE85D6B88FE509F' where ID = 125
delete from UsersByGroup where IDApplicationUser = 125
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(125,34)

update ApplicationUser set Password = '41EC43B7D39F6A9A9F34688217BF087E' where ID = 7
delete from UsersByGroup where IDApplicationUser = 7
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(7,1)

update ApplicationUser set Password = '16EB3A96D77F4952E15C55638317287A' where ID = 126
delete from UsersByGroup where IDApplicationUser = 126
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(126,1)

update ApplicationUser set Password = '0B704C050029F463F01AED50FEEDAD55' where ID = 162
delete from UsersByGroup where IDApplicationUser = 162
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(162,28)

update ApplicationUser set Password = '737FAE6F729F9B545C8C3EC706C977B2' where ID = 120
delete from UsersByGroup where IDApplicationUser = 120
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(120,28)

update ApplicationUser set Password = 'DFE85D6B88FE509F' where ID = 73
delete from UsersByGroup where IDApplicationUser = 73
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(73,35)

update ApplicationUser set Password = 'DFE85D6B88FE509F' where ID = 14
delete from UsersByGroup where IDApplicationUser = 14
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(14,36)

update ApplicationUser set Password = 'DFE85D6B88FE509F' where ID = 11
delete from UsersByGroup where IDApplicationUser = 11
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(11,36)

update ApplicationUser set Password = 'DFE85D6B88FE509F' where ID = 19
delete from UsersByGroup where IDApplicationUser = 19
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(19,36)

update ApplicationUser set Password = 'DFE85D6B88FE509F' where ID = 12
delete from UsersByGroup where IDApplicationUser = 12
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(12,36)

update ApplicationUser set Password = 'DFE85D6B88FE509F' where ID = 15
delete from UsersByGroup where IDApplicationUser = 15
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(15,36)

SET IDENTITY_INSERT [dbo].[ApplicationUser] ON
INSERT [dbo].[ApplicationUser] ([ID], [Code], [Name], [Password], [FirstName], [MiddleName], [LastName], [Edited], [LastLogin], [LastLogout], [IDLocality], [PositionName], [Identification], [EMail], [expiration], [Enabled], [Active], [ImageUser], [ChangePassword]) VALUES (164, N'asalazar', N'Andrea Salazar', N'2B98EC2068778B883B298342B47D8AD1', N'Andrea', NULL, N'Salazar', CAST(0x0000A23D00C1148E AS DateTime), CAST(0x0000A23D00C1148E AS DateTime), CAST(0x0000A23D00C1148E AS DateTime), 1, N'Lider de proyectos de fortalecimiento  comunitario', N'24337610', N'asalazar@saldarriagaconcha.org', 9999, N'T', N'T', N'NoImage.gif', N'F')
INSERT [dbo].[ApplicationUser] ([ID], [Code], [Name], [Password], [FirstName], [MiddleName], [LastName], [Edited], [LastLogin], [LastLogout], [IDLocality], [PositionName], [Identification], [EMail], [expiration], [Enabled], [Active], [ImageUser], [ChangePassword]) VALUES (165, N'mneira', N'Maria Neira', N'BC4D1258A9C9B061EFCA85507F9C2882', N'Maria', N'Alejandra', N'Neira', CAST(0x0000A23D00C14946 AS DateTime), CAST(0x0000A23D00C14946 AS DateTime), CAST(0x0000A23D00C14946 AS DateTime), 1, N'Gerente relaciones institucionales', N'51889837', N'mneira@saldarriagaconcha.org', 9999, N'T', N'T', N'NoImage.gif', N'F')
INSERT [dbo].[ApplicationUser] ([ID], [Code], [Name], [Password], [FirstName], [MiddleName], [LastName], [Edited], [LastLogin], [LastLogout], [IDLocality], [PositionName], [Identification], [EMail], [expiration], [Enabled], [Active], [ImageUser], [ChangePassword]) VALUES (166, N'crestrepo', N'Catalina  Restrepo', N'EB01B32B0E2BF459E541D8514DAA0697', N'Catalina', NULL, N'Restrepo', CAST(0x0000A23D00BF5734 AS DateTime), CAST(0x0000A23D00BF5734 AS DateTime), CAST(0x0000A23D00BF5734 AS DateTime), 1, N'Gerente Administrativa y Financiera', N'66864034', N'crestrepo@saldarriagaconcha.org', 9999, N'T', N'T', N'NoImage.gif', N'F')
INSERT [dbo].[ApplicationUser] ([ID], [Code], [Name], [Password], [FirstName], [MiddleName], [LastName], [Edited], [LastLogin], [LastLogout], [IDLocality], [PositionName], [Identification], [EMail], [expiration], [Enabled], [Active], [ImageUser], [ChangePassword]) VALUES (168, N'mvargas', N'Martha Isabel Vargas', N'CBE3F8D0CA1F9D1FB8FEC08A524B0D8D', N'Martha', N'Isabel', N'Vargas', CAST(0x0000A23D00BFD287 AS DateTime), CAST(0x0000A23D00BFD287 AS DateTime), CAST(0x0000A23D00BFD287 AS DateTime), 1, N'Asistente Juridica', N'52276634', N'mvargas@saldarriagaconcha.org', 9999, N'T', N'T', N'NoImage.gif', N'F')
INSERT [dbo].[ApplicationUser] ([ID], [Code], [Name], [Password], [FirstName], [MiddleName], [LastName], [Edited], [LastLogin], [LastLogout], [IDLocality], [PositionName], [Identification], [EMail], [expiration], [Enabled], [Active], [ImageUser], [ChangePassword]) VALUES (169, N'nrodriguez', N'Nidia  Rodriguez', N'F5381DA9A7922C04796FA0A70289EB51', N'Nidia', NULL, N'Rodriguez', CAST(0x0000A23D00C07446 AS DateTime), CAST(0x0000A23D00C07446 AS DateTime), CAST(0x0000A23D00C07446 AS DateTime), 1, N'Asistente Contable', N'52771975', N'nrodriguez@saldarriagaconcha.org', 999, N'T', N'T', N'NoImage.gif', N'F')
SET IDENTITY_INSERT [dbo].[ApplicationUser] OFF

insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(164,34)
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(165,34)
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(166,36)
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(168,35)
insert into UsersByGroup (IDApplicationUser,IDUserGroup) values(169,36)
