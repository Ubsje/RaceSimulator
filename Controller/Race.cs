using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Section;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random _random;
        private Dictionary<Section, SectionData> _positions = new Dictionary<Section, SectionData>();
        public SectionData GetSectionData(Section section)
        {
            if (!_positions.ContainsKey(section))
                _positions.Add(section, new SectionData());
            return _positions[section];
        }

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            PlaceParticipants();
        }
        public void RandomizeEquipment()
        {
            for (int i = 0; i < Participants.Count(); i++)
            {
                Car c = new Car();
                c.Quality = _random.Next(1, 100);
                c.Performance = _random.Next(1, 100);
                Participants[i].IEquipment = c;
            }
        }
        public void PlaceParticipants()
        {
            int unplaced = 0;

            foreach (Driver driver in Participants)
                unplaced += 1;

            foreach (Section section in Track.Sections)
                if (section.SectionType == SectionTypes.StartGrid)
                    for (int i = 0; i < 2; i++)
                        if (unplaced > 0)
                        {
                            if (unplaced % 2 == 1)
                                GetSectionData(section).Left = Participants[unplaced - 1];
                            else
                                GetSectionData(section).Right = Participants[unplaced - 1];

                            unplaced -= 1;
                        }
        }
    }
}
