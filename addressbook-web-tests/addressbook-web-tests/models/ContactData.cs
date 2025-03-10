

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;
using System.Linq;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string allDetailInfo;
        private string firstLastName;
        private string addressToDetail;
        private string allPhonesToDetail;
        public ContactData()
        {

        }
        public ContactData(string lastname, string firstname)
        {
            Lastname = lastname;
            Firstname = firstname;
        }
        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "address")]
        public string Address { get; set; }


        [Column(Name = "home")]
        public string HomePhone { get; set; }

        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }


        [Column(Name = "email")]
        public string Email { get; set; }

        [Column(Name = "email2")]
        public string Email2 { get; set; }

        [Column(Name = "email3")]
        public string Email3 { get; set; }

        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }


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
                    return (FormatEndRN(Email) + FormatEndRN(Email2) + FormatEndRN(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }
        private string FormatEndRN(string mail)
        {
            if (mail == null || mail == "")
            {
                return "";
            }
            else
            {
                return (mail + "\r\n");
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
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
            allPhones = value;
            }
        }
        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            else
            {
                return phone.
                    Replace(" ","").
                    Replace("-","").
                    Replace("(","").
                    Replace(")", "")
                    + "\r\n"
                    ;
            }
        }

        //Для пересборки данных из таблицы контактов
        //Фио первая строка
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
                    return (RemovedRn(Firstname) + " " + RemovedRn(Lastname)).Trim();
                }
            }
            set
            {
                firstLastName = value;
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

        //Адресс для пересборки
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

        //Телефоны с префиксами для деталки
        public string AllPhonesToDetail
        {
            get
            {
                if (allPhonesToDetail != null)
                {
                    return allPhonesToDetail;
                }
                else
                {
                    return (PrefixHome(HomePhone) + PrefixMobile(MobilePhone) + PrefixWork(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }
        private string PrefixHome(string homePhone)
        {
            if (homePhone == null || homePhone == "")
            {
                return "";
            }
            else
            {
                return "H: ";
            }
        }
        private string PrefixMobile(string mobilePhone)
        {
            if (mobilePhone == null || mobilePhone == "")
            {
                return "";
            }
            else
            {
                return "M: ";
            }
        }
        private string PrefixWork(string workPhone)
        {
            if (workPhone == null || workPhone == "")
            {
                return "";
            }
            else
            {
                return "W: ";
            }
        }

        //Пересборка всей информации из table под вид в деталке
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

        //Для сопоставления
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

        public static List<ContactData> GetAll()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from g in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select g).ToList();
            }
        }
    }
}
