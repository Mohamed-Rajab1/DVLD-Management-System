using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_First_Full_Real_Project
{
    public partial class LoginScreen : Form
    {
        clsUser _User;
        
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           _User = clsUser.FindByUserNameAndPassword(txtUserName.Text, txtPassword.Text);

            if (_User != null)
            {
                Main frmMain = new Main(_User.ID);
                frmMain.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid UserName / Password","Wrong credentials",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
