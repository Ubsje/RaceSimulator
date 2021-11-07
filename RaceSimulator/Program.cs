using Controller;
using Model;
using System;

namespace RaceSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Race.RaceStarted += Visualisation.OnRaceStarted;

            Data.Initialize();
            Data.NextRace();

            Visualisation.Initialize();

            Console.ReadLine();
            Console.SetCursorPosition(0, Console.WindowHeight);
        }
    }
}
