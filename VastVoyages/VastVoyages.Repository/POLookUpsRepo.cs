using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using VastVoyages.Model;
using System.Data;

namespace VastVoyages.Repository
{
    public class POLookUpsRepo
    {
        private DataAccess db;
        public POLookUpsRepo()
        {
            db = new DataAccess();
        }

        /// <summary>
        /// Retrieve purchase order status list
        /// </summary>
        /// <returns></returns>
        public List<POStatusLookUpsDTO> RetrievePOStatus()
        {
            DataTable dt = db.Execute("spGetPOStatusForLookup");

            List<POStatusLookUpsDTO> POStatus = new List<POStatusLookUpsDTO>();

            foreach (DataRow row in dt.Rows)
            {
                POStatus.Add(
                    new POStatusLookUpsDTO
                    {
                        POStatusId = Convert.ToInt32(row["POStatusId"]),
                        POStatus = row["POStatus"].ToString()
                    }
                );
            }

            return POStatus;
        }

        /// <summary>
        /// Retrieve item status list
        /// </summary>
        /// <returns></returns>
        public List<ItemStatusLookUpsDTO> RetrieveItemStatus()
        {
            DataTable dt = db.Execute("spGetItemStatusForLookup");

            List<ItemStatusLookUpsDTO> itemStatus = new List<ItemStatusLookUpsDTO>();

            foreach (DataRow row in dt.Rows)
            {
                itemStatus.Add(
                    new ItemStatusLookUpsDTO
                    {
                        ItemStatusId = Convert.ToInt32(row["ItemStatusId"]),
                        ItemStatus = row["ItemStatus"].ToString()
                    }
                );
            }

            return itemStatus;
        }

    }
}
