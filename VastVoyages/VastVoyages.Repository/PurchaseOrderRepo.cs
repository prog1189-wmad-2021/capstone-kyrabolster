using DAL;
using VastVoyages.Model;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VastVoyages.Types;

namespace VastVoyages.Repository
{
    public class PurchaseOrderRepo
    {
        private DataAccess db;

        public PurchaseOrderRepo()
        {
            db = new DataAccess();
        }

        #region Methods

        /// <summary>
        /// Retrieve Purchase order list by employee Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public List<PurchaseOrderDTO> RetrievePurchaseOrderList(int employeeId)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@EmployeeId", employeeId, SqlDbType.Int));

            DataTable dt = db.Execute("spGetPurchaseOrderByEmployee", parms);

            List<PurchaseOrderDTO> purchaseOrders = new List<PurchaseOrderDTO>();

            foreach(DataRow row in dt.Rows)
            {
                purchaseOrders.Add(
                    new PurchaseOrderDTO
                    {
                        PONumber = row["PONumber"].ToString(),
                        SubmissionDate = row["SubmissionDate"] == DBNull.Value ? (DateTime?) null : Convert.ToDateTime(row["SubmissionDate"]),
                        SubTotal = Convert.ToDecimal(row["SubTotal"]),
                        Tax = Convert.ToDecimal(row["Tax"]),
                        Total = Convert.ToDecimal(row["Total"]),
                        Employee = row["Employee"].ToString(),
                        Supervisor = row["Supervisor"].ToString(),
                        POStatus = row["POStatus"].ToString()
                    }                    
                );
            }

            return purchaseOrders;
        }

        /// <summary>
        /// Retrieve Purchase order by PO Number
        /// </summary>
        /// <param name="PONumber"></param>
        /// <returns></returns>
        public PurchaseOrderDTO RetrievePurchaseOrderByPONumber(int PONumber)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@PurchaseOrderNumber", PONumber, SqlDbType.Int));

            DataTable dt = db.Execute("spGetPurchaseOrderByPOnumber", parms);

            List<PurchaseOrderDTO> purchaseOrders = new List<PurchaseOrderDTO>();

            foreach (DataRow row in dt.Rows)
            {
                purchaseOrders.Add(
                    new PurchaseOrderDTO
                    {
                        PONumber = row["PONumber"].ToString(),                        
                        SubmissionDate = dt.Rows[0]["SubmissionDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dt.Rows[0]["SubmissionDate"]),
                        SubTotal = Convert.ToDecimal(row["SubTotal"]),
                        Tax = Convert.ToDecimal(row["Tax"]),
                        Total = Convert.ToDecimal(row["Total"]),
                        Employee = row["Employee"].ToString(),
                        Supervisor = row["Supervisor"].ToString(),
                        POStatus = row["POStatus"].ToString(),
                        RecordVersion = (byte[])row["RecordVersion"]
                    }
                );
            }

            return purchaseOrders.Count == 0 ? null : purchaseOrders[0];
        }

        /// <summary>
        /// Insert new purchase order object to db when fist item added
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <returns></returns>
        public PurchaseOrder Insert(PurchaseOrder purchaseOrder)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@RecordVersion", purchaseOrder.RecordVersion, SqlDbType.Timestamp, 0, ParameterDirection.Output));
            parms.Add(new ParmStruct("@PONumberOutputParm", purchaseOrder.PONumber, SqlDbType.Int, 0, ParameterDirection.Output));
            parms.Add(new ParmStruct("@EmployeeId", purchaseOrder.employeeId, SqlDbType.NVarChar, 8));
            parms.Add(new ParmStruct("@ItemName", purchaseOrder.items[0].ItemName, SqlDbType.NVarChar, 50));
            parms.Add(new ParmStruct("@ItemDescription", purchaseOrder.items[0].ItemDescription, SqlDbType.NVarChar, 100));
            parms.Add(new ParmStruct("@Justification", purchaseOrder.items[0].Justification, SqlDbType.NVarChar, 80));
            parms.Add(new ParmStruct("@Location", purchaseOrder.items[0].Location, SqlDbType.NVarChar, 50));
            parms.Add(new ParmStruct("@Price", purchaseOrder.items[0].Price, SqlDbType.Decimal));
            parms.Add(new ParmStruct("@Quantity", purchaseOrder.items[0].Quantity, SqlDbType.Int));
            parms.Add(new ParmStruct("@SubTotal", purchaseOrder.SubTotal, SqlDbType.Decimal));
            parms.Add(new ParmStruct("@Tax", purchaseOrder.Tax, SqlDbType.Decimal));

            if(db.ExecuteNonQuery("spInsertPurchaseOrder", parms) > 0)
            {
                purchaseOrder.PONumber = parms.Where(p => p.Name == "@PONumberOutputParm").FirstOrDefault().Value.ToString();
                purchaseOrder.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;
            }

            return purchaseOrder;
        }

        /// <summary>
        /// Update purchase order or final submit
        /// </summary>
        /// <param name="purchaseOrder"></param>
        /// <returns></returns>
        public PurchaseOrder Update(PurchaseOrder purchaseOrder)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@RecordVersion", purchaseOrder.RecordVersion, SqlDbType.Timestamp, 0, ParameterDirection.Output));
            parms.Add(new ParmStruct("@PONumber", purchaseOrder.PONumber, SqlDbType.Int));
            parms.Add(new ParmStruct("@SubTotal", purchaseOrder.SubTotal, SqlDbType.Decimal));
            parms.Add(new ParmStruct("@Tax", purchaseOrder.Tax, SqlDbType.Decimal));

            if (db.ExecuteNonQuery("spSubmitPurchaseOrder", parms) > 0)
            {
                purchaseOrder.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;
            }

            return purchaseOrder;

        }
        #endregion

    }
}
