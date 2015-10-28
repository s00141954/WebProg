using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace WebProgAssignment
{
    public partial class ViewResults : System.Web.UI.Page
    {
        string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Source ***************************************************
            // http://stackoverflow.com/questions/14171794/retrieve-data-from-a-sql-server-database-in-c-sharp

            string betTime = (string)Session["BetTime"];
            string userName = (string)Session["UserName"];

            //SqlConnection con = new SqlConnection(strConn);
            //con.Open();

            using (SqlConnection con = new SqlConnection(strConn))
            {
                

                string oString = "SELECT * from BetTbl where UserName=@UserName AND BetTime=@BetTime";
                SqlCommand oCmd = new SqlCommand(oString, con);
                oCmd.Parameters.AddWithValue("@UserName", userName);
                oCmd.Parameters.AddWithValue("@BetTime", betTime);
                con.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        string playerName = oReader["PlayerName"].ToString();
                        string playerTeam = oReader["PlayerTeam"].ToString();

                        lblTest.Text = string.Format("Name: {0}.. Team: {1}", playerName, playerTeam);
                    }
                    

                    con.Close();
                }
            }

            //con.Close();
        }
    }
}