using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPW212_UserRegistration
{
    public partial class frmEditUser : Form
    {
        public frmEditUser()
        {
            InitializeComponent();
        }

        private void frmEditUser_Load(object sender, EventArgs e)
        {
            PopulateUserList();
        }

        /// <summary>
        /// PopulateUserList() method gets all users from database
        /// and displays usernames in combobox
        /// </summary>
        private void PopulateUserList()
        {
            cboUsers.Items.Clear();

            try
            {
                List<User> users = UserDB.GetAllUsers();
                foreach(User u in users)
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

        /// <summary>
        /// If a user is selected from the combobox, then the textboxes
        /// will populate with that user's data from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboUsers.SelectedIndex < 0)  // if no user is selected
                return;

            User user = cboUsers.SelectedItem as User;
            txtEditUsername.Text = user.Username;
            txtEditPassword.Text = user.Password;
            txtEditEmail.Text = user.Email;
            dtpEditDOB.Value = user.DateOfBirth;
        }

        /// <summary>
        /// Updates selected user's data in database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cboUsers.SelectedIndex < 0)  // if no user is selected
            {
                MessageBox.Show("You must select a user.");
                return;
            }

            User selectedUser = cboUsers.SelectedItem as User;

            User updatedUser = new User()
            {
                Username = txtEditUsername.Text,
                Password = txtEditPassword.Text,
                Email = txtEditEmail.Text,
                DateOfBirth = dtpEditDOB.Value
            };

            if ( UserDB.EditUser(selectedUser, updatedUser) )
            {
                MessageBox.Show("User account updated successfully.");
                PopulateUserList();
            }
            else
            {
                MessageBox.Show("Unable to update user at this time.");
            }
        }
    }
}
