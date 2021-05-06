using DAL;
using VastVoyages.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VastVoyages.Types;
using System.Data;

namespace VastVoyages.Repository
{
    public class ItemRepo
    {
        private DataAccess db;

        public ItemRepo()
        {
            db = new DataAccess();
        }

        #region Methods

        /// <summary>
        /// Retrive item list by purchase order number
        /// </summary>
        /// <param name="PONumber"></param>
        /// <returns></returns>
        public List<ItemDTO> RetrieveItemListByPONumber(int PONumber)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@PurchaseOrderNumber", PONumber, SqlDbType.Int));

            DataTable dt = db.Execute("spGetItemByPONumber", parms);

            List<ItemDTO> items = new List<ItemDTO>();

            foreach (DataRow row in dt.Rows)
            {
                items.Add(
                    new ItemDTO
                    {
                        ItemId = Convert.ToInt32(row["ItemId"]),
                        ItemName = row["ItemName"].ToString(),
                        ItemDescription = row["ItemDescription"].ToString(),
                        Justification = row["Justification"].ToString(),
                        Location = row["Location"].ToString(),
                        Price = Convert.ToDecimal(row["Price"]),
                        Quantity = Convert.ToInt32(row["Quantity"]),
                        PONumber = row["PONumber"].ToString(),
                        POStatusId = Convert.ToInt32(row["POStatusId"]),
                        ItemStatusId = Convert.ToInt32(row["ItemStatusId"]),
                        ItemStatus = row["ItemStatus"].ToString(),
                        DecisionReason = row["DescisionReason"].ToString()
                    }
                );
            }

            return items;
        }

        /// <summary>
        /// retrieve item by item id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public ItemDTO RetrieveItemByItemId(int itemId, int? employeeId, int? supervisorId)
        {
            List<ParmStruct> parms = new List<ParmStruct>();
            parms.Add(new ParmStruct("@ItemId", itemId, SqlDbType.Int));
            parms.Add(new ParmStruct("@EmployeeId", employeeId, SqlDbType.Int));
            parms.Add(new ParmStruct("@SupervisorId", supervisorId, SqlDbType.Int));

            DataTable dt = db.Execute("spGetItemByItemId", parms);

            List<ItemDTO> items = new List<ItemDTO>();

            foreach (DataRow row in dt.Rows)
            {
                items.Add(
                    new ItemDTO
                    {
                        ItemId = Convert.ToInt32(row["ItemId"]),
                        ItemName = row["ItemName"].ToString(),
                        ItemDescription = row["ItemDescription"].ToString(),
                        Justification = row["Justification"].ToString(),
                        Location = row["Location"].ToString(),
                        Price = Convert.ToDecimal(row["Price"]),
                        Quantity = Convert.ToInt32(row["Quantity"]),
                        PONumber = row["PONumber"].ToString(),
                        ItemStatusId = Convert.ToInt32(row["ItemStatusId"]),
                        ItemStatus = row["ItemStatus"].ToString(),
                        DecisionReason = row["DescisionReason"].ToString(),
                        RecordVersion = (byte[])row["RecordVersion"]
                    }
                );
            }

            return items.Count == 0 ? null : items[0];
        }

        /// <summary>
        /// Insert new item in db
        /// </summary>
        /// <param name="item"></param>
        /// <param name="PO"></param>
        /// <returns></returns>
        public Item Insert(Item item, PurchaseOrder PO)
        {
            List<ParmStruct> parms = new List<ParmStruct>();


            parms.Add(new ParmStruct("@ItemId", item.ItemId, SqlDbType.Int, 0, ParameterDirection.Output));
            parms.Add(new ParmStruct("@PORecordVersion", PO.RecordVersion, SqlDbType.Timestamp, 0, ParameterDirection.InputOutput));
            parms.Add(new ParmStruct("@PONumber", PO.PONumber, SqlDbType.Int));
            parms.Add(new ParmStruct("@ItemName", item.ItemName, SqlDbType.NVarChar, 50));
            parms.Add(new ParmStruct("@ItemDescription", item.ItemDescription, SqlDbType.NVarChar, 100));
            parms.Add(new ParmStruct("@Justification", item.Justification, SqlDbType.NVarChar, 80));
            parms.Add(new ParmStruct("@Location", item.Location, SqlDbType.NVarChar, 50));
            parms.Add(new ParmStruct("@Price", item.Price, SqlDbType.Decimal));
            parms.Add(new ParmStruct("@Quantity", item.Quantity, SqlDbType.Int));

            if (db.ExecuteNonQuery("spInsertItems", parms) > 0)
            {
                item.ItemId = Convert.ToInt32(parms.Where(p => p.Name == "@ItemId").FirstOrDefault().Value.ToString());
                item.PORecordVersion = (byte[])parms.Where(p => p.Name == "@PORecordVersion").FirstOrDefault().Value;
            }

            return item;
        }

        /// <summary>
        /// Update existing item in db
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Item Update(Item item)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@RecordVersion", item.RecordVersion, SqlDbType.Timestamp, 0, ParameterDirection.InputOutput));
            parms.Add(new ParmStruct("@PORecordVersion", item.PORecordVersion, SqlDbType.Timestamp, 0, ParameterDirection.InputOutput));
            parms.Add(new ParmStruct("@ItemId", item.ItemId, SqlDbType.Int));
            parms.Add(new ParmStruct("@ItemName", item.ItemName, SqlDbType.NVarChar, 50));
            parms.Add(new ParmStruct("@ItemDescription", item.ItemDescription, SqlDbType.NVarChar, 100));
            parms.Add(new ParmStruct("@Justification", item.Justification, SqlDbType.NVarChar, 80));
            parms.Add(new ParmStruct("@Location", item.Location, SqlDbType.NVarChar, 50));
            parms.Add(new ParmStruct("@Price", item.Price, SqlDbType.Decimal));
            parms.Add(new ParmStruct("@Quantity", item.Quantity, SqlDbType.Int));
            parms.Add(new ParmStruct("@DescisionReason", item.DecisionReason, SqlDbType.NVarChar, 255));
            parms.Add(new ParmStruct("@PONumber", item.PONumber, SqlDbType.Int));
            parms.Add(new ParmStruct("@ItemStatusId", item.ItemStatusId, SqlDbType.Int));

            if (db.ExecuteNonQuery("spUpdateItems", parms) > 0)
            {
                item.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;
                item.PORecordVersion = (byte[])parms.Where(p => p.Name == "@PORecordVersion").FirstOrDefault().Value;
            }

            return item;
        }

        /// <summary>
        /// Delete item if duplicated item is found when user update item
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public bool Delete (int itemId)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@ItemId", itemId, SqlDbType.Int));

            int retVal = db.ExecuteNonQuery("spDeleteItemByItemId", parms);

            return retVal > 0;
        }

        /// <summary>
        /// Find and retrieve duplicated item from db
        /// </summary>
        /// <param name="item"></param>
        /// <param name="PONumber"></param>
        /// <returns></returns>
        public Item FindDuplicatedItem(Item item, int PONumber)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@ItemId", item.ItemId, SqlDbType.Int));
            parms.Add(new ParmStruct("@PONumber", PONumber, SqlDbType.Int));
            parms.Add(new ParmStruct("@ItemName", item.ItemName, SqlDbType.NVarChar, 50));
            parms.Add(new ParmStruct("@ItemDescription", item.ItemDescription, SqlDbType.NVarChar, 100));
            parms.Add(new ParmStruct("@Justification", item.Justification, SqlDbType.NVarChar, 80));
            parms.Add(new ParmStruct("@Location", item.Location, SqlDbType.NVarChar, 50));
            parms.Add(new ParmStruct("@Price", item.Price, SqlDbType.Decimal));

            List<Item> items = new List<Item>();
            DataTable dt = db.Execute("spFindDuplicatedItems", parms);

            foreach (DataRow row in dt.Rows)
            {
                items.Add(
                    new Item
                    {
                        ItemId = Convert.ToInt32(row["ItemId"]),
                        ItemName = row["ItemName"].ToString(),
                        ItemDescription = row["ItemDescription"].ToString(),
                        Justification = row["Justification"].ToString(),
                        Location = row["Location"].ToString(),
                        Price = Convert.ToDecimal(row["Price"]),
                        Quantity = Convert.ToInt32(row["Quantity"]),
                        PONumber = Convert.ToInt32(row["PONumber"]),
                        ItemStatusId = Convert.ToInt32(row["ItemStatusId"]),
                        DecisionReason = row["DescisionReason"].ToString(),
                        RecordVersion = (byte[])row["RecordVersion"]
                    }
                );
            }

            return items.Count == 0 ? null : items[0];
        }


        public bool CheckHeadSupervisorIdOfItem(Item item, int supervisorId)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@PONumber", item.PONumber, SqlDbType.Int));
            parms.Add(new ParmStruct("@SupervisorId", supervisorId, SqlDbType.Int));

            DataTable dt = db.Execute("spCheckHeadSupervisorIdOfPO", parms);

            return supervisorId == Convert.ToInt32(dt.Rows[0]["HeadSupervisor"]);

        }
        #endregion

    }
}
