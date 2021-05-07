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
    public class ItemController : Controller
    {
        PurchaseOrderService POservice = new PurchaseOrderService();
        ItemService itemService = new ItemService();
        POLookUpsService lookupsService = new POLookUpsService();

        /// <summary>
        /// Update item page
        /// </summary>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor, RoleName.HREmployee, RoleName.Employee)]
        public ActionResult ItemEdit(int? itemId)
        {
            try
            {
                if (itemId == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                ItemDTO itemDTO = itemService.GetItemByItemId(itemId.Value, Convert.ToInt32(Session["employeeId"]), null);
               
                if (itemDTO != null)
                {
                    PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(Convert.ToInt32(itemDTO.PONumber), Convert.ToInt32(Session["employeeId"]), null, false);

                    if (itemDTO.ItemStatus == "Pending" && itemDTO != null && purchaseOrderDTO.POStatus == "Pending")
                    {
                        Item item = new Item
                        {
                            ItemId = itemDTO.ItemId,
                            ItemName = itemDTO.ItemName,
                            ItemDescription = itemDTO.ItemDescription,
                            Justification = itemDTO.Justification,
                            Location = itemDTO.Location,
                            Price = itemDTO.Price,
                            Quantity = itemDTO.Quantity,
                            PONumber = Convert.ToInt32(itemDTO.PONumber),
                            RecordVersion = itemDTO.RecordVersion,
                            PORecordVersion = purchaseOrderDTO.RecordVersion
                        };

                        return View(item);
                    }
                    else
                    {
                        TempData["Error"] = "The item can not be modified. The purchase order has been processing.";
                        return RedirectToAction("Edit", "PurchaseOrder", new { PONumber = itemDTO.PONumber });
                    }
                }
                else
                {
                    ViewBag.errMsg = "You don't have permission to view the item.";
                    return View("Error");
                }

            }
            catch (Exception ex)
            {
                ViewBag.errMsg = ex.Message;
                return View("Error");
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
                itemService.UpdateItem(item, false, chkNoNeed != null ? true : false);
                                
                if (item.Errors.Count == 0)
                {
                    PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(item.PONumber, Convert.ToInt32(Session["employeeId"]), null, false);
                    PurchaseOrder PO = GeneratePurchaseOrderObject(purchaseOrderDTO);

                    if (PO.items.Where(i => i.ItemStatusId != 1).ToList().Count == PO.items.Count)
                    {
                        POservice.UpdatePurcaseOrder(PO);
                    }

                    TempData["PO"] = PO;
                    TempData["Edit"] = $"Item Id: {item.ItemId} has been updated successful!";
                    
                    if(PO.POstatusId != 2 && PO.POstatusId != 3)
                    {
                        return RedirectToAction("Edit", "PurchaseOrder", new { PONumber = Convert.ToInt32(PO.PONumber) });
                    }
                    else
                    {
                        return RedirectToAction("Index", "PurchaseOrder");
                    }
                }

                return View(item);

            }
            catch (Exception ex)
            {
                ViewBag.errMsg = ex.Message;
                return View("Error");
            }
        }


        /// <summary>
        /// Process item page
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor)]
        public ActionResult ItemProcess(int? itemId)
        {
            try
            {
                if (itemId == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                ItemDTO itemDTO = itemService.GetItemByItemId(itemId.Value, null, Convert.ToInt32(Session["employeeId"]));

                Item item = new Item();

                if (itemDTO != null)
                {
                    if(itemDTO.POStatusId != 3)
                    {
                        PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(Convert.ToInt32(itemDTO.PONumber), null, Convert.ToInt32(Session["employeeId"]), true);

                        if(purchaseOrderDTO != null)
                        {
                            item.ItemId = itemDTO.ItemId;
                            item.ItemName = itemDTO.ItemName;
                            item.ItemDescription = itemDTO.ItemDescription;
                            item.Justification = itemDTO.Justification;
                            item.Location = itemDTO.Location;
                            item.Price = itemDTO.Price;
                            item.Quantity = itemDTO.Quantity;
                            item.PONumber = Convert.ToInt32(itemDTO.PONumber);
                            item.ItemStatusId = itemDTO.ItemStatusId;
                            item.RecordVersion = itemDTO.RecordVersion;
                            item.DecisionReason = itemDTO.DecisionReason;
                            item.PORecordVersion = purchaseOrderDTO.RecordVersion;

                            var ItemStatusList = lookupsService.GetItemStatus();

                            ViewBag.ItemStatusList = new SelectList(ItemStatusList, "ItemStatusId", "ItemStatus");
                        }
                        else
                        {
                            ViewBag.errMsg = "You don't have permission to view the item.";
                            return View("Error");
                        }

                    }
                    else
                    {
                        TempData["Error"] = "The Purchase order is already closed. The item can not be modified.";
                        return RedirectToAction("ProcessList", "PurchaseOrder");
                    }                    
                }
                else
                {
                    ViewBag.errMsg = "You don't have permission to process the item.";
                    return View("Error");
                }
                       
                return View(item);
            }
            catch (Exception ex)
            {
                ViewBag.errMsg = ex.Message;
                return View("Error");
            }
        }

        /// <summary>
        /// Process item in purchase order
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor)]
        [HttpPost]
        public ActionResult ItemProcess(Item item)
        {
            try
            {
                PurchaseOrderDTO purchaseOrderDTO = POservice.GetPurchaseOrderByPONumber(Convert.ToInt32(item.PONumber), null, Convert.ToInt32(Session["employeeId"]), true);

                //item.PORecordVersion = purchaseOrderDTO.RecordVersion;
                item = itemService.ApproveOrDenyItem(item, Convert.ToInt32(Session["employeeId"]));

                if (item.Errors.Count == 0)
                {
                    PurchaseOrder purchaseOrder = GeneratePurchaseOrderObject(purchaseOrderDTO);

                    TempData["Edit"] = $"Item Id: {item.ItemId} has been updated successful!";

                    if (purchaseOrder.items.Where(i => i.ItemStatusId != 1).ToList().Count == purchaseOrder.items.Count())
                    {
                        TempData["Close"] = "Close";
                        purchaseOrder.POstatusId = 2;
                    }

                    POservice.UpdatePurcaseOrder(purchaseOrder);
                    return RedirectToAction("POProcess", "PurchaseOrder", new { PONumber = item.PONumber });
                }

                var ItemStatusList = lookupsService.GetItemStatus();
                ViewBag.ItemStatusList = new SelectList(ItemStatusList, "ItemStatusId", "ItemStatus");

                return View(item);
            }
            catch (Exception ex)
            {
                ViewBag.errMsg = ex.Message;
                return View("Error");
            }
        }

        #region Private Method

        /// <summary>
        /// Generate purchase order object for updating PO
        /// </summary>
        /// <param name="purchaseOrderDTO"></param>
        /// <returns></returns>
        private PurchaseOrder GeneratePurchaseOrderObject(PurchaseOrderDTO purchaseOrderDTO)
        {
            List<ItemDTO> items = itemService.GetItemListByPO(Convert.ToInt32(purchaseOrderDTO.PONumber), true);
            PurchaseOrder purchaseOrder = new PurchaseOrder();

            purchaseOrder.PONumber = purchaseOrderDTO.PONumber;
            purchaseOrder.SubmissionDate = purchaseOrderDTO.SubmissionDate;
            purchaseOrder.RecordVersion = purchaseOrderDTO.RecordVersion;
            purchaseOrder.employeeId = purchaseOrderDTO.EmployeeId;
            purchaseOrder.items = new List<Item>();

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
                    DecisionReason = i.DecisionReason,
                    ItemStatusId = i.ItemStatusId,
                    RecordVersion = i.RecordVersion
                });
            }

            return purchaseOrder;
        }

        #endregion
    }
}