
using System;
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

        public GroupHelper Create(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            InitNewGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
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

        public GroupHelper Modify(int p, GroupData newData)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroupByIndex(1);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupsPage();
            return this;
        }

        public GroupHelper InitNewGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }
        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }
        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);

            return this;
        }

        public GroupHelper SelectGroupByIndex(int index)
        {            
             driver.FindElement(By.XPath("(//input[@name= 'selected[]'])[" + index + "]")).Click();
             return this;            
        }                  
        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }
        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
            return this;

        }
        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }
        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }
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
    }
}
