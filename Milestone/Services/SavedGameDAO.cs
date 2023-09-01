using Milestone.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Milestone.Services
{
    public class SavedGameDAO : ISavedGamesDataService
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=cst350;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<SavedGameModel> AllGames()
        {
            List<SavedGameModel> foundGames = new List<SavedGameModel>();

            String sqlStatment = "SELECT * FROM dbo.savedgames";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundGames.Add(new SavedGameModel((int)reader[0], (int)reader[1], (string)reader[2], (string)reader[3], (string)reader[4], (string)reader[5], (string)reader[6]));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return foundGames;
        }

        public SavedGameModel GetSavedGameByGameId(int id)
        {
            SavedGameModel foundGames = null;

            String sqlStatment = "SELECT * FROM dbo.savedgames WHERE gameid = @id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatment, connection);

                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        foundGames = new SavedGameModel((int)reader[0], (int)reader[1], (string)reader[2], (string)reader[3], (string)reader[4], (string)reader[5], (string)reader[6]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return foundGames;
        }

        public bool DeleteOneGame(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                String sqlStatment = "DELETE FROM dbo.savedgames WHERE gameid = @Id";

                SqlCommand command = new SqlCommand(sqlStatment, connection);
                command.Parameters.AddWithValue("@id", id);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    Debug.WriteLine(rowsAffected);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                };
            }
        }

        public bool Insert(SavedGameModel savedGame)
        {
            bool success = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string insertSQLStatement = "INSERT INTO dbo.savedgames(userid, gameName, liveSites, time, date, buttonStates) VALUES(@userid,@gamename,@livesites,@time,@date,@buttonstates)";


                SqlCommand cmd = new SqlCommand(@insertSQLStatement, connection);
                cmd.Parameters.Add("@USERID", System.Data.SqlDbType.Int).Value = savedGame.UserId;
                cmd.Parameters.Add("@GAMENAME", System.Data.SqlDbType.VarChar, 100).Value = savedGame.GameName;
                cmd.Parameters.Add("@LIVESITES", System.Data.SqlDbType.VarChar,200).Value = savedGame.LiveSites;
                cmd.Parameters.Add("@TIME", System.Data.SqlDbType.VarChar,50).Value = savedGame.Time;
                cmd.Parameters.Add("@DATE", System.Data.SqlDbType.VarChar, 50).Value = savedGame.Date;
                cmd.Parameters.Add("@BUTTONSTATES", System.Data.SqlDbType.VarChar, 200).Value = savedGame.ButtonStates;

                try
                {
                    connection.Open();
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        success = true;
                    }
                    else
                    {
                        Debug.WriteLine("Error inserting user into database!");
                        success = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    success = false;
                };

                return success;
            }
        }
    }
}
