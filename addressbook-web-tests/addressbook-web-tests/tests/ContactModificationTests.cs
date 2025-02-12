﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {

        [Test]
        public void ContactModificationTest()
        {
            app.Contacts.IsAnyContact();

            ContactData newData = new ContactData("zzz","xxx");
            app.Contacts.Modify(newData);
        }
    }
}
