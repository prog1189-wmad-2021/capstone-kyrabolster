using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VastVoyages.Model
{
    public class EmployeeReviewsDTO : BaseEntity
    {
        public EmployeeDTO EmployeeDetails { get; set; }
        public List<Review> Reviews { get; set; }

    }
}

