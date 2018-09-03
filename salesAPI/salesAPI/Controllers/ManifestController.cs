using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using salesAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace salesAPI.Controllers
{
    public class ManifestController : ApiController
    {
        /* http://localhost:50804/api/manifest/1 */
        // GET api/manifest/5
        [ActionName("Getmanifest")]
        public DataTable Getmanifest(int Id, string val, int flag)
        {
            DataTable dt = new DataTable();
            ConnectionStringSettings objcon = ConfigurationManager.ConnectionStrings["ConnString"];
            SqlConnection sqlConn = new SqlConnection();
            sqlConn = new SqlConnection(objcon.ConnectionString);
            using (SqlCommand cmd = new SqlCommand("sp_mobile_searchInmanisfest", sqlConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userId", SqlDbType.Int);
                cmd.Parameters["@userId"].Value = Id;
                cmd.Parameters.Add("@cmlno", SqlDbType.VarChar);
                cmd.Parameters["@cmlno"].Value = val;
                cmd.Parameters.Add("@flag", SqlDbType.Int);
                cmd.Parameters["@flag"].Value = flag;
                sqlConn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    dt.Load(sdr);
                }
                sqlConn.Close();
            }
            return dt;
        }
        [HttpPost]
        public IHttpActionResult AddManifest([FromBody] manifest man)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                ConnectionStringSettings objcon = ConfigurationManager.ConnectionStrings["ConnString"];
                SqlConnection sqlConn = new SqlConnection();
                sqlConn = new SqlConnection(objcon.ConnectionString);
                using (SqlCommand cmd = new SqlCommand("sp_mobile_addmanifest", sqlConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@cmlno", SqlDbType.VarChar);
                    cmd.Parameters["@cmlno"].Value = man.Cmlno;
                    cmd.Parameters.Add("@customername", SqlDbType.VarChar);
                    cmd.Parameters["@customername"].Value = man.Name;
                    cmd.Parameters.Add("@productname", SqlDbType.VarChar);
                    cmd.Parameters["@productname"].Value = man.Productname;
                    cmd.Parameters.Add("@saleQty", SqlDbType.Int);
                    cmd.Parameters["@saleQty"].Value = man.Saleqty;
                    cmd.Parameters.Add("@userId", SqlDbType.Int);
                    cmd.Parameters["@userId"].Value = man.UserId;
                    cmd.Parameters.Add("@flag", SqlDbType.Int);
                    cmd.Parameters["@flag"].Value = 0;
                    sqlConn.Open();
                    cmd.ExecuteScalar();
                    sqlConn.Close();
                }
                return Ok("success");

            }
            catch (Exception)
            {
                return Ok("Something went wrong, try later");
            }
        }
    }
}
