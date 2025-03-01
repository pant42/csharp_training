
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(5)) {
                    Header = GenerateRandomString(10),
                    Footer = GenerateRandomString(15)
                });
            }    
            return groups;
        }
        public static IEnumerable<GroupData> GroupDataFromFile()
        {
            List<GroupData> groups = new List<GroupData>();
            String[] lines = File.ReadAllLines(@"groups.csv");
            foreach (string l in lines)
            {
                String[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0]) {
                    Footer = parts[1],
                    Header = parts[2]
                });
            }
            return groups;
        }



        [Test, TestCaseSource("GroupDataFromFile")]
        public void GroupCreationTest(GroupData group)
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);
            
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);

            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }



    }
}
