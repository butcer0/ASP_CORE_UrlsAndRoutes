using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlsAndRoutes.Infrastructure
{
    public class LegacyRoute : IRouter
    {
        private string[] urls;
        private IRouter mvcRoute;

        public LegacyRoute(IServiceProvider services, params string[] targetUrls)
        {
            this.urls = targetUrls;
            mvcRoute = services.GetRequiredService<MvcRouteHandler>();
        }

        public async Task RouteAsync(RouteContext context)
        {
            string requestedUrl = context.HttpContext.Request.Path.Value.TrimEnd('/');

            if (urls.Contains(requestedUrl, StringComparer.OrdinalIgnoreCase))
            {
                //Erik - 5/16/2018 Use MVCRouteHandler to pass context with controller, etc set
                context.RouteData.Values["controller"] = "Legacy";
                context.RouteData.Values["action"] = "GetLegacyUrl";
                context.RouteData.Values["legacyUrl"] = requestedUrl;
                await mvcRoute.RouteAsync(context);
            }
        }

        #region Depricated - 5/16/2018 Updated to Use MVC Controllers and Actions
        /*
           public LegacyRoute(params string[] targetUrls)
{
    this.urls = targetUrls;
}

public Task RouteAsync(RouteContext context)
{
    string requestedUrl = context.HttpContext.Request.Path.Value.TrimEnd('/');

    //Erik - 5/16/2018 Determine whether the Url will be handled by this custom Route
    if (urls.Contains(requestedUrl, StringComparer.OrdinalIgnoreCase))
    {
        //Erik - 5/16/2018 When Handler set, knows this route will be handled and the handler is the logic to generate response for client
        context.Handler = async ctx =>
        {
            HttpResponse response = ctx.Response;
            byte[] bytes = Encoding.ASCII.GetBytes($"URL: {requestedUrl}");
            //Erik - 5/16/2018 This just says return the client "URL: *the request URL" string as byte array (not actually routing to a page)
            await response.Body.WriteAsync(bytes, 0, bytes.Length);
        };
    }
    return Task.CompletedTask;
}

        */
        #endregion

        /// <summary>
        /// When generating outgoing Url and matching against this Custom Route
        /// This method defines how that URL will be generated if it matches
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            //Erik - 5/16/2018 To support Outgoing URLs
            if(context.Values.ContainsKey("legacyUrl"))
            {
                string url = context.Values["legacyUrl"] as string;
                if (urls.Contains(url))
                {
                    return new VirtualPathData(this, url);
                }
            }
            return null;
        }
    }
}
