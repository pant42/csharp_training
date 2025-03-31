using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
namespace mantis_tests
{
    public class ApplicationManager
    {
        // 1. WebDriver и связанные поля
        private ChromeDriver driver;

        // 2. URL и конфигурация
        public string baseUrl;
        protected string controlPageUrl;

        // 3. Helpers
        private LoginHelper loginHelper;
        private ProjectHelper projectHelper;
        private NavigationHelper navigator;
        private RegistrationHelper registrationHelper;
        private FtpHelper ftpHelper;
        private AdminHelper adminHelper;
        private APIHelper apiHelper;

        // 4. ThreadLocal для управления экземплярами
        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        // 5. Конструктор
        private ApplicationManager()
        {
            // Инициализация драйвера
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            // Базовые URL
            baseUrl = "http://localhost/mantisbt-2.22.1/";
            controlPageUrl = "http://localhost/mantisbt-2.22.1/manage_overview_page.php";

            // Инициализация хелперов (в порядке зависимости)
            navigator = new NavigationHelper(this, baseUrl, controlPageUrl);
            loginHelper = new LoginHelper(this);
            projectHelper = new ProjectHelper(this);
            registrationHelper = new RegistrationHelper(this);
            ftpHelper = new FtpHelper(this);
            adminHelper = new AdminHelper(this, baseUrl);
            apiHelper = new APIHelper(this);
        }

        // 6. Методы
        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = newInstance.baseUrl;
                app.Value = newInstance;
            }
            return app.Value;
        }

        public void Stop()
        {
            // Логика остановки
        }

        // 7. Свойства для доступа к Helpers
        public IWebDriver Driver => driver;
        public AdminHelper Admin => adminHelper;
        public APIHelper API => apiHelper;
        public FtpHelper Ftp => ftpHelper;
        public LoginHelper Auth => loginHelper;
        public NavigationHelper Navigator => navigator;
        public ProjectHelper Project => projectHelper;
        public RegistrationHelper Registration => registrationHelper;
    }
}