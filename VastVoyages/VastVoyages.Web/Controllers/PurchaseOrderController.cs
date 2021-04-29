using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VastVoyages.Service;

namespace VastVoyages.Web.Controllers
{
    public class PurchaseOrderController : Controller
    {
        PurchaseOrderService service = new PurchaseOrderService();

        // GET: PurchaseOrder
        public ActionResult Index()
        {
            try
            {
                return View(service.GetPurchaseOrderList(Convert.ToInt32(Session["employeeId"].ToString())));
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }
    }
}