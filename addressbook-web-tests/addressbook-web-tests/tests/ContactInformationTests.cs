using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformation() 
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);
            

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
                        
        }

        [Test]
        public void TestContactDetail()
        {
            string detailInfoContact = app.Contacts.GetDetailInfoContact(0);
            string tableInformationContact = app.Contacts.GetTableInformationContacttoString(0);
            System.Console.Out.Write("На деталке видно: " + detailInfoContact);
            System.Console.Out.Write("В таблице видно: " + tableInformationContact);

            Assert.AreEqual(detailInfoContact, tableInformationContact);

        }

    }

}

