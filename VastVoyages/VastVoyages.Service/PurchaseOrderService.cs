using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VastVoyages.Repository;
using VastVoyages.Model;
using VastVoyages.Types;
using System.ComponentModel.DataAnnotations;

namespace VastVoyages.Service
{
    public class PurchaseOrderService
    {
        #region Fields and Constructors

        private PurchaseOrderRepo repo;

        #endregion

        #region Public Methods

        public PurchaseOrderService()
        {
            repo = new PurchaseOrderRepo();
        }

        /// <summary>
        /// Retrieve purchase order list by employee id. The user can see only purchase order list they created.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public List<PurchaseOrderDTO> GetPurchaseOrderList(int employeeId, int? PONumber = null, DateTime? start = null, DateTime? end = null, int? status = null)
        {
            List<PurchaseOrderDTO> purchaseOrders = repo.RetrievePurchaseOrderList(employeeId, PONumber, start, end, status);

            // if status is pending, include purchase orders that are under review
            if(status == 1)
            {
                List<PurchaseOrderDTO> underReviews = repo.RetrievePurchaseOrderList(employeeId, PONumber, start, end, 2);

                foreach(PurchaseOrderDTO po in underReviews)
                {
                    purchaseOrders.Add(po);
                }
            }

            purchaseOrders = purchaseOrders.OrderByDescending(p => p.SubmissionDate).ToList();

            return purchaseOrders;
        }

        /// <summary>
        /// Retrieve purchase order list by employee id. The user can see only purchase order list they created.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public List<PurchaseOrderDTO> GetPurchaseOrderListBySupervisor(int supervisorId, int? status = null, int? PONumber = null, string employeeName = null, DateTime? start = null, DateTime? end = null)
        {
            List<PurchaseOrderDTO> purchaseOrders = repo.RetrievePurchaseOrderListBySupervisor(supervisorId, status, employeeName, start, end, PONumber);

            // if status is pending, include purchase orders that are under review
            if (status == 1)
            {
                List<PurchaseOrderDTO> underReviews = repo.RetrievePurchaseOrderListBySupervisor(supervisorId, 2, employeeName, start, end, PONumber);

                foreach (PurchaseOrderDTO po in underReviews)
                {
                    purchaseOrders.Add(po);
                }
            }
            purchaseOrders = purchaseOrders.OrderBy(p => p.SubmissionDate).ToList();

            return purchaseOrders;
        }

        /// <summary>
        /// Retrieve purchase order by purchase order number
        /// </summary>
        /// <param name="PONumber"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public PurchaseOrderDTO GetPurchaseOrderByPONumber(int PONumber, int? employeeId, int? supervisorId, bool isProcessing)
        {
            PurchaseOrderDTO purchaseOrder = repo.RetrievePurchaseOrderByPONumber(PONumber, employeeId, supervisorId);
            if(supervisorId != null)
            {
                if(isProcessing)
                {
                    if (supervisorId == purchaseOrder.HeadSupervisorId)
                    {
                        return purchaseOrder;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    if (supervisorId == purchaseOrder.SupervisorId || supervisorId == purchaseOrder.HeadSupervisorId)
                    {
                        return purchaseOrder;
                    }
                    else
                    {
                        return null;
                    }
                }               
            }
            return purchaseOrder;
        }

        /// <summary>
        /// Insert purchase order record when first item added
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <returns></returns>
        public PurchaseOrder AddPurchaseOrder(PurchaseOrder purchaseOrder, Item item)
        {
            if (ValidateItem(item))
            {
                purchaseOrder.items = new List<Item>();
                purchaseOrder.items.Add(item);
                purchaseOrder.SubTotal = CalculateSubTotal(purchaseOrder);
                purchaseOrder.Tax = CalculateTax(purchaseOrder);

                if (Validate(purchaseOrder))
                    return repo.Insert(purchaseOrder);
            }

            return purchaseOrder;

        }

        /// <summary>
        /// Update purchase order
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <returns></returns>
        public PurchaseOrder UpdatePurcaseOrder(PurchaseOrder purchaseOrder)
        {
            if (Validate(purchaseOrder))
            {
                purchaseOrder.SubmissionDate = purchaseOrder.SubmissionDate == null ? DateTime.Now : purchaseOrder.SubmissionDate;
                purchaseOrder.SubTotal = CalculateSubTotal(purchaseOrder);
                purchaseOrder.Tax = CalculateTax(purchaseOrder);
    
                if(purchaseOrder.POstatusId == 0)
                    SetPOStatus(purchaseOrder);
                
                return repo.Update(purchaseOrder);
            }

            return purchaseOrder;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Business rules validation method
        /// </summary>
        /// <param name="POToValidate"></param>
        /// <returns></returns>
        private bool Validate(PurchaseOrder POToValidate)
        {
            ValidationContext context = new ValidationContext(POToValidate);
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(POToValidate, context, results, true);

            foreach (ValidationResult e in results)
            {
                POToValidate.AddError(new ValidationError(e.ErrorMessage, ErrorType.Model));
            }

            if (POToValidate.items.Count == 0)
            {
                POToValidate.AddError(new ValidationError("Purchase order must habe at least one item.", ErrorType.Business));
            }

            if (POToValidate.POstatusId == 3)
            {
                if (POToValidate.items.Where(i => i.ItemStatusId == 1).ToList().Count > 0)
                {
                    POToValidate.AddError(new ValidationError("There is a pending item in the purchase order. All items must be processed.", ErrorType.Business));
                }

                if(GetPurchaseOrderByPONumber(Convert.ToInt32(POToValidate.PONumber), null, null, false).POStatus == "Closed")
                {
                    POToValidate.AddError(new ValidationError("This purchase order is already closed.", ErrorType.Business));
                }
            }

            return POToValidate.Errors.Count == 0;
        }


        /// <summary>
        /// Validation for item in purchase order
        /// </summary>
        /// <param name="ItemoValidate"></param>
        /// <returns></returns>
        private bool ValidateItem(Item ItemoValidate)
        {
            ValidationContext context = new ValidationContext(ItemoValidate);
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(ItemoValidate, context, results, true);

            foreach (ValidationResult e in results)
            {
                ItemoValidate.AddError(new ValidationError(e.ErrorMessage, ErrorType.Model));
            }

            return ItemoValidate.Errors.Count == 0;
        }

        /// <summary>
        /// Calculate subtotal
        /// </summary>
        /// <param name="po"></param>
        /// <returns></returns>
        private decimal CalculateSubTotal(PurchaseOrder po)
        {
            decimal subTotal = po.items.Sum(i => i.Price * i.Quantity);
            return subTotal;
        }

        /// <summary>
        /// Calculate tax
        /// </summary>
        /// <param name="po"></param>
        /// <returns></returns>
        private decimal CalculateTax(PurchaseOrder po) 
        {
            decimal tax = po.Tax = po.SubTotal * 0.15m;
            return tax;
        }

        /// <summary>
        /// Set purchase order status
        /// </summary>
        /// <param name="PO"></param>
        /// <returns></returns>
        private PurchaseOrder SetPOStatus(PurchaseOrder PO)
        {
            List<Item> processedItem = PO.items.Where(i => i.ItemStatusId != 1 && i.Quantity != 0).ToList();

            PO.POstatusId = 1;

            if (processedItem.Count >= 1)
            {
                PO.POstatusId = 2;
            }

            if (PO.items.Where(i => i.ItemStatusId != 1).ToList().Count == PO.items.Count)
            {
                PO.POstatusId = 3;
            }

            return PO;
        }
        #endregion
    }
}
