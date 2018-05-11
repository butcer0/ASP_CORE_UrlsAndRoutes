using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace UrlsAndRoutes
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                //Erik - 5/11/2018 Inline-constraint to only match first segment when begins with 'H'. Default applied before constraint evaluated
                routes.MapRoute(
                   name: "MyRoute",
                   template: "{controller:regex(^H.*)=Home}/{action=Index}/{id?}/{*catchall}"
                   );

                //Erik - 5/11/2018 Segment-Level constraints -> defaults required to be written as separate arguments along with constraints
                #region Depricated - 5/11/2018 Use Inline-Constraints

                //routes.MapRoute(
                //               name: "MyRoute",
                //               template: "{controller}/{action}/{id?}/{*catchall}",
                //               defaults: new { controller = "Home", action = "Index" },
                //               constraints: new
                //               {
                //                   controller = new Microsoft.AspNetCore.Routing.Constraints.RegexRouteConstraint("^H.*")
                //               });

                #endregion


                //Erik - 5/11/2018 catchall (either in <RouteData> or method parameter) will pick up anything passed 3rd segment together
                #region Depricated - 5/11/2018 Introduced Constraints
                /*
				           
                // ex: .../Customer/List/All/Delete/Perm -> id = "All", catchall = "Delete/Perm"
                routes.MapRoute(
                   name: "MyRoute",
                   template: "{controller=Home}/{action=Index}/{id?}/{*catchall}"
                   );
    
				*/
                #endregion

                //Erik - 5/11/2018 id can be picked up by controller method either in <RouteData> or as parameter named 'id'
                #region Depricated - 5/11/2018 Introduced variable length catchall
                /*
				
                routes.MapRoute(
                   name: "MyRoute",
                   template: "{controller=Home}/{action=Index}/{id?}"
                   );
				*/
                #endregion

                #region Depricated - 5/11/2018 Introduced Optional Custom URL Segment
                /*
				 routes.MapRoute(
                    name: "MyRoute",
                    template: "{controller=Home}/{action=Index}/{id=DefaultId}"
                    );
				*/
                #endregion

                #region Depricated - 5/11/2018 Introduced Custom Segment Variables
                /*
				 routes.MapRoute(
                    name: "ShopSchema2",
                    template: "Shop/OldAction",
                    defaults: new { controller = "Home", action = "Index" }
                    );

                routes.MapRoute(
                    name: "ShopSchema",
                    template: "Shop/{action}",
                    defaults: new { controller = "Home" });

                routes.MapRoute("", "X{controller}/{action}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}"
                    );

                routes.MapRoute(
                    name: "",
                    template: "Public/{controller=Home}/{action=Index}"
                    );
				*/
                #endregion

                #region Depricated - 5/11/2018 Use Inline Defaulting
                /*
				 routes.MapRoute(
                    name: "default", 
                    template: "{controller}/{action}",
                    defaults: new { action = "Index" });
				*/
                #endregion

            });
        }
    }
}
