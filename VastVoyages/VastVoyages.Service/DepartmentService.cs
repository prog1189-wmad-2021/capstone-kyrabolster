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

        /// <summary>
        /// Update department
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public Department UpdateDepartment(Department department, string role = null)
        {
            if (role == "Supervisor")
            {
                if (ValidateDepartmentModelOnly(department))
                    return repo.UpdateDepartment(department);
            }
            else
            {
                DateTime originalInvocationDate = repo.GetDepartmentById(department.DepartmentId).InvocationDate;

                if (department.InvocationDate.Date == originalInvocationDate.Date)
                {
                    if (ValidateDepartmentModelOnly(department))
                        return repo.UpdateDepartment(department);
                }
                else
                {
                    if (ValidateDepartment(department))
                        return repo.UpdateDepartment(department);
                    else
                        department.AddError(new ValidationError("Invocation Date Exception: The invocation date can be in the past if it is the original invocation date : " + originalInvocationDate.ToShortDateString(), ErrorType.Business));
                }
            }

            return department;
        }

        /// <summary>
        /// Get all departments
        /// </summary>
        /// <returns></returns>
        public List<Department> GetDepartments(string role = null, int? departmentId = 0)
        {
            List<Department> departments = new List<Department>();
            departments = repo.RetrieveDepartments();

            if (role == "Supervisor" && departmentId > 0)
                departments = departments.Where(d => d.DepartmentId.Equals(departmentId)).ToList();

            return departments;
        }

        /// <summary>
        /// Get department by id
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public Department GetDepartmentById(int departmentId)
        {
            return repo.GetDepartmentById(departmentId);
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

        private bool ValidateDepartmentModelOnly(Department department)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(department, new ValidationContext(department), results, true);

            foreach (ValidationResult e in results)
            {
                department.AddError(new ValidationError(e.ErrorMessage, ErrorType.Model));
            }

            return department.Errors.Count == 0;
        }

        #endregion
    }
}
