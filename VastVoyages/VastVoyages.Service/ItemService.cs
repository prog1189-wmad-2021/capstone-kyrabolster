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

        public List<ItemDTO> GetItemList(int PONumber)
        {
            return repo.RetrieveItemListByPONumber(PONumber);
        }

        public Item InsertItem(Item item, PurchaseOrder PO)
        {
            if(Validate(item))
            {
                Item duplicatedItem = DuplicatedItemId(item, Convert.ToInt32(PO.PONumber));

                if (duplicatedItem != null)
                {
                    duplicatedItem.Quantity += item.Quantity;

                    return UpdateItem(duplicatedItem);
                }

                return repo.Insert(item, PO);
            }

            return item;
        }

        public Item UpdateItem(Item item)
        {
            if (Validate(item))
                return repo.Update(item);

            return item;
        }

        #endregion

        #region Private Methods

        // Find duplicated item in the database
        private Item DuplicatedItemId(Item item, int PONumber)
        {
            return repo.FindDuplicatedItem(item, PONumber);
        }

        private bool Validate(Item ItemoValidate)
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
