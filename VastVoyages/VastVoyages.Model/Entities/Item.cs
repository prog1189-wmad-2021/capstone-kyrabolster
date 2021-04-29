using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VastVoyages.Model
{
    public class Item : BaseEntity
    {
        public int ItemId { get; set; }

        [Display(Name = "Item Name")]
        [Required(ErrorMessage = "Item name is required")]
        [StringLength(50, ErrorMessage = "The item name cannot exceed 50 characters.")]
        public string ItemName { get; set; }

        [Display(Name = "Item Description")]
        [Required(ErrorMessage = "Item escription is required")]
        [StringLength(100, ErrorMessage = "The item description cannot exceed 100 characters.")]
        public string ItemDescription { get; set; }

        [Required(ErrorMessage = "Justification is required")]
        [StringLength(80, ErrorMessage = "The Justification cannot exceed 80 characters.")]
        public string Justification { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(50, ErrorMessage = "The location cannot exceed 50 characters.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01f, double.MaxValue, ErrorMessage = "Price can not be less than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity can not be less than 0")]
        public int Quantity { get; set; }

        [Display(Name = "Decision Reason")]
        public string DecisionReason { get; set; }
        public int PONumber { get; set; }
        public int ItemStatusId { get; set; }
        public byte[] RecordVersion { get; set; }
    }
}
