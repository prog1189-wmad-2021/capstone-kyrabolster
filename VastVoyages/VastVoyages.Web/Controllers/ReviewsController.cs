using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VastVoyages.Model;
using VastVoyages.Service;
using VastVoyages.Types;
using VastVoyages.Web.CustomAuthoize;

namespace VastVoyages.API.Controllers
{
    public class ReviewsController : Controller
    {
        private ReviewService service = new ReviewService();
        private EmployeeService empService = new EmployeeService();


        // GET: Reviews
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.Supervisor)]
        public ActionResult Index()
        {
            try
            {
                int reviewerId = Convert.ToInt32(Session["employeeId"]);

                List<EmployeeDTO> employees = new List<EmployeeDTO>();

                employees = empService.GetAllEmployees().Where(e => e.SupervisorId.Equals(reviewerId)).ToList();

                //only return employees that are due for review ***
                employees = service.GetEmployeesDueFoReviewThisQuarter(employees);

                if (employees.Count <= 0)
                {
                    ViewBag.Reviews = "There are currently no employees due for review this quarter.";
                }

                return View(employees);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Reviews", "Index"));
            }
        }

        // GET: Reviews/Create
        public ActionResult Create(int? employeeId)
        {
            try
            {
                if (employeeId == null)
                {
                    return View("PageNotFound");
                }

                int reviewerId = Convert.ToInt32(Session["employeeId"]);

                EmployeeDTO employee = new EmployeeDTO();
                employee = empService.SearchEmployeesById(employeeId.Value)[0];

                //check if correct super but already had review
                List<Review> employeesReviews = service.GetEmployeeReviews(employee.EmpId);

                if(employee.SupervisorId != reviewerId || service.HadEmployeeReviewThisQuarter(employeesReviews))
                {
                    return View("PageNotFound");
                }
                

                ViewBag.EmployeeName = employee.FullName;
                ViewBag.EmployeeId = employee.EmpId;

                Review review = new Review() { EmployeeId = employeeId.Value, ReviewerId = reviewerId };

                return View(review);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Review", "Create"));
            }
        }

        // POST: Reviews/Create
        [HttpPost]
        public ActionResult Create(Review review)
        {
            try
            {
                //change rating to string in db?

                    if ((int)review.Rating <= 0)
                    {
                        review.AddError(new ValidationError("Please select a rating", ErrorType.Model));
                    } 
                    review.RatingId = (int)review.Rating;


                if (service.AddReview(review))
                {
                    TempData["Success"] = "Review successfully created. Id: " + review.EmployeeReviewId;
                    return RedirectToAction("Index", new { id = review.EmployeeId });
                }
                else
                {
                    return View(review);
                }

            }
            catch (Exception ex)
            {
                review.AddError(new ValidationError(ex.Message, ErrorType.Business));
                return View("review");
            }
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reviews/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
