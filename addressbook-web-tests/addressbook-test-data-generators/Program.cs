using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddressbookTests;


namespace addressbook_test_data_generators
{
    class Program
    {

        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]);
            StreamWriter writer = new StreamWriter(args[1]);

            for (int i = 0; i < count; i++)
            {
                writer.WriteLine(String.Format("{0},{1},{2}",
                    TestBase.GenerateRandomString(10), TestBase.GenerateRandomString(10), TestBase.GenerateRandomString(10)));
            }
            writer.Close();


            int countContacts = Convert.ToInt32(args[2]);
            StreamWriter writerContacts = new StreamWriter(args[3]);

            for (int i = 0; i < countContacts; i++)
            {
                writerContacts.WriteLine(String.Format("{0},{1},{2}",
                    TestBase.GenerateRandomString(5), TestBase.GenerateRandomString(8), TestBase.GenerateRandomString(15)));
            }
            writerContacts.Close();
        }
    }
}
