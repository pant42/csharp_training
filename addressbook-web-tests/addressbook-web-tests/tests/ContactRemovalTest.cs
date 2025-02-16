
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
            app.Contacts.IsAnyContact();
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.DeleteContact();

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.RemoveAt(0);
            Assert.AreEqual(newContacts.Count, oldContacts.Count);
        }
    }
}
