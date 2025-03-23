using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

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
            if (! IsLoggedIn())
            {
                Type(By.Name("username"), account.Name);
                Type(By.Name("password"), account.Password);
                driver.FindElement(By.XPath("//*[@type = 'submit']")).Click();
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
