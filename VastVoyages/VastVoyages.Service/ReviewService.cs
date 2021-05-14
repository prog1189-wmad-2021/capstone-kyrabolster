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
        /// Get review by id
        /// Set Quarter and Year - reviews are associated to the last quarter and year before the review date.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public Review GetReviewById(int reviewId)
        {
            Review review = repo.RetrieveReview(reviewId);

            if(review != null)
                review = SetReviewYearAndQuarter(review);

            return review;
        }

        /// <summary>
        /// Get list of all reviews for employee
        /// </summary>
        /// <returns></returns>
        public List<Review> GetEmployeeReviews(int employeeId)
        {
            List<Review> reviews = repo.RetrieveEmployeeReviews(employeeId);

            foreach (Review review in reviews)
            {
                Review newReview = SetReviewYearAndQuarter(review);

                //int quarter = (review.ReviewDate.Month - 1) / 3 + 1;
                review.Quarter = newReview.Quarter;
                review.Year = newReview.Year;
            }

            return reviews;
        }

        public Review SetReviewYearAndQuarter(Review review)
        {
            //set quarter (last quarter)
            int reviewDateQuarter = (review.ReviewDate.Month - 1) / 3 + 1;
            int reviewDateYear = review.ReviewDate.Year;
            DateTime firstDayOfReviewDateQuarter = new DateTime(reviewDateYear, (reviewDateQuarter - 1) * 3 + 1, 1);

            DateTime lastDayOfLastQuarter = firstDayOfReviewDateQuarter.AddDays(-1);
            int lastQuarterYear = lastDayOfLastQuarter.Year;
            int lastQuarter = (lastDayOfLastQuarter.Month - 1) / 3 + 1;

            //set quarter & year
            review.Quarter = lastQuarter;
            review.Year = lastQuarterYear;
            return review;
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

        //public List<EmployeeDTO> GetEmployeesDueFoReviewLastQuarter(List<EmployeeDTO> supervisorsEmployees)
        //{
        //    List<EmployeeDTO> employeesDueForReview = new List<EmployeeDTO>();

        //    foreach (EmployeeDTO emp in supervisorsEmployees)
        //    {
        //        List<Review> employeesReviews = repo.RetrieveEmployeeReviews(emp.EmpId);
        //        if (!HadEmployeeReviewLastQuarter(employeesReviews))
        //        {
        //            employeesDueForReview.Add(emp);
        //        }
        //    }

        //    return employeesDueForReview;
        //}


        /// <summary>
        /// Check if review occured this quarter
        /// </summary>
        /// <param name="reviews"></param>
        /// <returns></returns>
        public bool HadEmployeeReviewThisQuarter(List<Review> reviews)
        {
            //get quarter
            int currentQuarter = (DateTime.Now.Month - 1) / 3 + 1;
            int currentYear = DateTime.Now.Year;

            foreach (Review review in reviews)
            {
                int reviewQuarter = (review.ReviewDate.Month - 1) / 3 + 1;
                int reviewYear = review.ReviewDate.Year;

                if (reviewQuarter == currentQuarter && reviewYear == currentYear)
                    return true;
            }

            return false;
        }

        //public bool HadEmployeeReviewLastQuarter(List<Review> reviews)
        //{
        //    //get LAST quarter
        //    int currentQuarter = (DateTime.Now.Month - 1) / 3 + 1;
        //    int currentYear = DateTime.Now.Year;
        //    DateTime firstDayOfQuarter = new DateTime(currentYear, (currentQuarter - 1) * 3 + 1, 1);

        //    DateTime lastDayOfLastQuarter = firstDayOfQuarter.AddDays(-1);
        //    int lastQuarterYear = lastDayOfLastQuarter.Year;
        //    int lastQuarter = (lastDayOfLastQuarter.Month - 1) / 3 + 1;

        //    foreach (Review review in reviews)
        //    {
        //        int reviewQuarter = (review.ReviewDate.Month - 1) / 3 + 1;
        //        int reviewYear = review.ReviewDate.Year;

        //        if (reviewQuarter == lastQuarter && reviewYear == lastQuarterYear)
        //            return true;
        //    }

        //    return false;
        //}

        /// <summary>
        /// Check if review overdue
        /// </summary>
        /// <param name="reviews"></param>
        /// <returns></returns>
        //public bool IsReviewOverdue(Review review)
        //{
        //    //get quarter
        //    int currentQuarter = (DateTime.Now.Month - 1) / 3 + 1;
        //    DateTime firstDayOfQuarter = new DateTime(review.ReviewDate.Year, (currentQuarter - 1) * 3 + 1, 1);

        //    return (review.ReviewDate > firstDayOfQuarter.AddDays(30));
        //}


        /// <summary>
        /// Check 30 days past the quarter start date
        /// </summary>
        /// <returns></returns>
        public bool Is30DaysPastQuarter()
        {
            int currentQuarter = (DateTime.Now.Month - 1) / 3 + 1;
            int currentYear = DateTime.Now.Year;
            DateTime firstDayOfQuarter = new DateTime(currentYear, (currentQuarter - 1) * 3 + 1, 1);
            DateTime lastDayOfQuarter = firstDayOfQuarter.AddMonths(3).AddDays(-1);

            return (DateTime.Now > firstDayOfQuarter.AddDays(30));
            //return (DateTime.Now.AddDays(-30) > firstDayOfQuarter.AddDays(30));
        }

        /// <summary>
        /// Insert date into review reminders table
        /// </summary>
        public void TrackReviewReminderSent()
        {
            repo.InsertReviewReminderEmail();
        }

        /// <summary>
        /// Check if review reminders sent today
        /// </summary>
        /// <returns></returns>
        public bool HaveReviewEmailsBeenSentToday()
        {
            return repo.EmailSentToday();
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
