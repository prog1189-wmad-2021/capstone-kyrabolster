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
    public class ItemService
    {
        #region Fields and Constructors

        private ItemRepo repo;

        #endregion

        #region Public Methods

        public ItemService()
        {
            repo = new ItemRepo();
        }

        /// <summary>
        /// Retrieve item list by purchase order number
        /// </summary>
        /// <param name="PONumber"></param>
        /// <returns></returns>
        public List<ItemDTO> GetItemListByPO(int PONumber, bool isProcessing)
        {
            List<ItemDTO> items = repo.RetrieveItemListByPONumber(PONumber);

            if (!isProcessing)
            {
                foreach (ItemDTO item in items)
                {
                    if (item.POStatusId == 2)
                    {
                        item.ItemStatusId = 0;
                        item.ItemStatus = "Under Review";
                        item.DecisionReason = "";
                    }
                }
            }
      
            return items;
        }

        /// <summary>
        /// Retrieve item by item id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public ItemDTO GetItemByItemId(int itemId, int? employeeId, int? supervisorId)
        {
            ItemDTO item = repo.RetrieveItemByItemId(itemId, employeeId, supervisorId);
            if (supervisorId != null)
            {
                if (supervisorId == item.SupervisorId || supervisorId == item.HeadSupervisorId)
                {
                    return item;
                }
                else
                {
                    return null;
                }
            }
            return item;
        }

        /// <summary>
        /// insert new item to db, Check it is duplicated item in same purchase order
        /// </summary>
        /// <param name="item"></param>
        /// <param name="PO"></param>
        /// <returns></returns>
        public Item AddItem(Item item, PurchaseOrder PO)
        {
            if(Validate(item) && IsStatusPending(item))
            {
                Item duplicatedItem = FindDuplicatedItem(item, Convert.ToInt32(PO.PONumber));

                if (duplicatedItem != null)
                {
                    duplicatedItem.Quantity += item.Quantity;
                    duplicatedItem.PORecordVersion = item.PORecordVersion;
                    return UpdateItem(duplicatedItem, true, false);
                }

                return repo.Insert(item, PO);
            }

            return item;
        }

        /// <summary>
        /// Update item in db
        /// </summary>
        /// <param name="item"></param>
        /// <param name="newItem"></param>
        /// <param name="noNeed"></param>
        /// <returns></returns>
        public Item UpdateItem(Item item, bool newItem, bool noNeed)
        {
            if (Validate(item) && IsStatusPending(item))
            {
                item.ItemStatusId = 1;
                item.DecisionReason = "";

                if(newItem)
                {
                    return repo.Update(item);
                }
                else
                {
                    if(noNeed)
                    {                        
                        item.ItemStatusId = 3;
                        item.Quantity = 0;
                        item.ItemDescription = "No longer needed";                 
                    }
                    else
                    {
                        Item duplicatedItem = FindDuplicatedItem(item, Convert.ToInt32(item.PONumber));

                        if (duplicatedItem != null)
                        {
                            duplicatedItem.Quantity += item.Quantity;
                            duplicatedItem.PORecordVersion = item.PORecordVersion;
                            if(repo.Delete(item.ItemId))
                                return repo.Update(duplicatedItem);                            
                        }
                    }                  

                    return repo.Update(item);
                }               
            }
            return item;
        }

        /// <summary>
        /// Delete item. This method is used for only duplicated item is found when item is modified
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public bool DeleteItem(int itemId)
        {
            return repo.Delete(itemId);
        }


        public Item ApproveOrDenyItem(Item item, int supervisorId)
        {
            if (Validate(item) && IsReasonNeed(item) && CheckIsHeadSupervisor(item, supervisorId))
            {
                return repo.Update(item);
            }

            return item;
        }
        
        #endregion

        #region Private Methods

        /// <summary>
        /// Check if there is duplicated item in DB
        /// </summary>
        /// <param name="item"></param>
        /// <param name="PONumber"></param>
        /// <returns></returns>
        private Item FindDuplicatedItem(Item item, int PONumber)
        {
            return repo.FindDuplicatedItem(item, PONumber);
        }

        /// <summary>
        /// validation for model
        /// </summary>
        /// <param name="ItemoValidate"></param>
        /// <returns></returns>
        private bool Validate(Item ItemToValidate)
        {
            ValidationContext context = new ValidationContext(ItemToValidate);
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(ItemToValidate, context, results, true);

            foreach (ValidationResult e in results)
            {
                ItemToValidate.AddError(new ValidationError(e.ErrorMessage, ErrorType.Model));
            }

            return ItemToValidate.Errors.Count == 0;
        }

        /// <summary>
        /// Check if reason needed for save item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool IsReasonNeed(Item item)
        {
            //if denied
            if(item.ItemStatusId == 3 && string.IsNullOrEmpty(item.DecisionReason))
            {
                item.AddError(new ValidationError("Decision reason must be provided for denying the item", ErrorType.Business));
            }

            else
            {
                ItemDTO originalItem = repo.RetrieveItemByItemId(item.ItemId, null, null);
                if(item.Quantity != originalItem.Quantity || item.Location != originalItem.Location || item.Price != originalItem.Price)
                {
                    if(string.IsNullOrEmpty(item.DecisionReason))
                    {
                        item.AddError(new ValidationError("Decision reason must be provided for modifying the item", ErrorType.Business));
                    }
                }
            }
            return item.Errors.Count == 0;
        }

        /// <summary>
        /// Check if the supervisor is head supervisor
        /// </summary>
        /// <param name="item"></param>
        /// <param name="supervisorId"></param>
        /// <returns></returns>
        private bool CheckIsHeadSupervisor(Item item, int supervisorId)
        {
            bool result = repo.CheckHeadSupervisorIdOfItem(item, supervisorId);
            if (!result)
            {
                item.AddError(new ValidationError("You don't have permission to process this item.", ErrorType.Business));
            }
            return result;
        }

        /// <summary>
        /// Check if the purchase order is processing when item updated
        /// Check if the item is processing when item updated
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool IsStatusPending(Item item)
        {
            //When a user update item and purchase order has been processing(under reveiw or closed)
            if (GetItemListByPO(item.PONumber, false).FirstOrDefault().POStatusId != 1)
            {
                item.AddError(new ValidationError("Purchase order has been proccessing. You cannot modify this purchase order.", ErrorType.Business));
                return false;
            }

            if(item.ItemId != 0) 
            {
                if (GetItemByItemId(item.ItemId, null, null).ItemStatusId != 1)
                {
                    item.AddError(new ValidationError("Item status is not pending. You cannot modify this item.", ErrorType.Business));
                    return false;
                }
            }

            return true;

        }

        #endregion
    }
}
