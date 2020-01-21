using System;
using DataAccessLayer.Models;
using FluentValidation;
using Infrastructure;
using Infrastructure.Commands.AppRoles;

namespace DataAccessLayer.Services.Validators
{
    public class UpdatePermissionsForRoleCommandValidator : AbstractValidator<UpdatePermissionsForRoleCommand>
    {
        private readonly DatabaseContext databaseContext;

        public UpdatePermissionsForRoleCommandValidator(DatabaseContext databaseContext, SignInUser signInUser)
        {
            this.databaseContext = databaseContext;

            RuleFor(x => x.RoleId).NotEmpty().NotNull().Must(Test);
            RuleFor(x => x.PermissionIds).NotEmpty().ForEach(x => x.NotEmpty().NotNull());
        }

        private bool Test(Guid arg)
        {
            // kiem tra Id databaseContext
            return true;
        }
    }
}
