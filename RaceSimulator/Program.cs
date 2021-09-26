using Controller;
using System;

namespace RaceSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();
            Console.WriteLine($"Track naam: {Data.CurrentRace.Track.Name}");
        }
    }
}
