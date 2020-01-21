using DataAccessLayer.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace WebApi.Controllers.System
{
    public partial class AppRolePermissionsController : ODataController
    {
        private readonly DatabaseContext databaseContext;

        public AppRolePermissionsController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

		[EnableQuery]
        public IActionResult Get()
        {
            return Ok(databaseContext.AppRolePermissions);
        }

		[EnableQuery]
        public IActionResult Get(Int32 key)
        {
            return Ok(databaseContext.AppRolePermissions.FirstOrDefault(c => c.Id == key));
        }


        [EnableQuery]
        public IActionResult GetId([FromODataUri] Int32 key)
        {
            var result = databaseContext.AppRolePermissions.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.Id).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetRoleId([FromODataUri] Int32 key)
        {
            var result = databaseContext.AppRolePermissions.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.RoleId).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetPermissionId([FromODataUri] Int32 key)
        {
            var result = databaseContext.AppRolePermissions.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.PermissionId).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetPermission([FromODataUri] Int32 key)
        {
            var result = databaseContext.AppRolePermissions.Where(m => m.Id == key).Include(x => x.Permission);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.Permission).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetRole([FromODataUri] Int32 key)
        {
            var result = databaseContext.AppRolePermissions.Where(m => m.Id == key).Include(x => x.Role);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.Role).FirstOrDefault());
        }
    }
}
