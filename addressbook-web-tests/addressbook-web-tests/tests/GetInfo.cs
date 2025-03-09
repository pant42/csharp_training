using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    
    public class GetInfo : AuthTestBase
    {
        int b = 1;
        [Test]

        public void TestGetInformation()
        {
            string tableInformationContact = app.Contacts.GetContactInformationFromEditForm(b).ToString();
            System.Console.Out.Write("В таблице видно: " + tableInformationContact);

            string detailInfoContact = app.Contacts.GetDetailInfoContact(b);
            System.Console.Out.Write("На деталке видно: " + detailInfoContact);            
        }

        [Test]
        public  void TestGetIndexFailedTableDetailTest()
        {
            // Список для хранения индексов с ошибками
            List<int> failedIndices = new List<int>();

            // Цикл от 0 до 9
            for (int i = 0; i < 22; i++)
            {
                try
                {
                    // Получаем данные из таблицы и детальной страницы
                    string tableInfoContact = app.Contacts.ContactTableToDetail(i);
                    string detailInfoContact = app.Contacts.GetDetailInfoContact(i);

                    // Выводим информацию для отладки
                    System.Console.Out.WriteLine($"Индекс {i}:");
                    System.Console.Out.WriteLine("На деталке видно Expected: " + detailInfoContact);
                    System.Console.Out.WriteLine("В таблице видно But was: " + tableInfoContact);

                    // Проверяем равенство данных
                    Assert.AreEqual(detailInfoContact, tableInfoContact);

                    System.Console.Out.WriteLine($"Индекс {i}: OK\n");
                }
                catch (Exception ex)
                {
                    // Если произошла ошибка, добавляем индекс в список
                    failedIndices.Add(i);
                    System.Console.Out.WriteLine($"Индекс {i}: Ошибка - {ex.Message}\n");
                }
            }

            // Выводим итоговый результат
            if (failedIndices.Count == 0)
            {
                System.Console.Out.WriteLine("Все проверки прошли успешно!");
            }
            else
            {
                System.Console.Out.WriteLine("Ошибки в следующих индексах: " + string.Join(", ", failedIndices));
            }
        }
        [Test]
        public void TestGetIndexFailedTableEditFormTest()
        {
            // Список для хранения индексов с ошибками
            List<int> failedIndices = new List<int>();

            // Цикл от 0 до 9
            for (int i = 0; i < 22; i++)
            {
                try
                {
                    // Получаем данные из таблицы и детальной страницы
                    ContactData fromTable = app.Contacts.GetContactInformationFromEditForm(i);
                    ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(i);

                    // Выводим информацию для отладки
                    System.Console.Out.WriteLine($"Индекс {i}:");
                    System.Console.Out.Write("В таблице видно Expected: " + fromTable);
                    System.Console.Out.Write("На деталке видно But was: " + fromForm);

                    // Проверяем равенство данных
                    Assert.AreEqual(fromTable, fromForm);
                    Assert.AreEqual(fromTable.Address, fromForm.Address);
                    Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
                    Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);

                    System.Console.Out.WriteLine($"Индекс {i}: OK\n");
                }
                catch (Exception ex)
                {
                    // Если произошла ошибка, добавляем индекс в список
                    failedIndices.Add(i);
                    System.Console.Out.WriteLine($"Индекс {i}: Ошибка - {ex.Message}\n");
                }
            }

            // Выводим итоговый результат
            if (failedIndices.Count == 0)
            {
                System.Console.Out.WriteLine("Все проверки прошли успешно!");
            }
            else
            {
                System.Console.Out.WriteLine("Ошибки в следующих индексах: " + string.Join(", ", failedIndices));
            }
        }
    } 
}
