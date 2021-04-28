using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VastVoyages.Web.CustomAuthoize;

namespace VastVoyages.Web.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //example of custom authroization. CEO and HR Supervisor can access this
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //example of custom authroization. Only employee can access this
        [CustomizeAuthorize(RoleName.Employee)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}