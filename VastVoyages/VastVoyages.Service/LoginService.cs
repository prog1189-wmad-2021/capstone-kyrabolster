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

        public bool AttemptLogin(LoginDTO loginInfo)
        {
            if (Validate(loginInfo))
                return repo.Login(loginInfo);

            return false;
        }

        public EmployeeDTO GetEmpInfo(int employeeId)
        {
            return repo.RetrieveEmpInfoById(employeeId);
        }


        #endregion

        #region Private Methods

        private bool Validate(LoginDTO loginToValidate)
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
