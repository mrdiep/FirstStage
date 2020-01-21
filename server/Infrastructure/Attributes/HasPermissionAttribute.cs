using System;

namespace Infrastructure
{
    public class HasPermissionAttribute : Attribute
    {
        public HasPermissionAttribute(string permissionName)
        {
            PermissionName = permissionName;
        }

        public string PermissionName { get; }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class SendMailAfterExectutedAttribute : Attribute
    {
        public Type TemplateEmail { get; set; }
    }
}
