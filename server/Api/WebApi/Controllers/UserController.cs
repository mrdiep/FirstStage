using AppModule;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly UserPersistence userPersistence;

        public UserController(IConfiguration configuration, UserPersistence userPersistence)
        {
            this.configuration = configuration;
            this.userPersistence = userPersistence;
        }

        [HttpGet]
        [Permission(Constants.Permissions.UserManagementRead)]
        public IActionResult Read()
        {
            return Ok(userPersistence.GetUsers());
        }

        [HttpPut]
        [Permission(Constants.Permissions.UserManagementModify)]
        public IActionResult Modify()
        {
            return Ok(new
            {
                User = this.User.Identity.Name
            });
        }

        [HttpPost]
        [Permission(Constants.Permissions.UserManagementCreate)]
        public IActionResult Create()
        {
            return Ok(new
            {
                User = this.User.Identity.Name
            });
        }

        [HttpPost]
        [Route("active")]
        [Permission(Constants.Permissions.UserManagementActive)]
        public IActionResult Active()
        {
            return Ok(new
            {
                User = this.User.Identity.Name
            });
        }

        [HttpPost]
        [Route("deactive")]
        [Permission(Constants.Permissions.UserManagementDeactive)]
        public IActionResult Deactive()
        {
            return Ok(new
            {
                User = this.User.Identity.Name
            });
        }

        [HttpDelete]
        [Permission(Constants.Permissions.UserManagementDelete)]
        public IActionResult Delete()
        {
            return Ok(new
            {
                User = this.User.Identity.Name
            });
        }
    }
}
