using AppModule;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly UserPersistence userPersistence;
        private readonly DatabaseContext databaseContext;

        public RoleController(IConfiguration configuration, UserPersistence userPersistence, DatabaseContext databaseContext)
        {
            this.configuration = configuration;
            this.userPersistence = userPersistence;
            this.databaseContext = databaseContext;
        }

        [HttpGet]
        [Permission(Constants.Permissions.PermissionAccessRead)]
        public IActionResult Read()
        {

            return Ok(databaseContext.AppRoles.Select(x => new {
                RoleName = x.RoleName,
                Permisions = x.AppUserRoles.SelectMany(r => r.Role.AppRolePermissions.Select(t => new { DisplayName = t.Permission.DisplayName, PermissionName = t.Permission.PermissionName } )).OrderBy(t => t.PermissionName)
            }).ToList());
        }

        [HttpPut]
        [Permission(Constants.Permissions.PermissionAccessModify)]
        public IActionResult Modify()
        {
            return Ok(new
            {
                User = this.User.Identity.Name
            });
        }

        [HttpPost]
        [Permission(Constants.Permissions.PermissionAccessCreate)]
        public IActionResult Create()
        {
            return Ok(new
            {
                User = this.User.Identity.Name
            });
        }

        [HttpDelete]
        [Permission(Constants.Permissions.PermissionAccessDelete)]
        public IActionResult Delete()
        {
            return Ok(new
            {
                User = this.User.Identity.Name
            });
        }
    }
}
