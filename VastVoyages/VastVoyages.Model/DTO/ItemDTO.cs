using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VastVoyages.Model
{
    public class ItemDTO
    {
        public int ItemId { get; set; }

        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Item Description")]
        public string ItemDescription { get; set; }
        public string Justification { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        [Display(Name = "Decision Reason")]
        public string PONumber { get; set; }
        public string ItemStatus { get; set; }
        public string DecisionReason { get; set; }
    }
}
