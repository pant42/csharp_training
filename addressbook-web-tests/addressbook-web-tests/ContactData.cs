﻿

namespace WebAddressbookTests
{
    public class ContactData
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
