CREATE TABLE [dbo].[UsersByMailGroup] (
  [Id] [int] IDENTITY (1, 1) NOT NULL,
  [MailGroup] [int] NOT NULL,
  [User_Id] [int] NOT NULL,
) ON [PRIMARY];
GO
