using System;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Donations.Common.Interfaces;
using Donations.Common.Mapper;
using Donations.Common.Services;
using Donations.Data;
using Donations.Data.Interfaces;
using Donations.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Donations.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string CorsPolicy = "AllowAll";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDontainsDbContext(services);

            ConfigureMicrosoftIdentity(services);

            ConfigureAuthentication(services);

            ConfigureCors(services);

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            ConfigureDI(services);

            ConfigureMapper(services);

            ConfigureSwagger(services);
        }

        private void ConfigureDontainsDbContext(IServiceCollection services)
        {
            services.AddDbContext<DonationsDbContext>(options
                => options.UseSqlServer(Configuration["DonationsConnectionString"]));
        }

        private void ConfigureMicrosoftIdentity(IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<DonationsDbContext>()
            .AddDefaultTokenProviders();
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JwtKey"].ToString());

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

                        return Task.CompletedTask;
                    }
                };
            });
        }

        private void ConfigureDI(IServiceCollection services)
        {
            services.AddTransient<IDonationsRepository, DonationsRepository>();

            services.AddTransient<IAuthenticationService, AuthenticationService>();
        }

        private void ConfigureMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperProfile));
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Donations API", Version = "v1" });
            });
        }

        private void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ConfigureSwagger(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            ConfigureCors(app);

            ConfigureAuthentication(app);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Donations API v1");
                options.RoutePrefix = String.Empty;
            });
        }

        private void ConfigureCors(IApplicationBuilder app)
        {
            app.UseCors();
        }

        private void ConfigureAuthentication(IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}
