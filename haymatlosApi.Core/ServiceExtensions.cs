using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Text;
using haymatlosApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens; 
using Microsoft.OpenApi.Models;
using haymatlosApi.haymatlosApi.Utils;
using System.Text.Json.Serialization;
using haymatlosApi.haymatlosApi.Models;
using haymatlosApi.haymatlosApi.ElasticSearch;
using haymatlosApi.haymatlosApi.Utils.Pagination;

namespace haymatlosApi.haymatlosApi.Core.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            //using authorization with swagger ui here, it adds a box to authorize the token. 
            services.AddSwaggerGen(opt =>                                                      
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PostgresContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgresDatabase")));
        }

        public static void AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<tokenUtil>();
            services.AddScoped<PostService>();
            services.AddScoped<CommentService>();
            services.AddScoped<IndexerService>();
            services.AddSingleton<ElasticService>();
        }
        public static void AddPaginationLinkCreatorService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<UriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });
            services.AddControllers();
        }
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes("this_will_also_change_later");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public static void AddJsonSerializer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
            .AddJsonOptions(o => o.JsonSerializerOptions
               .ReferenceHandler = ReferenceHandler.Preserve);
        }
     }
}