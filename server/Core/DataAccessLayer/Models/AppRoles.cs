using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class AppRoles
    {
        public AppRoles()
        {
            AppRolePermissions = new HashSet<AppRolePermissions>();
            AppUserRoles = new HashSet<AppUserRoles>();
        }

        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public string DisplayName { get; set; }
        public string Category { get; set; }
        public bool IsEnabled { get; set; }

        [Reference]
        public virtual ICollection<AppRolePermissions> AppRolePermissions { get; set; }

        [Reference]
        public virtual ICollection<AppUserRoles> AppUserRoles { get; set; }
    }
}
