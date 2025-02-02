using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebAddressbookTests
{
    public class HelperBase
    {
        protected IWebDriver driver;

        public HelperBase (IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}