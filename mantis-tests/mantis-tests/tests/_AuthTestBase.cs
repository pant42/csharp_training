using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class _AuthTestBase : TestBase
    {
        [TestFixtureSetUp]
        public void SetupLogin()
        {
            app.Driver.Navigate().GoToUrl("http://localhost/mantisbt-2.22.1/login_page.php");
            app.Auth.Login(new AccountData("administrator", "root"));
        }
    }
}