﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using Google.Protobuf.WellKnownTypes;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static LinqToDB.Reflection.Methods.LinqToDB;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {

        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        // Создание контакта
        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToContactsPage();
            InitNewContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToContactsPage();
            return this;
        }
        // Простые методы для создания контакта
        public ContactHelper InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("work"), contact.WorkPhone);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            return this;
        }
        public ContactHelper SimpleFillingContactForm(ContactData contact)
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

        // Изменение (Modify) контакта
        // Изменение первого (index = 0) контакта в таблице контактов
        public ContactHelper Modify(ContactData newData)
        {
            manager.Navigator.GoToContactsPage();
            InitContactModificationByIntIndex(0);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToContactsPage();
            return this;
        }
        // Изменение контакта по id bp передаваемого контакта из бд 
        public ContactHelper ModifyThisContact(ContactData contact, ContactData newData)
        {
            manager.Navigator.GoToContactsPage();
            InitContactModificationByStringId(contact.Id);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToContactsPage();
            return this;
        }
        // Простые методы для (Modify) изменения
        public void InitContactModificationByIntIndex(int index)
        {
            driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"))[7].FindElement(By.TagName("a")).Click();
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }
        public void InitContactModificationByStringId(string IdFromContact )
        {
            int id = Int32.Parse(IdFromContact);
            driver.FindElement(By.XPath("//a[@href='edit.php?id=" + id + "']")).Click();
        }
        public void EditContactById(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
    .FindElements(By.TagName("td"))[6]
    .FindElement(By.TagName("a")).Click();
        }

        // Удаление контакта
        public ContactHelper DeleteContact()
        {
            manager.Navigator.GoToContactsPage();
            InitContactModificationByIntIndex(0);
            RemoveContact();
            ReturnToContactsPage();
            return this;
        }
        public ContactHelper DeleteThisContact(ContactData contact)
        {
            manager.Navigator.GoToContactsPage();
            InitContactModificationByStringId(contact.Id);
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

        // Селектор [] контакта
        public ContactHelper SelectFirstContact()
        {
            driver.FindElement(By.Name("selected[]")).Click();
            return this;
        }
        public ContactHelper SelectContactByStringId(string id)
        {
            driver.FindElement(By.XPath("(//input[@name= 'selected[]' and @value='" + id + "'])")).Click();
            return this;
        }

        // Кнопка "Удалить" внутри контакта
        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//form[2]/input[2]")).Click();
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

            String ContactTableToDetail = GetContactInformationFromTable(index).AllDetailInfo.Trim();

            return ContactTableToDetail;

        }
        // Для изъятия одного контакта из EditForm
        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToContactsPage();
            InitContactModificationByIntIndex(index);


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

            return driver.FindElement(By.CssSelector("div#content")).Text
                .Replace("H: ", "")
                .Replace("M: ", "")
                .Replace("W: ", "")
                ;
        }

        // Для тестов "Сколько найдено"
        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }
        // Для GCRAdding
        // Для предусловия
        // Установить фильтр контактов по [none] - не входящих в группу
        public void ShowContactsNoneGroup()
        {
            manager.Navigator.GoToContactsPage();
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[none]");
        }

        // Для тестов по добавлению контактов в группы
        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            ShowContactsNoneGroup();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();

            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }
        public void AddContactToGroupByContactId(string id, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            ShowContactsNoneGroup();
            SelectContact(id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();

            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }
        private void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }
        private void SelectContact(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }
        private void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }
        private void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        // Методы для удаления контакта из группы
        // Ищем контакты, входящие в группу groupName
        public ContactHelper FindContactsInGroup(string groupName)
        {
            SelectFilterByGroupName(groupName);            
            return this;
        }
        // Установить фильтр группы по groupName
        public void SelectFilterByGroupName(string groupName)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(groupName);
        }


        // Нажатие кнопки "удалить из группы
        public void RemovingContactFromGroup(string groupName)
        {
            driver.FindElement(By.Name("remove")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
.Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            manager.Navigator.GoToContactsPage();
        }
        


        public string GetFirstContactId()
        {
            return driver.FindElement(By.XPath("//table[@id='maintable']//tr[@name='entry'][1]//input[@name='selected[]']"))
                .GetAttribute("value");
        }
        public void CheckIfThereAnyContact()
        {
            manager.Navigator.GoToContactsPage();
            int co = GetContactCount();
            if (co == 0)
            {
                ContactData CreatedContact = new ContactData("GCRAddingLnNone", "GCRAddingFnNone");
                Create(CreatedContact);
            }
        }
        public string GetContactIdGroupNone()
        {
            // Устанавливаем фильтр групп на [none], чтобы выбрать контакты, не входящие в группы
            ShowContactsNoneGroup();

            // Получаем ID первого контакта из таблицы с помощью метода GetFirstContactId
            string firstContactNoneGroupId = GetFirstContactId();
            SelectContactByStringId(firstContactNoneGroupId);
            return firstContactNoneGroupId;
        }

        public void CheckIfThereAnyContactInNoneGroup()
        {
            manager.Navigator.GoToContactsPage();
            ShowContactsNoneGroup();
            int co = GetContactCount();
            if (co == 0)
            {
                ContactData CreatedContact = new ContactData("GCRAddingLnNone", "GCRAddingFnNone");
                Create(CreatedContact);
            }
        }

        public void AddAnyContactToAnyGroup(GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            ShowContactsNoneGroup();
            SelectFirstContact();
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();

            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }
        public List<GroupContactRelation> GetNotNullGCREntires(List<GroupContactRelation> gcrEntries)
        {
            if (gcrEntries.Count == 0)
            {
                List<GroupData> groups = GroupData.GetAll();
                GroupData groupAdd = groups[0];
                AddAnyContactToAnyGroup(groupAdd);
                manager.Navigator.GoToContactsPage();
                // Перепроверяем в БД GroupContactRelation и перезаписываем gcrEntries
                List<GroupContactRelation> gcrEntriesAfterCountZero;
                using (var db = new AddressbookDB())
                {
                    gcrEntriesAfterCountZero = db.GetAllGroupContactRelations();
                }
                gcrEntries = gcrEntriesAfterCountZero;
            }

            return gcrEntries;
        }
    }
}
