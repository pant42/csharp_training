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

        // Таблицы
        public ITable<GroupData> Groups { get { return this.GetTable<GroupData>(); } }
        public ITable<ContactData> Contacts { get { return this.GetTable<ContactData>(); } }
        public ITable<GroupContactRelation> GCR { get { return this.GetTable<GroupContactRelation>(); } }

        // Метод для получения всех записей из таблицы GroupContactRelation
        public List<GroupContactRelation> GetAllGroupContactRelations()
        {
            return (from gcr in GCR
                    select gcr).ToList();
        }
    }
}