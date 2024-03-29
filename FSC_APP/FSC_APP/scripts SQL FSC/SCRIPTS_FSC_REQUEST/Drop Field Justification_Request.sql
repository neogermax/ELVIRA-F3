/*
   lunes, 16 de junio de 201408:49:48 a.m.
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
ALTER TABLE dbo.Request
	DROP COLUMN Justification_Request
GO
ALTER TABLE dbo.Request SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
