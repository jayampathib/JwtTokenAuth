using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using JwtDomain;
using Microsoft.Extensions.Configuration;
using JwtDomain.Repo;
using JwtWebApi.BLL;
using JwtWebApi.Models;

namespace JwtWebApi
{
    public class Startup
    {
        private readonly IConfiguration _IConfiguration;
        public Startup(IConfiguration iConfiguration)
        {
            _IConfiguration = iConfiguration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<JwtModuleDbContext>(opt => 
                    opt.UseSqlServer(_IConfiguration.GetConnectionString("sqlConnection")));

            services.AddTransient<IAppUserRepo, AppUserRepo>();
            services.AddTransient<ISecurityManager, SecurityManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc(opt => opt.MapRoute("Default", "{controller}/{action}/{id?}"));
     
        }
        public JwtSettings GetJwtSettings()
        {
            JwtSettings settings = new JwtSettings();

            settings.Key = _IConfiguration["JwtSettings:key"];
            settings.Audience = _IConfiguration["JwtSettings:audience"];
            settings.Issuer = _IConfiguration["JwtSettings:issuer"];
            settings.MinutesToExpiration =
             Convert.ToInt32(
                _IConfiguration["JwtSettings:minutesToExpiration"]);

            return settings;
        }
    }
}
