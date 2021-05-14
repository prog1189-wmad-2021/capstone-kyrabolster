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
                POStatusLookUpsDTO poStatus = new POStatusLookUpsDTO { POStatusId = 0, POStatus = "All POs" };
                POStatusList.Insert(0, poStatus);
                POStatusList.RemoveAt(2);
                ViewBag.POStatusList = new SelectList(POStatusList, "POStatusId", "POStatus");

                if (Start != null)
                {
                    ViewBag.startDate = Start.Value.Date.ToShortDateString();
                }

                ViewBag.endDate = End == null ? DateTime.Now.ToShortDateString() : End.Value.Date.ToShortDateString();
                ViewBag.poNumber = PONumber;

                POStatusId = POStatusId == null ? 1 : POStatusId == 0 ? null : POStatusId;

                var searchValidation = ValidationSearch(PONumber, null, POStatusId, Start, End);
                ViewBag.searchError = searchValidation.Item6;

                return View(POservice.GetPurchaseOrderList(Convert.ToInt32(Session["employeeId"].ToString()), searchValidation.Item1, searchValidation.Item4, searchValidation.Item5, searchValidation.Item3));
            }
            catch (Exception ex)
            {
                ViewBag.errMsg = ex.Message;
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
                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(PONumber.Value, Convert.ToInt32(Session["employeeId"]), null, false);
                if (purchaseOrderDTO != null)
                {
                    List<ItemDTO> items = itemService.GetItemListByPO(PONumber.Value, false);
                    purchaseOrderDTO.items = items;
                    return View(purchaseOrderDTO);
                }
                else
                {
                    ViewBag.errMsg = "You don't have permission to view purchase order";
                    return RedirectToAction("Error", "Login", new { returnUrl = Request.Url} );
                }
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

                //When first item added, create purchase order with the item
                if (PO.PONumber == null)
                {
                    PO = POservice.AddPurchaseOrder(PO, item);
                }
                //After first item added, add item in the purchase order
                else
                {
                    item.PONumber = Convert.ToInt32(PO.PONumber);
                    item.PORecordVersion = PO.RecordVersion;
                    itemService.AddItem(item, PO);
                }

                //if inserted item is valid, load create page with the purchase order
                if (item.Errors.Count == 0)
                {
                    //Set new record version after item added
                    PO.RecordVersion = POservice.GetPurchaseOrderByPONumber(Convert.ToInt32(PO.PONumber), Convert.ToInt32(Session["employeeId"]), null, false).RecordVersion;
                    TempData["PO"] = PO;
                    return RedirectToAction("Create");
                }

                if(PO.PONumber != null)
                {
                    PO.items = GeneratePOItemList(PO, false).items;

                    ViewBag.subTotal = (PO.items.Sum(i => i.Price * i.Quantity)).ToString("C");
                    ViewBag.Tax = (PO.items.Sum(i => i.Price * i.Quantity) * 0.15m).ToString("C");
                    ViewBag.Total = (PO.items.Sum(i => i.Price * i.Quantity) * 1.15m).ToString("C");
                }                              

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
                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(PONumber, null, null, false);
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
                    TempData["PO"] = null;
                    TempData["Create"] = "Purchase order has been submitted successful!";
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

                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(PONumber.Value, Convert.ToInt32(Session["employeeId"]), null, false);

                if (purchaseOrderDTO != null)
                {
                    if (purchaseOrderDTO.POStatus == "Pending" && purchaseOrderDTO != null)
                    {
                        PurchaseOrder purchaseOrder = GeneratePurchaseOrderObject(PONumber.Value, purchaseOrderDTO);

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
                else
                {
                    ViewBag.errMsg = "You don't have permission to view purchase order";
                    return RedirectToAction("Error", "Login", new { returnUrl = Request.Url });
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
                item.PONumber = Convert.ToInt32(PO.PONumber);
                item.PORecordVersion = PO.RecordVersion;

                itemService.AddItem(item, PO);

                if (item.Errors.Count == 0)
                {
                    return RedirectToAction("Edit", new { PONumber = Convert.ToInt32(PO.PONumber) });
                }

                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(Convert.ToInt32(PO.PONumber), null, null, false);

                PO = GeneratePurchaseOrderObject(item.PONumber, purchaseOrderDTO);

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
        /// process purchase order list page for supervisor
        /// </summary>
        /// <param name="EmpName"></param>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <param name="POStatusId"></param>
        /// <returns></returns>
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor)]
        public ActionResult ProcessList(string EmpName, DateTime? Start, DateTime? End, int? POStatusId, string PONumber)
        {
            try
            {
                var POStatusList = lookupsService.GetPOStatus();
                POStatusLookUpsDTO poStatus = new POStatusLookUpsDTO { POStatusId = 0, POStatus = "All POs" };
                POStatusList.Insert(0, poStatus);
                POStatusList.RemoveAt(2);

                ViewBag.POStatusList = new SelectList(POStatusList, "POStatusId", "POStatus");

                if (Start != null)
                {
                    ViewBag.startDate = Start.Value.Date.ToShortDateString();
                }

                ViewBag.endDate = End == null ? DateTime.Now.ToShortDateString() : End.Value.Date.ToShortDateString();
                ViewBag.empName = EmpName;
                ViewBag.PONumber = PONumber;

                var searchValidation = ValidationSearch(PONumber, EmpName, POStatusId, Start, End);

                ViewBag.searchError = searchValidation.Item6;

                POStatusId = POStatusId == null ? 1 : POStatusId == 0 ? null : POStatusId;

                return View(POservice.GetPurchaseOrderListBySupervisor(Convert.ToInt32(Session["employeeId"].ToString()), POStatusId, searchValidation.Item1, searchValidation.Item2, searchValidation.Item4, searchValidation.Item5));
            }
            catch (Exception ex)
            {
                ViewBag.errMsg = ex.Message;
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

                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(PONumber.Value, null, Convert.ToInt32(Session["employeeId"]), false);
                if (purchaseOrderDTO != null)
                {
                    List<ItemDTO> items = itemService.GetItemListByPO(PONumber.Value, true);
                    purchaseOrderDTO.items = items;
                    TempData["ProcessDetail"] = "ProcessDetail";

                    return View("Details", purchaseOrderDTO);
                }
                else
                {
                    ViewBag.errMsg = "You don't have permission to process purchase order";
                    return RedirectToAction("Error", "Login", new { returnUrl = Request.Url });
                }
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

                if(Convert.ToBoolean(Session["isHeadSupervisor"]) || Session["department"].ToString() == "CEO")
                {
                    PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(PONumber.Value, null, Convert.ToInt32(Session["employeeId"]), true);

                    if(purchaseOrderDTO != null)
                    {
                        if (purchaseOrderDTO.POStatus != "Closed" && purchaseOrderDTO != null)
                        {
                            PurchaseOrder purchaseOrder = GeneratePurchaseOrderObject(PONumber.Value, purchaseOrderDTO);

                            ViewBag.Total = (purchaseOrderDTO.SubTotal + purchaseOrderDTO.Tax).ToString("C");
                            purchaseOrderDTO.items = itemService.GetItemListByPO(Convert.ToInt32(purchaseOrderDTO.PONumber), true);
                            purchaseOrder.items = GeneratePOItemList(purchaseOrder, true).items;

                            Item item = new Item();

                            var tuple = new Tuple<PurchaseOrderDTO, Item>(purchaseOrderDTO, item);

                            var ItemStatusList = lookupsService.GetItemStatus();
                            ViewBag.ItemStatusList = new SelectList(ItemStatusList, "ItemStatusId", "ItemStatus");

                            return View(tuple);
                        }
                        else
                        {
                            TempData["Error"] = "The purchase order is already closed";
                            return RedirectToAction("ProcessList", "PurchaseOrder");
                        }
                    }
                    else
                    {
                        ViewBag.errMsg = "You don't have permission to process purchase order";
                        return RedirectToAction("Error", "Login", new { returnUrl = Request.Url });
                    }

                }
                else
                {
                    ViewBag.errMsg = "You don't have permission to process purchase order";
                    return RedirectToAction("Error", "Login", new { returnUrl = Request.Url });
                }

            }
            catch (Exception ex)
            {
                ViewBag.errMsg = ex.Message;
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }

        /// <summary>
        /// Close purchase order
        /// </summary>
        /// <param name="PONumber"></param>
        /// <returns></returns>
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClosePurchaseOrder(int? PONumber)
        {
            try
            {
                if (PONumber == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(PONumber.Value, null, Convert.ToInt32(Session["employeeId"]), true);
                
                if(purchaseOrderDTO != null)
                {
                    PurchaseOrder purchaseOrder = GeneratePurchaseOrderObject(PONumber.Value, purchaseOrderDTO);
                    purchaseOrder.POstatusId = 3;

                    POservice.UpdatePurcaseOrder(purchaseOrder);

                    if (purchaseOrder.Errors.Count == 0)
                    {
                        EmployeeService employeeService = new EmployeeService();
                        EmployeeDTO emp = employeeService.SearchEmployeesById(purchaseOrder.employeeId).FirstOrDefault();

                        SendEmail(emp, purchaseOrder);
                        TempData["EmailSent"] = "Notification Email has been sent successful!";
                        return RedirectToAction("ProcessList", "PurchaseOrder");
                    }
                    else
                    {
                        string errorMsg = "";

                        foreach (ValidationError error in purchaseOrder.Errors)
                        {
                            errorMsg += error.Description + Environment.NewLine;
                        }

                        TempData["Error"] = errorMsg;
                    }
                }
                return RedirectToAction("POProcess", "PurchaseOrder", new { PONumber = PONumber.Value });
            }
            catch (Exception ex)
            {
                ViewBag.errMsg = ex.Message;
                return View("Error", new HandleErrorInfo(ex, "PurchaseOrder", "Index"));
            }
        }

        #region Helper methods

        /// <summary>
        /// Generate purchase order object using by purchase order number
        /// </summary>
        /// <param name="PONumber"></param>
        /// <returns></returns>
        private PurchaseOrder GeneratePurchaseOrderObject(int PONumber, PurchaseOrderDTO purchaseOrderDTO)
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder();
     
            if (purchaseOrderDTO != null)
            {   
                purchaseOrder.PONumber = purchaseOrderDTO.PONumber;
                purchaseOrder.SubmissionDate = purchaseOrderDTO.SubmissionDate;
                purchaseOrder.RecordVersion = purchaseOrderDTO.RecordVersion;
                purchaseOrder.SubTotal = purchaseOrderDTO.SubTotal;
                purchaseOrder.Tax = purchaseOrderDTO.Tax;
                purchaseOrder.employeeId = purchaseOrderDTO.EmployeeId;
            }

            purchaseOrder = GeneratePOItemList(purchaseOrder, true);

            return purchaseOrder;
        }

        /// <summary>
        /// Generate item list for specific purchase order
        /// </summary>
        /// <param name="PO"></param>
        /// <returns></returns>
        private PurchaseOrder GeneratePOItemList(PurchaseOrder PO, bool isProcessing)
        {
            PO.items = new List<Item>();

            List<ItemDTO> items = itemService.GetItemListByPO(Convert.ToInt32(PO.PONumber), isProcessing);

            if(items != null)
            {
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
            }            

            return PO;
        }

        /// <summary>
        /// Send email notification
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="purchaseOrder"></param>
        private void SendEmail(EmployeeDTO emp, PurchaseOrder purchaseOrder)
        {
            EmailService emailService = new EmailService();

            PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(Convert.ToInt32(purchaseOrder.PONumber), emp.EmpId, null, false);
            purchaseOrderDTO.items = itemService.GetItemListByPO(Convert.ToInt32(purchaseOrderDTO.PONumber), true);

            Email email = new Email();

            email.mailTo = emp.Email;
            email.mailFrom = "Admin@VastVoyages.ca";
            email.subject = "Purchase order final decision notification";
            email.body = $"<h2>Your purchase order is closed.</h2>" +
                         $"<p>Purchase Order Number: {purchaseOrderDTO.PONumber}</p>" +
                         $"<p>Submission Date: {purchaseOrderDTO.SubmissionDate}</p>" +
                         $"<p>Total cost: {purchaseOrderDTO.Total.ToString("C")}</p>" +
                         "<hr><table style='border:1px solid #dddddd; border-collapse:collapse; width: 60%;'><tr><th style='border:1px solid #dddddd; text-align:left;'>Item Name</th><th style='text-align:left;'>Item Status</th></tr>";

            foreach (ItemDTO item in purchaseOrderDTO.items)
            {
                email.body += $"<tr><td style='border:1px solid #dddddd;'> {item.ItemName}</td>" +
                              $"<td style='border:1px solid #dddddd;'>{item.ItemStatus}</td></tr>";
            }

            email.body += $"</table>";
            email.body += $"<br><a href='https://localhost:44370/PurchaseOrder/Details?PONumber={purchaseOrder.PONumber}'>View Purchase Order</a>";

            emailService.SendNotificationEmail(email);
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