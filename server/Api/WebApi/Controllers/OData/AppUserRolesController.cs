using DataAccessLayer.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace WebApi.Controllers.System
{
    public partial class AppUserRolesController : ODataController
    {
        private readonly DatabaseContext databaseContext;

        public AppUserRolesController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

		[EnableQuery]
        public IActionResult Get()
        {
            return Ok(databaseContext.AppUserRoles);
        }

		[EnableQuery]
        public IActionResult Get(Int32 key)
        {
            return Ok(databaseContext.AppUserRoles.FirstOrDefault(c => c.Id == key));
        }


        [EnableQuery]
        public IActionResult GetId([FromODataUri] Int32 key)
        {
            var result = databaseContext.AppUserRoles.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.Id).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetUserId([FromODataUri] Int32 key)
        {
            var result = databaseContext.AppUserRoles.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.UserId).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetRoleId([FromODataUri] Int32 key)
        {
            var result = databaseContext.AppUserRoles.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.RoleId).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetRole([FromODataUri] Int32 key)
        {
            var result = databaseContext.AppUserRoles.Where(m => m.Id == key).Include(x => x.Role);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.Role).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetUser([FromODataUri] Int32 key)
        {
            var result = databaseContext.AppUserRoles.Where(m => m.Id == key).Include(x => x.User);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.User).FirstOrDefault());
        }
    }
}
