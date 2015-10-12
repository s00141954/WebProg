using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace WebProgAssignment
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
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
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                conn.Open();

                string insertQuery = "insert into UserTbl (UserName,Email,Password,DOB,ContactNo) values (@UserName, @Email, @Password, @DOB, @ContactNo)";
                SqlCommand com = new SqlCommand(insertQuery, conn);
                com.Parameters.AddWithValue("@UserName", tbxUserName.Text);
                com.Parameters.AddWithValue("@Email", tbxEmail.Text);
                com.Parameters.AddWithValue("@Password", tbxPassword.Text);
                com.Parameters.AddWithValue("@DOB", tbxDOB.Text);
                com.Parameters.AddWithValue("@ContactNo", tbxContactNum.Text);

                com.ExecuteNonQuery();
                // for testing
                 Response.Redirect("Users.aspx");
                //Response.Write("Registration Successful");

                conn.Close();
            }
            catch(Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
        }
    }
}