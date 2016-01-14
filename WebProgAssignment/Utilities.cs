using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// Added namespace
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;

namespace WebProgAssignment
{
    /***** Delegates *****/

    // Get the team name for the Home.aspx page
    public delegate string ReturnTeamName(int teamId);

    // Get bets that the user has placed for the ViewResults.aspx page
    public delegate List<Bet> ReturnBets(string betTime, string userName);

    public class Utilities
    {
        // SQL
        static string strConn = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        static SqlConnection connection = new SqlConnection(strConn);
        static SqlCommand command = new SqlCommand();

        // List of bets
        public static List<Bet> bets = new List<Bet>();

        // Hash password
        public static string GetMD5Hash(string input)
        {
            string output = "";

            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                foreach (byte b in data)
                {
                    output = output + b.ToString("x2");
                }
            }
            return output;
        }

        // Get team name for each Id
        public static string GetTeamName(int teamId)
        {
            connection.Open();
            try
            {
                command.Connection = connection;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@TeamId", teamId);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetTeamName";

                return command.ExecuteScalar().ToString();
            }
            finally
            {
                connection.Close();
            }
        }// End GetTeamName()

        // This method will get the bets that the user has 
        // just placed out of the database using the 
        // betTime to tell the difference between new and
        // old bets
        public static List<Bet> GetBets(string betTime, string userName)
        {
            connection.Open();
            command.Connection = connection;
            command.Parameters.Clear();
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@UserName", userName);
            command.Parameters.AddWithValue("@BetTime", betTime);

            command.CommandText = "GetBets";

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Bet newBets = new Bet()
                {
                    BetId = Convert.ToInt32(reader["BetId"]),
                    FixtureId = Convert.ToInt32(reader["FixtureId"]),
                    BetTime = reader["BetTime"].ToString(),
                    User = reader["UserName"].ToString(),
                    PlayerId = reader["PlayerId"].ToString()
                };
                bets.Add(newBets);
            }
            connection.Close();
            return bets;
        }
    }

   
}