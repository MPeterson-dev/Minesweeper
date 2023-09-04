using Milestone.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Milestone.Services
{
    public class UserDAO : IUserDataService
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=cst350;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// The function FindUserByNameAndPasswordValid checks if a user with a given username and password exists in the
        /// database.
        /// </summary>
        /// <param name="UserModel">A model class that represents a user. It contains properties for the user's username and
        /// password.</param>
        /// <returns>
        /// The method is returning a boolean value indicating whether the user with the given username and password exists
        /// in the database.
        /// </returns>
        public bool FindUserByNameAndPasswordValid(UserModel user)
        {
            bool success = false;

            string sqlStatment = "SELECT * FROM dbo.users WHERE username = @username and password = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatment, connection);
                cmd.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 50).Value = user.UserName;
                cmd.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 50).Value = user.Password;

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                        success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
                return success;
            }
        }

        /// <summary>
        /// The function FindUserIdByNameAndPassword takes a UserModel object as input and returns the user ID of the user
        /// with the matching username and password in the database.
        /// </summary>
        /// <param name="UserModel">A model class that represents a user. It contains properties for the user's username and
        /// password.</param>
        /// <returns>
        /// The method is returning an integer value, which represents the user ID.
        /// </returns>
        public int FindUserIdByNameAndPassword(UserModel user)
        {
            int userId = 0;

            string sqlStatment = "SELECT TOP 1 * FROM dbo.users WHERE username = @username AND password = @password ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatment, connection);
                cmd.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 50).Value = user.UserName;
                cmd.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 50).Value = user.Password;

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        userId = ((int)reader[0]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }
            return userId;
        }

        /// <summary>
        /// The function `RegisterUserValid` inserts a user into a database table and returns a boolean value indicating
        /// whether the operation was successful or not.
        /// </summary>
        /// <param name="UserModel">A model class that represents a user with properties such as FirstName, LastName, Sex,
        /// Age, State, Email, UserName, and Password.</param>
        /// <returns>
        /// The method is returning a boolean value indicating whether the user registration was successful or not.
        /// </returns>
        public bool RegisterUserValid(UserModel user)
        {
            bool success = false;

            string sqlStatment = "INSERT INTO dbo.users (firstname,lastname,sex,age,state,email,username,password) VALUES (@firstname,@lastname,@sex,@age,@state,@email,@username,@password)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlStatment, connection);
                cmd.Parameters.Add("@FIRSTNAME", System.Data.SqlDbType.VarChar, 50).Value = user.FirstName;
                cmd.Parameters.Add("@LASTNAME", System.Data.SqlDbType.VarChar, 50).Value = user.LastName;
                cmd.Parameters.Add("@SEX", System.Data.SqlDbType.VarChar, 1).Value = user.Sex;
                cmd.Parameters.Add("@AGE", System.Data.SqlDbType.Int).Value = user.Age;
                cmd.Parameters.Add("@STATE", System.Data.SqlDbType.VarChar, 50).Value = user.State;
                cmd.Parameters.Add("@EMAIL", System.Data.SqlDbType.VarChar, 50).Value = user.Email;
                cmd.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 50).Value = user.UserName;
                cmd.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 50).Value = user.Password;

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
