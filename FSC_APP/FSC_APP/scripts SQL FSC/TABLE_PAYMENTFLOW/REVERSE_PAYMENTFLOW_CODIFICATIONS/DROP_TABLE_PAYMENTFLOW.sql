USE [FSC_eProject]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Paymentflow_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[Paymentflow]'))
ALTER TABLE [dbo].[Paymentflow] DROP CONSTRAINT [FK_Paymentflow_Project]
GO

USE [FSC_eProject]
GO

/****** Object:  Table [dbo].[Paymentflow]    Script Date: 08/09/2013 08:48:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Paymentflow]') AND type in (N'U'))
DROP TABLE [dbo].[Paymentflow]
GO

