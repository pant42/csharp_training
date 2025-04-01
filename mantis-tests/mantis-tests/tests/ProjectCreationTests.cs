using System;
using System.Collections.Generic;
using System.Linq;
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
        public void NOT_ACTUAL_ProjectCreationWithNameAndDescription()
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


        [Test]
        public void ProjectCreationByApi()
        {
            // 1. Генерируем данные для нового проекта
            ProjectData createdProject = new ProjectData(GenerateRandomString(9))
            {
                Description = GenerateRandomString(30)
            };

            // 2. Получаем список проектов ДО создания (через API)
            List<Mantis.ProjectData> beforeCreateProjectsList = app.API.GetProjectsByApi().ToList();

            // 3. Создаём проект через UI
            app.Project.CreateProject(createdProject);

            // 4. Получаем список проектов ПОСЛЕ создания (через API)
            List<Mantis.ProjectData> afterCreateProjectsList = app.API.GetProjectsByApi().ToList();

            // 5. Проверяем, что количество проектов увеличилось на 1
            Assert.AreEqual(
                beforeCreateProjectsList.Count + 1,
                afterCreateProjectsList.Count,
                "Количество проектов должно увеличиться на 1 после создания."
            );

            var beforeProjectsWithoutNew = beforeCreateProjectsList
                .OrderBy(p => p.name)
                .Select(p => new { p.name, p.description });

            var afterProjectsWithoutNew = afterCreateProjectsList
                .Where(p => p.name != createdProject.Name) // Исключаем созданный проект
                .OrderBy(p => p.name)
                .Select(p => new { p.name, p.description });

            // Проверяем, что все остальные проекты остались неизменными
            CollectionAssert.AreEqual(
                beforeProjectsWithoutNew,
                afterProjectsWithoutNew,
                "Списки проектов (кроме созданного) должны совпадать."
            );
        }
    }
}
