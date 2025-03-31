using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager)  {  }

        public Mantis.ProjectData[] GetProjectsByApi()
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            return client.mc_projects_get_user_accessible("administrator", "root");
             
        }
        public void CreateProjectsByApi()
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            
            Mantis.ProjectData project = new Mantis.ProjectData();
            project.name = GeneratedRandAzNub(5);
            project.description = GeneratedRandAzNub(8);

            client.mc_project_add("administrator", "root", project);
        }

        // Предусловие для проверки наличия проектов
        public void ThereAlwaysSomeProjectByApi()
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();

            // Получаем список доступных проектов
            var projects = client.mc_projects_get_user_accessible("administrator", "root");

            // Если проектов нет или список пуст, создаем новый
            if (projects == null || projects.Length == 0)
            {
                Mantis.ProjectData project = new Mantis.ProjectData();
                project.name = "randAPI" + GeneratedRandAzNub(5);
                project.description = "randAPI" + GeneratedRandAzNub(8);

                client.mc_project_add("administrator", "root", project);
                Console.WriteLine("Создан новый проект: " + project.name);
            }
        }
    }
}
