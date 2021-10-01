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
            Visualisation.Initialize();
            Visualisation.DrawTrack(Data.CurrentRace.Track);
            Console.SetCursorPosition(0, Console.WindowHeight);
        }
    }
}
