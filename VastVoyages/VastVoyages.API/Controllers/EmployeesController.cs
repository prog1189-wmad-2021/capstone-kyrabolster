using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VastVoyages.Model;
using VastVoyages.Model.Entities;
using VastVoyages.Service;

namespace VastVoyages.API.Controllers
{
    [RoutePrefix("api")]
    public class EmployeesController : ApiController
    {
        EmployeeService service = new EmployeeService();
        DepartmentService departmentService = new DepartmentService();

        [Route("employees/{departmentId}")]
        public IHttpActionResult Get(int departmentId)
        {
            try
            {
                List<EmployeeDTO> employees = service.GetAllEmployees().OrderBy(e => e.LastName).ToList();

                //if (employeeId > 0)
                //{
                //    employees = employees.Where(a => a.EmpId.Equals(employeeId)).ToList();
                //}
                if (departmentId > 0)
                {
                    employees = employees.Where(a => a.DepartmentId.Equals(departmentId)).ToList();
                }

                return Ok(employees);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occured. Please contact the system administrator");
            }
        }

        [Route("departments")]
        public IHttpActionResult Get()
        {
            try
            {
                List<Department> departments = departmentService.GetDepartments().OrderBy(d => d.DepartmentName).ToList();

                return Ok(departments);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occured. Please contact the system administrator");
            }
        }

    }
}
