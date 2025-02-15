

using System;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
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
            return Firstname == other.Firstname;
            return Lastname == other.Lastname;
        }

        public int CompareTo(ContactData other)
        {
            if (!Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Firstname.CompareTo(other.Firstname);
            return Lastname.CompareTo(other.Lastname);
        }

        public override int GetHashCode()
        {
            return Firstname.GetHashCode();
            return Lastname.GetHashCode();
        }
        public override string ToString()
        {
            return "Firstname = " + Firstname;
            return "Lastname = " + Lastname;

        }
    }
}
