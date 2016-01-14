using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// Added namespace
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;

namespace WebProgAssignment
{
    // This class is used to seperate fixtures for different weeks
    static class DateTimeExtensions
    {
        static GregorianCalendar gc = new GregorianCalendar();

        public static int GetWeekOfMonth(this DateTime time)
        {
            DateTime first = new DateTime(time.Year, time.Month, 1);
            return time.GetWeekOfYear() - first.GetWeekOfYear() + 1;
        }

        static int GetWeekOfYear(this DateTime time)
        {
            return gc.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
    }

    public partial class Home : System.Web.UI.Page
    {
        // These collections of fixtures and teams will be filled in later
        // These teams are part of the fixures
        private static List<Fixture> fixtures = new List<Fixture>();
        private static List<Fixture> tempFixtures = new List<Fixture>();
        private static List<Team> teams = new List<Team>();
        private static List<Bet> bets = new List<Bet>();


        // SQL
        static string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlCommand command = new SqlCommand();
        SqlConnection connection = new SqlConnection(strConn);

       
        protected void Page_Load(object sender, EventArgs e)
        {
            //string betTime = System.DateTime.Now.ToString();
            //const DateTime betTime = System.DateTime.Now();
            if (Session["BetTime"] == null)
            {
                string betTime = System.DateTime.Now.ToString();
                Session.Add("BetTime", betTime);
            }

            if (!IsPostBack)
            {
                GetFixtures();
                CreateTeams();
            }
        }

        private void GetFixtures()
        {
            DateTime time = DateTime.Now;
            int thisWeek = Convert.ToInt16(time.GetWeekOfMonth());
            /* Select all fixtures from FixtureTbl and create a class for each fixture */
            
            connection.Open();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "GetFixtures";

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Fixture fix = new Fixture()
                {
                    FixtureId = Convert.ToInt32(reader["FixtureId"]),
                    GameWeek = Convert.ToInt16(reader["GameWeek"]),
                    HTeamID = Convert.ToInt32(reader["HTeamId"]),
                    ATeamID = Convert.ToInt32(reader["ATeamId"])
                };
                tempFixtures.Add(fix);
            }

            reader.Close();
            connection.Close();

            //http://stackoverflow.com/questions/2136487/calculate-week-of-month-in-net/2136549#2136549

            foreach (Fixture fix in tempFixtures)
            {
                if (fix.GameWeek == thisWeek)
                {
                    fixtures.Add(fix);
                }
            }
        }// End GetFixtures()

        private void CreateTeams()
        {
            foreach (var fix in fixtures)
            {
                // Delegate and method are in the Utilities class
                ReturnTeamName teamNameDelegate = Utilities.GetTeamName;
                var hTeamName = teamNameDelegate(fix.HTeamID);
                var aTeamName = teamNameDelegate(fix.ATeamID);

                Team hTeam = new Team()
                {
                    TeamId = fix.HTeamID,
                    Name = hTeamName
                };

                Team aTeam = new Team()
                {
                    TeamId = fix.ATeamID,
                    Name = aTeamName
                };

                hTeam.Players = GetTeamPlayers(fix.HTeamID);
                aTeam.Players = GetTeamPlayers(fix.ATeamID);

                teams.Add(hTeam);
                teams.Add(aTeam);

                ddlFixture1.Items.Add(new ListItem(hTeam.Name + " v " + aTeam.Name, fix.FixtureId.ToString()));
                ddlFixture2.Items.Add(new ListItem(hTeam.Name + " v " + aTeam.Name, fix.FixtureId.ToString()));
                ddlFixture3.Items.Add(new ListItem(hTeam.Name + " v " + aTeam.Name, fix.FixtureId.ToString()));
                ddlFixture4.Items.Add(new ListItem(hTeam.Name + " v " + aTeam.Name, fix.FixtureId.ToString()));
                ddlFixture5.Items.Add(new ListItem(hTeam.Name + " v " + aTeam.Name, fix.FixtureId.ToString()));
                ddlFixture6.Items.Add(new ListItem(hTeam.Name + " v " + aTeam.Name, fix.FixtureId.ToString()));
            }
        }// End CreateTeams()

        private List<Player> GetTeamPlayers(int teamId)
        {
            List<Player> players = new List<Player>();
            
            connection.Open();

            command.Connection = connection;
            command.Parameters.Clear();
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TeamId", teamId);
            command.CommandText = "GetTeamPlayers";

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Player player = new Player()
                {
                    PlayerId = Convert.ToInt32(reader["PlayerId"]),
                    PlayerName = reader["PlayerName"].ToString()
                };
                players.Add(player);
            }
            reader.Close();
            connection.Close();

            return players;
        }


        protected void ddlFixture1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int fixtureId = Convert.ToInt32((ddlFixture1.SelectedItem as ListItem).Value);

            Fixture fixture = (from f in fixtures where f.FixtureId == fixtureId select f).First();
            Team home = (from t in teams where t.TeamId == fixture.HTeamID select t).First();
            Team away = (from t in teams where t.TeamId == fixture.ATeamID select t).First();

            foreach (Player p in home.Players)
            {
                ddlPlayer1.Items.Add(new ListItem(p.PlayerName, p.PlayerId.ToString()));
            }
            foreach (Player p in away.Players)
            {
                ddlPlayer1.Items.Add(new ListItem(p.PlayerName, p.PlayerId.ToString()));
            }
        }

        // The following 5 methods are identical to the one
        // above in order to fill players in the appropriate
        // drop down lists
        #region Fill Players
        protected void ddlFixture2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int fixtureId = Convert.ToInt32((ddlFixture2.SelectedItem as ListItem).Value);

            Fixture fixture = (from f in fixtures where f.FixtureId == fixtureId select f).First();
            Team home = (from t in teams where t.TeamId == fixture.HTeamID select t).First();
            Team away = (from t in teams where t.TeamId == fixture.ATeamID select t).First();

            foreach (Player p in home.Players)
            {
                ddlPlayer2.Items.Add(new ListItem(p.PlayerName, p.PlayerId.ToString()));
            }
            foreach (Player p in away.Players)
            {
                ddlPlayer2.Items.Add(new ListItem(p.PlayerName, p.PlayerId.ToString()));
            }
        }

        protected void ddlFixture3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int fixtureId = Convert.ToInt32((ddlFixture3.SelectedItem as ListItem).Value);

            Fixture fixture = (from f in fixtures where f.FixtureId == fixtureId select f).First();
            Team home = (from t in teams where t.TeamId == fixture.HTeamID select t).First();
            Team away = (from t in teams where t.TeamId == fixture.ATeamID select t).First();

            foreach (Player p in home.Players)
            {
                ddlPlayer3.Items.Add(new ListItem(p.PlayerName, p.PlayerId.ToString()));
            }
            foreach (Player p in away.Players)
            {
                ddlPlayer3.Items.Add(new ListItem(p.PlayerName, p.PlayerId.ToString()));
            }
        }

        protected void ddlFixture4_SelectedIndexChanged(object sender, EventArgs e)
        {
            int fixtureId = Convert.ToInt32((ddlFixture4.SelectedItem as ListItem).Value);

            Fixture fixture = (from f in fixtures where f.FixtureId == fixtureId select f).First();
            Team home = (from t in teams where t.TeamId == fixture.HTeamID select t).First();
            Team away = (from t in teams where t.TeamId == fixture.ATeamID select t).First();

            foreach (Player p in home.Players)
            {
                ddlPlayer4.Items.Add(new ListItem(p.PlayerName, p.PlayerId.ToString()));
            }
            foreach (Player p in away.Players)
            {
                ddlPlayer4.Items.Add(new ListItem(p.PlayerName, p.PlayerId.ToString()));
            }
        }

        protected void ddlFixture5_SelectedIndexChanged(object sender, EventArgs e)
        {
            int fixtureId = Convert.ToInt32((ddlFixture5.SelectedItem as ListItem).Value);

            Fixture fixture = (from f in fixtures where f.FixtureId == fixtureId select f).First();
            Team home = (from t in teams where t.TeamId == fixture.HTeamID select t).First();
            Team away = (from t in teams where t.TeamId == fixture.ATeamID select t).First();

            foreach (Player p in home.Players)
            {
                ddlPlayer5.Items.Add(new ListItem(p.PlayerName, p.PlayerId.ToString()));
            }
            foreach (Player p in away.Players)
            {
                ddlPlayer5.Items.Add(new ListItem(p.PlayerName, p.PlayerId.ToString()));
            }
        }

        protected void ddlFixture6_SelectedIndexChanged(object sender, EventArgs e)
        {
            int fixtureId = Convert.ToInt32((ddlFixture6.SelectedItem as ListItem).Value);

            Fixture fixture = (from f in fixtures where f.FixtureId == fixtureId select f).First();
            Team home = (from t in teams where t.TeamId == fixture.HTeamID select t).First();
            Team away = (from t in teams where t.TeamId == fixture.ATeamID select t).First();

            foreach (Player p in home.Players)
            {
                ddlPlayer6.Items.Add(new ListItem(p.PlayerName, p.PlayerId.ToString()));
            }
            foreach (Player p in away.Players)
            {
                ddlPlayer6.Items.Add(new ListItem(p.PlayerName, p.PlayerId.ToString()));
            }
        }
        #endregion


        protected void ddlPlayer1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bet bet = new Bet();
            {
                bet.FixtureId = Convert.ToInt16(ddlFixture1.SelectedValue);

                // This bet time is used to tell the difference between past and present bets
                // It is declared in the class above with the list items at the top of the page
                //bet.BetTime = betTime;
                bet.BetTime = (string)(Session["BetTime"]);
                bet.User = (string)(Session["User"]);
                bet.PlayerId = ddlPlayer1.SelectedValue;

                bets.Add(bet);
            }
        }

        // The following 5 methods are identical to the one above
        // in order to save each selection as a bet object
        #region Save As Bets
        protected void ddlPlayer2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bet bet = new Bet();
            {
                bet.FixtureId = Convert.ToInt16(ddlFixture2.SelectedValue);
                bet.BetTime = (string)(Session["BetTime"]);
                bet.User = (string)(Session["User"]);
                bet.PlayerId = ddlPlayer2.SelectedValue;

                bets.Add(bet);
            }
        }

        protected void ddlPlayer3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bet bet = new Bet();
            {
                bet.FixtureId = Convert.ToInt16(ddlFixture3.SelectedValue);
                bet.BetTime = (string)(Session["BetTime"]);
                bet.User = (string)(Session["User"]);
                bet.PlayerId = ddlPlayer3.SelectedValue;

                bets.Add(bet);
            }
        }

        protected void ddlPlayer4_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bet bet = new Bet();
            {
                bet.FixtureId = Convert.ToInt16(ddlFixture4.SelectedValue);
                bet.BetTime = (string)(Session["BetTime"]);
                bet.User = (string)(Session["User"]);
                bet.PlayerId = ddlPlayer4.SelectedValue;

                bets.Add(bet);
            }
        }

        protected void ddlPlayer5_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bet bet = new Bet();
            {
                bet.FixtureId = Convert.ToInt16(ddlFixture5.SelectedValue);
                bet.BetTime = (string)(Session["BetTime"]);
                bet.User = (string)(Session["User"]);
                bet.PlayerId = ddlPlayer5.SelectedValue;

                bets.Add(bet);
            }
        }

        protected void ddlPlayer6_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bet bet = new Bet();
            {
                bet.FixtureId = Convert.ToInt16(ddlFixture6.SelectedValue);
                bet.BetTime = (string)(Session["BetTime"]);
                bet.User = (string)(Session["User"]);
                bet.PlayerId = ddlPlayer6.SelectedValue;

                bets.Add(bet);
            }
        }
        #endregion

        // Go through each bet and save them to the database
        // The DateTime is used to seperate current and past bets
        protected void btnPlaceBet_Click(object sender, EventArgs e)
        {

            connection.Open();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;

            command.CommandText = "InsertBets";

            command.Parameters.Add("@FixtureId", SqlDbType.Int);
            command.Parameters.Add("@BetTime", SqlDbType.VarChar);
            command.Parameters.Add("@UserName", SqlDbType.NVarChar);
            command.Parameters.Add("@PlayerId", SqlDbType.Int);

            foreach (Bet bet in bets)
            {
                command.Parameters["@FixtureId"].Value = bet.FixtureId;
                command.Parameters["@BetTime"].Value = bet.BetTime;
                command.Parameters["@UserName"].Value = bet.User;
                command.Parameters["@PlayerId"].Value = bet.PlayerId;

                command.ExecuteNonQuery();
            }

            bets.Clear();

            connection.Close();
            Response.Redirect("ViewResults.aspx");
        }


        //public static int GetWeekOfMonth(this DateTime time)
        //{
        //    DateTime first = new DateTime(time.Year, time.Month, 1);
        //    return time.GetWeekOfYear() - first.GetWeekOfYear() + 1;
        //}

        //public static int GetWeekOfYear(this DateTime time)
        //{
        //    return gc.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        //}
    }
}