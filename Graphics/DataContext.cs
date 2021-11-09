using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    class DataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string CurrentTrack => Data.CurrentRace?.Track.Name;

        public void OnDriversChanged(object sender, DriversChangedEventArgs eventArgs)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
        }

        public DataContext()
        {
            Race.RaceStarted += OnRaceStarted;
        }

        public void OnRaceStarted(object sender, EventArgs args)
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
        }
    }
}
