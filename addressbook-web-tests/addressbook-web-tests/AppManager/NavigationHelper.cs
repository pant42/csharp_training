﻿
using OpenQA.Selenium;

namespace WebAddressbookTests
{
    public class NavigationHelper : HelperBase
    {
        private string baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager)
        {            
            this.baseURL = baseURL;
        }

        public void GoToHomePage()
        {
            if (driver.Url == baseURL)
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL);
        }
        public void GoToGroupsPage()
        {
            if (driver.Url == baseURL
                && IsElementPresent(By.Name("new"))) 
            {
                return;
            }

            driver.FindElement(By.LinkText("groups")).Click();
        }

        public void GoToContactsPage()
        {
            if (driver.Url == baseURL)
            {
                return;
            }
            driver.FindElement(By.LinkText("home")).Click();
        }
    }
}
