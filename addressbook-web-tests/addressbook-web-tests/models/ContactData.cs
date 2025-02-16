

using System;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string lastname;
        private string firstname;        

        public ContactData (string lastname,string firstname)
        {
            this.lastname = lastname;
            this.firstname = firstname;
            
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

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Lastname == other.Lastname;
            return Firstname == other.Firstname;
            
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Lastname.CompareTo(other.Lastname);
            return Firstname.CompareTo(other.Firstname);
            
        }

        public override int GetHashCode()
        {
            return Lastname.GetHashCode();
            return Firstname.GetHashCode();
            
        }
        public override string ToString()
        {
            return "Lastname = " + Lastname;
            return "Firstname = " + Firstname;
            

        }
    }
}
