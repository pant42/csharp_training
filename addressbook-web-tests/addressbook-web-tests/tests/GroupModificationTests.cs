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
            // Проверка наличия группы
            app.Groups.IsAnyGroup();

            // Создаем GroupData объект, на который изменим группу
            GroupData groupModifyData = new GroupData("MdN");
            groupModifyData.Header = "MdH";
            groupModifyData.Footer = "MdF";

            // Сбор информации ДО удаления
            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeModify = oldGroups[0];

            // Само изменение группы
            app.Groups.ModifyThisGroup(toBeModify, groupModifyData);

            // 1. Сравнение: [кол-ва До] = [кол-во ПОСЛЕ] - 1
            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            // (подготовка 2) Снова собираем список групп ПОСЛЕ УДАЛЕНИЯ
            List<GroupData> newGroups = GroupData.GetAll();
            // (подготовка 2) Из старого списка переименовываем группу под индексом [0]
            oldGroups[0].Name = groupModifyData.Name;

            // 2. Сравнение списков newGroups и oldGroups
            Assert.AreEqual(oldGroups, newGroups);

            // 3. Проверка индексов
            foreach (GroupData group in newGroups)
            {
                if (group.Id == toBeModify.Id)
                {
                    Assert.AreEqual(groupModifyData.Name, group.Name);
                }
            }
        }
    }
}
