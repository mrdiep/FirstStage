using System;

namespace Infrastructure.Commands.AppRoles
{
    public sealed class UpdatePermissionsForRoleCommand : ICommand
    {
        public Guid RoleId { get; set; }
        public Guid[] PermissionIds { get; set; }
    }
}
