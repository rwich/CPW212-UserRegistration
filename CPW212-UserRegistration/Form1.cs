using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPW212_UserRegistration
{
    public partial class frmUserRegistration : Form
    {
        public frmUserRegistration()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructs user object from form input and sends it to AddUser()
        /// After success or failure, the form is reset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            User u = new User();
            u.Username = txtUsername.Text;
            u.Password = txtPassword.Text;
            u.Email = txtEmail.Text;
            u.DateOfBirth = dtpDOB.Value;

            if (UserDB.AddUser(u))
            {
                MessageBox.Show("User successfully added");
            }
            else
            {
                MessageBox.Show("Could not add user at this time");
            }
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtEmail.Text = "";
            dtpDOB.Value = new DateTime(1960, 1, 1);
        }

        /// <summary>
        /// Opens EditUser form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditUser_Click(object sender, EventArgs e)
        {
            frmEditUser editUserForm = new frmEditUser();
            editUserForm.ShowDialog();
        }

        /// <summary>
        /// Opens DeleteUser form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            frmDeleteUser deleteUserForm = new frmDeleteUser();
            deleteUserForm.ShowDialog();
        }

        private void frmUserRegistration_Load(object sender, EventArgs e)
        {

        }
    }
}
