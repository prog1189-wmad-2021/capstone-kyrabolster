using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using VastVoyages.Model;
using VastVoyages.Model.Entities;
using VastVoyages.Repository;
using VastVoyages.Types;

namespace VastVoyages.Service
{
    public class EmployeeService
    {
        private EmployeeRepo repo = new EmployeeRepo();

        #region Public Methods
        public bool AddEmployee(Employee employee)
        {
            if (ValidateEmployee(employee))
            {
                GenerateUsername(employee);

                repo.AddEmployee(employee);

                repo.InsertPassword(employee.EmployeeId, GeneratePassword());

                return true;
            }
            else
                return false;
        }
        #endregion

        #region Private Methods
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

        private int DuplicateUsernameCount(string username)
        {
            return repo.CheckDuplicateUsername(username);
        }

        private string GeneratePassword()
        {
            return Membership.GeneratePassword(8, 1);
        }
        private bool IsJobStartDateInValid(DateTime jobStartDate, DateTime seniorityDate)
        {
            return (jobStartDate < seniorityDate);
        }

        private bool IsSupervisorRatioExceeded(int departmentId, int supervisorId)
        {
            // if they are not a supervisor
            if (supervisorId != 10000000)
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

        private bool ValidateEmployee(Employee employee)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(employee, new ValidationContext(employee), results, true);

            foreach (ValidationResult e in results)
            {
                employee.AddError(new ValidationError(e.ErrorMessage, ErrorType.Model));
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
