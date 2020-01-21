GO
SET IDENTITY_INSERT [AppRolePermissions] ON
GO

insert into [dbo].[AppUsers] ([Id] ,[FirstName] ,[MidName] ,[LastName], [Birthday] ,[Username] ,[Password] ,[Email] ,[IsActive],[Avartar])
	select newid(), 'User1', '', 'Last1', '1990-04-25', 'user1', '123456', 'user1@gmail.com', 1, null union all
	select newid(), 'User2', '', 'Last2', '1990-04-25', 'user2', '123456', 'user2@gmail.com', 1, null union all
	select newid(), 'User3', '', 'Last3', '1990-04-25', 'user3', '123456', 'user3@gmail.com', 1, null union all
	select newid(), 'User4', '', 'Last4', '1990-04-25', 'user4', '123456', 'user4@gmail.com', 1, null union all
	select newid(), 'User5', '', 'Last5', '1990-04-25', 'user5', '123456', 'user5@gmail.com', 1, null union all
	select newid(), 'User6', '', 'Last6', '1990-04-25', 'user6', '123456', 'user6@gmail.com', 1, null union all
	select newid(), 'User7', '', 'Last7', '1990-04-25', 'user7', '123456', 'user7@gmail.com', 1, null union all
	select newid(), 'User8', '', 'Last8', '1990-04-25', 'user8', '123456', 'user8@gmail.com', 1, null

insert into [dbo].[AppRoles]([Id], [RoleName], [DisplayName], Category, [IsEnabled])
	select NEWID(), 'SystemAdmin', 'System Admin', 'Admin', 1 union all
	select NEWID(), 'CompanyReporter', 'Company Level Reporter', 'Report', 1 union all
	select NEWID(), 'AgencyReporter', 'Agency Level Reporter', 'Report', 1

insert into AppUserRoles([UserId], [RoleId])
    select (select Id from AppUsers where Username='User1'), (select Id from AppRoles where RoleName='SystemAdmin') union all
	select (select Id from AppUsers where Username='User1'), (select Id from AppRoles where RoleName='CompanyReporter') union all
    select (select Id from AppUsers where Username='User2'), (select Id from AppRoles where RoleName='CompanyReporter') union all
	select (select Id from AppUsers where Username='User3'), (select Id from AppRoles where RoleName='AgencyReporter')

insert into AppPermissions ([Id], [PermissionName], [DisplayName], Category)
	select newid(), 'UserManagement/Read', 'Access user', 'User Management' union all
	select newid(), 'UserManagement/Create', 'Create user', 'User Management' union all
	select newid(), 'UserManagement/Modify', 'Modify user', 'User Management' union all
	select newid(), 'UserManagement/Delete', 'Delete user', 'User Management' union all
	select newid(), 'UserManagement/Deactive', 'Deactive user', 'User Management' union all
	select newid(), 'UserManagement/Active', 'Active user', 'User Management' union all

	select newid(), 'PermissionAccess/Read', 'Access', 'Permission Access' union all
	select newid(), 'PermissionAccess/Modify', 'Modify', 'Permission Access' union all
	select newid(), 'PermissionAccess/Create', 'Create', 'Permission Access' union all
	select newid(), 'PermissionAccess/Delete', 'Delete', 'Permission Access' union all

	select newid(), 'AppRoleManagement/Read', 'Access', 'Role Management' union all
	select newid(), 'AppRoleManagement/Modify', 'Modify', 'Role Management' union all
	select newid(), 'AppRoleManagement/Create', 'Create', 'Role Management' union all
	select newid(), 'AppRoleManagement/Delete', 'Deactive', 'Role Management'



insert into AppRolePermissions ([RoleId], [PermissionId])
	select (select Id from AppRoles where [RoleName]='SystemAdmin'), Id from AppPermissions union all
	select (select Id from AppRoles where [RoleName]='CompanyReporter'), (select Id from AppPermissions where [PermissionName]='UserManagement/Read')
