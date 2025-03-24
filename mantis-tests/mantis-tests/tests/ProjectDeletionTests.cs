using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

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
    }
}
