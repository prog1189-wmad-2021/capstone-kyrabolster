using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Security;
using VastVoyages.Model;
using VastVoyages.Model.DTO;
using VastVoyages.Model.Entities;
using VastVoyages.Repository;
using VastVoyages.Types;

namespace VastVoyages.Service
{
    public class EmployeeService
    {
        private EmployeeRepo repo = new EmployeeRepo();
        LookupsRepo lookupsRepo = new LookupsRepo();


        #region Public Methods

        /// <summary>
        /// Add new employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool AddEmployee(Employee employee)
        {
            if (ValidateEmployee(employee))
            {
                GenerateUsername(employee);

                HashCode hc = new HashCode();
                string password = GeneratePassword();

                repo.AddEmployee(employee, hc.CalculateSHA256(password));

                //repo.InsertPassword(employee.EmployeeId, hc.CalculateSHA256(password));

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Get Employee to modify by id
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public Employee UpdatePersonalInfoWeb(Employee employee)
        {
            if (ValidateEmployee(employee))
                return repo.UpdatePersonalInfoWeb(employee);

            return employee;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            if (ValidateEmployee(employee))
            {
                return repo.UpdateEmployee(employee);
            }
            return employee;
        }

        /// <summary>
        /// Get Employee to modify by id
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public Employee GetEmployeeToModifyById(int employeeId)
        {
            return repo.RetrieveEmployeeToModify(employeeId);
        }


        /// <summary>
        /// Get list of all employees
        /// </summary>
        /// <returns></returns>
        public List<EmployeeDTO> GetAllEmployees()
        {
            return repo.RetrieveAllEmployees();
        }

        /// <summary>
        /// Search employees by employee Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public List<EmployeeDTO> SearchEmployeesById(int employeeId)
        {
            List<EmployeeDTO> employees = repo.SearchEmployeesById(employeeId);

            return employees;
        }

        /// <summary>
        /// Search employees by last name
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public List<EmployeeDTO> SearchEmployeesByLastName(string lastName)
        {
            return repo.SearchEmployeesByLastName(lastName);
        }

        /// <summary>
        /// Get CEO information
        /// </summary>
        /// <returns></returns>
        public List<SupervisorLookupsDTO> GetCEO()
        {
            return lookupsRepo.RetrieveCEO();
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Generate employee username 
        /// Format: Last name plus First name initial, and append incremented number if duplicated
        /// </summary>
        /// <param name="employee"></param>
        private void GenerateUsername(Employee employee)
        {
            string username = employee.LastName + employee.FirstName[0];

            int usernameCount = DuplicateUsernameCount(username);

            if (usernameCount > 0)
            {
                username += (usernameCount + 1);
            }

            employee.UserName = username;
        }

        /// <summary>
        /// Count number of duplicate usernames (excluding appended number)
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private int DuplicateUsernameCount(string username)
        {
            return repo.CheckDuplicateUsername(username);
        }

        /// <summary>
        /// Generate random 8 character password
        /// </summary>
        /// <returns></returns>
        private string GeneratePassword()
        {
            return Membership.GeneratePassword(8, 1);
        }

        /// <summary>
        /// Check that job start date is not prior to seniority date
        /// </summary>
        /// <param name="jobStartDate"></param>
        /// <param name="seniorityDate"></param>
        /// <returns></returns>
        private bool IsJobStartDateInValid(DateTime jobStartDate, DateTime seniorityDate)
        {
            return (jobStartDate < seniorityDate);
        }

        /// <summary>
        /// Verify if supervisor has maximum number of employees (10)
        /// </summary>
        /// <param name="departmentId"></param>
        /// <param name="supervisorId"></param>
        /// <returns></returns>
        private bool IsSupervisorRatioExceeded(int employeeId, int departmentId, int supervisorId)
        {
            SupervisorLookupsDTO ceo = new SupervisorLookupsDTO();
            ceo = lookupsRepo.RetrieveCEO()[0];

            EmployeeRepo employeeRepo = new EmployeeRepo();

            // if they are not a supervisor
            if (supervisorId != ceo.SupervisorId)
            {
                //get employees assigned to supervisor
                List<EmployeeDTO> supervisorsEmployees = 
                    employeeRepo.RetrieveAllEmployees().Where(x => x.SupervisorId.Equals(supervisorId)).ToList();
                List<int> supervisorsEmployeesIds = new List<int>();

                foreach(EmployeeDTO emp in supervisorsEmployees)
                {
                    supervisorsEmployeesIds.Add(emp.EmpId);
                }

                int employeeCount = repo.GetEmployeeCount(departmentId, supervisorId);

                //check if supervisor already assigned this employee (for modify employee use case)
                if (supervisorsEmployeesIds.Contains(employeeId))
                {
                    return (employeeCount >= 11);
                }

                return (employeeCount >= 10);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check employee is 18 years old or older
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns></returns>
        private bool IsBelowLegalAge(DateTime dateOfBirth)
        {
            return (dateOfBirth.Date > DateTime.Now.Date.AddYears(-18));
        }

        private bool IsValidatePostalCode(Employee employee)
        {
            if (employee.Country == "Canada")
            {
                //Regex rgx = new Regex(@"^[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]$");
                //Regex rgx = new Regex(@"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$");
                Regex rgx = new Regex(@"^([A-Z]\d[A-Z])\ {0,1}(\d[A-Z]\d)$");

                return rgx.IsMatch(employee.PostalCode);
            }
            else
            {
                Regex rgx = new Regex(@"^\d{5}(?:[-\s]\d{4})?$");
                return rgx.IsMatch(employee.PostalCode);
            }
        }

        private bool IsBelowRetirementAge(DateTime dateOfBirth)
        {
            return (dateOfBirth.Date > DateTime.Now.Date.AddYears(-55));
        }

        /// <summary>
        /// Validate new employee object
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private bool ValidateEmployee(Employee employee)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(employee, new ValidationContext(employee), results, true);

            foreach (ValidationResult e in results)
            {
                employee.AddError(new ValidationError(e.ErrorMessage, ErrorType.Model));
            }

            if (!IsValidatePostalCode(employee))
            {
                if (employee.Country == "Canada")
                    employee.AddError(new ValidationError("Postal Code must be in correct Canadian format.", ErrorType.Business));
                else
                    employee.AddError(new ValidationError("ZipCode must be in correct US format.", ErrorType.Business));
            }

            if (IsBelowLegalAge(employee.DateOfBirth))
            {
                employee.AddError(new ValidationError("The employee must be of legal age.", ErrorType.Business));
            }

            if (IsJobStartDateInValid(employee.JobStartDate, employee.SeniorityDate))
            {
                employee.AddError(new ValidationError("Job Start Date cannot be prior to Seniority Date", ErrorType.Business));
            }

            if (IsSupervisorRatioExceeded(employee.EmployeeId, employee.DepartmentId, employee.SupervisorId))
            {
                employee.AddError(new ValidationError("This supervisor already has 10 employees.", ErrorType.Business));
            }

            if (IsBelowRetirementAge(employee.DateOfBirth) && employee.EmployeeStatusId == 3)
            {
                employee.AddError(new ValidationError("The employee cannot retire below the age of 55.", ErrorType.Business));
            }
            return employee.Errors.Count == 0;
        }

        private bool ValidateEmployeeDTO(EmployeeDTO employee)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(employee, new ValidationContext(employee), results, true);

            foreach (ValidationResult e in results)
            {
                employee.AddError(new ValidationError(e.ErrorMessage, ErrorType.Model));
            }

            return employee.Errors.Count == 0;
        }
        #endregion
    }
}
