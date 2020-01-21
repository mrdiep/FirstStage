CREATE VIEW [dbo].[UserPermission] AS 
SELECT        AppUsers.Username, AppPermissions.DisplayName as 'PermissionDisplayName', AppPermissions.PermissionName AS Permission
FROM            AppPermissions INNER JOIN
                         AppUserRoles INNER JOIN
                         AppUsers ON AppUserRoles.UserId = AppUsers.Id INNER JOIN
                         AppRoles ON AppUserRoles.RoleId = AppRoles.Id INNER JOIN
                         AppRolePermissions ON AppRoles.Id = AppRolePermissions.RoleId ON AppPermissions.Id = AppRolePermissions.PermissionId