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
    [RoutePrefix("api/employees")]
    public class EmployeesController : ApiController
    {
        EmployeeService service = new EmployeeService();

        [Route("")]
        public IHttpActionResult Get(int employeeId = 0, string lastName = null)
        {
            try
            {
                List<EmployeeDTO> employees = service.GetAllEmployees();

                if (employeeId > 0)
                {
                    employees = employees.Where(a => a.EmpId.Equals(employeeId)).ToList();
                }
                if (!string.IsNullOrEmpty(lastName))
                {
                    employees = employees.Where(a => a.LastName.ToLower().Contains(lastName.ToLower())).ToList();
                }

                return Ok(employees);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError,
                    "An internal error has occured. Please contact the system administrator");
            }
        }
    }
}
