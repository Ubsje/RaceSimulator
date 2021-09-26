using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }
        public static void Initialize()
        {
            Competition = new Competition();
            Competition.Participants = new List<IParticipant>();
            Competition.Tracks = new Queue<Track>();
            AddParticipants();
            AddTracks();
        }
        public static void AddParticipants()
        {
            Competition.Participants.Add(new Driver());
            Competition.Participants.Add(new Driver());
            Competition.Participants.Add(new Driver());
            Competition.Participants.Add(new Driver());
        }
        public static void AddTracks()
        {
            Competition.Tracks.Enqueue(new Track("track1", new Section.SectionTypes[1]));
            Competition.Tracks.Enqueue(new Track("track2", new Section.SectionTypes[1]));
        }
        public static void NextRace()
        {
            Track t = Competition.NextTrack();
            if (t != null)
                CurrentRace = new Race(t, Competition.Participants);
        }
    }
}
