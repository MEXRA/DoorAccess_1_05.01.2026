using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DoorAccess_1.Models;

namespace DoorAccess_1.DataAccess
{
    public class AccessRepository
    {
        public void Insert(AccessRecord rec)
        {
            const string sql = @"
INSERT INTO dbo.Table_1
(PersonID, PersonName, AccesTimeDate, DoorId, IsDeleted, CreatedDate, UpdatedDate)
VALUES
(@PersonID, @PersonName, @AccessTime, @DoorId, 0, GETDATE(), NULL);";

            using (SqlConnection con = Db.CreateConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@PersonID", rec.PersonID);
                cmd.Parameters.AddWithValue("@PersonName", rec.PersonName);
                cmd.Parameters.AddWithValue("@AccessTime", rec.AccesTimeDate);
                cmd.Parameters.AddWithValue("@DoorId", (object)rec.DoorId ?? DBNull.Value);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public bool Update(AccessRecord rec)
        {
            const string sql = @"
UPDATE dbo.Table_1
SET PersonID = @PersonID,
    PersonName = @PersonName,
    AccesTimeDate = @AccessTime,
    DoorId = @DoorId,
    UpdatedDate = GETDATE()
WHERE RecordID = @RecordID;";

            using (SqlConnection con = Db.CreateConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@PersonID", rec.PersonID);
                cmd.Parameters.AddWithValue("@PersonName", rec.PersonName);
                cmd.Parameters.AddWithValue("@AccessTime", rec.AccesTimeDate);
                cmd.Parameters.AddWithValue("@DoorId", (object)rec.DoorId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@RecordID", rec.RecordID);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public AccessRecord GetById(int recordId)
        {
            const string sql = @"
SELECT RecordID, PersonID, PersonName, AccesTimeDate, DoorId
FROM dbo.Table_1
WHERE RecordID = @RecordID AND IsDeleted = 0;";

            using (SqlConnection con = Db.CreateConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@RecordID", recordId);

                con.Open();
                using (var r = cmd.ExecuteReader())
                {
                    if (!r.Read()) return null;

                    return new AccessRecord
                    {
                        RecordID = (int)r["RecordID"],
                        PersonID = r["PersonID"].ToString(),
                        PersonName = r["PersonName"].ToString(),
                        AccesTimeDate = Convert.ToDateTime(r["AccesTimeDate"]),
                        DoorId = r["DoorId"] == DBNull.Value ? (int?)null : Convert.ToInt32(r["DoorId"])
                    };
                }
            }
        }

        public List<AccessRecord> List(bool includeDeleted)
        {
            string sql = includeDeleted
                ? @"
SELECT t.RecordID, t.PersonID, t.PersonName, t.AccesTimeDate, t.DoorId,
       t.IsDeleted, t.CreatedDate, t.UpdatedDate,
       d.DoorName
FROM dbo.Table_1 t
LEFT JOIN dbo.Door d ON d.DoorId = t.DoorId
ORDER BY t.AccesTimeDate DESC;"
                : @"
SELECT t.RecordID, t.PersonID, t.PersonName, t.AccesTimeDate, t.DoorId,
       t.IsDeleted, t.CreatedDate, t.UpdatedDate,
       d.DoorName
FROM dbo.Table_1 t
LEFT JOIN dbo.Door d ON d.DoorId = t.DoorId
WHERE t.IsDeleted = 0
ORDER BY t.AccesTimeDate DESC;";

            var result = new List<AccessRecord>();

            using (SqlConnection con = Db.CreateConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        result.Add(new AccessRecord
                        {
                            RecordID = (int)r["RecordID"],
                            PersonID = r["PersonID"].ToString(),
                            PersonName = r["PersonName"].ToString(),
                            AccesTimeDate = Convert.ToDateTime(r["AccesTimeDate"]),
                            DoorId = r["DoorId"] == DBNull.Value ? (int?)null : Convert.ToInt32(r["DoorId"]),
                            IsDeleted = Convert.ToBoolean(r["IsDeleted"]),
                            CreatedDate = Convert.ToDateTime(r["CreatedDate"]),
                            UpdatedDate = r["UpdatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["UpdatedDate"]),
                            DoorName = r["DoorName"] == DBNull.Value ? null : r["DoorName"].ToString()
                        });
                    }
                }
            }

            return result;
        }

        public bool SoftDelete(int recordId)
        {
            const string sql = @"
UPDATE dbo.Table_1
SET IsDeleted = 1,
    UpdatedDate = GETDATE()
WHERE RecordID = @RecordID;";

            using (SqlConnection con = Db.CreateConnection())
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@RecordID", recordId);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}

