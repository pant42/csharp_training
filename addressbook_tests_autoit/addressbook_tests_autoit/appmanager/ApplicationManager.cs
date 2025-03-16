using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoItX3Lib;

namespace addressbook_tests_autoit
{
    public class ApplicationManager
    {
        public static string WINTITLE = "Free Address Book";
        public ApplicationManager()
        {
            groupHelper = new GroupHelper(this);
            aux = new AutoItX3();
            try
            {
                aux = new AutoItX3();
                if (aux == null)
                {
                    Console.WriteLine("Ошибка: объект AutoItX3 не создан.");
                    throw new InvalidOperationException("Не удалось создать объект AutoItX3.");
                }

                Console.WriteLine("AutoItX3 успешно инициализирован.");
                aux.Run(@"C:\Tools\AppsForTesting\Addressbook\AddressBook.exe", "", aux.SW_SHOW);
                aux.WinWait(WINTITLE);
                aux.WinActivate(WINTITLE);
                aux.WinWaitActive(WINTITLE);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при инициализации AutoItX3: " + ex.Message);
                throw;
            }
        }

        private AutoItX3 aux;
        public AutoItX3 Aux
        {
            get { return aux; }
        }
        
        public void Stop()
        {
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d510");
        }


        private GroupHelper groupHelper;
        public GroupHelper Groups
        {
            get
            {
                return groupHelper;
            }
        }
    }
}
