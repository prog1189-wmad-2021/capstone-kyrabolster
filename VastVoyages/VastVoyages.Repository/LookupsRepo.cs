using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VastVoyages.Model.DTO;
using VastVoyages.Types;

namespace VastVoyages.Repository
{
    public class LookupsRepo
    {
        DataAccess db = new DataAccess();

        /// <summary>
        /// Retrieve all job assignments
        /// </summary>
        /// <returns></returns>
        public List<JobAssignmentsLookupsDTO> RetrieveJobAssignments()
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            DataTable dt = db.Execute("spGetJobAssignments", parms);

            List<JobAssignmentsLookupsDTO> jobAssignments = new List<JobAssignmentsLookupsDTO>();

            foreach (DataRow row in dt.Rows)
            {
                jobAssignments.Add(
                    new JobAssignmentsLookupsDTO
                    {
                        JobAssignmentId = Convert.ToInt32(row["JobAssignmentId"]),
                        JobAssignment = row["JobAssignment"].ToString()
                    }
                );
            }

            return jobAssignments;
        }

        /// <summary>
        /// Retrieve all Departments
        /// </summary>
        /// <returns></returns>
        public List<DepartmentLookupsDTO> RetrieveDepartments()
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            DataTable dt = db.Execute("spGetDepartments", parms);

            List<DepartmentLookupsDTO> departments = new List<DepartmentLookupsDTO>();

            foreach (DataRow row in dt.Rows)
            {
                departments.Add(
                    new DepartmentLookupsDTO
                    {
                        DepartmentId = Convert.ToInt32(row["DepartmentId"]),
                        DepartmentName = row["DepartmentName"].ToString()
                    }
                );
            }

            return departments;
        }

        /// <summary>
        /// Retrieve all supervisors
        /// Supervisors are characterised as employees whose supervisor is the CEO,
        /// or the CEO themselves.
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public List<SupervisorLookupsDTO> RetrieveSupervisors(int departmentId)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@DepartmentId", departmentId, SqlDbType.Int));

            DataTable dt = db.Execute("spGetSupervisors", parms);

            List<SupervisorLookupsDTO> supervisors = new List<SupervisorLookupsDTO>();

            foreach (DataRow row in dt.Rows)
            {
                supervisors.Add(
                    new SupervisorLookupsDTO
                    {
                        SupervisorId = Convert.ToInt32(row["EmployeeId"]),
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        MiddleInitial = row["MiddleInit"].ToString()
                    }
                );
            }

            return supervisors;
        }

        /// <summary>
        /// Get head supervisor for the department
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public List<SupervisorLookupsDTO> RetrieveHeadSupervisor(int departmentId)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@DepartmentId", departmentId, SqlDbType.Int));

            DataTable dt = db.Execute("spGetHeadSupervisor", parms);

            List<SupervisorLookupsDTO> supervisors = new List<SupervisorLookupsDTO>();

            foreach (DataRow row in dt.Rows)
            {
                supervisors.Add(
                    new SupervisorLookupsDTO
                    {
                        SupervisorId = Convert.ToInt32(row["EmployeeId"]),
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        MiddleInitial = row["MiddleInit"].ToString()
                    }
                );
            }

            return supervisors;
        }

        /// <summary>
        /// Retrieve CEO
        /// </summary>
        /// <returns></returns>
        public List<SupervisorLookupsDTO> RetrieveCEO()
        {
            DataTable dt = db.Execute("spGetCEO");

            List<SupervisorLookupsDTO> supervisors = new List<SupervisorLookupsDTO>();

            foreach (DataRow row in dt.Rows)
            {
                supervisors.Add(
                    new SupervisorLookupsDTO
                    {
                        SupervisorId = Convert.ToInt32(row["EmployeeId"]),
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        MiddleInitial = row["MiddleInit"].ToString()
                    }
                );
            }

            return supervisors;
        }

        /// <summary>
        /// Get Employee Statuses
        /// </summary>
        /// <returns></returns>
        public List<EmployeeStatusLookupsDTO> RetrieveEmployeeStatus()
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            DataTable dt = db.Execute("spGetEmployeeStatus", parms);

            List<EmployeeStatusLookupsDTO> employeeStatus = new List<EmployeeStatusLookupsDTO>();

            foreach (DataRow row in dt.Rows)
            {
                employeeStatus.Add(
                    new EmployeeStatusLookupsDTO
                    {
                        EmployeeStatusId = Convert.ToInt32(row["EmployeeStatusId"]),
                        EmployeeStatus = row["EmployeeStatus"].ToString()
                    }
                );
            }

            return employeeStatus;
        }
    }
}
