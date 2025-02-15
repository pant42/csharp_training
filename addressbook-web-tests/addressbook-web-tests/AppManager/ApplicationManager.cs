using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebAddressbookTests
{
    public class ApplicationManager
    {
        
        private StringBuilder verificationErrors;
        private ChromeDriver driver;
        protected string baseURL;
        private bool acceptNextAlert = true;

        protected LoginHelper loginHelper;
        protected NavigationHelper navigator;
        protected GroupHelper groupHelper;
        protected ContactHelper contactHelper;

        private static ThreadLocal<ApplicationManager> app= new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            baseURL = "http://localhost/addressbook";
            loginHelper = new LoginHelper(this);
            navigator = new NavigationHelper(this, baseURL);
            groupHelper = new GroupHelper(this);
            contactHelper = new ContactHelper(this);
        }


          ~ApplicationManager()
        {
            loginHelper.Logout();
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance () 
        {
            if (! app.IsValueCreated) 
            {
                ApplicationManager newInstance = new ApplicationManager();
                app.Value = newInstance;
                newInstance.Navigator.GoToHomePage();
            }
            return app.Value;
        }

        public IWebDriver Driver 
        {
            get 
            { 
                return driver; 
            }
        }

        public LoginHelper Auth
        {

            get
            {
                return loginHelper;
            }
        }
        public NavigationHelper Navigator 
        {
            get 
            {
                return navigator;
            }
        }
        public GroupHelper Groups
        {
            get
            {
                return groupHelper;
            }
        }
        public ContactHelper Contacts
        {
            get
            {
                return contactHelper;
            }
        }
        


        public void Stop()
        {


        }
    }
}
