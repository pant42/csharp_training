using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace addressbook_tests_autoit
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void TestGroupCreation()
        {
            // Запоминаем список групп ДО создания в oldGroups
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            // (Предсоздание) Создаем новую группу. Сначала создаем объект newGroup с Name = "GrNaAutoIt"
            GroupData newGroup = new GroupData() 
            {
                Name = "GrNaAutoIt" 
            };
            // (СОЗДАНИЕ ГРУППЫ) Затем добавляем новую группу с newGroup с Name = "GrNaAutoIt"
            app.Groups.Add(newGroup);

            // Запоминаем новый список групп
            List<GroupData> newGroups = app.Groups.GetGroupList();

            // В старый добавляем новую созданную группу и сортируем оба списка
            oldGroups.Add(newGroup);
            oldGroups.Sort();
            newGroups.Sort();
            // Сравниваем оба списка (oldGroups = newGroups, потому что в oldGroups мы добавили группу newGroup 
            Assert.AreEqual(oldGroups.Count, newGroups.Count);
        }
    }
}
