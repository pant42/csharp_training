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
            GroupData removedGroupName = oldGroups.FirstOrDefault();
            // (УДАЛЕНИЕ ГРУППЫ)

            app.Groups.RemovingGroup(removedGroupName);

            // Запоминаем новый список групп
            List<GroupData> newGroups = app.Groups.GetGroupList();

            // Из старой группы удаляем удаленную группу, и сортируем оба списка
            oldGroups.Remove(removedGroupName);
            oldGroups.Sort();
            newGroups.Sort();
            // Сравниваем оба списка (oldGroups = newGroups, потому что в oldGroups мы добавили группу newGroup 
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}