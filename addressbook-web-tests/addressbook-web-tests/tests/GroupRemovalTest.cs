
using NUnit.Framework;


namespace WebAddressbookTests 
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        
        [Test]
        public void GroupRemovalTest()
        {
            app.Groups.IsAnyGroup();
            app.Groups.RemoveGroup(1);
        }
    }
}
