
using System.Collections.Generic;
using System.Security.Cryptography;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        
        [Test]
        public void TheContactRemovalTest()
        {
            // Проверка на наличие контакта для удаления, и создание контакта чтобы его тут же удалить
            app.Contacts.IsAnyContact();

            // Сбор информации ДО удаления
            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeRemoved = oldContacts[0];

            //Само удаление контакта
            app.Contacts.DeleteThisContact(toBeRemoved);

            // 1. Сравнение: [кол-ва До] = [кол-во ПОСЛЕ] - 1
            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());

            // 2. (подготовка) Снова собираем список контактов ПОСЛЕ УДАЛЕНИЯ
            List<ContactData> newContacts = ContactData.GetAll();

            // 2. (подготовка) Из старого списка удаляем удаляемый контакт под индексом [0]
            oldContacts.RemoveAt(0);

            // 2. Сравнение списков newContacts и oldContacts
            Assert.AreEqual(newContacts, oldContacts);
            // 3. Проверка индексов
            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}
