using AdventOfCode.Models;

namespace AdventOfCode.Y2022.Days
{
    public class Day07 : Day
    {
        private List<Directory> _allDirectories;

        public Day07(int year, int day, bool test) : base(year, day, test)
        {
            _allDirectories = new List<Directory>();

            Directory currentDirectory = new Directory("/", null);
            _allDirectories.Add(currentDirectory);

            foreach (var input in Inputs.Skip(1))
            {
                var commands = input.Split(" ");

                switch (commands[0])
                {
                    case "$":
                        if (commands[1] == "cd")
                        {
                            if (commands[2] == "..")
                            {
                                currentDirectory = currentDirectory.ParentDirectory;
                            }
                            else
                            {
                                if (currentDirectory.Directories.Where(d => d.Name == commands[2]).Count() == 0)
                                {
                                    Directory newDirectory = new Directory(commands[2], currentDirectory);
                                    currentDirectory.Directories.Add(newDirectory);
                                    _allDirectories.Add(newDirectory);
                                }

                                currentDirectory = currentDirectory.Directories.FirstOrDefault(d => d.Name == commands[2]);
                            }
                        }
                        break;
                    case "dir":
                        if (currentDirectory.Directories.Where(d => d.Name == commands[1]).Count() == 0)
                        {
                            Directory newDirectory = new Directory(commands[1], currentDirectory);
                            currentDirectory.Directories.Add(newDirectory);
                            _allDirectories.Add(newDirectory);
                        }
                        break;
                    default:
                        currentDirectory.Files.Add(new File(commands[1], Convert.ToInt64(Convert.ToDecimal(commands[0]))));
                        break;
                }
            }
        }

        public override string RunPart1()
        {
            return _allDirectories
                .Where(d => d.Size <= 100000)
                .Sum(d => d.Size)
                .ToString();
        }

        public override string RunPart2()
        {
            return _allDirectories
                .Where(d => d.Size > (_allDirectories.FirstOrDefault(d => d.Name == "/").Size - (70000000 - 30000000)))
                .Min(d => d.Size)                
                .ToString();
        }
    }

    public class Directory
    {
        public string Name { get; set; }
        public Directory ParentDirectory { get; set; }
        public List<Directory> Directories { get; set; }
        public List<File> Files { get; set; }

        public Directory(string name, Directory parentDirectory)
        {
            Name = name;
            ParentDirectory = parentDirectory;
            Directories = new List<Directory>();
            Files = new List<File>();
        }

        public long Size
        {
            get
            {
                return Directories.Sum(d => d.Size) + Files.Sum(f => f.Size);
            }
        }
    }

    public class File
    {
        public string Name { get; set; }
        public long Size { get; set; }

        public File(string name, long size)
        {
            Name = name;
            Size = size;
        }
    }
}