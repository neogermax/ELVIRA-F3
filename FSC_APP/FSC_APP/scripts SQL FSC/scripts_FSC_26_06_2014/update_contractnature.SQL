/*
 * Object: ContractNature
 * --------------------------------------------------------------
 * Created By: Pedro Cruz
 * Created At: 2014/06/26 17:09:25
 * Comments: Script para insertar datos catalogo tabla Naturaleza del contrato.
 *
 * --------------------------------------------------------------
 */

SET IDENTITY_INSERT [dbo].[ContractNature] ON
INSERT [dbo].[ContractNature] ([id], [ContractNature_id], [Nature_Name]) VALUES (1, 1, N'Contrato')
INSERT [dbo].[ContractNature] ([id], [ContractNature_id], [Nature_Name]) VALUES (2, 2, N'Convenio')
INSERT [dbo].[ContractNature] ([id], [ContractNature_id], [Nature_Name]) VALUES (3, 3, N'Orden de prestación de servicios')
INSERT [dbo].[ContractNature] ([id], [ContractNature_id], [Nature_Name]) VALUES (4, 4, N'Orden de compraventa')
INSERT [dbo].[ContractNature] ([id], [ContractNature_id], [Nature_Name]) VALUES (5, 5, N'Otro si')
INSERT [dbo].[ContractNature] ([id], [ContractNature_id], [Nature_Name]) VALUES (6, 6, N'Otros...')
SET IDENTITY_INSERT [dbo].[ContractNature] OFF