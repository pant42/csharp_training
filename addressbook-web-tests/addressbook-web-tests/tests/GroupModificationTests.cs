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

            app.Groups.Modify(1,newData);
        }
    }
}
