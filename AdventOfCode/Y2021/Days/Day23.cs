using AdventOfCode.Models;
using System.Text;

namespace AdventOfCode.Y2021.Days
{
    public class Day23 : Day
    {
        private Dictionary<char, int> _amphipodEnergy;
        private char[] _hallway;
        private List<char[]> _rooms;

        public Day23(int year, int day, bool test) : base(year, day, test)
        {
            _amphipodEnergy = new Dictionary<char, int>() { { 'A', 1 }, { 'B', 10 }, { 'C', 100 }, { 'D', 1000 } };
            _hallway = new char[9];
            _rooms = new();

            int c = 3;

            for (int i = 0; i < 4; i++)
            {
                char[] room = new char[2];

                room[0] = Inputs[2][c];
                room[1] = Inputs[3][c];

                _rooms.Add(room);

                c += 2;
            }
        }

        public override string RunPart1()
        {
            // can move to to hallway
            // never stop above room
            // can't move in hallway
            // only moves to destination room from hallway
            // if destination room does not contain wrong amphipod

            PrintBurrow();

            long totalEnergy = 0;
            bool isOrganised = CheckIfOrganised();

            while (!isOrganised)
            {
                var toMove = _rooms.Min(r => r[0]);


                isOrganised = CheckIfOrganised();
            }

            return totalEnergy.ToString();
        }


        public override string RunPart2()
            => "undefined";

        private bool CheckIfOrganised()
        {
            if (_rooms[0].Count(c => c == 'A')
                + _rooms[1].Count(c => c == 'B')
                + _rooms[2].Count(c => c == 'C')
                + _rooms[3].Count(c => c == 'D') == 8)
                return true;
            else
                return false;
        }

        private void PrintBurrow()
        {
            Console.WriteLine($"Hallway: {String.Join(",", _hallway)}");

            int i = 1;

            foreach (var room in _rooms)
                Console.WriteLine($"Room {i++}: {String.Join(",", room)}");

            Console.WriteLine("------------------");
        }
    }
}