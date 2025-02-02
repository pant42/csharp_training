
using NUnit.Framework;


namespace WebAddressbookTests 
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        
        [Test]
        public void GroupRemovalTest()
        {
            app.Navigator.GoToGroupsPage();
            
            app.Groups
                .SelectGroupByIndex(1)
                .RemoveGroup();

            app.Groups.ReturnToGroupsPage();
            app.Auth.Logout();
        }
    }
}
