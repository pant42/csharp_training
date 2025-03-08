using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class TestBase
    {
        public static bool PERFORM_LONG_UI_CHECK = true;
        // Инициализация ApplicationManager в SetUp тестов (перед началом тестов)
        protected ApplicationManager app; 

        [SetUp]
        public void SetupApplicationManager()
        {
            
            app = ApplicationManager.GetInstance();
            
        }

        // Функция рендома из заданных символов
        public static Random rnd = new Random();
        public static string GenerateRandomString(int max)
        {
            const string chars =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "abcdefghijklmnopqrstuvwxyz" +
                "0123456789" +
                "!#$%()*+-./:;<=>?@[]^_{|}~";

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
