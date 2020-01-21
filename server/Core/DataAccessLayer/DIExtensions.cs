using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Linq;
using System.Reflection;
namespace DataAccessLayer
{
    public static class DIExtensions
    {
        public static void RegisterDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDbConnectionFactory>(x => new OrmLiteConnectionFactory(configuration.GetConnectionString("AppDatabase"), SqlServerDialect.Provider));
            var type = typeof(ICommandHandler<ICommand>);

            var list = Assembly.GetAssembly(typeof(DIExtensions)).GetTypes()
                .Where(x => x.IsClass && x.GetInterfaces().Any(t => t.Namespace + t.Name == type.Namespace + type.Name)).ToList();

            foreach(var implementClass in list)
            {
                var item = implementClass.GetInterfaces().Where(t => t.Namespace + t.Name == type.Namespace + type.Name).ToList();
                foreach (var interfaceType in item)
                {
                    services.AddScoped(interfaceType, implementClass);
                }
            }

            var validators = Assembly.GetAssembly(typeof(DIExtensions)).GetTypes()
               .Where(x => x.IsClass && x.BaseType.Name == typeof(FluentValidation.AbstractValidator<>).Name).ToList();

            foreach(var validator in validators)
            {
                services.AddScoped(validator);
            }

            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        }
    }
}
