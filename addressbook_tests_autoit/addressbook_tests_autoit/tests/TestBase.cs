using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace addressbook_tests_autoit
{
    
    public class TestBase
    {
        //SetUp и TearDown ApplicationManager
        public ApplicationManager app;

        [TestFixtureSetUp]
        public void InitApplication()
        {
            app = new ApplicationManager();
        }
        [TestFixtureTearDown]

        public void StopApplication()
        {
            app.Stop();
        }
    }
}
