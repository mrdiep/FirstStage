CREATE VIEW [dbo].[RolePermission] AS 
SELECT       AppRoles.RoleName, AppPermissions.DisplayName AS PermissionDescription, AppPermissions.PermissionName AS PermissionName
FROM            AppPermissions INNER JOIN
                         AppRoles INNER JOIN
                         AppRolePermissions ON AppRoles.Id = AppRolePermissions.RoleId ON AppPermissions.Id = AppRolePermissions.PermissionId