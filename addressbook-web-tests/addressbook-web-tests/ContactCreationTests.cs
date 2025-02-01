
using NUnit.Framework;



namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            GoToHomePage();
            Login(new AccountData("admin", "secret"));
            InitNewContactCreation();

            ContactData contact = new ContactData("asd", "qwe");

            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToContactsPage();
            Logout();
        }
    }
}
