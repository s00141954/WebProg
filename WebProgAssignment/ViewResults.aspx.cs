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

namespace WebProgAssignment
{
    public partial class ViewResults : System.Web.UI.Page
    {
        // SQL
        static string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlCommand command = new SqlCommand();
        SqlConnection connection = new SqlConnection(strConn);

        // List of bets that have just been placed
        public static List<Bet> bets = new List<Bet>();
        private static List<Goal> correctBets = new List<Goal>();
        private static List<Player> players = new List<Player>();
        private static List<Fixture> fixtures = new List<Fixture>();
        private static List<DisplayItem> displayedBets = new List<DisplayItem>();


        protected void Page_Load(object sender, EventArgs e)
        {
            string betTime = (string)Session["BetTime"];
            string userName = (string)Session["User"];

            // Get bets that the user has placed
            //GetBets(betTime, userName);
            ReturnBets returnBetsDelegate = Utilities.GetBets;
            returnBetsDelegate(betTime, userName);
 
            // Display bets placed
            //DisplayBets();

            // Compare bets against goas scored
            GetGoals(Utilities.bets);

            //GetPlayerNames();

            //GetFixtures();

           

            //gvPlacedBets.DataSource = bets;
            //gvPlacedBets.DataBind();

            //gvCorrectBets.DataSource = correctBets;
            //gvCorrectBets.DataBind();

            gvPlacedBets.DataSource = Utilities.bets;
            gvPlacedBets.DataBind();
        }

        // This method will compare the bets that the user has
        // placed against players that have scored and return
        // a list of correct bets
        private List<Goal> GetGoals(List<Bet> bets)
        {
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "GetReleventBets";

            command.Parameters.Add("@FixtureId", SqlDbType.Int);
            command.Parameters.Add("@PlayerId", SqlDbType.Int);

            connection.Open();

            foreach (var bet in bets)
            {
                command.Parameters["@FixtureId"].Value = bet.FixtureId;
                command.Parameters["@PlayerId"].Value = bet.PlayerId;

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Goal goal = new Goal()
                    {
                        GoalId = Convert.ToInt32(reader["GoalId"]),
                        FixtureId = Convert.ToInt32(reader["FixtureId"]),
                        TeamId = Convert.ToInt32(reader["TeamId"]),
                        PlayerId = Convert.ToInt32(reader["PlayerId"])
                    };
                    correctBets.Add(goal);
                }
                reader.Close();
                gvCorrectBets.DataSource = correctBets;
                gvCorrectBets.DataBind();
            }

            connection.Close();

            return correctBets;
        }

        // Get the player names for display in GridView
        private List<Player> GetPlayerNames()
        {
            //command.Connection = connection;
            //command.CommandType = CommandType.StoredProcedure;
            //command.CommandText = "GetTeamPlayers";

            connection.Open();

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Player player = new Player()
                {
                    PlayerId = Convert.ToInt32(reader["PlayerId"]),
                    PlayerName = reader["PlayerName"].ToString(),
                    TeamId = Convert.ToInt32(reader["TeamId"])
                };
                players.Add(player);
            }
            reader.Close();
            connection.Close();

            return players;
        }

        private List<Fixture> GetFixtures()
        {
            //command.Connection = connection;
            //command.CommandType = CommandType.StoredProcedure;
            //command.CommandText = "GetFixtures";

            connection.Open();

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Fixture fixture = new Fixture()
                {
                    FixtureId = Convert.ToInt32(reader["FixtureId"]),
                    HTeamID = Convert.ToInt32(reader["HTeamId"]),
                    ATeamID = Convert.ToInt32(reader["ATeamId"])
                };
                fixtures.Add(fixture);
            }
            return fixtures;
        }

      
        /***************** try to get information using joins? ******************/
        //http://stackoverflow.com/questions/13983786/displaying-foreign-key-data-in-gridview-detailsview
        //http://www.sqlfiddle.com/#!2/b99ba/1


      
    }
}