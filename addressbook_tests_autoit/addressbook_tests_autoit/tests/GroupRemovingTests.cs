using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_tests_autoit
{
    internal class GroupRemovingTests : TestBase
    {
        [Test]
        public void TestGroupRemoving()
        {
            // Запоминаем список групп ДО создания в oldGroups
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            // (УДАЛЕНИЕ ГРУППЫ)
            app.Groups.RemovingGroup();

            // Запоминаем новый список групп
            List<GroupData> newGroups = app.Groups.GetGroupList();

            // Сравниваем оба списка (oldGroups = newGroups, потому что в oldGroups мы добавили группу newGroup 
            Assert.AreEqual(oldGroups.Count-1 , newGroups.Count);
        }
    }
}