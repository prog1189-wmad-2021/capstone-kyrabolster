using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VastVoyages.Model
{
    public class PurchaseOrder : BaseEntity
    {
        public string PONumber { get; set; }
        public DateTime SubmissionDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public int employeeId { get; set; }
        public int POstatusId { get; set; }
        public byte[] RecordVersion { get; set; }
        public List<Item> items { get; set; }
    }
}
