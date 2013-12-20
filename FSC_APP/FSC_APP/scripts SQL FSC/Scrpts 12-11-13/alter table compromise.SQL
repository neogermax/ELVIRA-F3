USE [FSC_eproject]

ALTER TABLE [dbo].[Compromise]
ALTER COLUMN [id] [numeric] (18, 0) NOT NULL;
GO

ALTER TABLE [dbo].[Compromise]
ADD [proceeding_log_id] [int] NULL;
GO

ALTER TABLE [dbo].[Compromise]
WITH CHECK 
ADD CONSTRAINT [FK_Compromise_Proceeding_Logs]
FOREIGN KEY ([proceeding_log_id])
 REFERENCES [dbo].[Proceeding_Logs]
            ([Id])
ON DELETE NO ACTION
ON UPDATE NO ACTION;
GO
ALTER TABLE [dbo].[Compromise] CHECK CONSTRAINT [FK_Compromise_Proceeding_Logs];
GO

ALTER TABLE [dbo].[Proceeding_Logs]
WITH CHECK 
ADD CONSTRAINT [FK_Proceeding_Logs_Project]
FOREIGN KEY ([Project_Id])
 REFERENCES [dbo].[Project]
            ([Id])
ON DELETE NO ACTION
ON UPDATE NO ACTION;
GO
ALTER TABLE [dbo].[Proceeding_Logs] CHECK CONSTRAINT [FK_Proceeding_Logs_Project];
GO