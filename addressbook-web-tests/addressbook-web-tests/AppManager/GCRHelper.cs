using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAddressbookTests;

namespace WebAddressbookTests
{
    public class GCRHelper : HelperBase
    {
        public GCRHelper(ApplicationManager manager) : base(manager)
        {
        }

        public static List<GroupContactRelation> GetGCRList()
        {
            List<GroupContactRelation> gcrEntries;
            using (var db = new AddressbookDB())
            {
                gcrEntries = db.GetAllGroupContactRelations();
            }

            return gcrEntries;
        }
        public static void LogsOutContactGroupIds(List<GroupContactRelation> gcrEntries)
        {
            foreach (var entry in gcrEntries)
            {
                Console.WriteLine($"GroupId: {entry.GroupId}, ContactId: {entry.ContactId}");
            }
        }
        public static void GetIdContactAndGroupFromGCRList(List<GroupContactRelation> gcrEntries, out string groupId, out string contactId)
        {
            // Берем первую запись из таблицы GroupContactRelation
            GroupContactRelation gcrEntry = gcrEntries[0];

            // Получаем ID группы и контакта
            groupId = gcrEntry.GroupId;
            contactId = gcrEntry.ContactId;
        }

    }
}
