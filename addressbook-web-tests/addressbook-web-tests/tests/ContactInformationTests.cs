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
        //Индекс тестируемого контакта
        int a = 1;

        [Test]
        public void TableAsseptEditForm() 
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(a);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(a);

            System.Console.Out.Write("В таблице видно Expected: " + fromTable);
            System.Console.Out.Write("На деталке видно But was: " + fromForm);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);

        }

        [Test]
        public void DetailAccertTable()
        {
            String detailInfoContact = app.Contacts.GetDetailInfoContact(a);
            String tableInfoContact = app.Contacts.ContactTableToDetail(a);
            
             
            System.Console.Out.Write("Деталка контакта (Expected): " + detailInfoContact);
            System.Console.Out.Write("Собрано из данных в таблице (But was): " + tableInfoContact);

            Assert.AreEqual(detailInfoContact,tableInfoContact);
        }

    }

}

