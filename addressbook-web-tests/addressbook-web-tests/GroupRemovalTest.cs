
using NUnit.Framework;


namespace WebAddressbookTests 
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        
        [Test]
        public void GroupRemovalTest()
        {
            app.Navigator.GoToHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Navigator.GoToGroupPage();
            app.Groups.SelectGroupByIndex(1);
            app.Groups.RemoveGroup();
            app.Navigator.ReturnToGroupsPage();
            app.Auth.Logout();
        }
    }
}
