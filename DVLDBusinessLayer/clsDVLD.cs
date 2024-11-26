using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsDVLD
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ID { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public string NationalNumber { set; get; }

        public int Gender { set; get; }

        public DateTime DateOfBirth { set; get; }

        public string ImagePath { set; get; }

        public int CountryID { set; get; }

        public clsDVLD()

        {
            this.ID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.Email = "";
            this.Gender = 0;
            this.NationalNumber = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = -1;
            this.ImagePath = "";

            Mode = enMode.AddNew;

        }

        private clsDVLD(int ID, string FirstName, string SecondName, string ThirdName, string LastName,string NationalNumber,
            string Email, string Phone,int Gender, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)

        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalNumber=NationalNumber;
            this.Email = Email;
            this.Phone = Phone;
            this.Gender = Gender;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;

            Mode = enMode.Update;

        }
        public static DataTable GetAllPeople()
        {
            return clsDVLD_Data.GetAllPeople();

        }

        public static DataTable GetAllCountries()
        {
            return clsDVLD_Data.GetAllCountries();
        }

        public static clsDVLD Find(int ID)
        {

            string FirstName = "", SecondName = "", ThirdName = "" ,LastName = "", Email = "", Phone = "", Address = "", ImagePath = "" , NationalNumber="";
            DateTime DateOfBirth = DateTime.Now;
            int CountryID = -1;
            int Gender =0; 

            if (clsDVLD_Data.GetPersonInfoByID(ID, ref FirstName,ref SecondName,ref ThirdName, ref LastName,
                          ref NationalNumber,ref Email, ref Phone,ref Gender, ref Address, ref DateOfBirth, ref CountryID, ref ImagePath))

                return new clsDVLD(ID,  FirstName, SecondName,ThirdName, LastName,
                          NationalNumber, Email, Phone, Gender, Address, DateOfBirth, CountryID, ImagePath);
            else
                return null;

        }

        public static clsDVLD FindByNationalNo(string NationalNo)
        {

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "", NationalNumber = "";
            DateTime DateOfBirth = DateTime.Now;
            int CountryID = -1;
            int ID=0;
            int Gender = 0;

            if (clsDVLD_Data.GetPersonInfoByNationalNo( NationalNo,ref ID, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
                          ref Email, ref Phone, ref Gender, ref Address, ref DateOfBirth, ref CountryID, ref ImagePath))

                return new clsDVLD(ID, FirstName, SecondName, ThirdName, LastName,
                          NationalNo, Email, Phone, Gender, Address, DateOfBirth, CountryID, ImagePath);
            else
                return null;

        }

        private bool _AddNewPerson()
        {
            //call DataAccess Layer 

            this.ID = clsDVLD_Data.AddNewPerson(this.FirstName, this.SecondName, this.ThirdName, this.LastName,this.NationalNumber,
           this.Email, this.Phone, this.Gender, this.Address,
           this.DateOfBirth, this.CountryID, this.ImagePath);

            return (this.ID != -1);
        }

        private bool _UpdatePerson()
        {
            //call DataAccess Layer 

            return clsDVLD_Data.UpdatePerson(this.ID, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.NationalNumber,
           this.Email, this.Phone, this.Gender, this.Address,
           this.DateOfBirth, this.CountryID, this.ImagePath);

        }



        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {

                        Mode = enMode.Update;

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePerson();

            }

            return false;
        }

        public static bool IsNationalNumberExist(string NationalNo)
        {
            return clsDVLD_Data.GetNationalNumber(NationalNo);
        }

    }
}
