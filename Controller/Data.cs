using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using static Model.Section;

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
            Competition.Participants.Add(new Driver("Arnold", IParticipant.TeamColors.Red));
            Competition.Participants.Add(new Driver("Bert", IParticipant.TeamColors.Blue));
            Competition.Participants.Add(new Driver("Cherard", IParticipant.TeamColors.Green));
            Competition.Participants.Add(new Driver("D", IParticipant.TeamColors.Yellow));
        }
        public static void AddTracks()
        {
            Competition.Tracks.Enqueue(new Track("De Ubsje Race", new Section.SectionTypes[] {
                    SectionTypes.RightCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.StartGrid,
                    SectionTypes.Finish,
                    SectionTypes.Straight,
                    SectionTypes.LeftCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.LeftCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.LeftCorner,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner
                    }));
            Competition.Tracks.Enqueue(new Track("Lijpe track", new Section.SectionTypes[] {
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
                    SectionTypes.StartGrid,
                    SectionTypes.StartGrid,
                    SectionTypes.Finish,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.RightCorner,
                    SectionTypes.Straight,
                    SectionTypes.Straight,
                    SectionTypes.Straight
                    }));
        }
        public static void NextRace()
        {
            Track t = Competition.NextTrack();
            if (t != null)
            {
                CurrentRace = new Race(t, Competition.Participants);
                CurrentRace.SetupDriversChanged();
            }
        }
    }
}
