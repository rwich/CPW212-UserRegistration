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
        public static void AddUser(User user)
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
            }
            catch (SqlException ex)
            {

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
            return new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=UserRegistrationDB;Integrated Security=True;");
        }

        public static void EditUser()
        {
            throw new NotImplementedException();
        }

        public static void DeleteUser(User u)
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
                int success = deleteUser.ExecuteNonQuery();
                if (success > 0)
                {
                    System.Windows.Forms.MessageBox.Show("User deleted");
                }
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("User could not be deleted at this time");
            }
            finally
            {
                con.Dispose();
            }
        }
    }
}
