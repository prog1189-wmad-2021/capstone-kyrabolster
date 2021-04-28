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
        public ActionResult Login(Login model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (service.AttemptLogin(model))
                {
                    EmployeeDTO emp = service.GetEmpInfo(model.EmployeeId);
                    Session["employeeId"] = emp.EmployeeId;

                    if (emp.MiddleInit != null)
                    {
                        Session["employeeName"] = emp.FirstName + ' ' + emp.MiddleInit + ' ' + emp.LastName;
                    }
                    else
                    {
                        Session["employeeName"] = emp.FirstName + ' ' + emp.LastName;
                    }

                    Session["department"] = emp.Department;
                    Session["job"] = emp.Job;
                    Session["supervisor"] = emp.Supervisor;
                    Session["role"] = emp.Role;

                    return RedirectToAction("Index", "Home");
                }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Error()
        {
            return View("PermissionError");
        }

    }
}