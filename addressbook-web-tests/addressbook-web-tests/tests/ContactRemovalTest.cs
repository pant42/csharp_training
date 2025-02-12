
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
            app.Contacts.DeleteContact();

        }
    }
}
