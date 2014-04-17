--campos nuevos en Documents

USE [FSC_eProject]
alter TABLE [dbo].[Documents] add [id_document] int NULL
alter TABLE [dbo].[Documents] add [Id_Entity_Zone] varchar(100) NULL
