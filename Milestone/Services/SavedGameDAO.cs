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
    }
}
