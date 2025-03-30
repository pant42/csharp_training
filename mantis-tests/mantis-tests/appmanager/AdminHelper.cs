using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SimpleBrowser.WebDriver;

namespace mantis_tests
{
    public class AdminHelper : HelperBase
    {
        private string baseUrl;

        public AdminHelper(ApplicationManager manager, String baseUrl) : base(manager) 
        { this.baseUrl = baseUrl; }

        // Для аккаунтов, которые не должны повторяться, мы их будем проверять в обход браузера
        public List<AccountData> GetAllAccounts()
        {
            return null;
        }

        public void DeleteAccount(AccountData account)
        {
           IWebDriver driver = OpenAppAndLogin();
        }

        private IWebDriver OpenAppAndLogin()
        {
            IWebDriver driver = new SimpleBrowserDriver();
            driver.Url = baseUrl;
            driver.FindElement(By.Name("username")).SendKeys("administrator");
            driver.FindElement(By.Name("password")).SendKeys("root"); 
            driver.FindElement(By.XPath("//*[@type = 'submit']")).Click();
            return driver;
        }

        // Для ПРОЕКТОВ, которые должны быть, если мы удаляем проект какой-либо, мы их будем проверять в обход браузера
    }
}
