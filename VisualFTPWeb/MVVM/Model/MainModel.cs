using System;
using VisualFTPWeb.Core;

namespace VisualFTPWeb.MVVM.ViewModel
{
    class MainModel : ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand BrowserViewCommand { get; set; }
        public RelayCommand ExplorerViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public BrowserViewModel BrowserVM { get; set; }
        public ExplorerViewModel ExplorerVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }


        public MainModel()
        {
            HomeVM = new HomeViewModel();
            BrowserVM = new BrowserViewModel();
            ExplorerVM = new ExplorerViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o => 
            {
                CurrentView = HomeVM;
            });

            BrowserViewCommand = new RelayCommand(o =>
            {
                CurrentView = BrowserVM;
            });

            ExplorerViewCommand = new RelayCommand(o =>
            {
                CurrentView = ExplorerVM;
            });
        }
    }
}
