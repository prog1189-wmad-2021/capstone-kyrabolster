using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                return repo.AddEmployee(employee);
            }
            else
                return false;
        }
        #endregion

        #region Private Methods
        private bool ValidateEmployee(Employee employee)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(employee, new ValidationContext(employee), results, true);

            foreach (ValidationResult e in results)
            {
                employee.AddError(new ValidationError(e.ErrorMessage, ErrorType.Model));
            }

            return employee.Errors.Count == 0;
        }

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
        #endregion
    }
}
