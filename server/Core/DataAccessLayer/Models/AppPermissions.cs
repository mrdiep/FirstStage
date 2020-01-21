using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class AppPermissions
    {
        public AppPermissions()
        {
            AppRolePermissions = new HashSet<AppRolePermissions>();
        }

        public Guid Id { get; set; }
        public string PermissionName { get; set; }
        public string DisplayName { get; set; }
        public string Category { get; set; }
        public bool? IsEnabled { get; set; }

        public virtual ICollection<AppRolePermissions> AppRolePermissions { get; set; }
    }
}
