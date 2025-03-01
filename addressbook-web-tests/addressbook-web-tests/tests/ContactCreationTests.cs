
using System.Collections.Generic;
using System.Security.Cryptography;
using NUnit.Framework;
using System.IO;
using System;
using System.Xml.Serialization;



namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        //Генерация случайных данных для контактов
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(30), GenerateRandomString(30)));
            }
            return contacts;
        }
        //Достаём данные из csv файла
        public static IEnumerable<ContactData> ContactDataFromCsvFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            String[] lines = File.ReadAllLines(@"contacts.csv");
            foreach (string l in lines)
            {
                String[] parts = l.Split(',');
                contacts.Add(new ContactData(parts[0], parts[1])
                {
                    Address = parts[2],
                });
            }
            return contacts;
        }
        //Достаём данные из xml файла
        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            return (List<ContactData>)
                new XmlSerializer(typeof(List<ContactData>))
                .Deserialize(new StreamReader(@"contacts.xml"));
        }


        //Сам тест
        [Test, TestCaseSource("ContactDataFromXmlFile")]
        public void ContactCreationTest(ContactData contact)
        {
            //Сохраняем oldContacts - старый список контактов
            List<ContactData> oldContacts = app.Contacts.GetContactList();

            // Добавляем новый контакт на основе данных contact - созданные вверху, в TestFixture 
            app.Contacts.Create(contact);

            //Наши проверки:

            //1. Кол-во контактов "до" (oldContacts) + 1 = Текущему кол-ву контактов
            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());


            //(подготовка) newContacts - это список контактов после добавления + в список oldContacts добавляем созданный
            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);

            //(подготовка) сортируем список "до" и список "после"
            oldContacts.Sort();
            newContacts.Sort();
            //2. Сравнение списков контактов
            Assert.AreEqual(oldContacts, newContacts);
            
        }

    }
}
