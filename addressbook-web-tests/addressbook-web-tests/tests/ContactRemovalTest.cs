
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        
        [Test]
        public void TheContactRemovalTest()
        {
            app.Navigator.GoToContactPage();

            app.Contacts
                .SelectContactByIndex("1")
                .RemoveContact();

            app.Navigator.ReturnToContactsPage();
            app.Auth.Logout();
        }
    }
}
