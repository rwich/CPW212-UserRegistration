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

        private void btnRegister_Click(object sender, EventArgs e)
        {
            User u = new User();
            u.Username = txtUsername.Text;
            u.Password = txtPassword.Text;
            u.Email = txtEmail.Text;
            u.DateOfBirth = dtpDOB.Value;

            UserDB.AddUser(u);
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            frmEditUser editUserForm = new frmEditUser();
            editUserForm.ShowDialog();
        }

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
