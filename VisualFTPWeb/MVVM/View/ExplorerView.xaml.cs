using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VisualFTPWeb.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour ExplorerView.xaml
    /// </summary>
    public partial class ExplorerView : UserControl
    {
        public ObservableCollection<ExplorerItem> ExplorerItemsCollection { get; set; }
        public string directoryPath = "C:/Users/Ludovic/Documents/Philosophie";

        public ExplorerView()
        {
            InitializeComponent();
            UpdateDisplayedItems();
        }

        private void ExplorerPrevious(object sender, RoutedEventArgs e)
        {
            while (directoryPath[directoryPath.Length-1].ToString() != "/")
            {
                directoryPath = directoryPath.Remove(directoryPath.Length - 1);
            }

            directoryPath = directoryPath.Remove(directoryPath.Length - 1);
            DirectoryPath.Text = directoryPath;
            UpdateDisplayedItems();
        }

        private void ExplorerNext(object sender, RoutedEventArgs e)
        {

        }

        private void ExplorerRefresh(object sender, RoutedEventArgs e)
        {
            UpdateDisplayedItems();
        }

        public void UpdateDisplayedItems()
        {
            ExplorerItemsCollection = new ObservableCollection<ExplorerItem>();

            DirectoryInfo folder = new DirectoryInfo(directoryPath);
            DirectoryInfo[] directoryDirectories = folder.GetDirectories();
            FileInfo[] directoryFiles = folder.GetFiles("*");

            DirectoryPath.Text = directoryPath;

            foreach (DirectoryInfo directory in directoryDirectories)
            {
                ExplorerItemsCollection.Add(new ExplorerItem()
                {
                    ItemThumbnail = "/Resources/Images/doc_unknown.png",
                    ItemPath = directory.FullName,
                    ItemName = directory.Name,
                    ItemType = "Directory"
                });
            }

            foreach (FileInfo file in directoryFiles)
            {
                if (System.IO.Path.GetExtension(file.FullName) == ".jpg" || System.IO.Path.GetExtension(file.FullName) == ".png" ||
                    System.IO.Path.GetExtension(file.FullName) == ".gif" || System.IO.Path.GetExtension(file.FullName) == ".bmp" ||
                    System.IO.Path.GetExtension(file.FullName) == ".ico")
                {

                    ExplorerItemsCollection.Add(new ExplorerItem()
                    {
                        ItemThumbnail = file.FullName,
                        ItemPath = file.FullName,
                        ItemName = file.Name
                    });

                }

                else if (System.IO.Path.GetExtension(file.FullName) == ".txt" || System.IO.Path.GetExtension(file.FullName) == ".log")
                {

                    ExplorerItemsCollection.Add(new ExplorerItem()
                    {
                        ItemThumbnail = "/Resources/Images/notepad.jpg",
                        ItemPath = file.FullName,
                        ItemName = file.Name
                    });

                }

                else if (System.IO.Path.GetExtension(file.FullName) == ".docx" || System.IO.Path.GetExtension(file.FullName) == ".odt")
                {
                    ExplorerItemsCollection.Add(new ExplorerItem()
                    {
                        ItemThumbnail = "/Resources/Images/doc_word.png",
                        ItemPath = file.FullName,
                        ItemName = file.Name
                    });
                }

                else if (System.IO.Path.GetExtension(file.FullName) == ".xls" || System.IO.Path.GetExtension(file.FullName) == ".csv")
                {
                    ExplorerItemsCollection.Add(new ExplorerItem()
                    {
                        ItemThumbnail = "/Resources/Images/excel.png",
                        ItemPath = file.FullName,
                        ItemName = file.Name
                    });
                }

                else
                {
                    ExplorerItemsCollection.Add(new ExplorerItem()
                    {
                        ItemThumbnail = "/Resources/Images/doc_unknown.png",
                        ItemPath = file.FullName,
                        ItemName = file.Name
                    });
                }
            }

            ListBoxFiles.ItemsSource = ExplorerItemsCollection;
        }

        public void OpenItem(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                if ((ListBoxFiles.SelectedItem as ExplorerItem).ItemType == "Directory")
                {
                    directoryPath += "/" + (ListBoxFiles.SelectedItem as ExplorerItem).ItemName;
                    UpdateDisplayedItems();
                }
                else
                {
                    Process.Start((ListBoxFiles.SelectedItem as ExplorerItem).ItemPath);
                }
            }
        }
    }

    public class ExplorerItem
    {
        public string ItemThumbnail { get; set; }
        public string ItemPath { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
    }
}
