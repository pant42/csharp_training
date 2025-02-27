

using System;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string allDetailInfo;
        public ContactData (string lastname,string firstname)
        {
            Lastname = lastname;
            Firstname = firstname;            
        }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Id { get; set; }
        public string Address { get; set; }


        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }

        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return CleanUp(Email) + CleanUp(Email2) + CleanUp(Email3).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }
        public string AllPhones
        {
            get
            {
                if (allPhones != null )
                {
                    return allPhones;
                }
                else
                {
                    return CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone).Trim();
                }
            }
            set
            {
            allPhones = value;
            }
        }

        public string AllDetailInfo
        {
            get
            {
                if (allDetailInfo != null)
                {
                    return allDetailInfo;
                }
                else
                {
                    return CleanUp(AllPhones) + CleanUp(AllEmails).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "" )
            {
                return "";
            }
            else 
            {
                return Regex.Replace(phone,"[ -()]","") + "\r\n";
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
            return Lastname == other.Lastname && Firstname == other.Firstname;            
            
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            int result = Lastname.CompareTo(other.Lastname);
            if (result == 0)
            {
                result = Firstname.CompareTo(other.Firstname);
            }
            return result;
        }

        public override int GetHashCode()
        {
            return Lastname.GetHashCode() ^ Firstname.GetHashCode();
        }

        public override string ToString()
        {
            return
            "lastName = " + Lastname +
            "\nfirstName = " + Firstname +
            "\naddress = " + Address +
            "\nallPhones = " + AllPhones +
            "\nallEmails = " + AllEmails
            ;


        }
    }
}
