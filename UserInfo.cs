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
    public partial class UserInfo : Form
    {
        clsUser _User;
        clsDVLD _Person;
        int _UserID;
        public UserInfo(int Userid)
        {
            InitializeComponent();
            _UserID = Userid;
            _User = clsUser.Find(_UserID);
            _Person = clsDVLD.Find(_User.PersonID);
        }

        private void _LoadData()
        {
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

        private void ucUserDetails1_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
    }
}
