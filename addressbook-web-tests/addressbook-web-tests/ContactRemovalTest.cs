
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        
        [Test]
        public void TheContactRemovalTest()
        {
            GoToHomePage();
            Login(new AccountData("admin", "secret"));
            GoToContactPage();
            SelectContactByIndex("1");
            RemoveContact();
            ReturnToContactsPage();
            Logout();
        }
    }
}
