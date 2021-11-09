using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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

        private Timer timer;

        public event EventHandler<DriversChangedEventArgs> DriversChanged;
        public static event EventHandler RaceStarted;
        public SectionData GetSectionData(Section section)
        {
            if (!_positions.ContainsKey(section))
                _positions.Add(section, new SectionData());
            return _positions[section];
        }

        private Section lastSection;
        private IParticipant left = null;
        private IParticipant right = null;

        private int sectionLength = 100;
        private int racers;

        public void OnTimedEvent(object obj, EventArgs e)
        {
            bool moved = false;
            
            foreach (Section section in Track.Sections)
            {
                SectionData sd = GetSectionData(section);
                SectionData lsd = GetSectionData(lastSection);

                for (int i = 0; i < 2; i++)
                {
                    IParticipant next = null;
                    if (i == 0)
                        next = left;
                    else
                        next = right;

                    if (next != null)
                    {
                        if (!next.IEquipment.IsBroken)
                            if (_random.Next(0, next.IEquipment.Quality) == 0)
                            {
                                next.IEquipment.IsBroken = true;
                                next.IEquipment.Speed = Math.Max(20, (int)(next.IEquipment.Speed * .9f));
                            }

                        if (sd.Left == null)
                        {
                            sd.Left = next;
                            if (i == 0)
                            {
                                lsd.Left = null;
                                sd.DistanceLeft = lsd.DistanceLeft - sectionLength;
                                lsd.DistanceLeft = 0;
                            }
                            else
                            {
                                lsd.Right = null;
                                sd.DistanceLeft = lsd.DistanceRight - sectionLength;
                                lsd.DistanceRight = 0;
                            }
                            moved = true;
                            if (section.SectionType == SectionTypes.Finish)
                                next.RoundsDriven += 1;
                        }
                        else if (sd.Right == null)
                        {
                            sd.Right = next;
                            if (i == 0)
                            {
                                lsd.Left = null;
                                sd.DistanceRight = lsd.DistanceLeft - sectionLength;
                                lsd.DistanceLeft = 0;
                            }
                            else
                            {
                                lsd.Right = null;
                                sd.DistanceRight = lsd.DistanceRight - sectionLength;
                                lsd.DistanceRight = 0;
                            }
                            moved = true;
                            if (section.SectionType == SectionTypes.Finish)
                                next.RoundsDriven += 1;
                        }
                        else
                        {
                            if (i == 0)
                                lsd.DistanceLeft = sectionLength;
                            else
                                lsd.DistanceRight = sectionLength;
                        }
                    }
                }

                left = null;
                right = null;
                
                if (sd.Left != null)
                {
                    if (sd.Left.RoundsDriven >= 1)
                    {
                        sd.Left = null;
                        racers--;
                    }
                    else if (!sd.Left.IEquipment.IsBroken)
                    {
                        sd.DistanceLeft += sd.Left.IEquipment.Speed * sd.Left.IEquipment.Performance;
                        if (sd.DistanceLeft >= sectionLength)
                            left = sd.Left;
                    }
                    else if (_random.Next(0, 8) == 0)
                        sd.Left.IEquipment.IsBroken = false;

                    moved = true;
                } 
                
                if (sd.Right != null)
                {
                    if (sd.Right.RoundsDriven >= 1)
                    {
                        sd.Right = null;
                        racers--;
                    }
                    else if (!sd.Right.IEquipment.IsBroken)
                    {
                        sd.DistanceRight += sd.Right.IEquipment.Speed * sd.Right.IEquipment.Performance;
                        if (sd.DistanceRight >= sectionLength)
                            right = sd.Right;
                    }
                    else if (_random.Next(0, 8) == 0)
                        sd.Right.IEquipment.IsBroken = false;

                    moved = true;
                }

                lastSection = section;
            }

            if (moved == true)
            {
                DriversChangedEventArgs eventArgs = new DriversChangedEventArgs(Track);
                DriversChanged.Invoke(this, eventArgs);
            }

            if (racers <= 0)
            {
                CleanUp();
                timer.Stop();
                Data.NextRace();
            }
        }

        public void Start()
        {
            timer.Start();
        }
        public void SetupDriversChanged()
        {
            RaceStarted.Invoke(null, null);
        }

        public void CleanUp()
        {
            DriversChanged = null;
        }

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);

            RandomizeEquipment();
            PlaceParticipants();

            timer = new Timer(500);
            timer.Elapsed += OnTimedEvent;

            lastSection = Track.Sections.Last();
            sectionLength = 100;

            Start();
        }
        public void RandomizeEquipment()
        {
            for (int i = 0; i < Participants.Count(); i++)
            {
                Car c = new Car();
                c.Quality = _random.Next(1, 100);
                c.Performance = _random.Next(1, 2);
                c.Speed = _random.Next(10, 20);
                Participants[i].IEquipment = c;
            }
        }
        public void PlaceParticipants()
        {
            int unplaced = 0;

            foreach (Driver driver in Participants)
            {
                unplaced += 1;
                racers += 1;

                driver.RoundsDriven = -1;
            }

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
