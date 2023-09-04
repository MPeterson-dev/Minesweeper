using Milestone.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Milestone.Services
{
    public class SavedGameDAO : ISavedGamesDataService
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=cst350;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// The function retrieves all saved games from a database and returns them as a list of SavedGameModel objects.
        /// </summary>
        /// <returns>
        /// The method is returning a List of SavedGameModel objects.
        /// </returns>
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

        /// <summary>
        /// The function retrieves a list of saved game models from the database based on the provided user ID.
        /// </summary>
        /// <param name="id">The "id" parameter is an integer representing the user ID for which we want to retrieve the
        /// saved games.</param>
        /// <returns>
        /// The method is returning a List of SavedGameModel objects.
        /// </returns>
        public List<SavedGameModel> GetSavedGameByUserId(int id)
        {
            List<SavedGameModel> foundGames = new List<SavedGameModel>();

            String sqlStatment = "SELECT * FROM dbo.savedgames WHERE userid = @id";

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

        /// <summary>
        /// The function retrieves a saved game from the database based on the provided game ID.
        /// </summary>
        /// <param name="id">The parameter `id` is an integer representing the game ID. It is used to filter the saved games
        /// and retrieve the saved game with the matching game ID.</param>
        /// <returns>
        /// The method is returning a SavedGameModel object.
        /// </returns>
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

        /// <summary>
        /// The DeleteOneGame function deletes a game from the savedgames table in the database based on the provided game
        /// id.
        /// </summary>
        /// <param name="id">The id parameter is an integer that represents the game id of the game that needs to be deleted
        /// from the "savedgames" table in the database.</param>
        /// <returns>
        /// The method is returning a boolean value. It returns true if the deletion of the game with the specified id was
        /// successful, and false if there was an exception or error during the deletion process.
        /// </returns>
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

        /// <summary>
        /// The function inserts a SavedGameModel object into a database table and returns a boolean indicating the success
        /// of the operation.
        /// </summary>
        /// <param name="SavedGameModel">SavedGameModel is a model class that represents a saved game. It contains the
        /// following properties:</param>
        /// <returns>
        /// The method is returning a boolean value indicating whether the insertion of the saved game was successful or
        /// not.
        /// </returns>
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
                cmd.Parameters.Add("@BUTTONSTATES", System.Data.SqlDbType.VarChar, 400).Value = savedGame.ButtonStates;

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
