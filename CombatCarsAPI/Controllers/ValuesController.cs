using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CombatCarsAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {

            string s = @"Data Source=msdb6.surftown.se;Initial Catalog=fauser7_combatcars;Persist Security Info=True;User ID=fauser7_combatcars;Password=combat1234";
            using (SqlConnection conn = new SqlConnection(s))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM VEHICLE", conn);

                List<string> ss = new List<string>();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ss.Add(reader["VehicleId"].ToString());
                        ss.Add((string)reader["Name"]);
                    }

                    return ss.ToArray();
                }
            }

            return new string[] { "Empty" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            string s = @"Data Source=localhost;Initial Catalog=fauser7_combatcars;Persist Security Info=True;User ID=fauser7_combatcars;Password=combat1234";
            using (SqlConnection conn = new SqlConnection(s))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM VEHICLE", conn);

                List<string> ss = new List<string>();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ss.Add(reader["VehicleId"].ToString());
                        ss.Add((string)reader["Name"]);
                    }

                    return ss.ToArray().First(); ;
                }
            }

            return "Empty";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}