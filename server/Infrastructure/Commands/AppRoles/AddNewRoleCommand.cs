using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Commands.AppRoles
{
    public sealed class AddNewRoleCommand : ICommand
    {
        public string RoleName { get; set; }
        public string Category { get; set; }
        public string DisplayName { get; set; }
        public Guid[] PermissionIds { get; set; }
    }

    public sealed class UpdateRoleInfoCommand: ICommand
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public string Category { get; set; }
        public string DisplayName { get; set; }
        public bool IsEnabled { get; set; }
    }
}
