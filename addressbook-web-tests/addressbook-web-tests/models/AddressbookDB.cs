using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using OpenQA.Selenium.DevTools.V130.Database;

namespace WebAddressbookTests
{
    public class AddressbookDB : LinqToDB.Data.DataConnection
    {
        public AddressbookDB() : base("Addressbook") 
        {      

        }
        public ITable<GroupData> Groups { get { return this.GetTable<GroupData>(); } }
        public ITable<ContactData> Contacts { get { return this.GetTable<ContactData>(); } }
    }
}
