using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlsAndRoutes.Models;

namespace UrlsAndRoutes.Controllers
{
    //Erik - 5/15/2018 Mixes static and variabble route segments

    #region Depricated - 5/15/2018 Introduced Custom Constraint
    /*
    [Route("app/[controller]/actions/[action]/{id?}")]
    */
    #endregion
    [Route("app/[controller]/actions/[action]/{id:weekday?}")]
    public class CustomerController : Controller
    {
        #region Depricated - 5/15/2018 Using direct Route isn't usually very useful, only route you can get to method and replaces and prevents previous path
        /*
        [Route("myroute")]
        */
        #endregion
        #region Depricated - 5/15/2018 Use complex attribute routing
        /*
        [Route("[controller]/MyAction")]
        */
        #endregion        
        public ViewResult Index() => View("Result",
            new Result
            {
                Controller = nameof(CustomerController),
                Action = nameof(Index)
            });

        public ViewResult List(string id)
        {
            Result r = new Result
            {
                Controller = nameof(HomeController),
                Action = nameof(List),
            };
            r.Data["Id"] = id ?? "<no value>";
            r.Data["catchall"] = RouteData.Values["catchall"];
            return View("Result", r);
        }
        
        #region Depricated - 5/11/2018 Introduced custom segment and variable catchall segment
        /*
          public ViewResult List() => View("Result",
    new Result
    {
        Controller = nameof(CustomerController),
        Action = nameof(List)
    });
        */
        #endregion

    }
}
