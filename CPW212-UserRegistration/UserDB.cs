using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPW212_UserRegistration
{
    static class UserDB
    {

        /// <summary>
        /// AddUser() method adds user to database
        /// </summary>
        /// <param name="user">User to be added</param>
        public static bool AddUser(User user)
        {
            SqlConnection con = GetConnection();

            SqlCommand addUserCmd = new SqlCommand();
            addUserCmd.Connection = con;
            addUserCmd.CommandText =
                @"INSERT INTO Users(Username, Password, Email, DateOfBirth)
                  VALUES(@Username, @Password, @Email, @DOB)";
            addUserCmd.Parameters.AddWithValue("@Username", user.Username);
            addUserCmd.Parameters.AddWithValue("@Password", user.Password);
            addUserCmd.Parameters.AddWithValue("@Email", user.Email);
            addUserCmd.Parameters.AddWithValue("@DOB", user.DateOfBirth);

            try
            {
                con.Open();
                int rowsAffected = addUserCmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
                throw ex;
            }
            finally
            {
                con.Dispose();
            }
        }

        /// <summary>
        /// GetAllUsers() method gets list of all users in database
        /// </summary>
        /// <returns>Returns list of users</returns>
        public static List<User> GetAllUsers()
        {
            SqlConnection con = GetConnection();

            SqlCommand selQuery = new SqlCommand();
            selQuery.Connection = con;
            selQuery.CommandText =
                @"SELECT UserID, Username, Password, Email, DateOfBirth
                  FROM Users";

            try
            {
                con.Open();
                SqlDataReader rdr = selQuery.ExecuteReader();
                List<User> userList = new List<User>();
                while (rdr.Read())
                {
                    User user = new User();
                    user.UserID = (int)rdr["UserID"];
                    user.Username = (string)rdr["Username"];
                    user.Password = (string)rdr["Password"];
                    user.Email = (string)rdr["Email"];
                    user.DateOfBirth = (DateTime)rdr["DateOfBirth"];
                    userList.Add(user);
                }
                return userList;
            }
            finally
            {
                con.Dispose();
            }
        }

        /// <summary>
        /// GetConnection() method gets connection to database
        /// </summary>
        /// <returns>Returns connection to UserRegistration database</returns>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=UserRegistration;Integrated Security=True");
        }

        /// <summary>
        /// EditUser() method updates a row in the database for a particular user
        /// </summary>
        /// <param name="oldUser"></param>
        /// <param name="updatedUser"></param>
        /// <returns>boolean value so appropriate success/failure message can be shown</returns>
        public static bool EditUser(User oldUser, User updatedUser)
        {
            SqlConnection con = GetConnection();
            SqlCommand editUser = new SqlCommand();
            editUser.Connection = con;
            editUser.CommandText = @"UPDATE Users
                                    SET Username = @username,
                                        Password = @password,
                                        Email = @email,
                                        DateOfBirth = @dob
                                    WHERE Username = @oldusername";
            editUser.Parameters.AddWithValue("@username", updatedUser.Username);
            editUser.Parameters.AddWithValue("@password", updatedUser.Password);
            editUser.Parameters.AddWithValue("@email", updatedUser.Email);
            editUser.Parameters.AddWithValue("@dob", updatedUser.DateOfBirth);
            editUser.Parameters.AddWithValue("@oldusername", oldUser.Username);

            try
            {
                con.Open();
                byte success = (byte)editUser.ExecuteNonQuery();
                if (success > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                con.Dispose();
            }
        }

        /// <summary>
        /// DeleteUser() method deletes a user from the database
        /// </summary>
        /// <param name="u"></param>
        /// <returns>boolean value so appropriate success/failure message can be shown</returns>
        public static bool DeleteUser(User u)
        {
            SqlConnection con = GetConnection();
            SqlCommand deleteUser = new SqlCommand();
            deleteUser.Connection = con;
            deleteUser.CommandText = @"DELETE FROM Users
                                       WHERE Username = @username";
            deleteUser.Parameters.AddWithValue("@username", u.Username);

            try
            {
                con.Open();
                byte success = (byte)deleteUser.ExecuteNonQuery();
                if (success > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Dispose();
            }
        }
    }
}