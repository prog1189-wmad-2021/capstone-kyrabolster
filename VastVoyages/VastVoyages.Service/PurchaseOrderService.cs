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
        public List<PurchaseOrderDTO> GetPurchaseOrderList(int employeeId)
        {
            return repo.RetrievePurchaseOrderList(employeeId);
        }

        /// <summary>
        /// Insert purchase order record when first item added
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <returns></returns>
        public PurchaseOrder InsertPurchaseOrder(PurchaseOrder purchaseOrder, Item item)
        {
            if(ValidateItem(item))
            {
                purchaseOrder.items = new List<Item>();
                purchaseOrder.items.Add(item);
                purchaseOrder.SubTotal = purchaseOrder.items[0].Price * purchaseOrder.items[0].Quantity;
                purchaseOrder.Tax = purchaseOrder.SubTotal * 0.15m;

                if (Validate(purchaseOrder))
                    return repo.Insert(purchaseOrder);
            }

            return purchaseOrder;
            
        }

        public PurchaseOrder UpdatePurcaseOrder(PurchaseOrder purchaseOrder)
        {
            if (Validate(purchaseOrder))
            {
                purchaseOrder.Tax = purchaseOrder.SubTotal * 0.15m;
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

            if(POToValidate.items.Count == 0)
            {
                POToValidate.AddError(new ValidationError("Purchase order must habe at least one item.", ErrorType.Business));
            }

            return POToValidate.Errors.Count == 0;
        }

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
        #endregion
    }
}
