using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
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

                repo.AddEmployee(employee);

                HashCode hc = new HashCode();
                string password = GeneratePassword();

                repo.InsertPassword(employee.EmployeeId, hc.CalculateSHA256(password));

                return true;
            }
            else
                return false;
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
            List <EmployeeDTO> employees = repo.SearchEmployeesById(employeeId);

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
        private bool IsSupervisorRatioExceeded(int departmentId, int supervisorId)
        {
            SupervisorLookupsDTO ceo = new SupervisorLookupsDTO();
            ceo = lookupsRepo.RetrieveCEO()[0];

            // if they are not a supervisor
            if (supervisorId != ceo.SupervisorId)
            {
                int employeeCount = repo.GetEmployeeCount(departmentId, supervisorId);
                //int supervisorCount = repo.GetSupervisorCount(departmentId, supervisorId);

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

            if (IsBelowLegalAge(employee.DateOfBirth))
            {
                employee.AddError(new ValidationError("The employee must be of legal.", ErrorType.Business));
            }

            if (IsJobStartDateInValid(employee.JobStartDate, employee.SeniorityDate))
            {
                employee.AddError(new ValidationError("Job Start Date cannot be prior to SeniorityDate", ErrorType.Business));
            }

            if (IsSupervisorRatioExceeded(employee.DepartmentId, employee.SupervisorId))
            {
                employee.AddError(new ValidationError("This supervisor already has 10 employees.", ErrorType.Business));
            }

            return employee.Errors.Count == 0;
        }

        #endregion
    }
}
