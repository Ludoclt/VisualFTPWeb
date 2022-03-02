using FluentFTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VisualFTPWeb.Cores
{
    internal static class FtpSystem
    {
        private static string _host;
        public static string host { get { return _host; } set { _host = value; } }

        private static string _login;
        public static string login { get { return _login; } set { _login = value; } }

        private static string _password;
        public static string password { get { return _password; } set { _password = value; } }

        private static string _port;
        public static string port { get { return _port; } set { _port = value; } }

        private static string _filepath;
        public static string filepath { get { return _filepath; } set { _filepath = value; } }

        public static FtpClient ftpClient;

        private static string localWebPage = "LocalWebPage";


        public static void InitializeFtpConnection()
        {
            ftpClient = new FtpClient();
            ftpClient.Host = host;
            ftpClient.Port = Int32.Parse(port);
            ftpClient.Credentials = new NetworkCredential(login, password);

            ftpClient.Connect();

            if (!Directory.Exists(localWebPage))
            {
                Directory.CreateDirectory(localWebPage);
            }
        }

        public static void CloneWebsite()
        {
            ftpClient.DownloadDirectory(localWebPage, "/", FtpFolderSyncMode.Update);
            MessageBox.Show("Succefully Cloned");
        }

        public static void UploadHtmlFile(string _replacedCode)
        {
            File.WriteAllText($"{localWebPage}/{filepath}", _replacedCode);

            ftpClient.UploadFile($"{localWebPage}/{filepath}", filepath, FtpRemoteExists.Overwrite);
            MessageBox.Show("Upload File Completed");
        }
    }
}
