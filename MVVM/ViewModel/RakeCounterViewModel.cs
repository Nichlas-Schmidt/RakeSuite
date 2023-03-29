using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rake_Counter.MVVM.Model;
using Rake_Counter.Core;
using Rake_Counter.Core.Toolbox;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Win32;
using System.Globalization;

namespace Rake_Counter.MVVM.ViewModel
{
    internal class RakeCounterViewModel : ObservableObject
    {

        RakeCounter rc = new RakeCounter();

        private ObservableCollection<Player> _players;

        public ObservableCollection<Player> Players
        {
            get { return _players; }
            set {
                _players = value;
                OnPropertyChanged(); }
        }


        private double _ratio = 50;

        public double Ratio
        {
            get { return _ratio; } 
            set {
                _ratio = value;
                OnPropertyChanged();
                
            }
        }

        public RelayCommand UploadCommand { get; set; }

        public RelayCommand RefreshRatioCommand { get; set; }

        public RakeCounterViewModel()
        {
            UploadCommand = new RelayCommand(o =>
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                bool? response = fileDialog.ShowDialog();
                if (response == true)
                {
                    Players = new ObservableCollection<Player>();
                    string filepath = fileDialog.FileName;
                    UserMapping userValue = rc.Calculate(filepath);
                    foreach(var user in userValue)
                    {
                        Player p = new Player(user.Value, user.Key, Ratio);
                        Players.Add(p);
                    }
                    List<Player> ordered = Players.OrderByDescending(x => x.Amount).ToList();
                    Players.Clear();
                    ordered.ForEach(x => Players.Add(x));
                }


            });

            RefreshRatioCommand = new RelayCommand(o =>
            {
                List<Player> ordered = Players?.ToList();
                Players?.Clear();


                foreach (Player player in ordered)
                {
                    player.FGAmount = player.Amount / Ratio;
                }
                ordered.ForEach(x => Players.Add(x));
            });
        }
       
    }
}
