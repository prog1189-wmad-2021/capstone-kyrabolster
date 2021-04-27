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

        public bool Login(LoginDTO loginInfo)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@EmployeeId", loginInfo.EmployeeId, SqlDbType.Int));
            parms.Add(new ParmStruct("@Password", loginInfo.Password, SqlDbType.NVarChar, 100));

            DataTable dt = db.Execute("spLogin", parms);

            return Convert.ToInt32(dt.Rows[0]["EmployeeId"]) > 0;
        }

        public EmployeeDTO RetrieveEmpInfoById(int employeeId)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@EmployeeId", employeeId, SqlDbType.Int));

            DataTable dt = db.Execute("spGetEmployeeById", parms);

            EmployeeDTO emp = new EmployeeDTO
            {
                EmployeeId = Convert.ToInt32(dt.Rows[0]["EmployeeId"]),
                UserName = dt.Rows[0]["UserName"].ToString(),
                FirstName = dt.Rows[0]["FirstName"].ToString(),
                MiddleInit = dt.Rows[0]["MiddleInit"].ToString(),
                LastName = dt.Rows[0]["LastName"].ToString(),
                Job = dt.Rows[0]["JobAssignment"].ToString(),
                DepartmentId = Convert.ToInt32(dt.Rows[0]["DepartmentId"]),
                Department = dt.Rows[0]["DepartmentName"].ToString(),
                SupervisorId = Convert.ToInt32(dt.Rows[0]["SupervisorId"]),
                Supervisor = dt.Rows[0]["Supervisor"].ToString()
            };

            return emp;
        }
    }
}
