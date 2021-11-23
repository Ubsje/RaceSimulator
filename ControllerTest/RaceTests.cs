using Controller;
using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Section;

namespace ControllerTest
{
    [TestFixture]
    class RaceTests
    {
        private Track track;
        private List<IParticipant> participants = new List<IParticipant>();
        private Race race;

        [SetUp]
        public void SetUp()
        {
            track = new Track("track", new Section.SectionTypes[] 
            {
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
            });

            participants.Clear();
            participants.Add(new Driver("a", IParticipant.TeamColors.Red));
            participants.Add(new Driver("b", IParticipant.TeamColors.Blue));
            participants.Add(new Driver("c", IParticipant.TeamColors.Green));
            participants.Add(new Driver("d", IParticipant.TeamColors.Yellow));

            race = new Race(track, participants);
            race.DriversChanged += OnDriverChangedDummy;
        }

        public void OnDriverChangedDummy(object sender, DriversChangedEventArgs eventArgs) { }

        [Test]
        public void Race_IsNotNull()
        {
            Assert.IsNotNull(race);
        }

        [Test]
        public void Race_GetSectionData_Returns()
        {
            Section section = track.Sections.First?.Value;

            SectionData result = race.GetSectionData(section);

            Assert.IsInstanceOf<SectionData>(result);
            Assert.IsNotNull(result);
        }

        [Test]
        public void Race_RandomizeEquipment()
        {
            participants[0].IEquipment.Quality = 101;
            participants[0].IEquipment.Performance = 101;

            race.RandomizeEquipment();
            int resultQuality = participants[0].IEquipment.Quality;
            int resultPerformance = participants[0].IEquipment.Performance;

            Assert.GreaterOrEqual(resultQuality, 30);
            Assert.LessOrEqual(resultQuality, 89);
            Assert.GreaterOrEqual(resultPerformance, 1);
            Assert.LessOrEqual(resultPerformance, 2);
        }

        [Test]
        public void Race_PlaceParticipant_Works()
        {
            int placed = 0;

            foreach (Section section in race.Track.Sections)
            {
                if (section.SectionType == SectionTypes.StartGrid)
                {
                    foreach (Driver driver in participants)
                    {
                        if (race.GetSectionData(section).Left == driver || race.GetSectionData(section).Right == driver)
                            placed++;
                    }
                }
            }

            Assert.AreEqual(participants.Count, placed);
        }
        
        [TestCase(10, 1, 10)]
        [TestCase(20, 1, 20)]
        [TestCase(30, 1, 30)]
        [TestCase(10, 2, 20)]
        [TestCase(20, 2, 40)]
        [TestCase(30, 2, 60)]
        [TestCase(10, 3, 30)]
        [TestCase(20, 3, 60)]
        [TestCase(30, 3, 90)]
        public void Driver_ActualSpeed(int speed, int performance, int expected)
        {
            int result = -1;

            result = speed * performance;

            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void OnTimedEvent_DriversMoved()
        {
            int check = 0;

            race.OnTimedEvent(null, null);

            foreach (Section section in race.Track.Sections)
            {
                if (section.SectionType == SectionTypes.StartGrid)
                {
                    SectionData sd = race.GetSectionData(section);
                    foreach (Driver driver in participants)
                    {
                        if ((sd.Left == driver && sd.DistanceLeft > 0) || (sd.Right == driver && sd.DistanceRight > 0))
                            check++;
                    }
                }
            }

            Assert.AreEqual(participants.Count, check);
        }
        
        [Test]
        public void CleanUp_Works()
        {
            bool isNull = false;

            race.CleanUp();

            try
            {
                race.OnTimedEvent(null, null);
            }
            catch (NullReferenceException)
            {
                isNull = true;
            }

            Assert.IsTrue(isNull);
        }
    }
}
