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
    public class LoginService
    {
        #region Fields and Constructors

        private LoginRepo repo;

        #endregion

        #region Public Methods

        public LoginService()
        {
            repo = new LoginRepo();
        }

        /// <summary>
        /// Check employeee id and password are correct 
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public bool AttemptLogin(Login loginInfo)
        {
            if (Validate(loginInfo))
            {
                HashCode hc = new HashCode();
                loginInfo.Password = hc.CalculateSHA256(loginInfo.Password);
                return repo.Login(loginInfo);
            }

            return false;
        }

        /// <summary>
        /// Get employee's information by employee id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public LoginDTO GetEmpInfo(string employeeId)
        {
            LoginDTO empInfo = repo.RetrieveEmpInfoById(employeeId);

            if (empInfo.Supervisor == "")
            {
                empInfo.Role = "CEO";
            }
            else
            {
                if (empInfo.Department == "HR")
                {
                    empInfo.Role = empInfo.SupervisorId == 10000000 ? "HR Supervisor" : "HR Employee";
                }
                else
                {
                    empInfo.Role = empInfo.SupervisorId == 10000000 ? "Supervisor" : "Employee";
                }
            }

            return empInfo;
        }


        #endregion

        #region Private Methods

        /// <summary>
        /// Validation for login entity
        /// </summary>
        /// <param name="loginToValidate"></param>
        /// <returns></returns>
        private bool Validate(Login loginToValidate)
        {
            ValidationContext context = new ValidationContext(loginToValidate);
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(loginToValidate, context, results, true);

            foreach (ValidationResult e in results)
            {
                loginToValidate.AddError(new ValidationError(e.ErrorMessage, ErrorType.Model));
            }

            return loginToValidate.Errors.Count == 0;
        }

        #endregion
    }
}
