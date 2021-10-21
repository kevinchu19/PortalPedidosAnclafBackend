using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using PortalPedidosAnclafBackend.Entities;
using PortalPedidosAnclafBackend.Repositories;
using PortalPedidosAnclafBackend.Repositories.Interfaces;
using PortalPedidosAnclafBackend.Repositories.Persistance;
using AutoMapper;
using PortalPedidosAnclafBackend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.HttpOverrides;
using PortalPedidosAnclafBackend.Services;
using Serilog;
using PortalPedidosAnclafBackend.Helpers.Options;
using PortalPedidosAnclafBackend.Services.Interfaces;

namespace PortalPedidosAnclafBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureServices(services);
            
            services.AddHostedService<ConsumeScopedServiceHostedService>();
            services.AddScoped<IScopedProcessingService, PostearPedidoEnSoftlandService>();

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<PasswordOptions>(Configuration.GetSection("PasswordOptions"));
            services.AddScoped<IPasswordHasher, PasswordService>();
            
            services.AddMvc(Options =>{})
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            
            services.AddSingleton<Serilog.ILogger>(options =>
            {
                var connstring = Configuration["Serilog:SerilogConnectionString"];
                var tableName = Configuration["Serilog:TableName"];

                return new LoggerConfiguration()
                            .WriteTo
                            .MySQL(
                                connectionString: connstring,
                                tableName: tableName,
                                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                            .CreateLogger();

            });




            var key = Configuration["key"];
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
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

            });


            services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()//.WithOrigins("http://localhost:4200", "http://localhost:44349")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    );
            });

            services.AddDbContext<PortalPedidosAnclaflexContext>(options =>
               options.UseMySql(Configuration.GetConnectionString("PortalPedidosAnclaflex"),
               new MySqlServerVersion(new Version(8, 0, 23)),
               mySqlOptions => mySqlOptions
                .CharSetBehavior(CharSetBehavior.NeverAppend))
                .EnableSensitiveDataLogging()
               );

            //Unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(configuration =>
            {
                configuration.CreateMap<Cliente, ClienteTypehead>()
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.RazonSocial));
                
                configuration.CreateMap<Pedido, PedidoDTO>()
                .ReverseMap();

                configuration.CreateMap<Pedidositem,PedidoItemsDTO>()
                .ReverseMap();
            }
                , typeof(Startup));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseDeveloperExceptionPage();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            //app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
