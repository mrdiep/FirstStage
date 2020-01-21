using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers.System
{
    public partial class AppRolesController : ODataController
    {
        [Route("Permissions")]
        [HttpPut]
        public async Task<IActionResult> UpdatePermissions([FromBody](Guid roleId, Guid[] permission) requestBody)
        {
            return Ok();
        }
    }
}
