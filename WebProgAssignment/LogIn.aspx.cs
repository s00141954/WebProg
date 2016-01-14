using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// Added namespace
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using System.Web.Security;

namespace WebProgAssignment
{
    public partial class LogIn : System.Web.UI.Page
    {
        static string connString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        SqlConnection conn = new SqlConnection(connString);
        SqlCommand com = new SqlCommand();
        SqlDataReader reader;

        User currentUser = new User();




        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                /* Open connection and pass UserName and hashed Password to the Proc in order to test if
                it is a valid user and the correct password.  Then redirect to the home page if it is 
                correct, or alert in the error label if it is incorrect.  Close connection.*/
                conn.Open();
                com.Connection = conn;

                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "LogIn";

                com.Parameters.AddWithValue("@UserName", tbxUserName.Text);
                com.Parameters.AddWithValue("@Password", Utilities.GetMD5Hash(tbxPassword.Text));



                reader = com.ExecuteReader();

                if (reader.Read())
                {
                    // Look up this
                    // http://stackoverflow.com/questions/10940037/getting-error-system-indexoutofrangeexception-why
                    // try this in sprocs
                    // lblError.Text = reader["UserId"].ToString();

                    //reader.Close(); 

                    //ID = Convert.ToInt32(reader["Id"]),
                    //FName = reader["FName"].ToString(),
                    //LName = reader["LName"].ToString()


                    //currentUser.UserId = Convert.ToInt32(reader["UserId"]);
                    //currentUser.UserName = Convert.ToString(reader["UserName"]);
                    //currentUser.Email = Convert.ToString(reader["Email"]);
                    //currentUser.DOB = Convert.ToDateTime(reader["DOB"]);
                    //currentUser.ContactNumber = Convert.ToString(reader["ContactNo"]);

                    //User currentUser = new User()
                    //{
                    //    UserId = Convert.ToInt32(reader["UserId"]),
                    //    UserName = Convert.ToString(reader["UserName"]),
                    //    Email = Convert.ToString(reader["Email"]),
                    //    DOB = Convert.ToDateTime(reader["DOB"]),
                    //    ContactNumber = Convert.ToString(reader["ContactNumber"])
                    //};
                    conn.Close();
                    Session.Add("User", tbxUserName.Text);
                    FormsAuthentication.RedirectFromLoginPage(tbxUserName.Text, true);
                }
                else
                    lblError.Text = "Wrong user name or password";
            }

            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
    }
}