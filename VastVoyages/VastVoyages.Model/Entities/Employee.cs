using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VastVoyages.Model.Entities
{
    public class Employee : BaseEntity
    {
        [Required]
        public int EmployeeId { get; set; }

        public byte[] RecordVersion { get; set; }
        
        public string UserName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(40, ErrorMessage = ("First name cannot exceed 40 characters"))]
        public string FirstName { get; set; }

        [StringLength(1, ErrorMessage = ("Middle initial cannot exceed 1 character"))]
        public string MiddleInitial { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(40, ErrorMessage = ("Last name cannot exceed 40 characters"))]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date Of Birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Street is required")]
        [StringLength(50, ErrorMessage = ("Street cannot exceed 50 characters"))]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(20, ErrorMessage = ("City cannot exceed 20 characters"))]
        public string City { get; set; }

        [Required(ErrorMessage = "Province is required")]
        [StringLength(2, ErrorMessage = ("Province cannot exceed 2 characters"))]
        public string Province { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(100, ErrorMessage = ("Country cannot exceed 100 characters"))]
        public string Country { get; set; }

        [Required(ErrorMessage = "PostalCode is required")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Work Phone is required")]
        [Phone(ErrorMessage = "Work Phone must be a valid phone number")]
        public string WorkPhone { get; set; }

        [Required(ErrorMessage = "Cell Phone is required")]
        [Phone(ErrorMessage = "Cell Phone must be a valid phone number")]
        public string CellPhone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email must be in valid Email address format")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Job Start Date is required.")]
        public DateTime JobStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Seniority Date is required.")]
        public DateTime SeniorityDate { get; set; }

        [Required(ErrorMessage = "SIN is required")]
        [RegularExpression("[0-9][0-9][0-9]-[0-9][0-9][0-9]-[0-9][0-9][0-9]",
        ErrorMessage = "SIN must be in correct Canadian format (###-###-###).")]
        public string SIN { get; set; }

        public int SupervisorId { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public int DepartmentId { get; set; }

        public int EmployeeStatusId { get; set; }

        [Required(ErrorMessage = "Job Assignment is required.")]
        public int JobAssignmentId { get; set; }
    }
}
