using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VastVoyages.Model.DTO
{
    public class JobAssignmentsLookupsDTO
    {
        public int JobAssignmentId { get; set; }
        public string JobAssignment { get; set; }
    }

    public class SupervisorLookupsDTO
    {
        public int SupervisorId { get; set; }
        public string SupervisorName { get; set; }
    }

    public class DepartmentLookupsDTO
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }

    public class EmployeeStatusLookupsDTO
    {
        public int EmployeeStatusId { get; set; }
        public string EmployeeStatus { get; set; }
    }
}
