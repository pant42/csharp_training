using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {

        [Test]
        public void ContactModificationTest()
        {
            // Проверка наличия контакта
            app.Contacts.IsAnyContact();

            // Создаем ContactData объект, на который изменим группу
            ContactData newData = new ContactData("LnMdc","FnMdc");

            // Сбор информации ДО удаления
            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeModify = oldContacts[0];

            // Само изменение контакта
            app.Contacts.ModifyThisContact(toBeModify, newData);

            // 1. Сравнение: [кол-ва До] = [кол-во ПОСЛЕ] - 1
            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            // (подготовка 2) Снова собираем список контактов ПОСЛЕ УДАЛЕНИЯ
            List<ContactData> newContacts = ContactData.GetAll();
            // (подготовка 2) Из старого списка переименовываем контакт под индексом [0]
            oldContacts[0].Lastname = newData.Lastname;
            oldContacts[0].Firstname = newData.Firstname;

            // 2. Сравнение списков newGroups и oldGroups
            Assert.AreEqual(oldContacts, newContacts);

            // 3. Проверка индексов
            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == toBeModify.Id)
                {
                    Assert.AreEqual(newData.Lastname, contact.Lastname);
                    Assert.AreEqual(newData.Firstname, contact.Firstname);
                }
            }
        }
    }
}
