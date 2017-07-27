using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPW212_UserRegistration
{
    /// <summary>
    /// Helper class for EditUser and DeleteUser forms
    /// </summary>
    public static class UserFormHelper
    {

        /// <summary>
        /// PopulateUserList() method gets all users from database
        /// and displays usernames in combobox
        /// </summary>
        public static void PopulateUserList(ComboBox cboUsers)
        {
            cboUsers.Items.Clear();

            try
            {
                List<User> users = UserDB.GetAllUsers();
                foreach (User u in users)
                {
                    cboUsers.Items.Add(u);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Unable to load user list. Try again later.");
                Application.Exit();
            }
        }
    }
}
