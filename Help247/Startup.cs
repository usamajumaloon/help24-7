﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Help247.Utilities.Configurations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Help247.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Swagger;

namespace Help247
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            //Connection String
            services.AddDbContext<AppDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            //Identity
            services.AddIdentity<Help247.Data.Entities.User, IdentityRole>()
                        .AddEntityFrameworkStores<AppDbContext>();

            //JWT
            services.AddAuthentication()
               .AddJwtBearer(cfg =>
               {
                   cfg.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidIssuer = Configuration["Tokens:Issuer"],
                       ValidAudience = Configuration["Tokens:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                   };
               });

            //AutoMapper
            var mapping = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperConfig());
            });

            IMapper mapper = mapping.CreateMapper();
            services.AddSingleton(mapper);

            //Dependency Injection
            IocConfig.InjectServices(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                { 
                    Title = "Help24/7 API", 
                    Version = "v1", 
                    Description = "Help 24/7",
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseCors(builder => builder.WithOrigins("http://localhost:4200")/*.AllowAnyOrigin()*/.AllowAnyHeader().AllowAnyMethod());
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Help24/7 API V1");
                c.RoutePrefix = string.Empty;
            });
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
        }
    }
}
