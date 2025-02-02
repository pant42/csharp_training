
using NUnit.Framework;



namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            app.Contacts.InitNewContactCreation();

            ContactData contact = new ContactData("asd", "qwe");

            app.Contacts
                .FillContactForm(contact)
                .SubmitContactCreation();

            app.Navigator.ReturnToContactsPage();
            app.Auth.Logout();
        }
    }
}
