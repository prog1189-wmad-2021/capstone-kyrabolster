using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VastVoyages.Model
{
    public class EmployeeDTO : BaseEntity
    {
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInit { get; set; }
        public string LastName { get; set; }
        public string Job { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public int SupervisorId { get; set; }
        public string Supervisor { get; set; }
    }
}
