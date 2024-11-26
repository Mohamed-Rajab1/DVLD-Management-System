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
    public partial class PersonDetails : Form
    {
        int _PersonID;
        clsDVLD _Person;
        public PersonDetails(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
        }

        private void PersonDetails_Load(object sender, EventArgs e)
        {
            _Person = clsDVLD.Find(_PersonID);

            userControl1.PersonID = _PersonID.ToString();
            userControl1.Name = _Person.FirstName + " " + _Person.SecondName + " " +
            _Person.ThirdName + " " + _Person.LastName;
            userControl1.NationalNo = _Person.NationalNumber;
            userControl1.Gender = _Person.Gender;
            userControl1.Email = _Person.Email;
            userControl1.Address = _Person.Address;
            userControl1.DateOfBirth = _Person.DateOfBirth.ToString();
            userControl1.Phone = _Person.Phone;
            userControl1.Country = clsCountry.Find(_Person.CountryID).CountryName;
            if (_Person.ImagePath != "")
            {
                userControl1.imagePath = _Person.ImagePath;

            }            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddOREditPersonInfo frmEdit = new AddOREditPersonInfo((int)_PersonID);
            frmEdit.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
