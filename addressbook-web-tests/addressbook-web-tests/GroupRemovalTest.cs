
using NUnit.Framework;


namespace WebAddressbookTests 
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        
        [Test]
        public void GroupRemovalTest()
        {
            navigator.GoToHomePage();
            loginHelper.Login(new AccountData("admin", "secret"));
            navigator.GoToGroupPage();
            groupHelper.SelectGroupByIndex(1);
            groupHelper.RemoveGroup();
            navigator.ReturnToGroupsPage();
            loginHelper.Logout();
        }
    }
}
