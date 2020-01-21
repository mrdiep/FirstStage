using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using AppModule;
using DataAccessLayer.DataQuery;
using DataAccessLayer.Models;
using Infrastructure;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Swagger;
using Infrastructure.Commands.AppRoles;
using DataAccessLayer;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterDataAccessLayer(Configuration);
            services.AddScoped<SignInUser>();
            services.AddMvc(x => {
                x.EnableEndpointRouting = false;
                foreach (var formatter in x.OutputFormatters
                    .OfType<ODataOutputFormatter>()
                    .Where(it => !it.SupportedMediaTypes.Any()))
                {
                    formatter.SupportedMediaTypes.Add(
                        new MediaTypeHeaderValue("application/prs.mock-odata"));
                }
                foreach (var formatter in x.InputFormatters
                    .OfType<ODataInputFormatter>()
                    .Where(it => !it.SupportedMediaTypes.Any()))
                {
                    formatter.SupportedMediaTypes.Add(
                        new MediaTypeHeaderValue("application/prs.mock-odata"));
                }
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();
            services.AddScoped<UserPersistence>();
            services.AddDbContext<DatabaseContext>(x => x.UseSqlServer(Configuration["connectionString"]));
            services.AddOData();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:SecurityKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async ctx =>
                    {
                        var userPersistence = ctx.HttpContext.RequestServices.GetService<UserPersistence>();
                        var signInUser = ctx.HttpContext.RequestServices.GetService<SignInUser>();

                        ClaimsPrincipal userPrincipal = ctx.Principal;
                        var userName = userPrincipal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                        var userId = userPrincipal.Claims.FirstOrDefault(c => c.Type == "UserId").Value;
                        
                        var permissions = userPersistence.GetPermissions(userName);

                        var appIdentity = new ClaimsIdentity(permissions.Select(t => new Claim(ClaimTypes.Role, t)));

                        signInUser.UserId = Guid.Parse(userId);
                        signInUser.Permissions = permissions;
                        signInUser.Username = userName;

                        ctx.Principal.AddIdentity(appIdentity);
                    }
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "App API",
                    Description = "Web API",
                    TermsOfService = "None"
                });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityRequirement(security);
                // Set the comments path for the Swagger JSON and UI.

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger, ICommandDispatcher commandDispatcher)
        {
            logger.LogInformation("start");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc(b =>
            {
                b.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                b.MapODataServiceRoute("odata", "odata", DataQueryConfigurationExtensions.GetEdmModel());
            });
        }
    }
}
