using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using NUnit.Framework;

namespace mantis_tests
{
    public class TestBase
    {
        // Инициализация ApplicationManager в SetUp тестов (перед началом тестов)
        protected ApplicationManager app; 

        [TestFixtureSetUp]
        public void SetupApplicationManager()
        {
            
            app = ApplicationManager.GetInstance();
            
        }
    }
}
