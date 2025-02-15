
using System.Collections.Generic;
using NUnit.Framework;


namespace WebAddressbookTests 
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        
        [Test]
        public void GroupRemovalTest()
        {
            app.Groups.IsAnyGroup();
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.RemoveGroup(1);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            Assert.AreEqual(oldGroups.Count-1, newGroups.Count);
        }
    }
}
