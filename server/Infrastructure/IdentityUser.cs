using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public class SignInUser
    {
        public Guid? UserId { get; set; }
        public string Username { get; set; }
        public IEnumerable<string> Permissions { get; set; } = Enumerable.Empty<string>();
    }
}
