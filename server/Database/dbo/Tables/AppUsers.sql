CREATE TABLE [dbo].[AppUsers]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(255) NULL, 
    [MidName] NVARCHAR(255) NULL, 
    [LastName] NVARCHAR(255) NULL, 
    [Birthday] DATE NULL, 
    [Username] NVARCHAR(255) NOT NULL, 
    [Password] NCHAR(255) NULL,
	[Email] NVARCHAR(255) NULL,
	[IsActive] BIT NOT NULL Default 1,
    [Avartar] NVARCHAR(255) NULL 
)
GO

CREATE UNIQUE INDEX [IX_AppUsers_Username] ON [dbo].[AppUsers] ([Username])
GO
CREATE UNIQUE INDEX [IX_AppUsers_Email] ON [dbo].[AppUsers] ([Email])
