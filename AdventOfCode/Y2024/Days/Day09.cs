using AdventOfCode.Models;

namespace AdventOfCode.Y2024.Days
{
    public class Day09 : Day
    {
        public Day09(int year, int day, bool test) : base(year, day, test) { }

        public override string RunPart1()
        {
            List<int> disk = new();

            int id = 0;

            for (int i = 0; i < Inputs[0].Count(); i += 2)
            {
                for (int j = 0; j < int.Parse(Inputs[0][i].ToString()); j++)
                    disk.Add(id);

                if (i + 1 < Inputs[0].Count())
                    for (int j = 0; j < int.Parse(Inputs[0][i + 1].ToString()); j++)
                        disk.Add(-1);

                id++;
            }

            while (disk.IndexOf(-1) > 0)
            {
                if (disk.Last() > 0)
                    disk[disk.IndexOf(-1)] = disk.Last(d => d > -1);

                disk = disk.GetRange(0, disk.Count() - 1);
            }

            return disk.Select((d, i) => (d, i)).Sum(d => (decimal)d.d * (decimal)d.i).ToString();
        }

        public override string RunPart2()
        {
            decimal result = 0;

            List<File> disk = new();
            List<EmptySpace> emptySpaces = new();

            int id = 0;
            int index = 0;

            for (int i = 0; i < Inputs[0].Count(); i += 2)
            {
                int length = int.Parse(Inputs[0][i].ToString());
                disk.Add(new File(id, index, length));

                id++;
                index += length;

                if (i + 1 < Inputs[0].Count())
                {
                    var emptySpace = int.Parse(Inputs[0][i + 1].ToString());
                    emptySpaces.Add(new EmptySpace(index, emptySpace));
                    index += emptySpace;
                }
            }

            disk.Reverse();

            foreach (var file in disk)
            {
                Console.WriteLine($"{file.Id}");

                if (emptySpaces.Exists(e => e.Index < file.Index && e.Length >= file.Length))
                {
                    var emptyspace = emptySpaces.FirstOrDefault(e => e.Index < file.Index && e.Length >= file.Length);

                    file.Index = emptyspace.Index;
                    emptyspace.Index += file.Length;
                    emptyspace.Length -= file.Length;
                }

                for (decimal i = file.Index; i < file.Index + file.Length; i++)
                    result += i * file.Id;
            }

            return result.ToString();
        }
    }

    public class File
    {
        public int Id;
        public int Index;
        public int Length;

        public File(int id, int index, int length)
        {
            this.Id = id;
            this.Index = index;
            this.Length = length;
        }
    }

    public class EmptySpace
    {
        public int Index;
        public int Length;

        public EmptySpace(int index, int length)
        {
            this.Index = index;
            this.Length = length;
        }
    }
}