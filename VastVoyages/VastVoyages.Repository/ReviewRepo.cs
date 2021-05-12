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
    public class ReviewRepo
    {
        DataAccess db = new DataAccess();

        /// <summary>
        /// Add Review
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        public bool AddReview(Review review)
        {
            List<ParmStruct> parms = new List<ParmStruct>();

            parms.Add(new ParmStruct("@EmployeeReviewId", review.EmployeeReviewId, SqlDbType.Int, 0, ParameterDirection.Output));
            parms.Add(new ParmStruct("@Date", review.ReviewDate, SqlDbType.DateTime2));
            parms.Add(new ParmStruct("@Comment", review.Comment, SqlDbType.NVarChar));
            parms.Add(new ParmStruct("@EmployeeId", review.EmployeeId, SqlDbType.Int));
            parms.Add(new ParmStruct("@ReviewerId", review.ReviewerId, SqlDbType.Int));
            parms.Add(new ParmStruct("@RatingId", review.RatingId, SqlDbType.Int));

            if (db.ExecuteNonQuery("spInsertReview", parms) > 0)
            {
                review.EmployeeReviewId = (int)parms.Where(p => p.Name == "@EmployeeReviewId").FirstOrDefault().Value;
                return true;
            }

            return false;
        }

        public List<Review> RetrieveEmployeeReviews(int employeeId)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@EmployeeId", employeeId, SqlDbType.Int)
            };

            DataTable dt = db.Execute("spGetEmployeeReviews", parms);

            List<Review> reviews = new List<Review>();

            foreach (DataRow row in dt.Rows)
            {
                reviews.Add(new Review
                {
                    EmployeeReviewId = Convert.ToInt32(row["EmployeeReviewId"]),
                    ReviewDate = Convert.ToDateTime(row["Date"]),
                    Comment = row["Comment"].ToString(),
                    EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                    ReviewerId = Convert.ToInt32(row["ReviewerId"]),
                    RatingId = Convert.ToInt32(row["RatingId"]), //convert to string???
                });
            }

            return reviews;
        }

        public Review RetrieveReview(int reviewId)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@ReviewId", reviewId, SqlDbType.Int)
            };

            DataTable dt = db.Execute("spGetReviewById", parms);

            DataRow row = dt.Rows[0];

            Review review = new Review
            {
                EmployeeReviewId = Convert.ToInt32(row["EmployeeReviewId"]),
                ReviewDate = Convert.ToDateTime(row["Date"]),
                Comment = row["Comment"].ToString(),
                EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                ReviewerId = Convert.ToInt32(row["ReviewerId"]),
                RatingId = Convert.ToInt32(row["RatingId"]),
                SupervisorFirstName = row["FirstName"].ToString(),
                SupervisorMiddleInitial = row["MiddleInit"].ToString(),
                SupervisorLastName = row["LastName"].ToString(),
            };

            return review;
        }
    }
}
