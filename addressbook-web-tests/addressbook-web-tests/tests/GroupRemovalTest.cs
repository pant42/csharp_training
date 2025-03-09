
using System.Collections.Generic;
using NUnit.Framework;


namespace WebAddressbookTests 
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {
        
        [Test]
        public void GroupRemovalTest()
        {
            // Проверка на наличие группы для удаления, и создание группы чтобы ее тут же удалить
            app.Groups.IsAnyGroup();

            // Сбор информации ДО удаления
            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeRemoved = oldGroups[0];

            // Само удаление группы
            app.Groups.RemoveThisGroup(toBeRemoved);

            // 1. Сравнение: [кол-ва До] = [кол-во ПОСЛЕ] - 1
            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());

            // (подготовка 2) Снова собираем список групп ПОСЛЕ УДАЛЕНИЯ
            List<GroupData> newGroups = GroupData.GetAll();
            // (подготовка 2) Из старого списка удаляем удаляемую группу под индексом [0]
            oldGroups.RemoveAt(0);

            // 2. Сравнение списков newGroups и oldGroups
            Assert.AreEqual(oldGroups, newGroups);

            // 3. Проверка индексов
            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}
