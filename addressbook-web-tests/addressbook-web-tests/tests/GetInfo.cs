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
            string detailInfoContact = app.Contacts.GetDetailInfoContact(0);
            string tableInformationContact = app.Contacts.GetTableInformationContacttoString(0);

            System.Console.Out.Write("На деталке видно: " + detailInfoContact); 
            System.Console.Out.Write("В таблице видно: " + tableInformationContact);
        }

    } 
}
