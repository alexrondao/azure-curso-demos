using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using CadCli.Data;
using CadCli.Infra;
using Microsoft.Extensions.Configuration;

namespace CadCli
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //services.AddTransient --> por chamada
            //services.AddSingleton --> por servidor
            services.AddScoped<CadCliDataContext>();  // por usuário
            services.Configure<Keys>(_configuration.GetSection("Keys"));

            services.AddApplicationInsightsTelemetry("c8476ac1-88de-45d2-90bc-ba903614e150");

            //services.AddDistributedRedisCache(options=> {
            //    options.Configuration = _configuration.GetConnectionString("Redis");
            //});
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CadCliDataContext ctx)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                Data.DbInitializer.Initialize(ctx);
            }
            else {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseFileServer();
            app.UseMvcWithDefaultRoute();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Recurso nao encontrado");
            });
        }
    }
}
