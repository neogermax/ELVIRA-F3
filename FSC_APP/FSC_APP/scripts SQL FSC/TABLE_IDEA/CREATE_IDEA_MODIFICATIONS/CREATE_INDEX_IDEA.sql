USE [FSC_eProject]
GO

/****** Object:  Index [PK_Idea]    Script Date: 07/28/2013 10:30:08 ******/
ALTER TABLE [dbo].[Idea] ADD  CONSTRAINT [PK_Idea] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

