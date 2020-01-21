using System;
using System.Linq;
using DataAccessLayer.Models;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure.Commands.AppRoles;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.Services.Validators
{
    public class AddNewRoleCommandValidator : AbstractValidator<AddNewRoleCommand>
    {
        private readonly DatabaseContext databaseContext;

        public AddNewRoleCommandValidator(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;

            RuleFor(x => x.RoleName).NotNull().NotEmpty().MinimumLength(3).Must(CheckRoleName);

            RuleFor(x => x.DisplayName).NotNull().NotEmpty().MinimumLength(3);
        }

        private bool CheckRoleName(string roleName)
        {
            return !databaseContext.AppRoles.Any(x => x.RoleName == roleName);
        }
    }
}
