using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace salesAPI.Controllers
{
    public class LoginController : ApiController
    {
        /* http://localhost:50804/api/Login/GetLogin */
        [ActionName("GetLogin")]
        public string GetLogin(string username, string password)
        {
            string status = "", Encrypassword = "", Decrypassword = "";
            DataTable dt = new DataTable();
            ConnectionStringSettings objcon = ConfigurationManager.ConnectionStrings["ConnString"];
            SqlConnection sqlConn = new SqlConnection();
            sqlConn = new SqlConnection(objcon.ConnectionString);
            using (SqlCommand cmd = new SqlCommand("sp_mobile_userlogin", sqlConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@username", SqlDbType.VarChar);
                cmd.Parameters["@username"].Value = username;
                cmd.Parameters.Add("@password", SqlDbType.VarChar);
                cmd.Parameters["@password"].Value = password;
                sqlConn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    dt.Load(sdr);
                }
                sqlConn.Close();
                if (dt.Rows.Count > 0)
                {
                    Encrypassword = dt.Rows[0]["password"].ToString();
                    Decrypassword = Decryptdata(Encrypassword);
                    if (Decrypassword == password)
                    {
                        //status = dt.Rows[0][0].ToString();
                        status = "SUCCESSFUL";
                    }
                    else
                    {
                        status = "UNSUCCESSFUL";
                    }
                }
                else
                {
                    status = "UNSUCCESSFUL";
                }
                return status;
            }
        }
        public string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
    }
}
