using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void AddingContactToGroup()
        {

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).First();

            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();

            System.Console.Out.WriteLine(oldList);
            System.Console.Out.WriteLine(newList);

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
