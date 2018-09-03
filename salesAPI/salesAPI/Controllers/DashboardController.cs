﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace salesAPI.Controllers
{
    public class DashboardController : ApiController
    {
        /* http://localhost:50804/api/dashboard/1 */
        // GET api/spinner/1
        public DataTable Get(int Id)
        {
            DataTable dt = new DataTable();
            ConnectionStringSettings objcon = ConfigurationManager.ConnectionStrings["ConnString"];
            SqlConnection sqlConn = new SqlConnection();
            sqlConn = new SqlConnection(objcon.ConnectionString);
            using (SqlCommand cmd = new SqlCommand("sp_mobile_searchInhome", sqlConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@userId", SqlDbType.Int);
                cmd.Parameters["@userId"].Value = Id;
                cmd.Parameters.Add("@flag", SqlDbType.Int);
                cmd.Parameters["@flag"].Value = 0;
                sqlConn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    dt.Load(sdr);
                }
                sqlConn.Close();
            }
            return dt;
        }
    }
}
