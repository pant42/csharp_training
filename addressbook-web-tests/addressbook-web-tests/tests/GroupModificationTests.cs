using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase

    {
        [Test]
        public void GroupModificationTest()
        {
            app.Groups.IsAnyGroup();

            GroupData newData = new GroupData("zzz");
            newData.Header = "ttt";
            newData.Footer = "qqq";

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Modify(0,newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;

            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
