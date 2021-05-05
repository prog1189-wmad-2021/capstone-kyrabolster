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
        POLookUpsService lookupsService = new POLookUpsService();

        // GET: PurchaseOrder
        /// <summary>
        /// View purchase order list(for all employees)
        /// If search values are provided, filter the result
        /// </summary>
        /// <param name="PONumber"></param>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <param name="POStatusId"></param>
        /// <returns></returns>
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
        public ActionResult Index(string PONumber, DateTime? Start, DateTime? End, int? POStatusId)
        {
            try
            {
                var POStatusList = lookupsService.GetPOStatus();
                POStatusList.RemoveAt(1);
                ViewBag.POStatusList = new SelectList(POStatusList, "POStatusId", "POStatus");

                if (Start != null)
                    ViewBag.startDate = Start.Value.Date.ToShortDateString();
                ViewBag.endDate = End == null ? DateTime.Now.ToShortDateString() : End.Value.Date.ToShortDateString();
                ViewBag.poNumber = PONumber;

                var searchValidation = ValidationSearch(PONumber, null, POStatusId, Start, End);
                ViewBag.searchError = searchValidation.Item6;

                return View(POservice.GetPurchaseOrderList(Convert.ToInt32(Session["employeeId"].ToString()), searchValidation.Item1, searchValidation.Item4, searchValidation.Item5, searchValidation.Item3));
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }

        // GET: Purchase order detail
        /// <summary>
        /// View purchase order details
        /// </summary>
        /// <param name="PONumber"></param>
        /// <returns></returns>
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
        public ActionResult Details(int? PONumber)
        {
            try
            {
                if (PONumber == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                List<ItemDTO> items = itemService.GetItemListByPO(PONumber.Value, false);
                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(PONumber.Value, Convert.ToInt32(Session["employeeId"]), null);               
                purchaseOrderDTO.items = items;

                return View(purchaseOrderDTO);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }

        /// <summary>
        /// View purchase order details
        /// </summary>
        /// <param name="PONumber"></param>
        /// <returns></returns>
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor)]
        public ActionResult ProcessDetails(int? PONumber)
        {
            try
            {
                if (PONumber == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }
                List<ItemDTO> items = itemService.GetItemListByPO(PONumber.Value, true);
                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(PONumber.Value, null, Convert.ToInt32(Session["employeeId"]));
                purchaseOrderDTO.items = items;

                return View("Details", purchaseOrderDTO);
            }
            catch (Exception ex)
            {
                ViewBag.errMsg = ex.Message;
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }

        /// <summary>
        /// Create new purchase order page
        /// </summary>
        /// <returns></returns>
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
        public ActionResult Create()
        {
            PurchaseOrder PO = new PurchaseOrder();
            if (TempData["PO"] != null)
            {
                PO = TempData["PO"] as PurchaseOrder;
                PO.items = GeneratePOItemList(PO, false).items;
              
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

        /// <summary>
        /// Create purchase order when first item is added
        /// </summary>
        /// <param name="PO"></param>
        /// <param name="item"></param>
        /// <returns></returns>
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
                    PO = POservice.AddPurchaseOrder(PO, item);
                }
                else
                {
                    itemService.AddItem(item, PO);
                }
                if (item.Errors.Count == 0)
                {
                    TempData["PO"] = PO;
                    return RedirectToAction("Create");
                }

                PO.items = GeneratePOItemList(PO, false).items;

                ViewBag.subTotal = (PO.items.Sum(i => i.Price * i.Quantity)).ToString("C");
                ViewBag.Tax = (PO.items.Sum(i => i.Price * i.Quantity) * 0.15m).ToString("C");
                ViewBag.Total = (PO.items.Sum(i => i.Price * i.Quantity) * 1.15m).ToString("C");

                return View(new Tuple<PurchaseOrder, Item>(PO, item));
            }
            catch (Exception ex)
            {
                ViewBag.errMsg = ex.Message;
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }

        /// <summary>
        /// Final submit purchase order
        /// </summary>
        /// <param name="PONumber"></param>
        /// <returns></returns>
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
        [HttpPost]
        public ActionResult Submit(int PONumber)
        {
            try
            {
                List<ItemDTO> itemsDTO = itemService.GetItemListByPO(PONumber, false);
                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(PONumber, null, null);
                purchaseOrderDTO.items = itemsDTO;

                PurchaseOrder PO = new PurchaseOrder();

                PO.PONumber = purchaseOrderDTO.PONumber;
                PO.RecordVersion = purchaseOrderDTO.RecordVersion;

                if (PO.PONumber != null)
                {
                    PO.items = GeneratePOItemList(PO, false).items;

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
                ViewBag.errMsg = ex.Message;
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }

        /// <summary>
        /// Edit purchase order
        /// </summary>
        /// <param name="PONumber"></param>
        /// <returns></returns>
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
        public ActionResult Edit(int? PONumber)
        {
            try
            {
                if (PONumber == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(PONumber.Value, Convert.ToInt32(Session["employeeId"]), null);

                if (purchaseOrderDTO.POStatus == "Pending" && purchaseOrderDTO != null)
                {
                    PurchaseOrder purchaseOrder = new PurchaseOrder
                    {
                        PONumber = purchaseOrderDTO.PONumber,
                        SubmissionDate = purchaseOrderDTO.SubmissionDate,
                        RecordVersion = purchaseOrderDTO.RecordVersion,
                        SubTotal = purchaseOrderDTO.SubTotal,
                        Tax = purchaseOrderDTO.Tax
                    };

                    ViewBag.Total = (purchaseOrder.SubTotal + purchaseOrder.Tax).ToString("C");


                    purchaseOrder.items = GeneratePOItemList(purchaseOrder, false).items;

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

        /// <summary>
        /// Update purchase order with edited item
        /// </summary>
        /// <param name="PO"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Prefix = "Item1")] PurchaseOrder PO, [Bind(Prefix = "Item2")] Item item)
        {
            try
            {
                PO.employeeId = Convert.ToInt32(Session["employeeId"]);

                itemService.AddItem(item, PO);

                if (item.Errors.Count == 0)
                {
                    TempData["PO"] = PO;
                    return RedirectToAction("Edit", new { PONumber = Convert.ToInt32(PO.PONumber) });
                }

                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(Convert.ToInt32(PO.PONumber), null, null);

                PO = new PurchaseOrder
                {
                    PONumber = purchaseOrderDTO.PONumber,
                    SubmissionDate = purchaseOrderDTO.SubmissionDate,
                    RecordVersion = purchaseOrderDTO.RecordVersion,
                    SubTotal = purchaseOrderDTO.SubTotal,
                    Tax = purchaseOrderDTO.Tax,
                    items = new List<Item>()
                };

                PO.items = GeneratePOItemList(PO, false).items;

                ViewBag.Total = (PO.SubTotal + PO.Tax).ToString("C");
                var tuple = new Tuple<PurchaseOrder, Item>(PO, item);

                return View(tuple);
            }
            catch (Exception ex)
            {
                ViewBag.errMsg = ex.Message;
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }

        /// <summary>
        /// Update item page
        /// </summary>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
        public ActionResult ItemEdit(int? ItemId)
        {
            try
            {
                if (ItemId == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                ItemDTO ItemDTO = itemService.GetItemByItemId(ItemId.Value, Convert.ToInt32(Session["employeeId"]), null);

                if(ItemDTO != null)
                {
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

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="chkNoNeed"></param>
        /// <returns></returns>
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
        [HttpPost]
        public ActionResult ItemEdit(Item item, int? chkNoNeed)
        {
            try
            {
                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(item.PONumber, Convert.ToInt32(Session["employeeId"]), null);

                PurchaseOrder PO = new PurchaseOrder
                {
                    PONumber = purchaseOrderDTO.PONumber,
                    RecordVersion = purchaseOrderDTO.RecordVersion,
                };

                itemService.UpdateItem(item, false, chkNoNeed != null ? true: false);
            
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

        /// <summary>
        /// process purchase order list page for supervisor
        /// </summary>
        /// <param name="EmpName"></param>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <param name="POStatusId"></param>
        /// <returns></returns>
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor)]
        public ActionResult Process(string EmpName, DateTime? Start, DateTime? End, int? POStatusId)
        {
            try
            {
                var POStatusList = lookupsService.GetPOStatus();
                POStatusLookUpsDTO poStatus = new POStatusLookUpsDTO { POStatusId = 0, POStatus = "All POs" };
                POStatusList.Insert(0, poStatus);

                ViewBag.POStatusList = new SelectList(POStatusList, "POStatusId", "POStatus");

                if (Start != null)
                    ViewBag.startDate = Start.Value.Date.ToShortDateString();
                ViewBag.endDate = End == null ? DateTime.Now.ToShortDateString() : End.Value.Date.ToShortDateString();
                ViewBag.empName = EmpName;

                var searchValidation = ValidationSearch(null, EmpName, POStatusId, Start, End);
                ViewBag.searchError = searchValidation.Item6;
                POStatusId = POStatusId == null ? 1 : POStatusId == 0 ? null : POStatusId;

                return View(POservice.GetPurchaseOrderListBySupervisor(Convert.ToInt32(Session["employeeId"].ToString()), POStatusId, searchValidation.Item2, searchValidation.Item4, searchValidation.Item5));
            }
            catch (Exception ex)
            {
                ViewBag.errMsg = ex.Message;
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }

        /// <summary>
        /// Process purchase page
        /// </summary>
        /// <param name="PONumber"></param>
        /// <returns></returns>
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor)]
        public ActionResult POProcess(int? PONumber)
        {
            try
            {
                if (PONumber == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(PONumber.Value, null, Convert.ToInt32(Session["employeeId"]));

                if (purchaseOrderDTO.POStatus != "Closed" && purchaseOrderDTO != null)
                {
                    PurchaseOrder purchaseOrder = new PurchaseOrder
                    {
                        PONumber = purchaseOrderDTO.PONumber,
                        SubmissionDate = purchaseOrderDTO.SubmissionDate,
                        RecordVersion = purchaseOrderDTO.RecordVersion,
                        SubTotal = purchaseOrderDTO.SubTotal,
                        Tax = purchaseOrderDTO.Tax
                    };

                    ViewBag.Total = (purchaseOrder.SubTotal + purchaseOrder.Tax).ToString("C");


                    purchaseOrder.items = GeneratePOItemList(purchaseOrder, true).items;

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


        #region Helper methods

        /// <summary>
        /// Generate item list for specific purchase order
        /// </summary>
        /// <param name="PO"></param>
        /// <returns></returns>
        private PurchaseOrder GeneratePOItemList(PurchaseOrder PO, bool isProcessing)
        {
            PO.items = new List<Item>();

            List<ItemDTO> items = itemService.GetItemListByPO(Convert.ToInt32(PO.PONumber), isProcessing);

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

            return PO;
        }

        /// <summary>
        /// Validation method for search criteria. All fiels are optional. Return search values and error list
        /// </summary>
        /// <param name="PONumber"></param>
        /// <param name="empName"></param>
        /// <param name="POStatusId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private (int? PONumber, string empName, int? POStatus, DateTime? start, DateTime? end, List<string>) ValidationSearch(string PONumber = null, string empName = null, int? POStatusId = null, DateTime? start = null, DateTime? end = null)
        {
            List<string> error = new List<string>();
            int? PONumberParam = null; 

            if (!string.IsNullOrEmpty(PONumber))
            {
                if (!int.TryParse(PONumber, out int result))
                {
                    error.Add("Invalid purchase order number format. Purchase order number must be 8 digit.");
                }
                else
                {
                    PONumberParam = Convert.ToInt32(PONumber);
                }
            }

            if (start != null || end != null)
            {
                if (start > DateTime.Now || end > DateTime.Now)
                {
                    error.Add("Date can not be in the future.");
                }


                // if start and end date are provided
                if (start != null && end != null && start > end)
                {
                    error.Add("End Date cannot be prior to start date.");
                }
            }

            if(error.Count > 0)
            {
                return (null, null, null, null, null, error);
            } 
            else
            {
                return (PONumberParam, empName, POStatusId, start, end, error);
            }
        }

        #endregion

    }
}