using haymatlosApi.haymatlosApi.Core.Extensions;

namespace haymatlosApi.haymatlosApi.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwagger();
            services.AddDbContext(Configuration);
            services.AddScopedServices();
            services.AddJwtAuthentication(Configuration);
            services.AddPaginationLinkCreatorService(Configuration);
            services.AddAuthorization();
            services.AddJsonSerializer(Configuration);
            services.AddEndpointsApiExplorer();
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyMethod()
            );
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
