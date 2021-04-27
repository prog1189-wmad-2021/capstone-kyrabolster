using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VastVoyages.Model;
using VastVoyages.Service;
using VastVoyages.Types;
using System.Threading.Tasks;

namespace VastVoyages.Web.Controllers
{
    public class LoginController : Controller
    {
        private LoginService service = new LoginService();

        // GET: Login
        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginDTO model, string returnUrl)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if(service.AttemptLogin(model))
                    return RedirectToAction("Index", "Home");
               
                return View(model);

            }
            catch (Exception ex)
            {
                //model.AddError(new ValidationError(ex.Message, ErrorType.Model));
                ViewBag.Msg = ex.Message;
                return View("Index", model);
                //return View("Error", new HandleErrorInfo(ex, "Login", "index"));
            }

        }

    }
}