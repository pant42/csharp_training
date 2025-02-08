
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        
        [Test]
        public void TheContactRemovalTest()
        {           
            app.Contacts.DeleteContact();            
        }
    }
}
