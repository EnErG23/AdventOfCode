using AdventOfCode.Helpers;
using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day16 : Day
    {
        private List<Room> _rooms;
        private readonly List<Tuple<Room, Room>> _edges;
        private readonly Graph<Room> _graph;
        private readonly Algorithms _algorithms;
        private long _totalPressureReleased;

        public Day16(int year, int day, bool test) : base(year, day, test)
        {
            _rooms = Inputs
                .Select(i => i.Replace("Valve ", "").Replace("has flow rate=", "").Replace("; tunnels lead to valves", "").Replace("; tunnel leads to valve", "").Replace(", ", ",").Split(" "))
                .Select(i => new Room(i[0], int.Parse(i[1]), i[2].Split(",").ToList()))
                .ToList();

            _edges = new();

            foreach (Room room in _rooms)
                foreach (string conRoom in room.ConnectedRooms)
                    _edges.Add(new Tuple<Room, Room>(room, _rooms.FirstOrDefault(r => r.Name == conRoom)));

            _graph = new Graph<Room>(_rooms, _edges, false);
            _algorithms = new Algorithms();
        }

        public override string RunPart1()
        {
            int timeLeft = 30;
            Room currentRoom = _rooms.FirstOrDefault(r => r.Name == "AA");

            NextMove(_rooms.Where(r => !r.IsValveOpened && r.FlowRate > 0).ToList(), currentRoom, timeLeft, 0, new List<string>());

            return _totalPressureReleased.ToString();
        }

        public override string RunPart2()
        {
            return "undefined";
        }

        /*  Testcase
            DD  BB  JJ  HH  EE CC
            28  25  21  13  09 06
            20  13  21  22  03 02
            560 325 441 286 27 12 = 1651
        */
        public void NextMove(List<Room> rooms, Room currentRoom, int timeLeft, long totalPressureReleased, List<string> openedValves)
        {
            if (timeLeft <= 0)
                return;

            if (totalPressureReleased > _totalPressureReleased)
                _totalPressureReleased = totalPressureReleased;

            foreach (var nextRoom in rooms)
            {
                int timeToMoveAndOpen = TimeToMove(currentRoom, nextRoom) + 1;
                var newTimeLeft = timeLeft - timeToMoveAndOpen;

                var newTotalPressureReleased = totalPressureReleased + (newTimeLeft * nextRoom.FlowRate);

                var newOpenedValves = new List<string>();
                newOpenedValves.AddRange(openedValves);
                newOpenedValves.Add($"{nextRoom.Name} ({newTimeLeft * nextRoom.FlowRate})");

                NextMove(rooms.Where(p => p.Name != nextRoom.Name).ToList(), nextRoom, newTimeLeft, newTotalPressureReleased, newOpenedValves);
            }
        }

        /*  Testcase with elephant ???
            
        */
        public void NextMoveWithElephant(List<Room> rooms, Room currentRoom, Room elephantRoom, int timeLeft, long totalPressureReleased, List<string> openedValves)
        {
            if (timeLeft <= 0)
                return;

            if (totalPressureReleased > _totalPressureReleased)
                _totalPressureReleased = totalPressureReleased;

            foreach (var nextRoom in rooms)
            {
                int timeToMoveAndOpen = TimeToMove(currentRoom, nextRoom) + 1;
                var newTimeLeft = timeLeft - timeToMoveAndOpen;

                var newTotalPressureReleased = totalPressureReleased + (newTimeLeft * nextRoom.FlowRate);

                var newOpenedValves = new List<string>();
                newOpenedValves.AddRange(openedValves);
                newOpenedValves.Add($"{nextRoom.Name} ({newTimeLeft * nextRoom.FlowRate})");

                NextMove(rooms.Where(p => p.Name != nextRoom.Name).ToList(), nextRoom, newTimeLeft, newTotalPressureReleased, newOpenedValves);
            }
        }

        public int TimeToMove(Room from, Room to)
            => _algorithms.ShortestPathFunction(_graph, from)(to).Count() - 1;
    }

    public class Room
    {
        public Room(string name, int flowRate, List<string> connectedRooms)
        {
            Name = name;
            FlowRate = flowRate;
            IsValveOpened = name == "AA" ? true : false;
            ConnectedRooms = connectedRooms;
        }

        public string Name { get; set; }
        public int FlowRate { get; set; }
        public bool IsValveOpened { get; set; }
        public List<string> ConnectedRooms { get; set; }
    }
}