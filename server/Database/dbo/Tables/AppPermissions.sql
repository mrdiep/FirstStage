CREATE TABLE [dbo].[AppPermissions]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [PermissionName] NVARCHAR(50) NOT NULL, 
    [DisplayName] NVARCHAR(50) NOT NULL, 
	[Category] NVARCHAR(50) NULL, 
    [IsEnabled] BIT NOT NULL DEFAULT 1
)

GO

CREATE UNIQUE INDEX [IX_AppPermissions_PermissionName] ON [dbo].[AppPermissions] ([PermissionName])
