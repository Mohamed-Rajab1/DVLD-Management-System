using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;


namespace My_First_Full_Real_Project
{
    public partial class ManagePeople : Form
    {
        public ManagePeople()
        {
            InitializeComponent();
        }

        private void _RefreshPeopleList()
        {
            cbFilter.SelectedIndex = 0;
            dgvAllPeople.DataSource = clsDVLD.GetAllPeople();
            lblRecords.Text = clsDVLD.GetAllPeople().Rows.Count.ToString();
        }

        private DataView _FilterData(object txt1 , object txt2)
        {
            DataTable dt = clsDVLD.GetAllPeople();
            DataView dv = dt.DefaultView;
            if (cbFilter.SelectedIndex == 3)
            {
                dv.RowFilter = $"{Convert.ToString(txt1)} = '{Convert.ToString(txt2)}'";
            }
            else
            {
                dv.RowFilter = $"{txt1} Like '%{txt2}%'";
            }

            return dv;
        }
        
        private void ManagePeople_Load(object sender, EventArgs e)
        {
            _RefreshPeopleList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddOREditPersonInfo frmAddEditPerson = new AddOREditPersonInfo(-1);
            frmAddEditPerson.ShowDialog();
            _RefreshPeopleList();  
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (txtFilter.Text == "")
            {
                dgvAllPeople.DataSource = clsDVLD.GetAllPeople();
            }
            else
            {
                dgvAllPeople.DataSource = _FilterData(cbFilter.Text, txtFilter.Text);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = false;
            dgvAllPeople.DataSource = clsDVLD.GetAllPeople();
            if (cbFilter.SelectedIndex == 0)
            {
               
                txtFilter.Enabled = false;
                dgvAllPeople.DataSource = clsDVLD.GetAllPeople();

            }
            else
            {
                txtFilter.Visible = true;
                txtFilter.Enabled = true;
                
            }
            
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            AddOREditPersonInfo frmEdit = new AddOREditPersonInfo((int)dgvAllPeople.CurrentRow.Cells[0].Value);
            frmEdit.ShowDialog();

            _RefreshPeopleList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonDetails frmPersonDetails = new PersonDetails((int)dgvAllPeople.CurrentRow.Cells[0].Value);
            frmPersonDetails.ShowDialog();
        }
    }
}
