using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;

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

        // Генерируем случайные символы
        public static Random rnd = new Random();
        public static string GenerateRandomString(int max)
        {
            const string chars =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "abcdefghijklmnopqrstuvwxyz" +
                "0123456789" +
                " "
                ;

            var random = new Random();
            var result = new char[max];
            for (int i = 0; i < max; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }

            return new string(result);
        }
    }
}
