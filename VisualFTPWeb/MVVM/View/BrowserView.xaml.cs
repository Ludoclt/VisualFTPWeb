using CefSharp;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VisualFTPWeb.Cores;

namespace VisualFTPWeb.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour BrowserView.xaml
    /// </summary>
    public partial class BrowserView : UserControl
    {
        private string code;
        private string replacedCode;
        private string replacementText;

        public BrowserView()
        {
            InitializeComponent();
            Browser.Address = $"http://{FtpSystem.host}/";
            Browser.JavascriptObjectRepository.Register("dataAccessor", new DataAccessor(), true, BindingOptions.DefaultBinder);
            Browser.AddHandler(UIElement.MouseUpEvent, new MouseButtonEventHandler(OnMouseUp), true);
        }

        private void UserControlLoaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.KeyDown += HandleKeyPressAsync;
        }

        private void OnFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(async () =>
            {
                code = await Browser.GetSourceAsync();
            }));

            Browser.ExecuteScriptAsync(@"document.body.onmouseup = async function(){await CefSharp.BindObjectAsync('dataAccessor', 'bound'); dataAccessor.giveSelectedText(window.getSelection().toString());}");
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void HandleKeyPressAsync(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DialogWindow dialogWindow = new DialogWindow();
                dialogWindow.ShowDialog();
                replacementText = dialogWindow.Answer;

                if (replacedCode == null)
                {
                    replacedCode = ReplaceHtml(code);
                }
                else
                {
                    replacedCode = ReplaceHtml(replacedCode);
                }

                Browser.LoadHtml(replacedCode, Browser.Address);
            }

            if (e.Key == Key.F5)
            {
                UserUploadHtmlFile();
            }

            if(e.Key == Key.F6)
            {
                FtpSystem.CloneWebsite();
            }
        }

        private string ReplaceHtml(string _code)
        {
            return _code.Replace(DataAccessor.selectedText, replacementText);
        }

        private void UserUploadHtmlFile()
        {
            FtpSystem.UploadHtmlFile(replacedCode);
        }
    }

    public class DataAccessor
    {
        private static string _selectedText;
        public static string selectedText { get { return _selectedText; } set { _selectedText = value; } }

        public void giveSelectedText(string selection)
        {
            if (selection.Length > 2)
            {
                selectedText = selection;
            }
        }
    }
}
