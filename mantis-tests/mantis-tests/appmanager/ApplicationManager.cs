using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace mantis_tests
{
    public class ApplicationManager
    {
        
        private StringBuilder verificationErrors;
        private ChromeDriver driver;
        private bool acceptNextAlert = true;

        private static ThreadLocal<ApplicationManager> app= new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            Registration = new RegistrationHelper(this);
            Ftp = new FtpHelper(this);
        }
        public static ApplicationManager GetInstance () 
        {
            if (! app.IsValueCreated) 
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = "http://localhost/mantisbt-2.2.0/login_page.php";
                app.Value = newInstance;
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
        public RegistrationHelper Registration { get; private set; }
        public FtpHelper Ftp { get; private set; }

        public void Stop() { }
    }
}
