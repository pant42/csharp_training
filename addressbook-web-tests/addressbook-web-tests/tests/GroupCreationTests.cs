using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;
using NUnit.Framework.Constraints;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using OpenQA.Selenium.DevTools.V130.Page;
using System.Linq;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase
    {
        // Генерация случайных данных для групп
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(5))
                {
                    Header = GenerateRandomString(10),
                    Footer = GenerateRandomString(15)
                });
            }
            return groups;
        }
        //---------------------------------
        // Достаём данные из CSV файла
        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();
            String[] lines = File.ReadAllLines(@"groups.csv");
            foreach (string l in lines)
            {
                String[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    Footer = parts[1],
                    Header = parts[2]
                });
            }
            return groups;
        }
        // Достаём данные из XML файла
        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
            return (List<GroupData>)
                new XmlSerializer(typeof(List<GroupData>))
                .Deserialize(new StreamReader(@"groups.xml"));
        }
        // Достаём данные из JSON файла
        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(File.ReadAllText(@"groups.json"));
        }
        //---------------------------------


        // Сам тест
        [Test, TestCaseSource("GroupDataFromJsonFile")]
        public void GroupCreationTest(GroupData group)
        {
            // Сохраняем oldGroups - старый список групп
            List<GroupData> oldGroups = GroupData.GetAll();

            // Добавляем новую группу на основе данных group - созданные вверху, в TestFixture
            app.Groups.Create(group);

            // Наши проверки:

            // 1. Кол-во групп "до" (oldGroups) + 1 = Текущему кол-ву групп
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            // (подготовка) newGroups - это список групп после добавления + в список oldGroups добавляем созданную группу
            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.Add(group);

            // (подготовка) сортируем список "до" и список "после"
            oldGroups.Sort();
            newGroups.Sort();

            // 2. Сравнение списков групп
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void TestDBConnectivity()
        {
            DateTime start = DateTime.Now;
            List<GroupData> fromUi = app.Groups.GetGroupList();
            DateTime end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            List<GroupData> fromDb = GroupData.GetAll();
            end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));
        }

    }
}