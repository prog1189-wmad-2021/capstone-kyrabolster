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
    public class PurchaseOrderController : Controller
    {
        PurchaseOrderService POservice = new PurchaseOrderService();
        ItemService itemService = new ItemService();

        // GET: PurchaseOrder
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
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
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
        public ActionResult Details(int? PONumber)
        {
            try
            {
                if (PONumber == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                List<ItemDTO> items = itemService.GetItemList(PONumber.Value);
                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(PONumber.Value, Convert.ToInt32(Session["employeeId"]));
                purchaseOrderDTO.items = items;

                return View(purchaseOrderDTO);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }

        // GET: Create new purchase order
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
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

        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
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

        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
        [HttpPost]
        public ActionResult Submit(int PONumber)
        {
            try
            {
                List<ItemDTO> itemsDTO = itemService.GetItemList(PONumber);
                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(PONumber, null);
                purchaseOrderDTO.items = itemsDTO;

                PurchaseOrder PO = new PurchaseOrder();

                PO.PONumber = purchaseOrderDTO.PONumber;
                PO.RecordVersion = purchaseOrderDTO.RecordVersion;

                if (PO.PONumber != null)
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

        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
        public ActionResult Edit(int? PONumber)
        {
            try
            {
                if (PONumber == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(PONumber.Value, Convert.ToInt32(Session["employeeId"]));

                if (purchaseOrderDTO.POStatus == "Pending" && purchaseOrderDTO != null)
                {
                    PurchaseOrder purchaseOrder = new PurchaseOrder
                    {
                        PONumber = purchaseOrderDTO.PONumber,
                        SubmissionDate = purchaseOrderDTO.SubmissionDate,
                        RecordVersion = purchaseOrderDTO.RecordVersion,
                        SubTotal = purchaseOrderDTO.SubTotal,
                        Tax = purchaseOrderDTO.Tax,
                        items = new List<Item>()
                    };

                    ViewBag.Total = (purchaseOrder.SubTotal + purchaseOrder.Tax).ToString("C");

                    List<ItemDTO> items = itemService.GetItemList(PONumber.Value);

                    foreach (ItemDTO i in items)
                    {
                        purchaseOrder.items.Add(new Item
                        {
                            ItemId = i.ItemId,
                            ItemName = i.ItemName,
                            ItemDescription = i.ItemDescription,
                            Justification = i.Justification,
                            Location = i.Location,
                            Price = i.Price,
                            Quantity = i.Quantity,
                            ItemStatusId = i.ItemStatusId
                        });
                    }

                    Item item = new Item();

                    var tuple = new Tuple<PurchaseOrder, Item>(purchaseOrder, item);

                    return View(tuple);
                }
                else
                {
                    TempData["Error"] = "The purchase order can not be modified";
                    return RedirectToAction("Index", "PurchaseOrder");
                }

            }
            catch (Exception ex)
            {
                ViewBag.errMsg = ex.Message;
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }

        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Prefix = "Item1")] PurchaseOrder PO, [Bind(Prefix = "Item2")] Item item)
        {
            try
            {
                PO.employeeId = Convert.ToInt32(Session["employeeId"]);

                itemService.InsertItem(item, PO);

                if (item.Errors.Count == 0)
                {
                    TempData["PO"] = PO;
                    return RedirectToAction("Edit", new { PONumber = Convert.ToInt32(PO.PONumber) });
                }

                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(Convert.ToInt32(PO.PONumber), null);

                PO = new PurchaseOrder
                {
                    PONumber = purchaseOrderDTO.PONumber,
                    SubmissionDate = purchaseOrderDTO.SubmissionDate,
                    RecordVersion = purchaseOrderDTO.RecordVersion,
                    SubTotal = purchaseOrderDTO.SubTotal,
                    Tax = purchaseOrderDTO.Tax,
                    items = new List<Item>()
                };

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
                        Quantity = i.Quantity,
                        ItemStatusId = i.ItemStatusId,
                        DecisionReason = i.DecisionReason,
                        RecordVersion = i.RecordVersion
                    });
                }
                ViewBag.Total = (PO.SubTotal + PO.Tax).ToString("C");
                var tuple = new Tuple<PurchaseOrder, Item>(PO, item);

                return View(tuple);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }

        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
        public ActionResult ItemEdit(int? ItemId)
        {
            try
            {
                if (ItemId == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                ItemDTO ItemDTO = itemService.GetItemByItemId(ItemId.Value);

                if (ItemDTO.ItemStatus == "Pending" && ItemDTO != null)
                {
                    Item item = new Item
                    {
                        ItemId = ItemDTO.ItemId,
                        ItemName = ItemDTO.ItemName,
                        ItemDescription = ItemDTO.ItemDescription,
                        Justification = ItemDTO.Justification,
                        Location = ItemDTO.Location,
                        Price = ItemDTO.Price,
                        Quantity = ItemDTO.Quantity,
                        PONumber = Convert.ToInt32(ItemDTO.PONumber),
                        RecordVersion = ItemDTO.RecordVersion
                    };

                    return View(item);
                }
                else
                {
                    ViewBag.errMsg = "The item can not be modified";
                    return View("Error");
                }

            }
            catch (Exception ex)
            {
                ViewBag.errMsg = ex.Message;
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }

        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
        [HttpPost]
        public ActionResult ItemEdit(Item item)
        {
            try
            {
                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(item.PONumber, Convert.ToInt32(Session["employeeId"]));

                PurchaseOrder PO = new PurchaseOrder
                {
                    PONumber = purchaseOrderDTO.PONumber,
                    RecordVersion = purchaseOrderDTO.RecordVersion,
                };

                item.DecisionReason = "";
                item.ItemStatusId = 1;

                itemService.UpdateItem(item, false);
            
                if (item.Errors.Count == 0)
                {
                    TempData["PO"] = PO;
                    return RedirectToAction("Edit", new { PONumber = Convert.ToInt32(PO.PONumber) });
                }

                return View(item);

            }
            catch (Exception ex)
            {
                ViewBag.errMsg = ex.Message;
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }
    }
}