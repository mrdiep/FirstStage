using System;

namespace DataAccessLayer.Models
{
    public partial class AppUserRoles
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public virtual AppRoles Role { get; set; }
        public virtual AppUsers User { get; set; }
    }
}
