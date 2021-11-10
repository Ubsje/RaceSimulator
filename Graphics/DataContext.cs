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
        public string TrackLength => $"Track length: {(int)Data.CurrentRace?.Track.Sections.Count}0m";
        public string Racers
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("Racers\n--------\n");
                if (Data.CurrentRace != null)
                    foreach (Driver driver in Data.CurrentRace.Participants)
                    {
                        sb.AppendLine($"{driver.Name} ({driver.TeamColor})");
                    }

                return sb.ToString();
            }
        }
        public string Equipment
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("Equipment\n-------------\n");
                if (Data.CurrentRace != null)
                    foreach (Driver driver in Data.CurrentRace.Participants)
                    {
                        IEquipment eq = driver.IEquipment;
                        sb.AppendLine($"Quality: {eq.Quality}%, Performance: {eq.Performance}");
                    }

                return sb.ToString();
            }
        }
        public string Tracklist
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("Volgende tracks\n------------------\n");
                if (Data.CurrentRace != null)
                {
                    foreach (Track track in Data.Competition.Tracks)
                        sb.AppendLine($"{track.Name} (lengte: {track.Sections.Count}0m)");
                    if (Data.Competition.Tracks.Count == 0)
                        sb.AppendLine("Geen");
                }

                return sb.ToString();
            }
        }
        public string RacerPoints
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("Scores\n--------\n");
                if (Data.CurrentRace != null)
                    foreach (Driver driver in Data.Competition.Participants)
                        sb.AppendLine($"{driver.Name}: {driver.Points}");

                return sb.ToString();
            }
        }

        public void OnDriversChanged(object sender, DriversChangedEventArgs eventArgs)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
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
