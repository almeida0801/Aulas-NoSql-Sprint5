using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nyous.Api.NoSql.Contexts;
using Nyous.Api.NoSql.Interfaces.Repositories;
using Nyous.Api.NoSql.Repositories;

namespace Nyous.Api.NoSql
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Pega a informação do AppSettings
            services.Configure<NyousDatabaseSettings>(
                    Configuration.GetSection("NyousDatabaseSettings")
                 );

            // Faz a injeção de dependência
            services.AddSingleton<INyousDatabaseSettings>(
               sp => sp.GetRequiredService<IOptions<NyousDatabaseSettings>>().Value
                );

            services.AddSingleton<IEventoRepository, EventoRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
