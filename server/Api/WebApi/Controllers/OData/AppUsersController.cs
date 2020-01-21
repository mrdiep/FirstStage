using DataAccessLayer.Models;
using DataAccessLayer.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace WebApi.Controllers.System
{
    public partial class AppUsersController : ODataController
    {
        private readonly DatabaseContext databaseContext;

        public AppUsersController(DatabaseContext databaseContext, AppRolesServices appRolesServices)
        {
            this.databaseContext = databaseContext;
        }

		[EnableQuery]
        public IActionResult Get()
        {
            return Ok(databaseContext.AppUsers);
        }

		[EnableQuery]
        public IActionResult Get(Guid key)
        {
            return Ok(databaseContext.AppUsers.FirstOrDefault(c => c.Id == key));
        }


        [EnableQuery]
        public IActionResult GetId([FromODataUri] Guid key)
        {
            var result = databaseContext.AppUsers.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.Id).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetFirstName([FromODataUri] Guid key)
        {
            var result = databaseContext.AppUsers.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.FirstName).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetMidName([FromODataUri] Guid key)
        {
            var result = databaseContext.AppUsers.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.MidName).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetLastName([FromODataUri] Guid key)
        {
            var result = databaseContext.AppUsers.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.LastName).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetBirthday([FromODataUri] Guid key)
        {
            var result = databaseContext.AppUsers.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.Birthday).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetUsername([FromODataUri] Guid key)
        {
            var result = databaseContext.AppUsers.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.Username).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetPassword([FromODataUri] Guid key)
        {
            var result = databaseContext.AppUsers.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.Password).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetEmail([FromODataUri] Guid key)
        {
            var result = databaseContext.AppUsers.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.Email).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetIsActive([FromODataUri] Guid key)
        {
            var result = databaseContext.AppUsers.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.IsActive).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetAvartar([FromODataUri] Guid key)
        {
            var result = databaseContext.AppUsers.Where(m => m.Id == key);
            if (result == null) return NotFound();

            return Ok(result.Select(x => x.Avartar).FirstOrDefault());
        }

        [EnableQuery]
        public IActionResult GetAppUserRoles([FromODataUri] Guid key)
        {
            var result = databaseContext.AppUsers.Where(m => m.Id == key).Include(x => x.AppUserRoles);
            if (result == null) return NotFound();

            return Ok(result.SelectMany(x => x.AppUserRoles));
        }
    }
}
