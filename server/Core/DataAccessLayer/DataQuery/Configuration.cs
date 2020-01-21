using DataAccessLayer.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace DataAccessLayer.DataQuery
{
    public class DataQueryConfigurationExtensions
    {
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EnableLowerCamelCase();
            builder.EntitySet<AppPermissions>("AppPermissions");
            builder.EntitySet<AppRolePermissions>("AppRolePermissions");
            builder.EntitySet<AppRoles>("AppRoles");
            builder.EntitySet<AppUserRoles>("AppUserRoles");
            builder.EntitySet<AppUsers>("AppUsers");

            return builder.GetEdmModel();
        }
    }
}
