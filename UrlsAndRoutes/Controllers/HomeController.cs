using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlsAndRoutes.Models;

namespace UrlsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() => View("Result",
            new Result
            {
                Controller = nameof(HomeController),
                Action = nameof(Index)
            });

        //Erik - 5/11/2018 'id' must match with a custom segment in the URL routing template defined in startup Configure
        // Can use any parameter type -> MVC will attempt to automatically convert custom segment into type
        public ViewResult CustomVariable(string id)
        {
            Result r = new Result
            {
                Controller = nameof(HomeController),
                Action = nameof(CustomVariable),
            };
            //Erik - 5/11/2018 If no value supplied on optional custom segment, value is passed as null
            r.Data["Id"] = id ?? "<no value>";
            return View("Result", r);
        }

        #region Depricated - 5/11/2018 Pass Custom Segment in URL Pattern as Parameter
        /*
         public ViewResult CustomVariable()
{
    Result r = new Result
    {
        Controller = nameof(HomeController),
        Action = nameof(CustomVariable),
    };
    r.Data["Id"] = RouteData.Values["id"];
    return View("Result", r);
}
        */
        #endregion
    }
}
