
using System.Collections.Generic;
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

            List<ContactData> newContacts = app.Contacts.GetContactList();
            Assert.AreEqual(oldContacts.Count - 1, newContacts.Count);
        }
    }
}
