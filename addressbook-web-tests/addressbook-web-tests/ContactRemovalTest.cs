
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        
        [Test]
        public void TheContactRemovalTest()
        {
            navigator.GoToHomePage();
            loginHelper.Login(new AccountData("admin", "secret"));
            navigator.GoToContactPage();
            contactHelper.SelectContactByIndex("1");
            contactHelper.RemoveContact();
            navigator.ReturnToContactsPage();
            loginHelper.Logout();
        }
    }
}
