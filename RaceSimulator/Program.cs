using Controller;
using Model;
using System;

namespace RaceSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();

            //DriversChangedEventArgs eventArgs = new DriversChangedEventArgs();
            //eventArgs.Track = Data.CurrentRace.Track;
            Data.CurrentRace.DriversChanged += Visualisation.OnDriversChanged;

            Visualisation.Initialize();

            Console.ReadLine();
            Console.SetCursorPosition(0, Console.WindowHeight);
        }
    }
}
