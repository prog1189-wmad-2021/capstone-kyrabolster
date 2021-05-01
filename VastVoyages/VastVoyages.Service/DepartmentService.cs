using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VastVoyages.Model;
using VastVoyages.Repository;
using VastVoyages.Types;

namespace VastVoyages.Service
{
    public class DepartmentService
    {
        private DepartmentRepo repo = new DepartmentRepo();

        #region Public Methods

        /// <summary>
        /// Add new department
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public bool AddDepartment(Department department)
        {
            if (ValidateDepartment(department))
                return repo.AddDepartment(department);
            else
                return false;
        }

        #endregion

        #region Private Methods

        private bool IsInvocationDateInPast(DateTime invocationDate)
        {
            return (invocationDate.Date < DateTime.Now.Date);
        }

        /// <summary>
        /// Validate department to be added to satisfy the model and business rules
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        private bool ValidateDepartment(Department department)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(department, new ValidationContext(department), results, true);

            foreach (ValidationResult e in results)
            {
                department.AddError(new ValidationError(e.ErrorMessage, ErrorType.Model));
            }

            if (IsInvocationDateInPast(department.InvocationDate))
            {
                department.AddError(new ValidationError("Invocation date cannot be in the past", ErrorType.Business));
            }

            return department.Errors.Count == 0;
        }
        #endregion
    }
}
