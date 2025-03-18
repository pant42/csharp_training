using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace WebAddressbookTests
{
    public class GCRAddingContactToGroupTests : AuthTestBase 
    {
        [Test]
        public void GCRAddingContactToGroup()
        {
            // Предусловия
            // Проверка наличия групп, а если их нет - создаем
            app.Groups.CheckIfThereAnyGroup();
            // Получаем список всех групп И выбираем первую Группу
            List<GroupData> groups = GroupData.GetAll();
            GroupData group = groups[0];

            // Проверка наличия Контактов, а если их нет - создаем
            app.Contacts.CheckIfThereAnyContact();
            app.Contacts.CheckIfThereAnyContactInNoneGroup();

            string firstContactNoneGroupId = app.Contacts.GetContactIdGroupNone();

            // Получаем список контактов в выбранной группе до добавления
            List<ContactData> oldList = group.GetContacts();

            // Добавляем контакт в группу по ID
            app.Contacts.AddContactToGroupByContactId(firstContactNoneGroupId, group);

            // Получаем обновленный список контактов в группе
            List<ContactData> newList = group.GetContacts();

            // Проверяем, что список контактов в группе увеличился на 1
            Assert.AreEqual(oldList.Count + 1, newList.Count);
            // Проверяем, что добавленный контакт действительно находится в группе
            Assert.IsTrue(newList.Any(c => c.Id == firstContactNoneGroupId));
        }
    }
}