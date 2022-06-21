using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rake_Counter.Core;

namespace Rake_Counter.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {

        // Commands

        public RelayCommand RakeCounterViewCommand { get; set; }
        public RelayCommand HandCollectorViewCommand { get; set; }



        // ViewModels

        public HandCollectorViewModel HandCollectorVM { get; set; }
        public RakeCounterViewModel RakeCounterVM { get; set; }


        // Props
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            RakeCounterVM = new RakeCounterViewModel();
            HandCollectorVM = new HandCollectorViewModel();
            CurrentView = RakeCounterVM;

            RakeCounterViewCommand = new RelayCommand(o =>
            {
                CurrentView = RakeCounterVM;
            });

            HandCollectorViewCommand = new RelayCommand(o =>
            {
                CurrentView = HandCollectorVM;
            });
        }
    }
}
