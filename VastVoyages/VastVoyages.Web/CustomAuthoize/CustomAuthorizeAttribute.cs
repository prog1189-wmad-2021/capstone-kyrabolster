using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VastVoyages.Web.CustomAuthoize
{
    /// <summary>
    /// Custom authrization roles
    /// </summary>
    public class RoleName
    {
        public const string CEO = "CEO";
        public const string HRSupervisor = "HR Supervisor";
        public const string Supervisor = "Supervisor";
        public const string HREmployee = "HR Employee";
        public const string Employee = "Employee";
    }

    /// <summary>
    /// Custom autorization. If there are more than one role, generate generic list and compare it
    /// </summary>
    public class CustomizeAuthorize : AuthorizeAttribute, IAuthorizationFilter
    {
        public string CurrentRole { get; set; }
        public CustomizeAuthorize(string role) : base()
        {
            CurrentRole = role;
        }
        public CustomizeAuthorize(params string[] roles) : base()
        {
            CurrentRole = string.Join(",", roles);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            List<string> roles = CurrentRole.Split(',').ToList();
            string match = roles.FirstOrDefault(r => r == ((String)httpContext.Session["role"]));

            if(match != null)
            {
                return true;
            }

            return base.AuthorizeCore(httpContext);
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                                    new RouteValueDictionary
                                    {
                                        {"action","Error" },
                                        {"controller","Login" }
                                    });
        }
    }
}