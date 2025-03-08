using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase

    {
        [Test]
        public void GroupModificationTest()
        {
            app.Groups.IsAnyGroup();

            GroupData newData = new GroupData(app.Groups.RandGName(5)+ "MdN");
            newData.Header = app.Groups.RandGName(6) + "MdH";
            newData.Footer = app.Groups.RandGName(7) + "MdF";

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            GroupData oldData = oldGroups[0];

            app.Groups.Modify(0, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;

            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }
    }
}
