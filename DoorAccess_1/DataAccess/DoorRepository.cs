using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DoorAccess_1.Models;

namespace DoorAccess_1.DataAccess
{
    public class DoorRepository
    {
        public List<Door> GetAll()
        {
            var result = new List<Door>();

            using (SqlConnection con = Db.CreateConnection())
            using (SqlCommand cmd = new SqlCommand(
                "SELECT DoorId, DoorName, Location FROM dbo.Door ORDER BY DoorName", con))
            {
                con.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        result.Add(new Door
                        {
                            DoorId = (int)r["DoorId"],
                            DoorName = r["DoorName"].ToString(),
                            Location = r["Location"] == System.DBNull.Value ? null : r["Location"].ToString()
                        });
                    }
                }
            }

            return result;
        }
    }
}
