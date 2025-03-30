using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class AccountData
    {
        // Свойства
        public string Id { get; internal set; }
        public string Name { get; }
        public string Password { get; }
        public string Email { get; set; } // Опциональное свойство
        

        // Конструктор с обязательными параметрами
        public AccountData(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
