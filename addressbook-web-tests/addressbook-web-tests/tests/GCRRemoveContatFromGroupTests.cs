using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAddressbookTests
{
    public class GCRRemovingContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void GCRRemovingContactFromGroup()
        {
            // Предусловия Наличия групп и контактов
            app.Groups.CheckIfThereAnyGroup();
            app.Contacts.CheckIfThereAnyContact();

            // Получаем все записи из таблицы GroupContactRelation
            List<GroupContactRelation> gcrEntries = GCRHelper.GetGCRList();
            // Обеспечиваем НЕ пустой список gcrEntries
            gcrEntries = app.Contacts.GetNotNullGCREntires(gcrEntries);
            
            // Выводим в консоль все GroupId и ContactId а если пусто - так и пишем
            foreach (var entry in gcrEntries)
            {
                Console.WriteLine($"GroupId: {entry.GroupId}, ContactId: {entry.ContactId}");
            }
            Assert.IsTrue(gcrEntries.Count > 0, "Нет записей в таблице GroupContactRelation");

            // Берем первую запись из таблицы GroupContactRelation
            GroupContactRelation gcrEntry = gcrEntries[0];

            // Получаем ID группы и контакта
            string groupId = gcrEntry.GroupId;
            string contactId = gcrEntry.ContactId;
            Console.WriteLine("Взяли groupId: " + groupId + " Взяли ContactId: " + contactId);

            // Получаем имя группы по ID
            GroupData group = GroupData.GetAll().FirstOrDefault(g => g.Id == groupId);
            Assert.IsNotNull(group, "Группа с указанным ID не найдена");

            // Ищем контакты в группе
            app.Contacts.FindContactsInGroup(group.Name);

            // Выбираем контакт по ID
            app.Contacts.SelectContactByStringId(contactId);

            // Удаляем контакт из группы
            app.Contacts.RemovingContactFromGroup(group.Name);

            // Проверяем, что в таблице GroupContactRelation больше нет пары GroupId и ContactId
            List<GroupContactRelation> updatedGcrEntries;
            using (var db = new AddressbookDB())
            {
                updatedGcrEntries = db.GetAllGroupContactRelations();
            }

            Assert.IsFalse(updatedGcrEntries.Any(e => e.GroupId == groupId && e.ContactId == contactId),
                "Контакт не был удален из группы");
        }


    }
}