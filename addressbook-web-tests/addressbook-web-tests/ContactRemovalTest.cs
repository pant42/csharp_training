
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        
        [Test]
        public void TheContactRemovalTest()
        {
            app.Navigator.GoToHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Navigator.GoToContactPage();
            app.Contacts.SelectContactByIndex("1");
            app.Contacts.RemoveContact();
            app.Navigator.ReturnToContactsPage();
            app.Auth.Logout();
        }
    }
}
