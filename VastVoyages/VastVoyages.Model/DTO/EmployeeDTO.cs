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
        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }

        public virtual string FullAddress
        {
            get
            {
                return Street + ", " + City + ", " + Province + ", " + Country + ", " + PostalCode;
            }
        }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        ////Transfer to LoginDTO 

        //public string EmployeeId { get; set; }
        //public string UserName { get; set; }
        ////public string FirstName { get; set; }
        //public string MiddleInit { get; set; }
        ////public string LastName { get; set; }
        //public string Job { get; set; }
        //public int DepartmentId { get; set; }
        //public string Department { get; set; }
        //public int SupervisorId { get; set; }
        //public string Supervisor { get; set; }
        //public string Role { get; set; }
    }
}
