﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VastVoyages.Model;
using VastVoyages.Service;
using VastVoyages.Web.CustomAuthoize;

namespace VastVoyages.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private EmployeeService service = new EmployeeService();

        // GET: Employees
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.HREmployee)]
        public ActionResult Index(string search)
        {
            try
            {
                //List<EmployeeDTO> employees = service.GetAllEmployees();
                List<EmployeeDTO> employees = new List<EmployeeDTO>();

                ViewBag.CurrentFilter = search;

                if (!string.IsNullOrEmpty(search))
                {
                    if (int.TryParse(search, out int empId))
                    {
                        employees = service.SearchEmployeesById(empId);
                    }
                    else
                    {
                        employees = service.SearchEmployeesByLastName(search);
                    }

                    if (employees.Count() == 0)
                    {
                        ViewBag.Employees = "No Employees matching your search criteria.";
                    }
                }
                else
                {
                    ViewBag.Employees = "Please enter and Employee Id, last name, or partial last name to search.";
                }

                return View(employees);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Employee", "Edit"));
            }
        }

        // GET: Employees/Details/5
        [CustomizeAuthorize(RoleName.CEO, RoleName.HRSupervisor, RoleName.HREmployee)]
        public ActionResult Details(int? employeeId)
        {
            try
            {
                if (employeeId == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                List<EmployeeDTO> employeeDetails = service.SearchEmployeesById(employeeId.Value);

                if (employeeDetails.Count <= 0)
                    return HttpNotFound();

                EmployeeDTO employee = new EmployeeDTO()
                {
                    EmpId = employeeId.Value,
                    FirstName = employeeDetails[0].FirstName,
                    MiddleInitial = employeeDetails[0].MiddleInitial,
                    LastName = employeeDetails[0].LastName,

                    Street = employeeDetails[0].Street,
                    City = employeeDetails[0].City,
                    Province = employeeDetails[0].Province,
                    Country = employeeDetails[0].Country,
                    PostalCode = employeeDetails[0].PostalCode,

                    WorkPhone = employeeDetails[0].WorkPhone,
                    CellPhone = employeeDetails[0].CellPhone,
                    Email = employeeDetails[0].Email
                };

                return View(employee);
            }
            catch (Exception ex)
            {
                return View("Error", new HandleErrorInfo(ex, "Employee", "Details"));
            }
        }
    }
}