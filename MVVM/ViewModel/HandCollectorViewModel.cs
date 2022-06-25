using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rake_Counter.Core;
using Rake_Counter.Core.Toolbox;


namespace Rake_Counter.MVVM.ViewModel
{
    internal class HandCollectorViewModel : ObservableObject
    {
        public RelayCommand CollectCommand { get; set; }

        private string _status = "Idle";

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged();

            }
        }

        private bool _isBusy = false;

        public bool IsBusy
        {
            get { return !_isBusy; }
            set { _isBusy = value;
                OnPropertyChanged();
            }
        }


        private HandCollector bitch;

        public HandCollectorViewModel()
        {
            CollectCommand = new RelayCommand(async o =>
            {
                IsBusy = true;
                Status = "Working...";
                await Task.Run(() =>
                {
                    bitch = new HandCollector();
                    bitch.Collect();
                });
                Status = "Done!";
                IsBusy = false;
            });

        }
    }
}
