using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Middleware.Middleware;

namespace Middleware
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.MapWhen(context => context.Request.Path == "/favicon.ico", 
                (mapapp) =>
                {
                    mapapp.Run(context =>
                    {
                        context.Response.StatusCode = 404;
                        return Task.FromResult(0);
                    });
                });

            app.UseCustomMiddleware("Super Custom");

            app.Use(async (context, next) =>
            {
                //await context.Response.WriteAsync("<p>Middleware 1!</p>");
                await next.Invoke();
            });

            app.Use(async (context, next) =>
            {
                //await context.Response.WriteAsync("<p>Middleware 2!</p>");
                await next.Invoke();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("<p>Middleware 3!</p>");
            });
        }
    }
}
