--campos nuevos en proyecto
USE [FSC_eProject]
alter TABLE [dbo].[project] add [OtherResults] varchar(max) NULL
alter TABLE [dbo].[project] add [obligationsoftheparties] varchar(max) NULL
alter TABLE [dbo].[project] add [BudgetRoute] varchar(max) NULL
alter TABLE [dbo].[project] add [RisksIdentified] varchar(max) NULL
alter TABLE [dbo].[project] add [RiskMitigation] varchar(max) NULL
alter TABLE [dbo].[project] add [days] varchar(max) NULL
alter TABLE [dbo].[project] add [ideaappliesIVA] int NULL
alter TABLE [dbo].[project] ALTER COLUMN [Mother] int NULL
alter TABLE [dbo].[project] add [Project_derivados] int NULL


