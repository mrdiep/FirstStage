using ServiceStack.DataAnnotations;
using System;

namespace DataAccessLayer.Models
{
    public partial class AppRolePermissions
    {
        [AutoIncrement]
        public int Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }

        [Reference]
        public virtual AppPermissions Permission { get; set; }

        [Reference]
        public virtual AppRoles Role { get; set; }
    }
}
