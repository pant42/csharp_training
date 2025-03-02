
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

        // Сложнгые методы для Создания, Модификации и Удаления контакта
        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToContactsPage();
            InitNewContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
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
        public ContactHelper DeleteContact()
        {
            manager.Navigator.GoToContactsPage();
            InitContactModification(0);
            RemoveContact();
            ReturnToContactsPage();
            return this;
        }

        // Проверка наличия контакта в списке
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

        // Простые методы для сложных
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
        public void ReturnToContactsPage()
        {
            driver.FindElement(By.LinkText("home")).Click();
        }
        // Селектор [] контакта и "Изменить"
        public ContactHelper SelectContact()
        {
            driver.FindElement(By.Name("selected[]")).Click();
            return this;
        }
        public void InitContactModification(int index)
        {

            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
        }

        // Кнопки
        // Кнопка "update"
        public ContactHelper ModifyContact()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }
        // Кнопка "Удалить" внутри контакта
        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//form[2]/input[2]")).Click();
            contactCache = null;
            return this;
        }
        // Кнопка "Подтвердить"
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        // Для генерации новых имен контактов
        private string randomContactName;
        public string RandCName(int length)
        {
            return randomContactName = GeneratedRandAzNub(length);
        }

        // Для тестов сравнения данных контакта
        // Для кол-ва элементов на странице (по [] селекторам)
        public int GetContactCount()
        {
            return driver.FindElements(By.Name("selected[]")).Count;
        }
        // Просмотр формы деталки контакта
        public void InitContactDetailsPage(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
    .FindElements(By.TagName("td"))[6]
    .FindElement(By.TagName("a")).Click();
        }
        // Для Списка контактов из Table
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

        // Для изъятия одного контакта из Table
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
        // Пересобираем изъятые данные из Table
        public string ContactTableToDetail(int index)
        {
            String lastName = GetContactInformationFromTable(index).Lastname;
            String firstName = GetContactInformationFromTable(index).Firstname;
            String address = GetContactInformationFromTable(index).Address;

            String phones = GetContactInformationFromTable(index).AllPhones;
            String emails = GetContactInformationFromTable(index).AllEmails;

            String ContactTableToDetail = firstName + " " + lastName + "\r\n" + address + "\r\n" + "\r\n" + phones + "\r\n" + "\r\n" + emails;

            return ContactTableToDetail.Trim();

        }
        // Для изъятия одного контакта из EditForm
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
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3

            };
        }
        // Для изъятия одного контакта из DetailPage
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

                    Replace("H: ", "").
                    Replace("M: ", "").
                    Replace("W: ", "")
                    ;
            }
        }

        // Для тестов "Сколько найдено"
        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }
    }
}
