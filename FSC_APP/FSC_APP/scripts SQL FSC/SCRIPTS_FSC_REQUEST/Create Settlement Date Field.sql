/*
   jueves, 19 de junio de 201405:04:39 p.m.
   User: dbusr_FSC
   Server: (local)
   Database: FSC_eProject
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Request ADD
	Settlement_Date date NULL
GO
ALTER TABLE dbo.Request SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
