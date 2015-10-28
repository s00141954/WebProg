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
    // Check safety file saved on desktop *****************

    public partial class Home : System.Web.UI.Page
    {
        string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillFixture();
            }
        }

        private void FillFixture()
        {
            
            SqlConnection con = new SqlConnection(strConn);
            // http://csharpdotnetfreak.blogspot.com/2009/03/populate-dropdown-based-selection-other.html

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT FixtureId, HTeamName, ATeamName FROM FixtureTbl";
            DataSet objDs = new DataSet();
            SqlDataAdapter dAdapter = new SqlDataAdapter();
            dAdapter.SelectCommand = cmd;
            con.Open();
            dAdapter.Fill(objDs);
            con.Close();
            if (objDs.Tables[0].Rows.Count > 0)

            {
                ddlFixture1.DataSource = objDs.Tables[0];
                ddlFixture1.DataTextField = "FixtureId";
                ddlFixture1.DataValueField = "HTeamName";
                ddlFixture1.DataBind();
                ddlFixture1.Items.Insert(0, "--Select Fixture--");
            }
            else
            {
                lblFor1.Text = "No Fixtures found";
            }
            lblFor2.Text = ddlFixture1.DataValueField.ToString();
        }

        protected void ddlFixture1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string HTeamName = ddlFixture1.SelectedValue.ToString();
            FillPlayers(HTeamName);
        }

        private void FillPlayers(string HTeamName)
        {
            string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand("SELECT PlayerName FROM PlayerTbl WHERE TeamName = @HTeamName",con);
           
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@HTeamName", HTeamName);
            DataSet objDs = new DataSet();
            SqlDataAdapter dAdapter = new SqlDataAdapter();
            dAdapter.SelectCommand = cmd;
            con.Open();
            dAdapter.Fill(objDs);
            con.Close();
            if (objDs.Tables[0].Rows.Count >  0)
            {
                ddlPlayer1.DataSource = objDs.Tables[0];
                ddlPlayer1.DataTextField = "PlayerName";
                ddlPlayer1.DataValueField = "PlayerName";
                ddlPlayer1.DataBind();
                ddlPlayer1.Items.Insert(0, "--Select Player--");
            }
            else
            {
                lblFor2.Text = "No players found";
            }
        }

        protected void btnPlaceBet_Click(object sender, EventArgs e)
        {
            // Take UserName and betting values and store into BetTbl, using @BetTime to seperate current and past bets

            string betTime = DateTime.Now.ToLongTimeString();

            // connect, execute command, process results
            SqlConnection con = new SqlConnection(strConn);
            con.Open();

            string insertQuery = "insert into BetTbl (BetTime,UserName,PlayerName,PlayerTeam) values (@BetTime, @UserName, @PlayerName, @PlayerTeam)";
            SqlCommand com = new SqlCommand(insertQuery, con);
            com.Parameters.AddWithValue("@BetTime", betTime);
            com.Parameters.AddWithValue("@UserName", (string)Session["UserName"]);
            com.Parameters.AddWithValue("@PlayerName", ddlPlayer1.SelectedValue);
            com.Parameters.AddWithValue("@PlayerTeam", ddlFixture1.SelectedValue);

            com.ExecuteNonQuery();

            // Keep at bottom *******************
            Session.Add("BetTime", betTime);
            Response.Redirect("ViewResults.aspx");
        }
    }
}