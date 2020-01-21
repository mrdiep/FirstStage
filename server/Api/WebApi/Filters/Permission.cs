using Microsoft.AspNetCore.Authorization;

namespace WebApi.Filters
{
    public class PermissionAttribute : AuthorizeAttribute
    {
        public PermissionAttribute(string permisionName) 
        {
            Roles = permisionName;
        }
    }
}
