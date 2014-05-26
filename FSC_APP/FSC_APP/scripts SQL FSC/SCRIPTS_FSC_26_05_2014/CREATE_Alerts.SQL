/*
 * Object:Alerts
 * --------------------------------------------------------------
 * Created By: Pedro Cruz
 * Created At: 2014/05/26 09:59:44
 * Comments: Script para crear tabla alertas
 *
 * --------------------------------------------------------------
 */

CREATE TABLE [dbo].[Alerts] (
  [Alert_Id] [int] IDENTITY (1, 1) NOT NULL,
  [Name] [nvarchar] (255) NULL,
  [Users] [nvarchar] (MAX) NULL,
  [Groups] [nvarchar] (255) NULL,
  [Days] [int] NULL,
  [Subject] [nvarchar] (255) NULL,
  [Message] [nvarchar] (255) NULL,
) ON [PRIMARY]
TEXTIMAGE_ON [PRIMARY];
GO
