using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.FtpClient;

namespace mantis_tests
{
    public class FtpHelper : HelperBase
    {
        // Сам FTP хелпер
        private FtpClient client;
        public FtpHelper(ApplicationManager applicationManager) : base(applicationManager) 
        { 
            client = new FtpClient(); // Инициализация
            client.Host = "localhost";
            // Вход под Credentials (логин пароль mantis)
            client.Credentials = new System.Net.NetworkCredential("mantis", "mantis");
            client.Connect();
        }

        // Методы 
        public void BackupFile(string path)
        {
            String backupPath = path + ".bak";
            if (client.FileExists(backupPath))
            {
                return;
            }
            else
            {
                client.Rename(path, backupPath);
            }

        }
        public void RestoreBackupFile(string path) 
        {
            String backupPath = path + ".bak";
            if (! client.FileExists(backupPath)) // Если нет бэкапа - ничего не поделать уже
            {
                return;
            }
            else // А если есть, то...
            {
                if (client.FileExists(path)) // (если есть файл, то мы его удаляем)
                {
                    client.DeleteFile(path);
                }
                client.Rename(backupPath, path); // ..и потом переименовываем Бэкап.bak на норм название
            }
        }
        public void Upload(string path, Stream localFile)
        {
            if (client.FileExists(path)) // (если есть файл, то мы его удаляем)
            {
                client.DeleteFile(path);
            }
            using (Stream ftpStream = client.OpenWrite(path))
            {
                byte[] buffer = new byte[8 * 1024];
                int count = localFile.Read(buffer, 0, buffer.Length);
                while (count > 0)
                {
                    ftpStream.Write(buffer, 0, count);
                    count = localFile.Read(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
