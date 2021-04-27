using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VastVoyages.Model
{
    public class LoginDTO : BaseEntity
    {
        [Required(ErrorMessage = "Employee ID is required")]
        [Range(1, maximum:int.MaxValue, ErrorMessage = "Employee Id is required")]
        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
