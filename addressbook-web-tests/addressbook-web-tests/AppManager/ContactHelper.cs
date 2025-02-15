
using System;
using System.Collections.Generic;
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
            return this;
        }
        public void ReturnToContactsPage()
        {
            driver.FindElement(By.LinkText("home")).Click();
        }

        public ContactHelper DeleteContact()
        {
            manager.Navigator.GoToContactsPage();
            InitContactModification();
            RemoveContact();
            ReturnToContactsPage();
            return this;
        }

        public ContactHelper Modify(ContactData newData)
        {
            manager.Navigator.GoToContactsPage();
            InitContactModification();
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToContactsPage();

            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper InitContactModification()
        {
            driver.FindElement(By.XPath("//img[@alt='Edit']")).Click();
            return this;
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
        public List<ContactData> GetContactList()
        {
            List<ContactData> contacts = new List<ContactData>();
            manager.Navigator.GoToContactsPage();
            ICollection<IWebElement> elements = driver.FindElements(By.XPath("//*[@id=\"maintable\"]/tbody/tr[@name=\"entry\"]"));
            foreach (IWebElement element in elements)
            {
                String Lastname = element.FindElement(By.XPath("td[2]")).Text;
                String Firstname = element.FindElement(By.XPath("td[3]")).Text;
                
                contacts.Add(new ContactData(Lastname, Firstname));                
            }
            return contacts;
        }


    }
}
