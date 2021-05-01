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
        [StringLength(40, ErrorMessage = "First name must be between 2 and 40 characters", MinimumLength = 2)]
        public string FirstName { get; set; }

        [StringLength(2, ErrorMessage = "Middle initial cannot exceed 2 character")]
        public string MiddleInitial { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(40, ErrorMessage = "Last name must be between 2 and 40 characters", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date Of Birth is required")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Street is required")]
        [StringLength(50, ErrorMessage = "Street must be between 2 and 50 characters", MinimumLength = 2)]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(20, ErrorMessage = "City must be between 2 and 20 characters", MinimumLength = 2)]
        public string City { get; set; }

        [Required(ErrorMessage = "Province is required")]
        [StringLength(2, ErrorMessage = "Province must be between 2 and 2 characters", MinimumLength = 2)]
        public string Province { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(100, ErrorMessage = "Country must be between 2 and 100 characters", MinimumLength = 2)]
        public string Country { get; set; }

        [Required(ErrorMessage = "PostalCode is required")]
        [StringLength(7, ErrorMessage = "Postal code / zipcode length is invalid", MinimumLength = 4)]
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

        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Job Start Date is required.")]
        public DateTime JobStartDate { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
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

        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(MiddleInitial))
                    return FirstName + " " + LastName;
                else
                    return FirstName + " " + MiddleInitial + " " + LastName;
            }
        }
    }
}
