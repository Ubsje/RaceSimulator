using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using static Model.Section;

namespace Graphics
{
    static class Visualisation
    {
        static int dir, x, y;
        static Section section;
        #region graphics
        private const string _finishHorizontal = ".\\Images\\finishHorizontal.png";
        private const string _finishVertical = ".\\Images\\finishVertical.png";
        private const string _straightHorizontal = ".\\Images\\straightHorizontal.png";
        private const string _straightVertical = ".\\Images\\straightVertical.png";
        private const string _cornerUpRight = ".\\Images\\cornerUpRight.png";
        private const string _cornerUpLeft = ".\\Images\\cornerUpLeft.png";
        private const string _cornerDownRight = ".\\Images\\cornerDownRight.png";
        private const string _cornerDownLeft = ".\\Images\\cornerDownLeft.png";
        private const string _startUp = ".\\Images\\startUp.png";
        private const string _startRight = ".\\Images\\startRight.png";
        private const string _startDown = ".\\Images\\startDown.png";
        private const string _startLeft = ".\\Images\\startLeft.png";
        private const string _racerBlue = ".\\Images\\racerBlue.png";
        private const string _racerBroken = ".\\Images\\racerBroken.png";
        private const string _racerGreen = ".\\Images\\racerGreen.png";
        private const string _racerGrey = ".\\Images\\racerGrey.png";
        private const string _racerRed = ".\\Images\\racerRed.png";
        private const string _racerYellow = ".\\Images\\racerYellow.png";
        #endregion
        public static void Initialize()
        {

        }
        public static void OnDriversChanged(object sender, DriversChangedEventArgs eventArgs)
        {
            MainWindow.main.WindowImage.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    MainWindow.main.WindowImage.Source = null;
                    MainWindow.main.WindowImage.Source = Visualisation.DrawTrack(eventArgs.Track);
                }));
        }

        public static void OnRaceStarted(object sender, EventArgs args)
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
        }
        public static BitmapSource DrawTrack(Track track)
        {
            Bitmap image = ImageLoader.Empty(1280, 640);

            dir = 0;
            x = 3;
            y = 2;

            foreach (Section _section in track.Sections)
            {
                section = _section;
                switch (section.SectionType)
                {
                    case SectionTypes.Straight:
                        if (dir == 0 || dir == 2)
                            Draw(ref image, _straightVertical);
                        else
                            Draw(ref image, _straightHorizontal);
                        break;
                    case SectionTypes.LeftCorner:
                        if (dir == 0)
                            Draw(ref image, _cornerDownLeft);
                        else if (dir == 1)
                            Draw(ref image, _cornerUpLeft);
                        else if (dir == 2)
                            Draw(ref image, _cornerUpRight);
                        else
                            Draw(ref image, _cornerDownRight);
                        dir -= 1;
                        break;
                    case SectionTypes.RightCorner:
                        if (dir == 0)
                            Draw(ref image, _cornerDownRight);
                        else if (dir == 1)
                            Draw(ref image, _cornerDownLeft);
                        else if (dir == 2)
                            Draw(ref image, _cornerUpLeft);
                        else
                            Draw(ref image, _cornerUpRight);
                        dir += 1;
                        break;
                    case SectionTypes.StartGrid:
                        if (dir == 0)
                            Draw(ref image, _startUp);
                        else if (dir == 1)
                            Draw(ref image, _startRight);
                        else if (dir == 2)
                            Draw(ref image, _startDown);
                        else
                            Draw(ref image, _startLeft);
                        break;
                    case SectionTypes.Finish:
                        if (dir == 0 || dir == 2)
                            Draw(ref image, _finishVertical);
                        else
                            Draw(ref image, _finishHorizontal);
                        break;
                }
                if (dir < 0)
                    dir += 4;
                dir %= 4;
                MoveForward();
            }

            return ImageLoader.CreateBitmapSourceFromGdiBitmap(image);
        }
        static void Draw(ref Bitmap image, string _section)
        {
            DrawImageAt(ref image, _section, x, y, false, -1);
            DrawParticipants(ref image, section, x, y, dir);
        }
        static void DrawParticipants(ref Bitmap image, Section section, double x, double y, int dir)
        {
            SectionData sectionData = Data.CurrentRace.GetSectionData(section);

            if (!(sectionData.Left == null))
            {
                string color = _racerBroken;

                if (!sectionData.Left.IEquipment.IsBroken)
                    switch (sectionData.Left.TeamColor)
                    {
                        case IParticipant.TeamColors.Red:
                            color = _racerRed;
                            break;
                        case IParticipant.TeamColors.Blue:
                            color = _racerBlue;
                            break;
                        case IParticipant.TeamColors.Green:
                            color = _racerGreen;
                            break;
                        case IParticipant.TeamColors.Grey:
                            color = _racerGrey;
                            break;
                        case IParticipant.TeamColors.Yellow:
                            color = _racerYellow;
                            break;
                    }

                double _x = 0, _y = 0;

                switch (dir)
                {
                    case 0:
                        _x = x * 64;
                        _y = (y + 1) * 64 - 16 - (sectionData.DistanceLeft / 100d * 64);
                        break;
                    case 1:
                        _x = x * 64 - 16 + (sectionData.DistanceLeft / 100d * 64);
                        _y = y * 64;
                        break;
                    case 2:
                        _x = x * 64 + 32;
                        _y = y * 64 - 16 + (sectionData.DistanceLeft / 100d * 64);
                        break;
                    case 3:
                        _x = (x + 1) * 64 - 16 - (sectionData.DistanceLeft / 100d * 64);
                        _y = y * 64 + 32;
                        break;
                }
                
                DrawImageAt(ref image, color, _x, _y, true, dir);
            }

            if (!(sectionData.Right == null))
            {
                string color = _racerBroken;

                if(!sectionData.Right.IEquipment.IsBroken)
                    switch (sectionData.Right.TeamColor)
                    {
                        case IParticipant.TeamColors.Red:
                            color = _racerRed;
                            break;
                        case IParticipant.TeamColors.Blue:
                            color = _racerBlue;
                            break;
                        case IParticipant.TeamColors.Green:
                            color = _racerGreen;
                            break;
                        case IParticipant.TeamColors.Grey:
                            color = _racerGrey;
                            break;
                        case IParticipant.TeamColors.Yellow:
                            color = _racerYellow;
                            break;
                    }

                double _x = 0, _y = 0;

                switch (dir)
                {
                    case 0:
                        _x = x * 64 + 32;
                        _y = (y + 1) * 64 - 16 - (sectionData.DistanceRight / 100d * 64);
                        break;
                    case 1:
                        _x = x * 64 - 16 + (sectionData.DistanceRight / 100d * 64);
                        _y = y * 64 + 32;
                        break;
                    case 2:
                        _x = x * 64;
                        _y = y * 64 - 16 + (sectionData.DistanceRight / 100d * 64);
                        break;
                    case 3:
                        _x = (x + 1) * 64 - 16 - (sectionData.DistanceRight / 100d * 64);
                        _y = y * 64;
                        break;
                }

                DrawImageAt(ref image, color, _x, _y, true, dir);
            }
        }
        static void DrawImageAt(ref Bitmap bitmap, string imageName, double x, double y, bool isCar, int dir)
        {
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap))
            {
                double _x = x;
                double _y = y;
                if (!isCar)
                {
                    _x *= 64;
                    _y *= 64;

                    graphics.DrawImage(ImageLoader.GetImage(imageName), new Point((int)_x, (int)_y));
                } else
                {
                    Bitmap image = (Bitmap)ImageLoader.GetImage(imageName).Clone();

                    switch (dir)
                    {
                        case 0:
                            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            break;
                        case 2:
                            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            break;
                        case 3:
                            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            break;
                    }

                    graphics.DrawImage(image, new Point((int)_x, (int)_y));
                }
            }
        }
        static void MoveForward()
        {
            if (dir == 0)
                y -= 1;
            else if (dir == 1)
                x += 1;
            else if (dir == 2)
                y += 1;
            else
                x -= 1;
        }
    }
}
