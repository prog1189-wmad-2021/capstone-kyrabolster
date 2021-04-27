using DAL;
using VastVoyages.Types;
using VastVoyages.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace VastVoyages.Repository
{
    public class LoginRepo
    {
        private DataAccess db;

        public LoginRepo()
        {
            db = new DataAccess();
        }

        public bool Login(Login loginInfo)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@EmployeeId", loginInfo.EmployeeId, SqlDbType.NVarChar, 8));
            parms.Add(new ParmStruct("@Password", loginInfo.Password, SqlDbType.NVarChar, 100));

            DataTable dt = db.Execute("spLogin", parms);

            return Convert.ToInt32(dt.Rows[0]["EmployeeId"]) > 0;
        }

        public EmployeeDTO RetrieveEmpInfoById(string employeeId)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@EmployeeId", employeeId, SqlDbType.NVarChar, 8));

            DataTable dt = db.Execute("spGetEmployeeById", parms);

            EmployeeDTO emp = new EmployeeDTO
            {
                EmployeeId = dt.Rows[0]["EmployeeId"].ToString(),
                UserName = dt.Rows[0]["UserName"].ToString(),
                FirstName = dt.Rows[0]["FirstName"].ToString(),
                MiddleInit = dt.Rows[0]["MiddleInit"].ToString(),
                LastName = dt.Rows[0]["LastName"].ToString(),
                Job = dt.Rows[0]["JobAssignment"].ToString(),
                DepartmentId = Convert.ToInt32(dt.Rows[0]["DepartmentId"]),
                Department = dt.Rows[0]["DepartmentName"].ToString(),
                SupervisorId = Convert.ToInt32(dt.Rows[0]["SupervisorId"] != DBNull.Value ? dt.Rows[0]["SupervisorId"] : 0),
                Supervisor = dt.Rows[0]["Supervisor"] != DBNull.Value ? dt.Rows[0]["Supervisor"].ToString() : "",
                Role = dt.Rows[0]["Role"].ToString()
            };

            return emp;
        }
    }
}
