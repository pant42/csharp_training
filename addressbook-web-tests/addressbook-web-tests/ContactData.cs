﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    internal class ContactData
    {
        private string firstname;
        private string lastname;

        public ContactData (string firstname, string lastname)
        {
            this.firstname = firstname;
            this.lastname = lastname;
        }

        public string Firstname
        {
            get
            {
                return firstname;
               
            }
            set
            {
                this.firstname = value;
            }
         
        }
        public string Lastname
        {
            get
            {
                return lastname;
            }
            set
            {
                this.lastname = value;
            }
        }
    }
}
