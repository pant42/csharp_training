using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class ContactTestBase : AuthTestBase
    {
        [TearDown]
        public void CompareContactsUi_DB()
        {
            if (PERFORM_LONG_UI_CHECK)
            {
                List<ContactData> fromUi = app.Contacts.GetContactList();
                List<ContactData> fromDb = ContactData.GetAll();

                fromUi.Sort();
                fromDb.Sort();

                Assert.AreEqual(fromUi, fromDb);
            }
        }
    }
}
