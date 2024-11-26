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
    public partial class Manage_Users : Form
    {
        public Manage_Users()
        {
            InitializeComponent();

        }

        private void _RefreshUsersList()
        {
            cbFilter.SelectedIndex = 0;
            dgvAllUsers.DataSource = clsUser.GetAllUsers();
            lblRecords.Text = clsUser.GetAllUsers().Rows.Count.ToString();
        }

        private void Manage_Users_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = false;
            dgvAllUsers.DataSource = clsUser.GetAllUsers();
            if (cbFilter.SelectedIndex == 0)
            {
                
                txtFilter.Enabled = false;
                dgvAllUsers.DataSource = clsUser.GetAllUsers();

            }
            else if (cbFilter.SelectedIndex == 1 )
            {
                txtFilter.Visible = true;
                txtFilter.Enabled = true;
                txtFilter.Enabled = true;
            }
            else
            {
               
                txtFilter.Visible = true;
                txtFilter.Enabled = true;

            }

        }
        private DataView _FilterData(object txt1, object txt2)
        {
            DataTable dt = clsUser.GetAllUsers();
            DataView dv = dt.DefaultView;
            if ((cbFilter.SelectedIndex == 1 )|| (cbFilter.SelectedIndex == 4))
            {
               
                dv.RowFilter = $"{Convert.ToString(txt1)} = '{Convert.ToString(txt2)}'";
            }
            else
            {
                dv.RowFilter = $"{txt1} Like '%{txt2}%'";
            }

            return dv;
        }
       
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

              
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtFilter.Text == "")
            {
                dgvAllUsers.DataSource = clsUser.GetAllUsers();
            }
            else
            {
                dgvAllUsers.DataSource = _FilterData(cbFilter.Text, txtFilter.Text);
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            AddNewUser frmAddNewUser = new AddNewUser(-1);
            frmAddNewUser.ShowDialog();
            _RefreshUsersList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewUser frmAddNewUser = new AddNewUser(-1);
            frmAddNewUser.ShowDialog();
            _RefreshUsersList();
        }

        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewUser frmEdit = new AddNewUser((int)dgvAllUsers.CurrentRow.Cells[0].Value);
            frmEdit.ShowDialog();

            _RefreshUsersList();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword frmChange = new ChangePassword((int)dgvAllUsers.CurrentRow.Cells[0].Value);
            frmChange.ShowDialog();
            _RefreshUsersList();
        }

        private void showDetailsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            UserInfo frmshow = new UserInfo((int)dgvAllUsers.CurrentRow.Cells[0].Value);
            frmshow.ShowDialog();
            _RefreshUsersList();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((cbFilter.SelectedIndex == 1 ) || (cbFilter.SelectedIndex==4))
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
