using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebAddressbookTests;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;


namespace addressbook_test_data_generators
{
    class Program
    {

        static void Main(string[] args)

        {
            // Всего 4 параметра: [0] Сколько Групп [1] groups.csv [2] Сколько Контактов [3] contacts.csv [4] Формат файлов
            // 2 groups.json 3 contacts.json json

            int count = Convert.ToInt32(args[0]);
            StreamWriter writer = new StreamWriter(args[1]);

            int countContacts = Convert.ToInt32(args[2]);
            StreamWriter writerContacts = new StreamWriter(args[3]);

            string format = args[4];


            //Генерация групп
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                {
                    Header = TestBase.GenerateRandomString(10),
                    Footer = TestBase.GenerateRandomString(10)
                });
            }
            if (format == "csv")
            {
                writeGroupsToCsvFile(groups, writer);
            }
            else if (format == "xml")
            {
                writeGroupsToXmlFile(groups, writer);
            }
            else if (format == "json")
            {
                writeGroupsToJsonFile(groups, writer);
            }
            else
            {
                System.Console.Out.Write(" Не допустимый формат " + format + ". Допустимые значения формата(в конце вводимой строки): csv, xml, json");
            }
                writer.Close();

            //Генерация контактов
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < countContacts; i++)
            {
                contacts.Add(new ContactData(TestBase.GenerateRandomString(5), TestBase.GenerateRandomString(8))
                {
                    Address = TestBase.GenerateRandomString(15)
                });
            }
            if (format == "csv")
            {
                writeContactsToCsvFile(contacts, writerContacts);
            }
            else if (format == "xml")
            {
                writeContactsToXmlFile(contacts, writerContacts);
            }
            else if (format == "json")
            {
                writeContactsToJsonFile(contacts, writerContacts);
            }
            else
            {
                System.Console.Out.Write(" Не допустимый формат " + format + ". Допустимые значения формата(в конце вводимой строки): csv, xml, json");
            }
            writerContacts.Close();


            // Методы для генерации Csv
            //Групп
            static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
            {
                foreach (GroupData group in groups)
                {
                    writer.WriteLine(String.Format("{0},{1},{2}",
                        group.Name, group.Header, group.Footer));
                }
            }
            //Контактов
            static void writeContactsToCsvFile(List<ContactData> contacts, StreamWriter writerContacts)
            {
                foreach (ContactData contact in contacts)
                {
                    writerContacts.WriteLine(String.Format("{0},{1},{2}",
                        contact.Lastname, contact.Firstname, contact.Address));
                }
            }

            // Методы для генерации Xml
            //Групп
            static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
            {
                new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
            }

            //Контактов
            static void writeContactsToXmlFile(List<ContactData> contacts, StreamWriter writerContacts)
            {
                new XmlSerializer(typeof(List<ContactData>)).Serialize(writerContacts, contacts);
            }

            // Методы для генерации Json
            //Групп
            static void writeGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
            {
                writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
            }
            //Контактов
            static void writeContactsToJsonFile(List<ContactData> contacts, StreamWriter writerContacts)
            {
                writerContacts.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
            }
        }
    }
}
