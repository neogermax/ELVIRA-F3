USE [FSC_eProject]
GO

/****** Object:  Table [dbo].[Third]    Script Date: 07/28/2013 10:58:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Third](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[Name] [varchar](255) NOT NULL,
	[contact] [varchar](255) NULL,
	[documents] [varchar](50) NULL,
	[phone] [varchar](50) NULL,
	[email] [varchar](500) NULL,
	[Actions] [varchar](max) NULL,
	[Experiences] [varchar](max) NULL,
	[Enabled] [bit] NOT NULL,
	[IdUser] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Third] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

SET IDENTITY_INSERT [dbo].[Third] ON
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(2, N'51905610', N'Alba Francy Suárez Méndez', NULL, NULL, NULL, NULL, NULL, NULL,1,1, CAST(0x00009D3E013F84E0 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(3, N'800148631', N'Fundación Batuta', NULL, NULL, NULL, NULL, NULL, NULL,1,1, CAST(0x00009D5D0104F76C AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(4, N'8600383389', N'Fundacion Saldarriaga Concha', NULL, NULL, NULL, NULL, NULL, NULL,1,1, CAST(0x00009D7500BD3D50 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(6, N'1', N'Secretarias de integracion social', NULL, NULL, NULL, NULL, NULL, NULL,1,1, CAST(0x00009D7500F77D6C AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(9, N'999999999', N'Accion Social', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009DA3008F4FE4 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(10, N'9999995', N'Instituto Colombiano de Bienestar Familiar', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009DA3008F811C AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(11, N'99999996', N'Oficina Internacional de Migraciones', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009DA3008FB95C AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(12, N'9999997', N'Foro Nacional por Colombia', NULL, NULL, NULL, NULL, N'Operador', N'Reddis 1 Fase Incidencia Publica',1,3, CAST(0x00009DA3008FF520 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(13, N'999999998', N'Embajada Británica', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009DA300901CF8 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(14, N'811044436', N'Obra Social Beata Laura Montoya(Liliane Fonds)Colombia ', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009DA300905664 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(16, N'900280137', N'Fundacion Fahrenheit 451', NULL, NULL, NULL, NULL, N'Operador - Aliado', N'Operador del proyecto en primera fase con la FSC',1,3, CAST(0x00009EA4008F8E00 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(17, N'891502238', N'Fundacion para la estimulacion y el desarrollo en las artes FEDAR', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009EA400B48160 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(18, N'860403137', N'Organización de Estados Iberoamericanos - OEI', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009EA400EAEDCC AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(19, N'900150102', N'CORPORACION ECCOS CONTACTO COLOMBIA', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009EA401007070 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(20, N'900282782', N'Corporación Ventures', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009EA4010B8910 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(21, N'899999063', N'Universidad Nacional de Colombia', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009EA40149FF4C AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(22, N'51939359', N'Diana Patricia Martínez Gallego', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009EA4014A4DD0 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(23, N'899999114', N'Departamento de Cundinamarca', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009EA4014ABBF8 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(24, N'860021725', N'Conferencia Episcopal de Colombia', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009EA4016C833C AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(25, N'900007511', N'Pastoral Social Caritas la Dorada', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009EA50089A8A0 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(26, N'891901282', N'Corporación Diocesana Pro-comunidad cristiana', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009EA50089F850 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(27, N'827000274', N'Servicio Solidario y Misionero', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009EA5008A54E4 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(28, N'830130422', N'Corporación Colombiana de Padres y Madres- REDPAPAZ', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009EA500BDD0F8 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(29, N'806014972', N'Fundación Aluna', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009EA500C46AD0 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(30, N'860090032', N'Fundación Compartir', NULL, NULL, NULL, NULL, NULL, NULL,1,3, CAST(0x00009EA500CBA408 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(34, N'NULL', N'16', NULL, NULL, NULL, NULL, N'Operador proyecto', N'Trabajo con inclusion de poblaciones vulnerables a traves de la literatura',1,3, CAST(0x0000A03D009CBF51 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(35, N'NULL', N'23', NULL, NULL, NULL, NULL, N'Convenio con Secretaria de educacion para el desarrollo del proyecto', N'.',1,3, CAST(0x0000A03D009CBF51 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(36, N'NULL', N'Corporacion Eccos Contac Center', NULL, NULL, NULL, NULL, N'Socio Operador', N'Creador del modelo ECCOS Contac Center para personas con discapacidad',1,3, CAST(0x0000A03D009CBF51 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(37, N'NULL', N'Fundacion Luker', NULL, NULL, NULL, NULL, N'Socio', N'Experto en Educacion',1,3, CAST(0x0000A03D009CBF51 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(38, N'NULL', N'Secretaria de Educacion de Cartagena', NULL, NULL, NULL, NULL, N'Aporte de contrapartida', N'.',1,3, CAST(0x00009F5A01446758 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(39, N'NULL', N'Fulbright Colombia', NULL, NULL, NULL, NULL, N'Socio - Operador', N'Asignacion de Becas en EE.UU.',1,3, CAST(0x0000A03D009CBF51 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(40, N'NULL', N'Fundacion Carolina ', NULL, NULL, NULL, NULL, N'Socio - Operador', N'Manejo las becas de la Fundacion Carolina España',1,3, CAST(0x0000A05200E870C4 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(41, N'NULL', N'Ministerio de Educacion Nacional', NULL, NULL, NULL, NULL, N'Socio ', N'Pais',1,3, CAST(0x0000A05200E8CB29 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(42, N'NULL', N'Icetex', NULL, NULL, NULL, NULL, N'Socio - Operador', N'Pais',1,3, CAST(0x0000A05200E8CB94 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(43, N'NULL', N'Agencia PANDI', NULL, NULL, NULL, NULL, N'Operador', N'Tercera fase del proyecto de formacion a periodistas  posicionamiento de nuestros temas en los medios y monitoreo del tema en los mismos.',1,3, CAST(0x0000A0520104AA11 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(44, N'NULL', N'universidad popular del cesar', NULL, NULL, NULL, NULL, N'capacitador', N'423$"#$"%[]¨[´´ facultad de agricultura..',1,3, CAST(0x0000A05201108DAE AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(45, N'NULL', N'localidad de usaquen ', NULL, NULL, NULL, NULL, N'recursos fisicos y humanos. ', N'expericencia con el jardin botanico y la secretaria de bogota',1,3, CAST(0x0000A05201108DB3 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(46, N'NULL', N'VENTURES', NULL, NULL, NULL, NULL, N'Socio Operador', N'Creador del concurso de Emprendimiento',1,3, CAST(0x0000A0AD010AC023 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(47, N'NULL', N'Ministerio de Defensa Nacional ', NULL, NULL, NULL, NULL, N'Socio ', N'Fuerza Publica ',1,3, CAST(0x0000A0E001046E5C AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(48, N'NULL', N'Corporacion General Gustavo Matamoros D Acosta', NULL, NULL, NULL, NULL, N'Operador - Socio', N'Manejo de proyectos de la Fuerza Publica entre sus lineas se encuentra educacion basica y media y superior.',1,3, CAST(0x0000A0E001046E61 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(49, N'NULL', N'Secretaria de Educación de Chía', NULL, NULL, NULL, NULL, N'Socio Operador', NULL,1,3, CAST(0x0000A0AD010AC023 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(50, N'NULL', N'Red de Familias por el Cambio - Asdown', NULL, NULL, NULL, NULL, N'Socio Operador', N'Creador del concurso de Emprendimiento',1,3, CAST(0x0000A0AD010AC023 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(51, N'NULL', N'Fundación semana', NULL, NULL, NULL, NULL, N'Socio Operador', NULL,1,3, CAST(0x0000A0AD010AC023 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(52, N'NULL', N'Fundación Carvajal', NULL, NULL, NULL, NULL, N'Socio Operador', NULL,1,3, CAST(0x0000A0AD010AC023 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(53, N'NULL', N'AGENCIA DE ESTADOS UNIDOS PARA EL DESARROLLO INTERNACIONAL USAID', NULL, NULL, NULL, NULL, N'Socio Operador', NULL,1,3, CAST(0x0000A0AD010AC023 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(54, N'NULL', N'Agencia Nacional para la Superación de la Pobreza - ANSPE', NULL, NULL, NULL, NULL, N'Socio Operador', NULL,1,3, CAST(0x0000A0AD010AC023 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(55, N'NULL', N'Red de Liliane Fonds', NULL, NULL, NULL, NULL, N'Socio Operador', NULL,1,3, CAST(0x0000A0AD010AC023 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(56, N'NULL', N'ADRIÁN PELÁEZ', NULL, NULL, NULL, NULL, N'Socio Operador', NULL,1,3, CAST(0x0000A0AD010AC023 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(57, N'NULL', N'uninorte', NULL, NULL, NULL, NULL, N'Socio Operador', NULL,1,3, CAST(0x0000A0AD010AC023 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(58, N'NULL', N'Alcaldia de Manizales ', NULL, NULL, NULL, NULL, N'Socio Operador', NULL,1,3, CAST(0x0000A0AD010AC023 AS DateTime))
INSERT [dbo].[Third] ([Id], [Code], [Name],[contact],[documents],[phone],[email], [Actions], [Experiences], [Enabled], [IdUser], [CreateDate]) VALUES	(59, N'NULL', N'CORPORACIÓN DIOCESANA PRO- COMUNIDAD CRISTIANA DIOCESIS DE CARTAGO', NULL, NULL, NULL, NULL, N'Socio Operador',NULL,1,3, CAST(0x0000A0AD010AC023 AS DateTime))
SET IDENTITY_INSERT [dbo].[Third] OFF

