using CyberTools.Core;
using CyberTools.View;
using System;

//Importerer core bibloteket som er mappen min, importerer view mappen min og importerer net bibloteket altså sytem

namespace CyberTools.ViewModel

//Definerer navneområdet (namespace) 

{
    class MainViewModel : ObservableObject

//Vi definerer klassen MainViewModel som arver fra ObservableObject.

    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand KeyloggerViewCommand { get; set; }

        //Kommando for å bytte til HomeView og KeyloggerView. Når du trykker en knapp så bytter den. 

        public HomeViewModel HomeVM { get; set; }
        public KeyLoggerViewModel KeylogVM { get; set; }

        //Vi lager instanser av ViewModelene for visning.

        private object? _currentView;

        
        public object? CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        //Definerer current view, når vi endrer current view så endres UI-en seg.
        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            KeylogVM = new KeyLoggerViewModel();

           
            CurrentView = new HomeView(); 

            HomeViewCommand = new RelayCommand(o => { CurrentView = new HomeView(); });
            KeyloggerViewCommand = new RelayCommand(o => { CurrentView = new KeyLoggerView(); });
        }
    } //Lager objekter og starter visningen av programmet med HomeView.
}
