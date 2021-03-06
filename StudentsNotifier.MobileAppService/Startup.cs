﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

using StudentsNotifier.MobileAppService.Models;
using StudentsNotifier.MobileAppService.Configuration;
using StudentsNotifier.MobileAppService.NotificationHubs;

namespace StudentsNotifier.MobileAppService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<NotificationHubConfiguration>(Configuration.GetSection("NotificationHub"));
            services.AddMvc();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IRozvrhovaAkceRepository, RozvrhoveAkceRepository>();
            services.AddSingleton<IMessageRepository, MessageRepository>();
            services.AddSingleton<ILectionRating, LectionRatingRepository>();
            services.AddSingleton<INotificationHub, NotificationHubProxy>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Student Notificator API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student Notificator API V1");
            });

            app.Run(async (context) => await Task.Run(() => context.Response.Redirect("/swagger")));
        }
    }
}
