CREATE TABLE [dbo].[AppRolePermissions]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[RoleId] UNIQUEIDENTIFIER NOT NULL, 
    [PermissionId] UNIQUEIDENTIFIER NOT NULL

	CONSTRAINT PK_AppRolePermissions UNIQUE ([PermissionId], [RoleId]), 
    CONSTRAINT [FK_AppRolePermissions_AppRoles] FOREIGN KEY ([RoleId]) REFERENCES [AppRoles]([Id]),
	CONSTRAINT [FK_AppRolePermissions_AppPermissions] FOREIGN KEY ([PermissionId]) REFERENCES [AppPermissions]([Id])
)

