
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {

        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }
        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToContactsPage();
            InitNewContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToContactsPage();

            return this;
        }
        public ContactHelper InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[20]")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact()
        {
            driver.FindElement(By.Name("selected[]")).Click();
            return this;
        }
        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//form[2]/input[2]")).Click();
            contactCache = null;
            return this;
        }
        public void ReturnToContactsPage()
        {
            driver.FindElement(By.LinkText("home")).Click();
        }

        public ContactHelper DeleteContact()
        {
            manager.Navigator.GoToContactsPage();
            InitContactModification(0);
            RemoveContact();
            ReturnToContactsPage();
            return this;
        }

        public ContactHelper Modify(ContactData newData)
        {
            manager.Navigator.GoToContactsPage();
            InitContactModification(0);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToContactsPage();

            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public void InitContactModification(int index)
        {
            
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
            
        }

        public void InitContactDetailsPage(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
    .FindElements(By.TagName("td"))[6]
    .FindElement(By.TagName("a")).Click();
        }

        public ContactHelper ModifyContact()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper IsAnyContact()
        {
            manager.Navigator.GoToContactsPage();
            if (IsElementPresent(By.Name("selected[]")))
            {

            }
            else
            {
                ContactData CreatedContact = new ContactData("НеБылоКонтактаФамилия", "НеБылоКонтактаИмя");
                Create(CreatedContact);
            }
            return this;
        }

//Методы для генерации новых имен контактов
        private string randomContactName;
        public string RandCName(int length)
        {
            return randomContactName = GeneratedRandAzNub(length);
        }

//Методы для Сравнений списков извлеченных контактов
        private List<ContactData> contactCache = null;
        

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToContactsPage();
                ICollection<IWebElement> elements = driver.FindElements(By.XPath("//*[@id=\"maintable\"]/tbody/tr[@name=\"entry\"]"));
                foreach (IWebElement element in elements)
                {
                    String Lastname = element.FindElement(By.XPath("td[2]")).Text;
                    String Firstname = element.FindElement(By.XPath("td[3]")).Text;
                    ContactData contact = new ContactData(Lastname, Firstname)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    };

                    contactCache.Add(contact);
                }
            }
            List<ContactData> contacts = new List<ContactData>();

            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.Name("selected[]")).Count;
        }

//Методы для извлечения информации из форм контактов
        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToContactsPage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));

            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;
            

            return new ContactData(lastName, firstName)
            {
                Address = address,
                AllEmails = allEmails,
                AllPhones = allPhones

            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToContactsPage();
            InitContactModification(index);


            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value"); 
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");            

            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            
            return new ContactData(lastName, firstName)
            {
                Address = address,
                AllPhones = homePhone + "\r\n" + mobilePhone + "\r\n" + workPhone,
                AllEmails = email + "\r\n" + email2 + "\r\n" + email3

            };
        }

        public int GetNumberOfSearchResults ()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }
        
        public string GetDetailInfoContact(int index)
        {
            manager.Navigator.GoToContactsPage();
            InitContactDetailsPage(index);

            string allContactInfo = driver.FindElement(By.CssSelector("div#content")).Text;
            if (allContactInfo == null || allContactInfo == "")
            {
                return "";
            }
            else
            {
                return allContactInfo.
                    Replace("\r\n", " ").
                    Replace("H: ", "").
                    Replace("M: ", "").
                    Replace("W: ", "")
                    ;
            }

        }
        public string GetTableInformationContacttoString(int index)
        {
            String lastName = GetContactInformationFromTable(index).Lastname;
            String firstName = GetContactInformationFromTable(index).Firstname;
            String address = GetContactInformationFromTable(index).Address;

            String phones = GetContactInformationFromTable(index).AllPhones;
            String emails = GetContactInformationFromTable(index).AllEmails;

            string allContactInfotable = (
                lastName  + " " + firstName + " " + address + "  " + phones + "  " + emails);

            return allContactInfotable.Replace("\r\n", " ");
        }

    }
}
