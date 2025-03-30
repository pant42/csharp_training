using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Internal;

namespace mantis_tests
{
    public class ApplicationManager
    {
        
        private StringBuilder verificationErrors;
        private ChromeDriver driver;
        private bool acceptNextAlert = true;
        public string baseUrl;
        protected string controlPageUrl;

        // Для создания Проекта в Мантисе
        protected LoginHelper loginHelper;
        protected ProjectHelper projectHelper;
        protected NavigationHelper navigator;

        public AdminHelper Admin { get;  set; }

        private static ThreadLocal<ApplicationManager> app= new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            // Драйвер + подождать пока инициализируется
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
                        
            // Базовый адрес
            baseUrl = "http://localhost/mantisbt-2.2.0/login_page.php";
            controlPageUrl = "http://localhost/mantisbt-2.2.0/manage_overview_page.php";

            // Хэлперы все тут
            Registration = new RegistrationHelper(this);
            Ftp = new FtpHelper(this);
            
            loginHelper = new LoginHelper(this); // Для создания Проекта в Мантисе
            projectHelper = new ProjectHelper(this);
            navigator = new NavigationHelper(this, baseUrl, controlPageUrl);
            Admin = new AdminHelper(this, baseUrl);

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

        public LoginHelper Auth
        {

            get
            {
                return loginHelper;
            }
        }
        public ProjectHelper Project
        {
            get
            {
                return projectHelper;
            }
        }
        public NavigationHelper Navigator
        {
            get
            {
                return navigator;
            }
        }
    }
}
