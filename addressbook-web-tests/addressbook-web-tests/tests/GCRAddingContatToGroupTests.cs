using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class GCRAddingContatToGroupTests : AuthTestBase
    {
        [Test]
        public void GCRAddingContatToGroup()
        {
            // Берем весь список Групп GetAll и забираем в переменную group [0] элемент
            GroupData group = GroupData.GetAll()[0];
            // Сохраняем старый список Контактов В Группах
            List<ContactData> oldList = group.GetContacts();
            // Берем весь список Контактов GetAll и забираем в переменную contact First элемент
            ContactData contact = ContactData.GetAll().Except(oldList).First();

            // Сам метод помещения контакта в группу
            app.Contacts.AddContactToGroup(contact, group);

            // Сохраняем новый список Контактов
            List<ContactData> newList = group.GetContacts();
            // Добавляем в старый список Контактов В Группах, contact который добавили
            oldList.Add(contact);
            // Сортируем оба списка
            oldList.Sort();
            newList.Sort();

            System.Console.Out.WriteLine(oldList);
            System.Console.Out.WriteLine(newList);

            // Сравниваем оба списка (в старый добавили новый контакт, тепреь оба списка должны быть Equal
            Assert.AreEqual(oldList, newList);
        }
        [Test]
        public void CheckingGetAll()
        {
            IEnumerable<ContactData> contacts = ContactData.GetAll();
            IEnumerable<GroupData> groups = GroupData.GetAll();
            System.Console.Out.WriteLine("ContactData.GetAll() возвращает: " + contacts.Count() + " элементов.");
            System.Console.Out.WriteLine("ContactData.GetAll() возвращает: " + groups.Count() + " элементов.");
        }
    }
}
