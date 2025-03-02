
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace WebAddressbookTests
{
    public class GroupHelper : HelperBase

    {
        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }
        // Сложнгые методы для Создания, Модификации и Удаления uhegg
        public GroupHelper Create(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            InitNewGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();

            return this;
        }
        public GroupHelper Modify(int p, GroupData newData)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroupByIndex(p);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupsPage();
            return this;
        }
        public GroupHelper RemoveGroup(int p)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroupByIndex(p);
            RemoveGroup();
            ReturnToGroupsPage();

            return this;
        }

        // Проверка наличия контакта в списке
        public GroupHelper IsAnyGroup()
        {
            manager.Navigator.GoToGroupsPage();
            if (IsElementPresent(By.Name("selected[]")))
            {

            }
            else
            {
                GroupData GroupToDelete = new GroupData("НебылоНоСоздал");
                Create(GroupToDelete);
                driver.FindElement(By.XPath("(//input[@name= 'selected[]'])")).Click();

            }
            return this;

        }

        // Простые методы для сложных
        public GroupHelper InitNewGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }
        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);

            return this;
        }
        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            groupCache = null;
            return this;
        }
        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
            return this;

        }

        // Селектор [] контакта
        public GroupHelper SelectGroupByIndex(int index)
        {            
             driver.FindElement(By.XPath("(//input[@name= 'selected[]'])[" + (index + 1) + "]")).Click();
             return this;            
        }

        // Кнопки
        // Кнопка "delete"
        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            groupCache = null;
            return this;
        }
        // Кнопка "edit"
        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }
        // Кнопка "Подтвердить"
        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            groupCache = null;
            return this;
        }

        // Для генерации новых имен групп
        private string randomGroupName;
        public string RandGName(int length)
        {
            return randomGroupName = GeneratedRandAzNub(length);
        }

        // Для тестов сравнения данных групп
        // Для кол-ва элементов на странице (по span.group)
        public int GetGroupCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
        }

        // Для Списка контактов из Table
        private List<GroupData> groupCache = null;        
        public List<GroupData> GetGroupList()
        {
            if (groupCache == null) 
            {
                groupCache = new List<GroupData>();

                manager.Navigator.GoToGroupsPage();

                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
                
                foreach (IWebElement element in elements)
                {                                       
                    groupCache.Add(new GroupData(null)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
                string allGroupNames = driver.FindElement(By.CssSelector("div#content form")).Text;
                
                string[] parts = allGroupNames.Split('\n');
                int shift = groupCache.Count - parts.Length;
                for (int i = 0; i < groupCache.Count; i++)
                {
                    if (i < shift)
                    {
                        groupCache[i].Name = "";
                    }
                    else
                    {
                        groupCache[i].Name = parts[i-shift].Trim();
                    }
                    
                }
            }
            List<GroupData> groups = new List<GroupData>();

            return new List <GroupData> (groupCache);
        }


    }
}
