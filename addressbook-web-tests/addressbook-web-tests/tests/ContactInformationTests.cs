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
        public void ContactTableInfoAccert() 
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(0);

            System.Console.Out.Write("В таблице видно Expected: " + fromTable);
            System.Console.Out.Write("На деталке видно But was: " + fromForm);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);

        }

        [Test]
        public void ContactTableDetailAccert()
        {            
            string tableInformationContact = app.Contacts.GetTableInformationContacttoString(0);
            string detailInfoContact = app.Contacts.GetDetailInfoContact(0);

            System.Console.Out.Write("В таблице видно Expected: " + tableInformationContact); 
            System.Console.Out.Write("На деталке видно But was: " + detailInfoContact);
            

            Assert.AreEqual(tableInformationContact, detailInfoContact);

        }

    }

}

