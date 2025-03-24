using System;
using System.Collections.Generic;
using System.ComponentModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void CreateProject(ProjectData projectData)
        {
            manager.Navigator.GoToProjectPage();
            InitProjectCreation();
            FillProjectForm(projectData);
            ApplyProjectCreation();
            // Ожидание кнопки на окне списка проектов "Управление проектами"
            By manageProjectsLinkLocator = By.XPath("//a[@href='/mantisbt-2.2.0/manage_proj_page.php']");                        
            WaitForElementAndClick(manageProjectsLinkLocator);

            projectCache = null; // Очистка кэша
        }
        public void InitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@value='создать новый проект']")).Click();
        }
        public void FillProjectForm(ProjectData projectData)
        {
            driver.FindElement(By.Id("project-name")).Click();
            driver.FindElement(By.Id("project-name")).Clear();
            driver.FindElement(By.Id("project-name")).SendKeys(projectData.Name);
            driver.FindElement(By.Id("project-description")).Click();
            driver.FindElement(By.Id("project-description")).Clear();
            driver.FindElement(By.Id("project-description")).SendKeys(projectData.Description);
        }
        public void ApplyProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@value='Добавить проект']")).Click();
            projectCache = new List<ProjectData>();
        }
        public void WaitForElementAndClick(By locator, int timeoutInSeconds = 10)
        {
            // Создаем объект WebDriverWait с таймаутом
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));

            // Ожидаем, пока элемент станет видимым и кликабельным
            IWebElement element = wait.Until(driver =>
            {
                var elementToWait = driver.FindElement(locator);
                return (elementToWait != null && elementToWait.Displayed && elementToWait.Enabled) ? elementToWait : null;
            });
        }
        public void OpenControlPage()
        {
            manager.Navigator.GoToControlPage();
        }
        public void OpenProjectPage()
        {
            manager.Navigator.GoToProjectPage();

        }

        // Для Списка проектов из таблицы
        private List<ProjectData> projectCache = null;
        // Ждем пока будет таблица с проектами на странице
        public void WaitWhileProjectTableAppear()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(d => d.FindElement(By.XPath("//table[@class='table table-striped table-bordered table-condensed table-hover']")));
        }
        //Сам сбор проектов с таблицы, распаршивание по ProjectData.Id, ProjectData.Name, ProjectData.Description
        public List<ProjectData> GetAllProjecstList()
        {            
            if (projectCache == null)
            {
                projectCache = new List<ProjectData>();
                manager.Navigator.GoToProjectPage();

                // Ожидание загрузки таблицы
                WaitWhileProjectTableAppear();

                // Находим строки таблицы с проектами
                ICollection<IWebElement> rows = driver.FindElements(
                    By.XPath("//table[@class='table table-striped table-bordered table-condensed table-hover']/tbody/tr")
                );
                Console.WriteLine($"Найдено строк: {rows.Count}");

                foreach (IWebElement row in rows)
                {
                    try
                    {
                        // Извлекаем данные из столбцов таблицы
                        string id = row.FindElement(By.XPath("./td/a")).GetAttribute("href").Split('=')[1];
                        string name = row.FindElement(By.XPath("./td/a")).Text;
                        string description = row.FindElement(By.XPath("./td[5]")).Text;

                        Console.WriteLine($"ID: {id}, Name: {name}, Description: {description}");

                        // Создаем объект ProjectData и добавляем его в кэш
                        ProjectData project = new ProjectData(name)
                        {
                            Description = description,
                            Id = id
                        };

                        projectCache.Add(project);
                    }
                    catch (NoSuchElementException ex)
                    {
                        Console.WriteLine($"Ошибка при извлечении данных: {ex.Message}");
                    }
                }
            }

            // Возвращаем копию кэша, чтобы избежать изменений извне
            return new List<ProjectData>(projectCache);
        }

        // Для удаления проекта
        public void ThereAlwaysBeenSomeProject()
        {
            manager.Navigator.GoToControlPage();
            manager.Navigator.GoToProjectPage();
            WaitWhileProjectTableAppear();

            // Проверяем наличие строк в таблице проектов
            var projectsTable = driver.FindElement(
                By.XPath("//table[@class='table table-striped table-bordered table-condensed table-hover']"));

            var projectRows = projectsTable.FindElements(By.XPath(".//tbody/tr"));

            // Если нет строк с проектами (пустая таблица)
            if (projectRows.Count == 0)
            {
                Console.WriteLine("Проекты не найдены, создаем временный проект");
                ProjectData createdProjectToRemove = new ProjectData("NameProjectToRemove")
                {
                    Description = "Временный проект для тестирования"
                };
                CreateProject(createdProjectToRemove);
            }
        }

        public ProjectData TakeProject()
        {
            manager.Navigator.GoToProjectPage();
            // Ожидание загрузки таблицы
            WaitWhileProjectTableAppear();
            // Находим первую строку таблицы с проектами
            IWebElement firstRow = driver.FindElement(
                By.XPath("//table[@class='table table-striped table-bordered table-condensed table-hover']/tbody/tr[1]")
            );
            try
            {
                // Извлекаем данные из столбцов таблицы
                string id = firstRow.FindElement(By.XPath("./td/a")).GetAttribute("href").Split('=')[1];
                string name = firstRow.FindElement(By.XPath("./td/a")).Text;
                string description = firstRow.FindElement(By.XPath("./td[5]")).Text;

                Console.WriteLine($"Взятый проект: ID: {id}, Name: {name}, Description: {description}");

                // Создаем и возвращаем объект ProjectData
                ProjectData takedFirstProject = new ProjectData(name)
                {
                    Description = description,
                    Id = id
                };
                return takedFirstProject;
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine($"Не изъяли первый проект: {ex.Message}");
                return null; 
            }
        }
        public void DeleteFirstProject(ProjectData projectToDelete)
        {
            driver.FindElement(
                By.XPath(
                    "//table[@class='table table-striped table-bordered table-condensed table-hover']" +
                    "//a[text()=" + "'" + projectToDelete.Name + "'" + "]")).Click();
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
            projectCache = null;
        }
    }
}