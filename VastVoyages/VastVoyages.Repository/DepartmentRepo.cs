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

            parms.Add(new ParmStruct("@DepartmentId", department.DepartmentId, SqlDbType.Int, 0, ParameterDirection.Output));
            parms.Add(new ParmStruct("@DepartmentName", department.DepartmentName, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@DepartmentDescription", department.DepartmentDescription, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@InvocationDate", department.InvocationDate, SqlDbType.DateTime2));

            if (db.ExecuteNonQuery("spInsertDepartment", parms) > 0)
            {
                department.DepartmentId = (int)parms.Where(p => p.Name == "@DepartmentId").FirstOrDefault().Value;
                return true;
            }

            return false;
        }
        #endregion
    }
}
