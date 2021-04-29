﻿using DAL;
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
                        SupervisorName = row["FirstName"].ToString() + row["LastName"].ToString()
                    }
                );
            }

            return supervisors;
        }
    }
}
