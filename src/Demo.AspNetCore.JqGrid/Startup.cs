using Lib.AspNetCore.Mvc.JqGrid.Core.Request;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.AspNetCore.JqGrid
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            JqGridRequest.ParameterNames = new JqGridParametersNames() { PagesCount = "npage" };

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=JavaScript}/{action=Basics}");
            });
        }
    }
}
