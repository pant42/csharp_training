using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static System.Net.WebRequestMethods;

namespace mantis_tests
{
    public class NavigationHelper : HelperBase
    {
        private string baseURL;
        private string controlPageUrl;
        public NavigationHelper(ApplicationManager manager, string baseURL, string controlPageUrl) : base(manager)
        {
            this.baseURL = baseURL;
            this.controlPageUrl = controlPageUrl;
        }
        public void OpenHomePage()
        {
            if (driver.Url == baseURL || string.IsNullOrEmpty(driver.Url))
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL);
        }
        public void GoToControlPage()
        {
            if (driver.Url == controlPageUrl)
            {
                return;
            }
            driver.FindElement(By.XPath("//a[@href='/mantisbt-2.22.1/manage_overview_page.php']")).Click();
        }
        public void GoToProjectPage()
        {
            if (driver.Url == "http://localhost/mantisbt-2.22.1/manage_proj_page.php")
            {
                return;
            }
            driver.FindElement(By.XPath("//a[@href='/mantisbt-2.22.1/manage_proj_page.php']")).Click();
        }
    }
}
