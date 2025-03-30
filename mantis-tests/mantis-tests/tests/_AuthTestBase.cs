using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    [TestFixture]
    public class _AuthTestBase : TestBase
    {
        [SetUp]
        public void SetupLogin()
        {
            // 1. Открываем страницу входа через навигатор
            app.Navigator.OpenHomePage();

            // 2. Ожидаем загрузки формы входа
            WaitForLoginForm();

            // 3. Выполняем вход
            app.Auth.Login(new AccountData("administrator", "root"));

            // 4. Проверяем успешность входа
            VerifyIsLoggedIn();
        }

        private void WaitForLoginForm()
        {
            var wait = new WebDriverWait(app.Driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.Id("login-form")));
        }

        private void VerifyIsLoggedIn()
        {
            var wait = new WebDriverWait(app.Driver, TimeSpan.FromSeconds(10));
            Assert.IsTrue(wait.Until(d => d.FindElement(By.Id("logged-in-user"))).Displayed);
        }
    }
}