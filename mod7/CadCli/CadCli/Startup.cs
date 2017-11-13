using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using CadCli.Data;
using Microsoft.Extensions.Configuration;
using CadCli.Infra;

namespace CadCli
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<CadCliDataContext>();
            services.Configure<Keys>(_configuration.GetSection("Keys"));
            services.AddDistributedRedisCache(o => {
                o.Configuration = _configuration.GetConnectionString("Redis");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CadCliDataContext ctx)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                Data.DbInitializer.Initialize(ctx);
            }
            else
            {
                app.UseExceptionHandler("/Error/E404");
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
