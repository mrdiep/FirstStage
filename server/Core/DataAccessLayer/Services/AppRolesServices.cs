using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Services.Validators;
using Infrastructure;
using Infrastructure.Commands.AppRoles;
using DataAccessLayer.Models;
using ServiceStack.OrmLite;
using ServiceStack.Data;
using DataAccessLayer.Emails;

namespace DataAccessLayer.Services
{
    public partial class AppRolesServices : ServiceBase,
        ICommandHandler<UpdatePermissionsForRoleCommand>,
        ICommandHandler<AddNewRoleCommand>,
        ICommandHandler<UpdateRoleInfoCommand>
    {
        public AppRolesServices(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
        {
        }

        [HasPermission(Constants.Permissions.PermissionAccessModify)]
        [EnableValidator(typeof(UpdatePermissionsForRoleCommandValidator))]
        public async Task HandlerAsync(UpdatePermissionsForRoleCommand command)
        {
            ExecInTraction((db) =>
            {
                db.Delete<AppRolePermissions>(x => x.RoleId == command.RoleId);
                db.InsertAll(command.PermissionIds.Select(x => new AppRolePermissions
                {
                    RoleId = command.RoleId,
                    PermissionId = x
                }));
            });

        }

        [EnableValidator(typeof(AddNewRoleCommandValidator))]
        [HasPermission(Constants.Permissions.PermissionAccessCreate)]
        public async Task HandlerAsync(AddNewRoleCommand command)
        {
            ExecInTraction((db) =>
            {
                db.Insert<AppRoles>(new AppRoles { Id = Guid.NewGuid(), RoleName = command.RoleName, Category = command.Category, DisplayName = command.DisplayName, IsEnabled = true });
            });
        }

        [EnableValidator(typeof(UpdateRoleInfoCommandValidator))]
        [HasPermission(Constants.Permissions.PermissionAccessModify)]
        [SendMailAfterExectuted(TemplateEmail=typeof(A1EmailHandler))]
        [SendMailAfterExectuted(TemplateEmail=typeof(A2EmailHandler))]
        public async Task HandlerAsync(UpdateRoleInfoCommand command)
        {
            ExecInTraction((db) =>
            {
                db.UpdateOnly<AppRoles>(new AppRoles { RoleName = command.RoleName, Category = command.Category, DisplayName = command.DisplayName, IsEnabled = command.IsEnabled }, x => x.Id == command.Id);
            });
        }
    }
}
