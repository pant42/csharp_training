
using NUnit.Framework;
using OpenQA.Selenium;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        

        [Test]
        public void GroupCreationTest() 
        {
            navigator.GoToHomePage();
            loginHelper.Login(new AccountData("admin", "secret"));
            navigator.GoToGroupPage();
            groupHelper.InitNewGroupCreation();

            GroupData group = new GroupData("aaa");
            group.Header = "ddd";
            group.Footer = "fff";

            groupHelper.FillGroupForm(group);
            groupHelper.SubmitGroupCreation();
            navigator.ReturnToGroupsPage();
            loginHelper.Logout();
        }
    }
}
