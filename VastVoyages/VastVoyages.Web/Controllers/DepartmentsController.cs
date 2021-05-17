using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VastVoyages.Model;
using VastVoyages.Model.Entities;
using VastVoyages.Service;
using VastVoyages.Types;
using VastVoyages.Web.CustomAuthoize;

namespace VastVoyages.Web.Controllers
{
    public class DepartmentsController : Controller
    {
        private DepartmentService service = new DepartmentService();
        private EmployeeService empService = new EmployeeService();

        //DateTime originalInvocationDate;

        // GET: Departments
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.HREmployee, RoleName.Supervisor)]
        public ActionResult Index()
        {
            try
            {
                List<Department> departments = new List<Department>();
                //departments = service.GetDepartments();

                EmployeeDTO employee = new EmployeeDTO();
                employee = empService.SearchEmployeesById(Convert.ToInt32(Session["employeeId"]))[0];

                int departmentId = employee.DepartmentId;
                //int departmentId = (int)Session["departmentId"];

                string role = Session["role"].ToString();

                departments = service.GetDepartments(role, departmentId);

                return View(departments);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Department", "Edit"));
            }
        }

        // GET: Departments/Edit/5
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.HREmployee, RoleName.Supervisor)]
        public ActionResult Edit(int? departmentId)
        {
            try
            {
                EmployeeDTO employee = new EmployeeDTO();
                employee = empService.SearchEmployeesById(Convert.ToInt32(Session["employeeId"]))[0];

                if (Session["role"].ToString() == "Supervisor" && departmentId != employee.DepartmentId)
                {
                    return View("PageNotFound");
                }

                if (departmentId == null)
                    return View("PageNotFound");

                Department departmentDetails = service.GetDepartmentById(departmentId.Value);

                if (departmentDetails == null)
                    return View("PageNotFound");

                Department department = new Department()
                {
                    DepartmentId = departmentId.Value,
                    DepartmentName = departmentDetails.DepartmentName,
                    DepartmentDescription = departmentDetails.DepartmentDescription,
                    InvocationDate = departmentDetails.InvocationDate,
                    RecordVersion = departmentDetails.RecordVersion
                };

                //originalInvocationDate = department.InvocationDate;

                return View(department);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Department", "Edit"));
            }
        }

        // POST: Departments/Edit/5
        [HttpPost]
        public ActionResult Edit(Department department)
        {
            try
            {
                string role = Session["role"].ToString();
                ViewBag.DepartmentName = department.DepartmentName;

                //department = service.UpdateDepartment(department, originalInvocationDate, role);
                department = service.UpdateDepartment(department, role);

                if (department.Errors.Count == 0)
                {
                    TempData["Success"] = department.DepartmentName + " department sucessfully updated. ";
                    return RedirectToAction("Index", "Departments");
                }

                return View(department);
            }
            catch (Exception ex)
            {
                department.AddError(new ValidationError(ex.Message, ErrorType.Business));
                return View(department);
            }
        }
    }

}