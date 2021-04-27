using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VastVoyages.Model
{
    public class Login : BaseEntity
    {
        [Required(ErrorMessage = "Employee ID is required")]
        [Display(Name = "Employee ID")]
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
