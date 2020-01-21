using DataAccessLayer.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace WebApi.Controllers.System
{
    public partial class AppPermissionsController : ODataController
    {
        private readonly DatabaseContext databaseContext;

        public AppPermissionsController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

		[EnableQuery]
        public IActionResult Get()
        {
            return Ok(databaseContext.AppPermissions);
        }

		[EnableQuery]
        public IActionResult Get(Guid key)
        {
            return Ok(databaseContext.AppPermissions.FirstOrDefault(c => c.Id == key));
        }


        [EnableQuery]
        public IActionResult GetId([FromODataUri] Guid key)
        {
            var result = databaseContext.AppPermissions.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.Id).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetPermissionName([FromODataUri] Guid key)
        {
            var result = databaseContext.AppPermissions.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.PermissionName).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetDisplayName([FromODataUri] Guid key)
        {
            var result = databaseContext.AppPermissions.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.DisplayName).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetCategory([FromODataUri] Guid key)
        {
            var result = databaseContext.AppPermissions.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.Category).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetIsEnabled([FromODataUri] Guid key)
        {
            var result = databaseContext.AppPermissions.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.IsEnabled).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetAppRolePermissions([FromODataUri] Guid key)
        {
            var result = databaseContext.AppPermissions.Where(m => m.Id == key).Include(x => x.AppRolePermissions);
            if (result == null) return NotFound();

            return Ok(result.SelectMany(x => x.AppRolePermissions));
        }
    }
}
