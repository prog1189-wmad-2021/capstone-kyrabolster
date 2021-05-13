using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VastVoyages.Model;
using VastVoyages.Types;

namespace VastVoyages.Repository
{
    public class DepartmentRepo
    {
        DataAccess db = new DataAccess();

        #region Public Methods

        /// <summary>
        /// Add new department to the database
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public bool AddDepartment(Department department)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@RecordVersion", department.RecordVersion, SqlDbType.Timestamp, 0, ParameterDirection.Output));
            parms.Add(new ParmStruct("@DepartmentId", department.DepartmentId, SqlDbType.Int, 0, ParameterDirection.Output));
            parms.Add(new ParmStruct("@DepartmentName", department.DepartmentName, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@DepartmentDescription", department.DepartmentDescription, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@InvocationDate", department.InvocationDate, SqlDbType.DateTime2));

            if (db.ExecuteNonQuery("spInsertDepartment", parms) > 0)
            {
                department.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;
                department.DepartmentId = (int)parms.Where(p => p.Name == "@DepartmentId").FirstOrDefault().Value;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Delete Department
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public bool DeleteDepartment(Department department)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@DepartmentId", department.DepartmentId, SqlDbType.Int));

            return (db.ExecuteNonQuery("spDeleteDepartment", parms) > 0);
        }

        /// <summary>
        /// Update Department
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public Department UpdateDepartment(Department department)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@RecordVersion", department.RecordVersion, SqlDbType.Timestamp, 0, ParameterDirection.InputOutput));
            parms.Add(new ParmStruct("@DepartmentId", department.DepartmentId, SqlDbType.Int));
            parms.Add(new ParmStruct("@DepartmentName", department.DepartmentName, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@DepartmentDescription", department.DepartmentDescription, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@InvocationDate", department.InvocationDate, SqlDbType.DateTime2));

            if (db.ExecuteNonQuery("spUpdateDepartment", parms) > 0)
            {
                department.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;
            }

            return department;
        }

        /// <summary>
        /// Retrieve Employees by Department
        /// </summary>
        /// <param name="deparmentId"></param>
        /// <returns></returns>
        public List<EmployeeDTO> RetrieveEmployeesInDepartment(int deparmentId)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@DepartmentId", deparmentId, SqlDbType.Int));

            DataTable dt = db.Execute("spGetEmployeesByDepartment", parms);

            List<EmployeeDTO> employees = new List<EmployeeDTO>();

            foreach (DataRow row in dt.Rows)
            {
                employees.Add(
                    new EmployeeDTO
                    {
                        EmpId = Convert.ToInt32(row["EmployeeId"]),
                    }
                );
            }

            return employees;
        }

        /// <summary>
        /// Retrieve all Departments
        /// </summary>
        /// <returns></returns>
        public List<Department> RetrieveDepartments()
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            DataTable dt = db.Execute("spGetDepartments", parms);

            List<Department> departments = new List<Department>();

            foreach (DataRow row in dt.Rows)
            {
                departments.Add(
                    new Department
                    {
                        DepartmentId = Convert.ToInt32(row["DepartmentId"]),
                        RecordVersion = (byte[])row["RecordVersion"],
                        DepartmentName = row["DepartmentName"].ToString(),
                        DepartmentDescription = row["DepartmentDescription"].ToString(),
                        InvocationDate = Convert.ToDateTime(row["InvocationDate"])
                    }
                );
            }

            return departments;
        }

        /// <summary>
        /// Retrieve department by Id
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public Department GetDepartmentById(int departmentId)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@DepartmentId", departmentId, SqlDbType.Int)
            };

            DataTable dt = db.Execute("spGetDepartmentById", parms);

            return dt.Rows.Count > 0 ? PopulateDepartment(dt.Rows[0]) : null;
        }

        #endregion

        #region Private Methods
        private Department PopulateDepartment(DataRow row)
        {
            return new Department
            {
                DepartmentId = Convert.ToInt32(row["DepartmentId"]),
                RecordVersion = (byte[])row["RecordVersion"],
                DepartmentName = row["DepartmentName"].ToString(),
                DepartmentDescription = row["DepartmentDescription"].ToString(),
                InvocationDate = Convert.ToDateTime(row["InvocationDate"])
            };
        }

        #endregion
    }
}
