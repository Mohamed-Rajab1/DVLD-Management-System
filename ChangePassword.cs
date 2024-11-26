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
    public partial class ChangePassword : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;


        clsDVLD _Person;
        clsUser _User;
        int UserID;
        string CurrentPassword = "";
        public ChangePassword(int UserId)
        {
            InitializeComponent();
            UserID = UserId;
            _User = clsUser.Find(UserID);
            _Person = clsDVLD.Find(_User.PersonID);
        }

        private void _LoadData()
        {
             CurrentPassword = _User.Password;
            ucUserDetails1.PersonID = _Person.ID.ToString();
            ucUserDetails1.Name = _Person.FirstName + " " + _Person.SecondName + " " +
            _Person.ThirdName + " " + _Person.LastName;
            ucUserDetails1.NationalNo = _Person.NationalNumber;
            ucUserDetails1.Gender = _Person.Gender;
            ucUserDetails1.Email = _Person.Email;
            ucUserDetails1.Address = _Person.Address;
            ucUserDetails1.DateOfBirth = _Person.DateOfBirth.ToString();
            ucUserDetails1.Phone = _Person.Phone;
            ucUserDetails1.Country = clsCountry.Find(_Person.CountryID).CountryName;
            if (_Person.ImagePath != "")
            {
                ucUserDetails1.imagePath = _Person.ImagePath;
               
            }

            ucUserDetails1.UserID = _User.ID.ToString();
            ucUserDetails1.UserName = _User.UserName;
            ucUserDetails1.IsActive = _User.IsActive;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            _LoadData();    
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text) )
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "You have to insert a current password");

            }
            else if(txtPassword.Text != CurrentPassword)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "You have to insert a current password");

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPassword, "");
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "You have to insert a new password");

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNewPassword, "");
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "You have to confirm a password");
            }
            else if (txtConfirmPassword.Text != txtNewPassword.Text)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            _User = clsUser.Find(UserID);
            _Person = clsDVLD.Find(_User.PersonID);
            _User.Password = txtNewPassword.Text;
            if (_User.Save())
            {
                MessageBox.Show("Data Saved Successfully");
                
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully");
            }
        }
    }
}
