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
            HashCode hc = new HashCode();
            string hasedPassword = hc.CalculateSHA256(loginInfo.Password);
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@EmployeeId", loginInfo.EmployeeId, SqlDbType.Int));
            parms.Add(new ParmStruct("@Password", hasedPassword, SqlDbType.NVarChar, int.MaxValue));

            DataTable dt = db.Execute("spLogin", parms);

            return Convert.ToInt32(dt.Rows[0]["EmployeeId"]) > 0;
        }

        public LoginDTO RetrieveEmpInfoById(string employeeId)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@EmployeeId", employeeId, SqlDbType.NVarChar, 8));

            DataTable dt = db.Execute("spGetEmployeeById", parms);

            LoginDTO loginInfo = new LoginDTO
            {
                EmployeeId = dt.Rows[0]["EmployeeId"].ToString(),
                UserName = dt.Rows[0]["UserName"].ToString(),
                FullName = dt.Rows[0]["FullName"].ToString(),
                Job = dt.Rows[0]["JobAssignment"].ToString(),
                DepartmentId = Convert.ToInt32(dt.Rows[0]["DepartmentId"]),
                Department = dt.Rows[0]["DepartmentName"].ToString(),
                SupervisorId = dt.Rows[0]["SupervisorId"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["SupervisorId"]) : 0,
                Supervisor = dt.Rows[0]["Supervisor"] != DBNull.Value ? dt.Rows[0]["Supervisor"].ToString() : "",
                IsHeadSupervisor = dt.Rows[0]["IsHeadSupervisor"] != DBNull.Value ? true : false
            };

            return loginInfo;
        }
    }
}
