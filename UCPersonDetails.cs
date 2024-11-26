using DVLDBusinessLayer;
using My_First_Full_Real_Project.Properties;
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
    public partial class UCPersonDetails : System.Windows.Forms.UserControl
    {
        clsDVLD person;
        public UCPersonDetails()
        {

            InitializeComponent();
            
        }

        public string PersonID { set { lblPersonID.Text = value;}}
        public string Name { set { lblName.Text = value; }}
        public string NationalNo { set { lblNationalNo.Text = value;}}

        public int Gender { set { if (value == 0) { lblGender.Text = "Man"; pbGender.Image = Resources.Man_32;}
                else { lblGender.Text = "Woman"; pbGender.Image = Resources.Woman_32;}}}

        public string Email { set{ lblEmail.Text = value; }}

        public string Address { set { lblAddress.Text = value; }}

        public string DateOfBirth { set { lblDateOfBirth.Text = value; }}

        public string Phone { set { lblPhone.Text = value; }}

        public string Country { set { lblCountry.Text = value; }}

        public string imagePath { set { pictureBox1.Load(value);}}

        private void lklblEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(int.TryParse(lblPersonID.Text,out int id))
            {
                AddOREditPersonInfo frmEdit = new AddOREditPersonInfo(id);
                frmEdit.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("Invalid Person ID","Eror",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void lblEmail_Click(object sender, EventArgs e)
        {

        }
    }
}
