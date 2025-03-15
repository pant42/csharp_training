using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class ContactToGroupRemovingTests : AuthTestBase
    {
        [Test]
        public void ContactToGroupRemove()
        {
            // Проверка есть ли в БД [0] нулевой элемент в списке контактов/ групп
            app.Contacts.СheckIfThereContact();
            app.Groups.СheckIfThereGroup();

            // Забираем из БД [GetId контакта] и [GetName Имя группы], из таблицы связанных контактов и групп
            string contactId = app.Contacts.GetContactFromGCR();
            string groupName = app.Groups.GetGroupNameFromGCR();

            // Отбираем контакты входящие в группу groupName
            app.Contacts.FindContactsInGroup(groupName);
            // Выбираем из списка оборанных контактов - наш, по contactId
            app.Contacts.SelectContactByStringId(contactId);

            // Нажимаем Remove
            app.Contacts.RemovingContactFromGroup(groupName);



        }


    }
}
