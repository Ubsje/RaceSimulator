using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Graphics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow main;
        private RaceWindow raceWindow;
        private CompetitionWindow competitionWindow;
        public MainWindow()
        {
            InitializeComponent();

            main = this;

            Race.RaceStarted += Visualisation.OnRaceStarted;

            Data.Initialize();
            Data.NextRace();

            Visualisation.Initialize();
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Competition_Click(object sender, RoutedEventArgs e)
        {
            competitionWindow = new CompetitionWindow();
            competitionWindow.Show();
        }

        private void MenuItem_Race_Click(object sender, RoutedEventArgs e)
        {
            raceWindow = new RaceWindow();
            raceWindow.Show();
        }
    }
}
