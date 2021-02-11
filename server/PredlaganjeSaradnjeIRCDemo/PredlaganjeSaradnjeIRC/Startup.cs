using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PredlaganjeSaradnjeIRC.Data;
using PredlaganjeSaradnjeIRC.Data.Service;
using PredlaganjeSaradnjeIRC.Services;

namespace PredlaganjeSaradnjeIRC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(option =>
            {
                option.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddSingleton(Configuration);
            services.AddScoped<ICompany, CompanyService>();
            services.AddScoped<IContact, ContactService>();
            services.AddScoped<ILocation, LocationService>();
            services.AddScoped<IRequestForCooperation, RequestForCooperationService>();
            services.AddScoped<ICity, CityService>();
            services.AddScoped<IEmployee, EmployeeService>();
            //  services.AddAuthentication(OAuthValidationDefaults.AuthenticationScheme)
            //    .AddOAuthValidation();

            services.AddDbContext<ApplicationContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}