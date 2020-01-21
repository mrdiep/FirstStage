
using FluentValidation.Results;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
namespace Infrastructure
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<CommandExecutionResult> DispatchAsync(ICommand command)
        {
            //log command content here
            try
            {

                var typeName = $"{typeof(ICommandHandler<>).FullName}[{command.GetType().FullName}]";
                var handler = serviceProvider.GetService(Type.GetType(typeName));

                var methodHandlerAsync = handler.GetType().GetMethod("HandlerAsync", new Type[] { command.GetType() });
                var hasPermission = ProceedPermisisonAttribute(methodHandlerAsync);
                if (!hasPermission)
                {
                    return new FailCommandExecutionResult();
                }

                var validator = ProceedValidatorAttribute(command, methodHandlerAsync);
                if (!validator)
                {
                    return new FailCommandExecutionResult();
                }

                // exec HandlerAsync Method 
                await (Task)methodHandlerAsync.Invoke(handler, new[] { command });

                SendMail(command, methodHandlerAsync);

                return new SuccessCommandExecutionResult();
            }
            catch (Exception ex)
            {
                return new FailCommandExecutionResult();
            }
        }

        private void SendMail(ICommand command, MethodInfo methodHandlerAsync)
        {
            var customeAttributes = methodHandlerAsync.GetCustomAttributes(false)
                                .Where(x => x is SendMailAfterExectutedAttribute)
                                .Select(x => (SendMailAfterExectutedAttribute)x);
            if (!customeAttributes.Any())
            {
                foreach (var mail in customeAttributes)
                {
                    var template = mail.TemplateEmail;

                    var classEmail = (IEmail)serviceProvider.GetService(mail.TemplateEmail);
                    classEmail.SetCommand(command);
                }
            }
        }

        private bool ProceedValidatorAttribute(ICommand command, System.Reflection.MethodInfo methodHandlerAsync)
        {
            var customAttributes = methodHandlerAsync.GetCustomAttributes(false)
                                .Where(x => x is EnableValidatorAttribute)
                                .Select(x => (EnableValidatorAttribute)x);

            if (!customAttributes.Any())
            {
                foreach (var validator in customAttributes)
                {
                    object fluentValidation = serviceProvider.GetService(validator.Type);

                    ValidationResult validationResult = (ValidationResult)fluentValidation
                        .GetType()
                        .GetMethod("Validate", new Type[] { command.GetType() })
                        .Invoke(fluentValidation, new[] { command });

                    if (!validationResult.IsValid)
                    {
                        // log_error_here
                        return false;
                    }
                }
            }

            return true;
        }

        private bool ProceedPermisisonAttribute(System.Reflection.MethodInfo methodHandlerAsync)
        {
            var customAttributes = methodHandlerAsync.GetCustomAttributes(false)
                                .Where(x => x is HasPermissionAttribute)
                                .Select(x => (HasPermissionAttribute)x);
            if (!customAttributes.Any())
            {
                return true;
            }

            foreach (var hasPermisison in customAttributes)
            {
                var permissionName = hasPermisison.PermissionName;

                var signInUser = (SignInUser)serviceProvider.GetService(typeof(SignInUser));
                if (!signInUser.Permissions.Any())
                {
                    return false;
                }

                if (signInUser.Permissions.Contains(permissionName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
