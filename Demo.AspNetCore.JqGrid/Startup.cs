using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Lib.AspNetCore.Mvc.JqGrid.Core.Request;
using Lib.AspNetCore.Mvc.JqGrid.Infrastructure.Options;

namespace Demo.AspNetCore.JqGrid
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
                // .AddNewtonsoftJson();

            services.AddJqGrid();
                // .AddNewtonsoftJqGrid();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            JqGridRequest.ParametersNames = new JqGridParametersNames() { PagesCount = "npage" };

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=JavaScript}/{action=Basics}");
            });
        }
    }
}
