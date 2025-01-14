using AutoMapper;
using EFCore.AutoMapper;
using EFCore.Data.Models;
using EFCore.Models.Repository;
using EFCore.Models.Repository.Interfaces;
using EFCore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Web.Api.Authorization;
using Web.Api.Authorization.Requirements;
using Web.Api.Constants;
using Web.Api.Data.Context;

namespace Web.Api.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// For Swagger about and contact info
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerGenContactInfo(this IServiceCollection services)
        {
            return services.AddSwaggerGen(s => s.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Managment customers wit subscriptions api with jwt",
                Version = "v1",
                Description = "my first project web api for " +
                              "Managment customers wit subscriptions api with jwt",
                Contact = new OpenApiContact()
                {
                    Name = "Mosaad Ghanem",
                    Email = "mosaadghanem97@gmail.com",
                    Url = new("https://www.facebook.com/elmagekmosaad")
                },
            }));
        }
        /// <summary>
        /// For Swagger Authentication | adding Authorization button 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerGenAuthorizationButton(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter the JWT key dont forget to add **Bearer** before the token"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            Array.Empty<string>()
        }
    });
            });
        }
        /// <summary>
        /// For service registration | AddScoped,AddTransient,AddSingleton 
        /// </summary>
        /// <param name="Services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services, string? connectionStringConfigName)
        {
            Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionStringConfigName);
            });

            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped<ICustomerRepository, CustomerRepository>();
            Services.AddScoped<IComputerRepository, ComputerRepository>();
            Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            Services.AddAutoMapper(typeof(MappingProfile));
            Services.AddScoped<ITokenService, TokenService>();
            Services.AddTransient<IAuthService, AuthService>();
            Services.AddSingleton<IAuthorizationHandler, AdminAuthorizationHandler>();
            Services.AddSingleton<IAuthorizationHandler, CustomerAuthorizationHandler>();
            Services.AddTransient<IRoleService, RoleService>();
            //Services.AddTransient<DefaultSuperAdmin>();

            return Services;
        }
        /// <summary>
        /// For Add Application Identity 
        /// </summary>
        /// <param name="Services"></param>
        /// <returns></returns>
        public static IdentityBuilder AddApplicationIdentity(this IServiceCollection Services)
        {
            return Services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();
        }
        /// <summary>
        /// Adding Authentication to show error 401 otherwise error 404
        /// </summary>
        /// <param name="Services"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddApplicationJwtAuth(this IServiceCollection Services, JwtConfiguration jwt)
        {
            return Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })// Adding jwt Bearer
                .AddJwtBearer(options =>
                {
                    options.Authority = "http://localhost:5011";
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwt.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwt.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
                    };
                });
        }

        public static IServiceCollection AddApplicationAuthorization(this IServiceCollection services)
        {
            return services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.SuperAdmin, policy =>
                {
                    policy
                    .RequireAuthenticatedUser()
                    .RequireAssertion(context =>
                       context.User.IsInRole(Roles.SuperAdmin));
                });

                options.AddPolicy(Policies.Admin, policy =>
                {
                    policy
                    .RequireAuthenticatedUser()
                    .RequireAssertion(context =>
                       context.User.IsInRole(Roles.SuperAdmin) ||
                       context.User.IsInRole(Roles.Admin));

                });

                options.AddPolicy(Policies.Customer, policy =>
                {
                    policy
                    .RequireAuthenticatedUser()
                    .RequireAssertion(context =>
                       context.User.IsInRole(Roles.SuperAdmin) ||
                       context.User.IsInRole(Roles.Admin) ||
                       context.User.IsInRole(Roles.Customer));

                });


                //options.AddPolicy(Policies.Admin, policy =>
                //{
                //    policy.Requirements.Add(new AdminRequirement());

                //});
                //options.AddPolicy(Policies.Customer, policy =>
                //{
                //    policy.Requirements.Add(new CustomerRequirement());

                //});
            });
        }
        public static async Task AddApplicationDataSeedingAsync(this IHost app)
        {
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
            var loggerFactory = app.Services.GetService<ILoggerProvider>();
            var logger = loggerFactory.CreateLogger("app");
            using (var scope = scopedFactory?.CreateScope())
            {
                try
                {
                    var dbcontext = scope?.ServiceProvider.GetService<AppDbContext>();
                    dbcontext.Database.EnsureCreated();

                    var userManager = scope?.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                    var roleManager = scope?.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var roleService = scope?.ServiceProvider.GetRequiredService<IRoleService>();
                    var mapper = scope?.ServiceProvider.GetRequiredService<IMapper>();

                    await new Seeds.Seed(userManager, roleService, mapper).InitializeAsync();
                    logger.LogInformation("Finished Seeding Default Data");
                    logger.LogInformation("Application Starting");
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "An error occured while seeding data");
                }

            }


        }

    }
}
