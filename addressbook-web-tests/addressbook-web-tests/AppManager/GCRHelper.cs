using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
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

    }
}
