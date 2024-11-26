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
    public partial class AddNewUser : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        //int _PersonID;
        clsUser _User;
        int _UserID;
        clsDVLD _Person;
        public AddNewUser(int UserId)
        {
            InitializeComponent();
            this._UserID = UserId;
            if (_UserID == -1)
            {
                _Mode = enMode.AddNew;
            }
            else
            {
                _Mode = enMode.Update;
                _User = clsUser.Find(_UserID);
            }
        }

        private DataView _FilterData(object txt1, object txt2)
        {
            DataTable dt = clsUser.GetAllUsers();
            DataView dv = dt.DefaultView;
            if (cbFilter.SelectedIndex == 1)
            {

                dv.RowFilter = $"{Convert.ToString(txt1)} = '{Convert.ToString(txt2)}'";
            }
            else
            {
                dv.RowFilter = $"{txt1} Like '%{txt2}%'";
            }

            return dv;
        }

        private void _FillPersonDetails(clsDVLD person)
        {
           
            ucPersonDetails1.PersonID = person.ID.ToString();
            ucPersonDetails1.Name = person.FirstName + " " + person.SecondName + " " +
            person.ThirdName + " " + person.LastName;
            ucPersonDetails1.NationalNo = person.NationalNumber;
            ucPersonDetails1.Gender = person.Gender;
            ucPersonDetails1.Email = person.Email;
            ucPersonDetails1.Address = person.Address;
            ucPersonDetails1.DateOfBirth = person.DateOfBirth.ToString();
            ucPersonDetails1.Phone = person.Phone;
            ucPersonDetails1.Country = clsCountry.Find(person.CountryID).CountryName;
            if (person.ImagePath != "")
            {
                ucPersonDetails1.imagePath = person.ImagePath;

            }
        }
        private void _LoadData()
        {

           
            if (_Mode == enMode.AddNew)
            {
                
                lblMode.Text = "Add New User";
                _User = new clsUser();

                return;
            }

            _User = clsUser.Find(_UserID);
            _Person = clsDVLD.Find(_User.PersonID);

            if (_User == null)
            {
                MessageBox.Show("This form will be closed because No User with ID = " + _UserID);
                this.Close();

                return;
            }
            _FillPersonDetails(_Person);
            cbFilter.Enabled = false;
            txtFilter.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            
            lblMode.Text = "Update User";
            lblUserID.Text = _User.ID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
                      
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _Person = clsDVLD.FindByNationalNo(txtFilter.Text);
            if (_Person != null)
            {

                _FillPersonDetails(_Person);  
            }
            else
            {
                MessageBox.Show("The Person is not found","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex  +=1; 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            _User = clsUser.FindByPersonID(_Person.ID);
            _User.ID =Convert.ToInt32( lblUserID.Text);
            _User.UserName = txtUserName.Text;
            _User.Password = txtPassword.Text;
            _User.PersonID = _Person.ID;
             if (chkboxIsActive.Checked)
             {
               _User.IsActive = 1;
             }
             else
             {
               _User.IsActive = 0;
             }

           

            if(_User.Save())
            {
                MessageBox.Show("Data Saved Successfully");
                lblUserID.Text = _User.ID.ToString();
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddOREditPersonInfo frmAddNewPerson = new AddOREditPersonInfo(-1);
            frmAddNewPerson.DataBack += _FillPersonDetails;
            frmAddNewPerson.ShowDialog();
           
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "You have to insert a password");

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPassword, "");
            }

        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "You have to insert a user name");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, "");
            }
        }
                
        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "You have to confirm a password");
            }
            else if(txtConfirmPassword.Text != txtPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "You have to match a password");

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }
        }

        private void AddNewUser_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void ucPersonDetails1_Load(object sender, EventArgs e)
        {

        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilter.SelectedIndex == 0)
            {
                if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
