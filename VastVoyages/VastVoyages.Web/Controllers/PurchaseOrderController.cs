using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VastVoyages.Service;
using VastVoyages.Model;
using VastVoyages.Web.CustomAuthoize;

namespace VastVoyages.Web.Controllers
{
    [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
    public class PurchaseOrderController : Controller
    {
        PurchaseOrderService POservice = new PurchaseOrderService();
        ItemService itemService = new ItemService();

        // GET: PurchaseOrder
        public ActionResult Index()
        {
            try
            {
                return View(POservice.GetPurchaseOrderList(Convert.ToInt32(Session["employeeId"].ToString())));
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }

        // GET: Purchase order detail
        public ActionResult Details(int? PONumber)
        {
            try
            {
                if (PONumber == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                List<ItemDTO> items = itemService.GetItemList(PONumber.Value);
                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(PONumber.Value);
                purchaseOrderDTO.items = items;

                return View(purchaseOrderDTO);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }

        // GET: Create new purchase order
        public ActionResult Create()
        {
            PurchaseOrder PO = new PurchaseOrder();
            if (TempData["PO"] != null)
            {
                PO = TempData["PO"] as PurchaseOrder;
                PO.items = new List<Item>();
                List<ItemDTO> items = itemService.GetItemList(Convert.ToInt32(PO.PONumber));

                foreach (ItemDTO i in items)
                {
                    PO.items.Add(new Item
                    {
                        ItemId = i.ItemId,
                        ItemName = i.ItemName,
                        ItemDescription = i.ItemDescription,
                        Justification = i.Justification,
                        Location = i.Location,
                        Price = i.Price,
                        Quantity = i.Quantity
                    });
                }

                ViewBag.subTotal = (PO.items.Sum(i => i.Price * i.Quantity)).ToString("C");
                ViewBag.Tax = (PO.items.Sum(i => i.Price * i.Quantity) * 0.15m).ToString("C");
                ViewBag.Total = (PO.items.Sum(i => i.Price * i.Quantity) * 1.15m).ToString("C");
            }
            else
            {
                PO.employeeId = Convert.ToInt32(Session["employeeId"]);
                List<Item> items = new List<Item>();
                PO.items = items;
            }

            Item item = new Item();

            var tuple = new Tuple<PurchaseOrder, Item>(PO, item);

            return View(tuple);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Prefix = "Item1")] PurchaseOrder PO, [Bind(Prefix = "Item2")] Item item)
        {
            try
            {
                PO.employeeId = Convert.ToInt32(Session["employeeId"]);

                if (PO.PONumber == null)
                {
                    PO = POservice.InsertPurchaseOrder(PO, item);
                }
                else
                {
                    itemService.InsertItem(item, PO);
                }
                if (item.Errors.Count == 0)
                {
                    TempData["PO"] = PO;
                    return RedirectToAction("Create");
                }

                return View(new Tuple<PurchaseOrder, Item>(PO, item));
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }

        [HttpPost]
        public ActionResult Submit(int PONumber)
        {
            try
            {
                List<ItemDTO> itemsDTO = itemService.GetItemList(PONumber);
                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(PONumber);
                purchaseOrderDTO.items = itemsDTO;

                PurchaseOrder PO = new PurchaseOrder();

                PO.PONumber = purchaseOrderDTO.PONumber;
                PO.RecordVersion = purchaseOrderDTO.RecordVersion;

                if(PO.PONumber != null)
                {
                    PO.items = new List<Item>();
                    List<ItemDTO> items = itemService.GetItemList(Convert.ToInt32(PO.PONumber));

                    foreach (ItemDTO i in items)
                    {
                        PO.items.Add(new Item
                        {
                            ItemId = i.ItemId,
                            ItemName = i.ItemName,
                            ItemDescription = i.ItemDescription,
                            Justification = i.Justification,
                            Location = i.Location,
                            Price = i.Price,
                            Quantity = i.Quantity
                        });
                    }

                    if (PO.items.Count > 0)
                    {
                        POservice.UpdatePurcaseOrder(PO);

                        if (PO.Errors.Count > 0)
                        {
                            TempData["PO"] = PO;
                            return RedirectToAction("Create");
                        }
                    }
                    
                    return RedirectToAction("Index", "PurchaseOrder");
                }
                else
                {
                    return RedirectToAction("Create");
                }
               
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }
    }
}