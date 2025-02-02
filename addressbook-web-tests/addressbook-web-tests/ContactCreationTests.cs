
using NUnit.Framework;



namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            navigator.GoToHomePage();
            loginHelper.Login(new AccountData("admin", "secret"));
            contactHelper.InitNewContactCreation();

            ContactData contact = new ContactData("asd", "qwe");

            contactHelper.FillContactForm(contact);
            contactHelper.SubmitContactCreation();
            navigator.ReturnToContactsPage();
            loginHelper.Logout();
        }
    }
}
