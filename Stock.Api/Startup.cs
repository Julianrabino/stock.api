using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Stock.Api.Exceptions;
using Stock.AppService.Services;
using Stock.Repository.Contexts;
using Stock.Repository.Repositories;

namespace Stock.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<ProductService>();
            services.AddTransient<ProductTypeService>();
            services.AddTransient<ProductRepository>();
            services.AddTransient<ProductTypeRepository>();
            services.AddDbContext<ProductContext>(options =>
                options
                .UseLazyLoadingProxies()
                .UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ProductTypeContext>(options =>
                options
                .UseLazyLoadingProxies()
                .UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper();
        }

        private void OnShutdown()
        {
            MySqlConnection.ClearAllPools();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            IApplicationLifetime applicationLifetime,
            ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionMiddleware();
            }
            else
            {
                app.UseExceptionMiddleware();
                app.UseExceptionHandler();
            }

            loggerFactory.AddConsole();
            applicationLifetime.ApplicationStopping.Register(OnShutdown);                        
            app.UseMvc();
        }
    }
}
