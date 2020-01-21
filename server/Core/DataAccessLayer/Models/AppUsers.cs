using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class AppUsers
    {
        public AppUsers()
        {
            AppUserRoles = new HashSet<AppUserRoles>();
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public string Avartar { get; set; }

        public virtual ICollection<AppUserRoles> AppUserRoles { get; set; }
    }
}
