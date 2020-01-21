CREATE TABLE [dbo].[AppUserRoles]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[UserId] UNIQUEIDENTIFIER NOT NULL, 
    [RoleId] UNIQUEIDENTIFIER NOT NULL

	CONSTRAINT PK_UserRole UNIQUE ([UserId], [RoleId]), 
    CONSTRAINT [FK_AppUserRoles_AppRoles] FOREIGN KEY ([RoleId]) REFERENCES [AppRoles]([Id]),
	CONSTRAINT [FK_AppUserRoles_AppUsers] FOREIGN KEY ([UserId]) REFERENCES [AppUsers]([Id])

)
