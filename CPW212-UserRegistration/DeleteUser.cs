﻿using System;
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
    public partial class frmDeleteUser : Form
    {
        public frmDeleteUser()
        {
            InitializeComponent();
        }

        private void frmDeleteUser_Load(object sender, EventArgs e)
        {
            UserFormHelper.PopulateUserList(cboDeleteUser);
        }

        /// <summary>
        /// Deletes selected user's data from database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (cboDeleteUser.SelectedIndex < 0)  // if no user is selected
            {
                MessageBox.Show("You must select a user.");
                return;
            }

            User user = cboDeleteUser.SelectedItem as User;

            DialogResult answer = 
                MessageBox.Show($"Are you sure you want to delete {user.Username}?",
                "Delete User?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation);

            if (answer == DialogResult.Yes)
            {
                if ( UserDB.DeleteUser(user) )
                {
                    MessageBox.Show("User account deleted successfully.");
                    UserFormHelper.PopulateUserList(cboDeleteUser);
                }
                else
                {
                    MessageBox.Show("Unable to delete user at this time.");
                }
            }
            cboDeleteUser.Text = "";
        }
    }
}