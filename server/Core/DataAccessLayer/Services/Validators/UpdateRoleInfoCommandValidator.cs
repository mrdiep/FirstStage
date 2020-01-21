using System.Linq;
using DataAccessLayer.Models;
using FluentValidation;
using Infrastructure.Commands.AppRoles;

namespace DataAccessLayer.Services.Validators
{
    public class UpdateRoleInfoCommandValidator : AbstractValidator<UpdateRoleInfoCommand>
    {
        private readonly DatabaseContext databaseContext;

        public UpdateRoleInfoCommandValidator(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;

            RuleFor(x => x.RoleName).NotNull().NotEmpty().MinimumLength(3).Must(CheckRoleName);
        }

        private bool CheckRoleName(string roleName)
        {
            return !databaseContext.AppRoles.Any(x => x.RoleName == roleName);
        }
    }
}
