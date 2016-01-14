using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// Added namespace
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;
using System.Data;

namespace WebProgAssignment
{
    public partial class Register : System.Web.UI.Page
    {
        static string connString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        SqlConnection conn = new SqlConnection(connString);
        SqlCommand com = new SqlCommand();
        SqlDataReader reader;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                conn.Open();

                string checkUser = "select count(*) from UserTbl where UserName='"+ tbxUserName.Text +"'";
                SqlCommand com = new SqlCommand(checkUser,conn);
                int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
                if (temp == 1)
                {
                    Response.Write("Username already exsists");
                }

                conn.Close();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                com.Connection = conn;
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserName", tbxUserName.Text);
                com.Parameters.AddWithValue("@Email", tbxEmail.Text);
                com.CommandText = "CheckUsers";

                //string checkUsers = "SELECT UserName from UserTbl where UserName = @UserName OR Email = @Email";
                //SqlCommand checkCom = new SqlCommand(checkUsers, conn);
                //checkCom.Parameters.AddWithValue("@UserName", tbxUserName.Text);
                //checkCom.Parameters.AddWithValue("@Email", tbxEmail.Text);
                reader = com.ExecuteReader();

                if (reader.Read())
                {
                    lblError.Text = "User name or Email is unavailable";
                    reader.Close();
                }
                else
                {
                    reader.Close();
                    com.Connection = conn;
                    
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "RegisterUser";
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@UserName", tbxUserName.Text);
                    com.Parameters.AddWithValue("@Email", tbxEmail.Text);
                    com.Parameters.AddWithValue("@Password", Utilities.GetMD5Hash(tbxPassword.Text));
                    com.Parameters.AddWithValue("@DOB", tbxDOB.Text);
                    com.Parameters.AddWithValue("@ContactNo", tbxContactNum.Text);

                    com.ExecuteNonQuery();
                    reader = com.ExecuteReader();

                    Response.Redirect("LogIn.aspx");
                }
            }
            catch(Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
            finally
            {
                reader.Close();
                conn.Close();
            }
        }

        //static string GetMD5Hash(string input)
        //{
        //    string output = "";

        //    using (MD5 md5Hash = MD5.Create())
        //    {
        //        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        //        foreach (byte b in data)
        //        {
        //            output = output + b.ToString("x2");
        //        }
        //    }

        //    return output;
        //}
    }
}