using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VastVoyages.Model
{
    public class Email
    {
        public string mailTo { get; set; }
        public string mailFrom { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
}
