using System;
using System.Collections.Generic;
using System.Linq;
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
            app.Contacts.IsAnyContact();
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            ContactData newData = new ContactData("zzz","xxx");
            app.Contacts.Modify(newData);

            List<ContactData> newContacts = app.Contacts.GetContactList();
            Assert.AreEqual(oldContacts.Count, newContacts.Count);
        }
    }
}
