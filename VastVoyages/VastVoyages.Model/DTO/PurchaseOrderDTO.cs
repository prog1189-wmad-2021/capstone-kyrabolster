using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VastVoyages.Model
{
    public class PurchaseOrderDTO
    {
        [Display(Name = "Purchase Order Number")]
        public string PONumber { get; set; }

        [Display(Name = "Submission Date")]
        public DateTime SubmissionDate { get; set; }

        [Display(Name = "Sub Total")]
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; } 
        public string Employee { get; set; }
        public string Supervisor { get; set; }
        public string POStatus { get; set; }
        public byte[] RecordVersion { get; set; }
        public List<Item> items { get; set; } 
    }
}
