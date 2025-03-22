using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace mantis_tests 
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [TestFixtureSetUp]
        
        public void setUpConfig()
        {
            app.Ftp.BackupFile(nameConfigFile);
            using (Stream localFile = File.Open("config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload(nameConfigFile, localFile);
            }
            
        }
        string nameConfigFile = "/config_inc.php";

        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData()
            {
                Name = "testuser",
                Password = "password",
                Email = "testuser@localhost.localdomain",
            };

            app.Registration.Register(account);
        }

        [TestFixtureTearDown]
        public void RestoreConfig()
        {
            app.Ftp.RestoreBackupFile(nameConfigFile);
        }
    }
}
