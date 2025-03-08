using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WebAddressbookTests;

namespace WebAddressbookTests
{
    public class GroupTestBase : AuthTestBase
    {
        [TearDown]
        public void CompareGroupsUi_DB() 
        {
            if (PERFORM_LONG_UI_CHECK)
            {
                List<GroupData> fromUi = app.Groups.GetGroupList();
                List<GroupData> fromDb = GroupData.GetAll();

                fromUi.Sort();
                fromDb.Sort();

                Assert.AreEqual(fromUi, fromDb);
            }
        }
    }
}
