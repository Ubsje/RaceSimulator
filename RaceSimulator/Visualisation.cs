using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Section;

namespace RaceSimulator
{
    static class Visualisation
    {
        static int dir, x, y;
        static Section section;
        #region graphics
        private static string[] _finishHorizontal = {
            "-uu-",
            "1[] ",
            "2[] ",
            "-nn-" };
        private static string[] _finishVertical = {
            "|21|",
            "]nn[",
            "]uu[",
            "|  |" };
        private static string[] _straightHorizontal = {
            "----",
            "  1 ",
            " 2  ",
            "----" };
        private static string[] _straightVertical = {
            "|  |",
            "|2 |",
            "| 1|",
            "|  |" };
        private static string[] _cornerUpRight = {
            "|  O",
            "|1 2",
            "\\   ",
            " `--" };
        private static string[] _cornerUpLeft = {
            "O2 |",
            "   |",
            " 1 /",
            "--´ " };
        private static string[] _cornerDownRight = {
            " ,--",
            "/ 1 ",
            "|   ",
            "| 2O" };
        private static string[] _cornerDownLeft = {
            "--. ",
            "   \\",
            "2 1|",
            "O  |" };
        private static string[] _startUp = {
            "|n |",
            "|1n|",
            "| 2|",
            "|  |" };
        private static string[] _startRight = {
            "----",
            "  1]",
            " 2] ",
            "----" };
        private static string[] _startDown = {
            "|  |",
            "| 1|",
            "|2u|",
            "|u |" };
        private static string[] _startLeft = {
            "----",
            "[2  ",
            " [1 ",
            "----" };
        #endregion
        public static void Initialize()
        {

        }

        public static void DrawTrack(Track track)
        {
            dir = 0;
            x = 3;
            y = 2;

            foreach (Section _section in track.Sections)
            {
                section = _section;
                switch (section.SectionType)
                {
                    case SectionTypes.Straight:
                        if (dir == 0)
                            DrawSectionAt(_straightVertical, x, y);
                        else if (dir == 1)
                            DrawSectionAt(_straightHorizontal, x, y);
                        else if (dir == 2)
                            DrawSectionAt(_straightVertical, x, y);
                        else
                            DrawSectionAt(_straightHorizontal, x, y);
                        break;
                    case SectionTypes.LeftCorner:
                        if (dir == 0)
                            DrawSectionAt(_cornerDownLeft, x, y);
                        else if (dir == 1)
                            DrawSectionAt(_cornerUpLeft, x, y);
                        else if (dir == 2)
                            DrawSectionAt(_cornerUpRight, x, y);
                        else
                            DrawSectionAt(_cornerDownRight, x, y);
                        dir -= 1;
                        break;
                    case SectionTypes.RightCorner:
                        if (dir == 0)
                            DrawSectionAt(_cornerDownRight, x, y);
                        else if (dir == 1)
                            DrawSectionAt(_cornerDownLeft, x, y);
                        else if (dir == 2)
                            DrawSectionAt(_cornerUpLeft, x, y);
                        else
                            DrawSectionAt(_cornerUpRight, x, y);
                        dir += 1;
                        break;
                    case SectionTypes.StartGrid:
                        if (dir == 0)
                            DrawSectionAt(_startUp, x, y);
                        else if (dir == 1)
                            DrawSectionAt(_startRight, x, y);
                        else if (dir == 2)
                            DrawSectionAt(_startDown, x, y);
                        else
                            DrawSectionAt(_startLeft, x, y);
                        break;
                    case SectionTypes.Finish:
                        if (dir == 0)
                            DrawSectionAt(_finishVertical, x, y);
                        else if (dir == 1)
                            DrawSectionAt(_finishHorizontal, x, y);
                        else if (dir == 2)
                            DrawSectionAt(_finishVertical, x, y);
                        else
                            DrawSectionAt(_finishHorizontal, x, y);
                        break;
                }
                dir %= 4;
                MoveForward();
            }
        }

        static void DrawSectionAt(string[] _section, int x, int y)
        {
            for (int i = 0; i < _section.Length; i++)
            {
                Console.SetCursorPosition(x * _section.Length, y * _section.Length + i);
                Console.Write(PlaceParticipants(_section[i], Data.CurrentRace.GetSectionData(section).Left, Data.CurrentRace.GetSectionData(section).Right));
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

        static string PlaceParticipants(string str, IParticipant left, IParticipant right)
        {
            string output = str;

            char c1 = ' ';
            char c2 = ' ';

            if (left != null)
                c1 = left.Name.ToCharArray()[0];
            if (right != null)
                c2 = right.Name.ToCharArray()[0];

            output = output.Replace('1', c1);
            output = output.Replace('2', c2);

            return output;
        }
    }
}
