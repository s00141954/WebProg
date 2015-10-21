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

    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillFixture();
            }
        }

        private void FillFixture()
        {
            string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
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
                ddlFixture1.DataValueField = "HTeamName AND ATeamName";
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
            SqlCommand cmd = new SqlCommand("SELECT PlayerName FROM PlayerTbl WHERE TeamName = @HTeamName OR TeamName = @ATeamName",con);
           
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT PlayerName FROM PlayerTbl WHERE TeamName = @HTeamName";
            cmd.Parameters.AddWithValue("@HTeamName", HTeamName);
            //cmd.Parameters.AddWithValue("@ATeamName", ATeamName);
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
    }
}