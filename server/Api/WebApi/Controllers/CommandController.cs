using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Commands.AppRoles;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace WebApi.Controllers
{
    [Route("api/command")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ICommandDispatcher commandDispatcher;

        public CommandController(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ExecuteCommandRequestModel command)
        {
            var nameSpaceCode = typeof(AddNewRoleCommand).Namespace;
            var type = Assembly.GetAssembly(typeof(AddNewRoleCommand))
                .GetTypes()
                .FirstOrDefault(x => x.Name == command.CommandName);

            var code = (ICommand)command.CommandData.ToObject(type);
            var result = await this.commandDispatcher.DispatchAsync(code);
            return Ok();
        }
    }

    public class ExecuteCommandRequestModel
    {
        public string CommandName { get; set; }

        public JObject CommandData { get; set; }
    }
}