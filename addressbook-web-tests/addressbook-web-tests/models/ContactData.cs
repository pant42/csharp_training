

using System;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string allDetailInfo;
        private string firstLastName;
        private string addressToDetail;

        public ContactData()
        {

        }

        public ContactData (string lastname,string firstname)
        {
            Lastname = lastname;
            Firstname = firstname;            
        }
        public string Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }        
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
        public string FirstLastName
        {
            get
            {
                if (firstLastName != null)
                {
                    return firstLastName;
                }
                else
                {
                    return (RemovedRn(Firstname) + " " + RemovedRn(Lastname));
                }
            }
            set
            {
                firstLastName = value;
            }
        }
        public string AddressToDetail
        {
            get
            {
                if (addressToDetail != null)
                {
                    return addressToDetail;
                }
                else
                {
                    return BlockRn(Address);
                }
            }
            set
            {
                addressToDetail = value;
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
                    return (FirstLastName + AddressToDetail + BlockDoubleRn(AllPhones) + BlockDoubleRn(AllEmails));
                }
            }
            set
            {
                allDetailInfo = value;
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
        private string RemovedRn(string name)
        {
            if (name == null || name == "")
            {
                return "";
            }
            else
            {
                return name;
            }
        }
        private string BlockRn(string block)
        {
            if (block == null || block == "")
            {
                return "";
            }
            else
            {
                return "\r\n" + block;
            }
        }
        private string BlockDoubleRn(string block)
        {
            if (block == null || block == "")
            {
                return "";
            }
            else
            {
                return "\r\n\r\n" + block;
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
