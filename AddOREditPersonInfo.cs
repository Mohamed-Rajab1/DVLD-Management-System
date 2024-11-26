using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;
using My_First_Full_Real_Project.Properties;
using static DVLDBusinessLayer.clsDVLD;
namespace My_First_Full_Real_Project
{
    public partial class AddOREditPersonInfo : Form
    {
        
        public delegate void SendDataBack( clsDVLD person);
        public event SendDataBack DataBack;

        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        int _PersonID;
        clsDVLD _Person;
        public AddOREditPersonInfo(int personID)
        {
            InitializeComponent();
            _PersonID = personID;

            if (_PersonID == -1)
            {
                _Mode = enMode.AddNew;
            }
            else
            {
                _Mode = enMode.Update;
                _Person = clsDVLD.Find(_PersonID);
            }
        }

        private void _FillCountriesInComoboBox()
        {
            DataTable dtCountries = clsDVLD.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {

                cbCountry.Items.Add(row["CountryName"]);

            }

        }

        private void _LoadData()
        {

            _FillCountriesInComoboBox();
            cbCountry.SelectedIndex = 0;
            
            if (_Mode == enMode.AddNew)
            {
               lblRemoveImage.Visible = false;
                lblMode.Text = "Add New Person";
                _Person = new clsDVLD();

                return;
            }

            _Person = clsDVLD.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("This form will be closed because No Person with ID = " + _PersonID);
                this.Close();

                return;
            }
            lblMode.Text = "Update Person";
            lblRemoveImage.Visible=true;
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNumber.Text = _Person.NationalNumber;
            if (_Person.Gender == 0)
            {
                rbMale.Checked = true;

            }
            else
            {
                rbFemale.Checked = true;

            }
            txtEmail.Text = _Person.Email;
            txtPhone.Text = _Person.Phone;
            txtAddress.Text = _Person.Address;
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            if (_Person.ImagePath != "")
            {
                pictureBox1.Load(_Person.ImagePath);
            }



            //this will select the country in the combobox.
            cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Person.CountryID).CountryName);

        }
       

        private void AddOREditPersonInfo_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
          this.Close();            
        }

        private void dtpDateOfBirth_ValueChanged(object sender, EventArgs e)
        {
            dtpDateOfBirth.MaxDate = new DateTime(2006, 1, 1);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int CountryID = clsCountry.Find(cbCountry.Text).ID;
           
            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.NationalNumber = txtNationalNumber.Text;
            _Person.Email = txtEmail.Text;
            _Person.Phone = txtPhone.Text;
            _Person.Address = txtAddress.Text;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.CountryID = CountryID;

            if(rbMale.Checked)
            {
                _Person.Gender = 0;
            }
            else 
            {
                _Person.Gender = 1; 
            }


            if (pictureBox1.ImageLocation != null )
            {
                _Person.ImagePath = pictureBox1.ImageLocation;
            }
            else
            {
                _Person.ImagePath = "";
            }
            
            if (_Person.Save())
            {
                MessageBox.Show("Data Saved Successfully.");
                if (_Mode == enMode.AddNew)
                {
                    DataBack.Invoke(_Person);
                }
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.");
            }
            _Mode = enMode.Update;
            lblMode.Text = "Update Person";
        }

        private void lblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                // MessageBox.Show("Selected Image is:" + selectedFilePath);

                pictureBox1.Load(selectedFilePath);

            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.Female_512;
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || (txtEmail.Text.Contains("@gmail.com")))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, "");
            }
            else
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "You have to write a valid email");
            }
        }

        private void txtNationalNumber_Validating(object sender, CancelEventArgs e)
        {
            if(clsDVLD.IsNationalNumberExist(txtNationalNumber.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNumber, "National Number is exist");
            }
            else if (string.IsNullOrWhiteSpace(txtNationalNumber.Text))
            {
                e.Cancel= true;
                errorProvider1.SetError(txtNationalNumber, "You have to insert a national number");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNationalNumber, "");
            }
        }
    }
}
