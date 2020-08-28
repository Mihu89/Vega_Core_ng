using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vega.Persistence;
using AutoMapper;
using System;
using System.IO;
using Vega.Core;
using Vega.Core.Models;
using Vega.Controllers;

namespace Vega
{
    public class Startup
    {
        readonly string MyCustomPolicyOrigin = "_myCustomPolicyOrigin";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<PhotoSettings>(Configuration.GetSection("PhotoSettings"));

            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IPhotoService, PhotoService>();
            services.AddTransient<IPhotoStorage, FileSystemPhotoStorage>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyCustomPolicyOrigin,
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        //.WithOrigins("https://localhost:4200",
                        //   "https://localhost:44371")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
 
                    });
            });

            services.AddControllersWithViews();

            services.AddAutoMapper(typeof(Startup));
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddDbContext<VegaDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddAuthorization(options => {
                options.AddPolicy(Policies.RequireAdminRole, policy => policy.RequireClaim("https://vega.com/roles", "Admin"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "My API v1",
                    Description = "Api for Vega Project",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "StepIT",
                        Email = "sytep@gmail.com",
                        Url = new Uri("https://itstep.org")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "Open GPL3",
                        Url = new Uri("https://cts.it.md/license")
                    }
                });

                //var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            // app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseSwagger(); 

            app.UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
             //  c.RoutePrefix = string.Empty;
           });

            //var options = new JwtBearerOptions
            //{
            //    Audience = "https://api.vega.com",
            //    Authority = "https://vegaproject.auth0.com/"
            //};
            //app.UseJwtBearerAuthentication(options);

            app.UseRouting();

            app.UseCors(MyCustomPolicyOrigin);


            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });  
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
