using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PredlaganjeSaradnjeIRC.Data;
using PredlaganjeSaradnjeIRC.Data.Model;
using PredlaganjeSaradnjeIRC.Data.Service;
using PredlaganjeSaradnjeIRC.Services;
using PredlaganjeSaradnjeIRCDemo.GRPCService.Services;
using PredlaganjeSaradnjeIRCDemo.GRPCService.Utility;
using System.Security.Claims;
using System.Text;


namespace PredlaganjeSaradnjeIRCDemo.GRPCService
{
    public class Startup
    {

        readonly string _allowAll = "AllowAll";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            

            services.AddCors(o => o.AddPolicy(_allowAll, 
                builder =>
                {
                    builder.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
                }));
            //"Grpc-Encoding", "Grpc-Accept-Encoding"
            /*services.AddCors(option =>
            {
                option.AddPolicy(_allowAll,
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });*/

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthorization(options =>
            {
                options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim(ClaimTypes.Role);
                });
            });
            SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(key);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            ValidateActor = false,
                            ValidateLifetime = true,
                            IssuerSigningKey = SecurityKey
                        };
                });

            services.AddSingleton(Configuration);
            services.AddScoped<ICompany, PredlaganjeSaradnjeIRC.Services.CompanyService>();
            services.AddScoped<IContact, ContactService>();
            services.AddScoped<ILocation, LocationService>();
            services.AddScoped<IRequestForCooperation, RequestForCooperationService>();
            services.AddScoped<ICity, CityService>();
            services.AddScoped<IEmployee, EmployeeService>();
            services.AddScoped<IUser, UserService>();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddLogging();

            services.AddDbContext<ApplicationContext>
              (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseRouting(); 

            app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>().EnableGrpcWeb().RequireCors(_allowAll);
                endpoints.MapGrpcService<Services.CompanyService>().EnableGrpcWeb().RequireCors(_allowAll);
                endpoints.MapGrpcService<AuthenticateService>().EnableGrpcWeb().RequireCors(_allowAll);
            });
        }
    }
}