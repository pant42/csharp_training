using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    
    public class GetInfo : AuthTestBase
    {
        [Test]

        public void TestGetInformation()
        {
            string tableInformationContact = app.Contacts.GetContactInformationFromTable(0).ToString();
            System.Console.Out.Write("В таблице видно: " + tableInformationContact);

            string detailInfoContact = app.Contacts.GetDetailInfoContact(0);
            System.Console.Out.Write("На деталке видно: " + detailInfoContact);

            
        }

    } 
}
