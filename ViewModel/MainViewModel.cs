using CyberTools.Core;
using CyberTools.View;
using System;

namespace CyberTools.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand KeyloggerViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public KeyLoggerViewModel KeylogVM { get; set; }

        private object? _currentView;

        // Make _currentView nullable if you intend to initialize it later
        public object? CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            KeylogVM = new KeyLoggerViewModel();

            // Initialize with HomeView (this is a concrete instance of a view)
            CurrentView = new HomeView(); // Assigning a view to avoid nullability warning

            HomeViewCommand = new RelayCommand(o => { CurrentView = new HomeView(); });
            KeyloggerViewCommand = new RelayCommand(o => { CurrentView = new KeyLoggerView(); });
        }
    }
}
