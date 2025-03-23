using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : _AuthTestBase
    {        
        [Test]
        public void ProjectCreationWithNameAndDescription()
        {
            // Генерируем createdProject со случайными Именем и Описанием, типа ProjectData
            ProjectData createdProject = new ProjectData(GenerateRandomString(9))
            {
                Description = GenerateRandomString(30)
            };

            // Собираем список проектов ДО теста
            app.Project.OpenControlPage();
            List<ProjectData> beforeCreatejectsList = app.Project.GetAllProjecstList();

            // Создаем новый проект И собираем список проектов ПОСЛЕ создания
            app.Project.CreateProject(createdProject);

            app.Project.OpenProjectPage();

            List<ProjectData> afterCreateProjectsList = app.Project.GetAllProjecstList();
            beforeCreatejectsList.Add(createdProject);

            // Сортируем и сравниваем
            beforeCreatejectsList.Sort();
            afterCreateProjectsList.Sort();
            Assert.AreEqual(beforeCreatejectsList, afterCreateProjectsList);
        }
        [Test]

        public void testtest()
        {
            app.Project.OpenControlPage();
            app.Project.OpenProjectPage();
            List<ProjectData> projects = app.Project.GetAllProjecstList();
            Console.WriteLine("Получили списки проектов:");

            foreach (var project in projects)
            {
                Console.WriteLine($"ID: {project.Id}, Name: {project.Name}, Description: {project.Description}");
            }
        }
    }
}
