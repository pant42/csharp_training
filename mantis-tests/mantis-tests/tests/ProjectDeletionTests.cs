using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.DevTools.V132.Page;

namespace mantis_tests
{
    public class ProjectDeletionTests : _AuthTestBase
    {
        [Test]
        public void ProjectDeletion()
        {
            // Предусловие, всегда есть хоть 1 проект чтобы его удалить
            app.Project.ThereAlwaysBeenSomeProject();

            // Собираем список проектов ДО теста
            app.Project.OpenControlPage();
            List<ProjectData> beforeDeletionjectsList = app.Project.GetAllProjecstList();

            // Выбираем первый проект, запоминаем его, заносим в объект и УДАЛЯЕМ
            ProjectData projectToDelete = app.Project.TakeProject();
            app.Project.DeleteFirstProject(projectToDelete);

            app.Project.OpenProjectPage();

            List<ProjectData> afterCreateProjectsList = app.Project.GetAllProjecstList();
            beforeDeletionjectsList.Remove(projectToDelete);

            // Сортируем и сравниваем
            beforeDeletionjectsList.Sort();
            afterCreateProjectsList.Sort();
            Assert.AreEqual(beforeDeletionjectsList, afterCreateProjectsList);
        }

        [Test]
        public void ProjectDeletionCheckByApi()
        {
            // Предусловие
            app.API.ThereAlwaysSomeProjectByApi();

            // Получаем список проектов ДО теста (через API)
            List<Mantis.ProjectData> beforeDeletionList = app.API.GetProjectsByApi().ToList();

            // Удаляем проект через UI
            app.Project.OpenControlPage();
            ProjectData projectToDelete = app.Project.TakeProject();            
            app.Project.DeleteFirstProject(projectToDelete);

            // Получаем список ПОСЛЕ удаления (через API)
            List<Mantis.ProjectData> afterDeleteList = app.API.GetProjectsByApi().ToList();
            // Удаляем проект из исходного списка
            beforeDeletionList.RemoveAll(p => p.id == projectToDelete.Id);

            // Сортируем и сравниваем
            beforeDeletionList.Sort((p1, p2) => p1.name.CompareTo(p2.name));
            afterDeleteList.Sort((p1, p2) => p1.name.CompareTo(p2.name));
            Assert.AreEqual(beforeDeletionList.Count, afterDeleteList.Count);
        }
    }
}
