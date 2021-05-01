using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VastVoyages.Model
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Employee ID is required")]
        [Display(Name = "Employee ID")]
        public string EmployeeId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Job { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public int SupervisorId { get; set; }
        public string Supervisor { get; set; }
        public string Role { get; set; }
    }
}
