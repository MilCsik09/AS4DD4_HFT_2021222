using AS4DD4_HFT_2021222.Endpoint.Services;
using AS4DD4_HFT_2021222.Logic;
using AS4DD4_HFT_2021222.Models;
using AS4DD4_HFT_2021222.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AS4DD4_HFT_2021222.Endpoint
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

            services.AddSingleton<ComputerRepairDbContext>();
            services.AddTransient<IComputerRepairRepository<Computer>, ComputerRepository>();
            services.AddTransient<IComputerRepairLogic<Computer>, ComputerLogic>();
            services.AddTransient<IComputerRepairRepository<Brand>, BrandRepository>();
            services.AddTransient<IComputerRepairLogic<Brand>, BrandLogic>();
            services.AddTransient<IComputerRepairRepository<CPU>, CpuRepository>();
            services.AddTransient<IComputerRepairLogic<CPU>, CpuLogic>();
            services.AddTransient<IComputerRepairRepository<VGA>, VgaRepository>();
            services.AddTransient<IComputerRepairLogic<VGA>, VgaLogic>();

            services.AddSignalR();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AS4DD4_HFT_2021222.Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("http://localhost:24722"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AS4DD4_HFT_2021222.Endpoint v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub> ("hub");
            });

            
        }
    }
}
