using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager) { }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.CssSelector("a[href*='logout_page.php']"));
        }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedInAs(account.Name))
                    return;

                Logout();
            }

            OpenLoginPage();
            FillLoginForm(account.Name, account.Password);
            SubmitLogin();
            VerifyLoginSuccess(account.Name);
        }

        public bool IsLoggedInAs(string username)
        {
            return IsElementPresent(By.XPath($"//span[@class='user-info' and contains(text(), '{username}')]"));
        }

        public void Logout()
        {
            if (!IsLoggedIn()) return;

            WaitAndClick(By.CssSelector("span.user-info"));
            WaitAndClick(By.CssSelector("a[href*='logout_page.php']"));
            WaitForLogout();
        }

        private void OpenLoginPage()
        {
            driver.Navigate().GoToUrl(manager.baseUrl + "login_page.php");
        }

        private void FillLoginForm(string username, string password)
        {
            WaitAndSendKeys(By.Name("username"), username);
            WaitAndClick(By.XPath("//input[@value='Войти']"));
            WaitAndSendKeys(By.Name("password"), password);
            WaitAndClick(By.XPath("//input[@value='Войти']"));
        }

        private void SubmitLogin()
        {
            WaitAndClick(By.XPath("//input[@value='Войти']"));
        }

        private void VerifyLoginSuccess(string username)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElement(By.XPath($"//span[contains(@class,'user-info') and contains(text(), '{username}')]")));
        }

        private void WaitForLogout()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElement(By.Name("username")));
        }

        private void WaitAndClick(By locator, int timeout = 10)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(timeout))
                .Until(d => d.FindElement(locator))
                .Click();
        }

        private void WaitAndSendKeys(By locator, string text, int timeout = 10)
        {
            var element = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout))
                .Until(d => d.FindElement(locator));
            element.Clear();
            element.SendKeys(text);
        }
    }
}