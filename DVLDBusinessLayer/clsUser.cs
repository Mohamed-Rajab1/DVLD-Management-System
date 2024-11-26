using DVLDDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDBusinessLayer
{
    public class clsUser
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ID { set; get; }
        public int PersonID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
       
        public int IsActive { set; get; }

        public clsUser()
        {
            this.ID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = 0;
           
            Mode = enMode.AddNew;

        }

        private clsUser(int ID,int PersonID ,string UserName, string Password, int IsActive)
        {
            this.ID = ID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
           
            Mode = enMode.Update;

        }
        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();

        }

        public static DataTable GetAllCountries()
        {
            return clsDVLD_Data.GetAllCountries();
        }

        public static clsUser Find(int ID)
        {

            string UserName = "", Password = "";
            int IsActive = 0;
            int PersonID = -1;

            if (clsUserData.GetUserInfoByID( ID, ref PersonID, ref UserName, ref Password, ref IsActive))

                return new clsUser(ID,PersonID, UserName, Password, IsActive);
            else
                return null;

        }

        public static clsUser FindByPersonID(int PersonID)
        {

            string UserName = "", Password = "";
            int IsActive = 0;
            int ID = -1;

            if (clsUserData.GetUserInfoByPersonID(ref ID, PersonID, ref UserName, ref Password, ref IsActive))

                return new clsUser(ID, PersonID, UserName, Password, IsActive);
            else
                return null;

        }

        public static clsUser FindByUserNameAndPassword(string UserName,string Password)
        {

            int PersonID = -1;
            int IsActive = 0;
            int ID = -1;

            if (clsUserData.GetUserInfoByUserNameAndPassword(ref ID, ref PersonID,  UserName,  Password, ref IsActive))

                return new clsUser(ID, PersonID, UserName, Password, IsActive);
            else
                return null;

        }


        private bool _AddNewUser()
        {
            //call DataAccess Layer 

            this.ID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);

            return (this.ID != -1);
        }

        private bool _UpdateUser()
        {
            //call DataAccess Layer 

            return clsUserData.UpdateUser(this.ID,this.PersonID, this.UserName, this.Password, this.IsActive);

        }



        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }

            return false;
        }

        
    }


}

