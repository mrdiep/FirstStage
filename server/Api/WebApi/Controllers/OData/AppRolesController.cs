using DataAccessLayer.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace WebApi.Controllers.System
{
    public partial class AppRolesController : ODataController
    {
        private readonly DatabaseContext databaseContext;

        public AppRolesController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

		[EnableQuery]
        public IActionResult Get()
        {
            return Ok(databaseContext.AppRoles);
        }

		[EnableQuery]
        public IActionResult Get(Guid key)
        {
            return Ok(databaseContext.AppRoles.FirstOrDefault(c => c.Id == key));
        }


        [EnableQuery]
        public IActionResult GetId([FromODataUri] Guid key)
        {
            var result = databaseContext.AppRoles.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.Id).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetRoleName([FromODataUri] Guid key)
        {
            var result = databaseContext.AppRoles.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.RoleName).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetDisplayName([FromODataUri] Guid key)
        {
            var result = databaseContext.AppRoles.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.DisplayName).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetCategory([FromODataUri] Guid key)
        {
            var result = databaseContext.AppRoles.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.Category).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetIsEnabled([FromODataUri] Guid key)
        {
            var result = databaseContext.AppRoles.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.IsEnabled).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetAppRolePermissions([FromODataUri] Guid key)
        {
            var result = databaseContext.AppRoles.Where(m => m.Id == key).Include(x => x.AppRolePermissions);
            if (result == null) return NotFound();

            return Ok(result.SelectMany(x => x.AppRolePermissions));
        }

        [EnableQuery]
        public IActionResult GetAppUserRoles([FromODataUri] Guid key)
        {
            var result = databaseContext.AppRoles.Where(m => m.Id == key).Include(x => x.AppUserRoles);
            if (result == null) return NotFound();

            return Ok(result.SelectMany(x => x.AppUserRoles));
        }
    }
}
