
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

            app.Groups.RemoveGroup(0);

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            
            oldGroups.RemoveAt(0);
            Assert.AreEqual(newGroups.Count, oldGroups.Count);
        }
    }
}
