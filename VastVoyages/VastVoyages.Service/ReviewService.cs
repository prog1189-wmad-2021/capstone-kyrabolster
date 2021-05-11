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
    public class ReviewService
    {

        private ReviewRepo repo = new ReviewRepo();

        /// <summary>
        /// Add new review
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        public bool AddReview(Review review)
        {
            if (ValidateReview(review))
                return repo.AddReview(review);
            else
                return false;
        }

        /// <summary>
        /// Get list of all reviews for employee
        /// </summary>
        /// <returns></returns>
        public List<Review> GetEmployeeReviews(int employeeId)
        {
            return repo.RetrieveEmployeeReviews(employeeId);
        }

        /// <summary>
        /// Get list of all supervisor's employees due for review
        /// </summary>
        /// <returns></returns>
        public List<EmployeeDTO> GetEmployeesDueFoReviewThisQuarter(List<EmployeeDTO> supervisorsEmployees)
        {
            List<EmployeeDTO> employeesDueForReview = new List<EmployeeDTO>();

            foreach (EmployeeDTO emp in supervisorsEmployees)
            {
                List<Review> employeesReviews = repo.RetrieveEmployeeReviews(emp.EmpId);
                if (!HadEmployeeReviewThisQuarter(employeesReviews))
                {
                    employeesDueForReview.Add(emp);
                }
            }

            return employeesDueForReview;
        }

        /// <summary>
        /// Check if review occured this quarter
        /// </summary>
        /// <param name="reviews"></param>
        /// <returns></returns>
        private bool HadEmployeeReviewThisQuarter(List<Review> reviews)
        {
            //get quarter
            int currentQuarter = (DateTime.Now.Month - 1) / 3 + 1;
            int currentYear = DateTime.Now.Year;

            foreach(Review review in reviews)
            {
                int reviewQuarter = (review.ReviewDate.Month - 1) / 3 + 1;
                int reviewYear = review.ReviewDate.Year;

                if (reviewQuarter == currentQuarter && reviewYear == currentYear)
                    return true;
            }

            return false;
        }


        /// <summary>
        /// Check if review date is in the future
        /// </summary>
        /// <param name="reviewDate"></param>
        /// <returns></returns>
        private bool IsReviewDateInFuture(DateTime reviewDate)
        {
            return (reviewDate.Date > DateTime.Now.Date);
        }


        /// <summary>
        /// Validate review to be added to satisfy the model and business rules
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        private bool ValidateReview(Review review)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(review, new ValidationContext(review), results, true);

            foreach (ValidationResult e in results)
            {
                review.AddError(new ValidationError(e.ErrorMessage, ErrorType.Model));
            }

            if (IsReviewDateInFuture(review.ReviewDate))
            {
                review.AddError(new ValidationError("Review date cannot be in the future", ErrorType.Business));
            }

            return review.Errors.Count == 0;
        }
    }
}
