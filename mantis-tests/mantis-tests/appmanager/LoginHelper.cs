using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager)
        {
        }

        // Проверяем, залогинены ли
        public bool IsLoggedIn()
        {
            return IsElementPresent(By.Name("logout"));
        }

        // Логин
        public void Login(AccountData account)
        {
            if (!IsLoggedIn())
            {                
                driver.FindElement(By.Id("username")).Click();
                driver.FindElement(By.Id("username")).Clear();
                driver.FindElement(By.Id("username")).SendKeys("administrator");
                driver.FindElement(By.XPath("//input[@value='Войти']")).Click();
                driver.FindElement(By.Id("password")).Click();
                driver.FindElement(By.Id("password")).Clear();
                driver.FindElement(By.Id("password")).SendKeys("root");
                driver.FindElement(By.XPath("//input[@value='Войти']")).Click();
            }
        }

        // Логаут
        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.XPath("//div[@id='navbar-container']/div[2]/ul/li[2]/a/i[2]")).Click();
                driver.FindElement(By.LinkText("выход")).Click();
            }
        }
    }
}