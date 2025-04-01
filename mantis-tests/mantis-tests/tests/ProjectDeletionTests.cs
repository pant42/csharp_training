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
            // 1. Предусловие: гарантируем наличие проекта
            app.API.ThereAlwaysSomeProjectByApi();

            // 2. Получаем список проектов ДО удаления (API)
            List<Mantis.ProjectData> beforeDelete = app.API.GetProjectsByApi().ToList();

            // 3. Удаляем проект через UI
            app.Project.OpenControlPage();
            ProjectData projectToDelete = app.Project.TakeProject();
            app.Project.DeleteFirstProject(projectToDelete);

            // 4. Получаем список ПОСЛЕ удаления (API)
            List<Mantis.ProjectData> afterDelete = app.API.GetProjectsByApi().ToList();

            // 5. Проверки:
            // - Количество уменьшилось на 1
            Assert.AreEqual(beforeDelete.Count - 1, afterDelete.Count,
                "Количество проектов должно уменьшиться на 1");

            // - Удалённого проекта нет в новом списке
            Assert.IsFalse(afterDelete.Any(p => p.id == projectToDelete.Id),
                $"Проект с ID={projectToDelete.Id} не был удалён");

            // - Все оставшиеся проекты совпадают
            var expectedProjects = beforeDelete
                .Where(p => p.id != projectToDelete.Id)
                .OrderBy(p => p.name)
                .Select(p => p.name);

            var actualProjects = afterDelete
                .OrderBy(p => p.name)
                .Select(p => p.name);

            CollectionAssert.AreEqual(expectedProjects, actualProjects,
                "Списки проектов не совпадают после удаления");
        }
    }
}
