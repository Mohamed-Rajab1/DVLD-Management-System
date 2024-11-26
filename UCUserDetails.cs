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
    public partial class UCUserDetails : UserControl
    {
        public UCUserDetails()
        {
            InitializeComponent();
        }

        public string PersonID { set { ucPersonDetails1.PersonID = value; } }
        public string Name { set { ucPersonDetails1.Name = value; } }
        public string NationalNo { set { ucPersonDetails1.NationalNo = value; } }

        public int Gender{ set{ ucPersonDetails1.Gender = value;}}

        public string Email { set { ucPersonDetails1.Email = value; } }

        public string Address { set { ucPersonDetails1.Address = value; } }

        public string DateOfBirth { set { ucPersonDetails1.DateOfBirth = value; } }

        public string Phone { set { ucPersonDetails1.Phone = value; } }

        public string Country { set { ucPersonDetails1.Country = value; } }

        public string imagePath { set { ucPersonDetails1.imagePath =value; } }

        public string UserID { set { lblUserID.Text = value; } }
        public string UserName { set { lblUserName.Text = value; } }
        public int IsActive { set { if (value == 1) lblIsActive.Text = "Yes";
                else { lblIsActive.Text = "No"; } } }


       
    }

   
}
