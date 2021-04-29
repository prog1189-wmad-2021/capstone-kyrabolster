using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VastVoyages.Model.Entities;
using VastVoyages.Types;

namespace VastVoyages.Repository
{
    public class EmployeeRepo
    {
        DataAccess db = new DataAccess();

        #region Public Methods

        public bool AddEmployee(Employee employee)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@RecordVersion", employee.RecordVersion, SqlDbType.Timestamp, 0, ParameterDirection.Output));
            parms.Add(new ParmStruct("@EmployeeId", employee.EmployeeId, SqlDbType.Int, 0, ParameterDirection.Output));
            parms.Add(new ParmStruct("@UserName", employee.UserName, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@FirstName", employee.FirstName, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@LastName", employee.LastName, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@MiddleInit", employee.MiddleInitial, SqlDbType.NVarChar));

            parms.Add(new ParmStruct("@DateOfBirth", employee.DateOfBirth, SqlDbType.DateTime));
            parms.Add(new ParmStruct("@Street", employee.Street, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@City", employee.City, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@Province", employee.Province, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@Country", employee.Country, SqlDbType.NVarChar));

            parms.Add(new ParmStruct("@PostalCode", employee.PostalCode, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@WorkPhone", employee.WorkPhone, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@CellPhone", employee.CellPhone, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@Email", employee.Email, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@JobStartDate", employee.JobStartDate, SqlDbType.DateTime));

            parms.Add(new ParmStruct("@SeniorityDate", employee.SeniorityDate, SqlDbType.DateTime));
            parms.Add(new ParmStruct("@SIN", employee.SIN, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@SupervisorId", employee.SupervisorId, SqlDbType.Int));
            parms.Add(new ParmStruct("@DepartmentId", employee.DepartmentId, SqlDbType.Int));
            parms.Add(new ParmStruct("@EmployeeStatusId", employee.EmployeeStatusId, SqlDbType.Int));
            parms.Add(new ParmStruct("@JobAssignmentId", employee.JobAssignmentId, SqlDbType.Int));

            if (db.ExecuteNonQuery("spInsertEmployee", parms) > 0)
            {
                employee.EmployeeId = (int)parms.Where(p => p.Name == "@EmployeeId").FirstOrDefault().Value;
                return true;
            }

            return false;
        }

        public int CheckDuplicateUsername(string username)
        {
            int uNameCount = 0;

            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@UserName", username, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@UNameCount", uNameCount, SqlDbType.Int, 0, ParameterDirection.Output));

            db.ExecuteNonQuery("spCheckDuplicateUsername", parms);

            uNameCount = (int)parms.Where(p => p.Name == "@UNameCount").FirstOrDefault().Value;

            return uNameCount;
        }

        #endregion
    }
}
