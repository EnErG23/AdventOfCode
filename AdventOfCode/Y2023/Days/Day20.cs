using AdventOfCode.Helpers;
using AdventOfCode.Models;
using System.Linq;

namespace AdventOfCode.Y2023.Days
{
    public class Day20 : Day
    {
        private List<Module> _modules;
        public Day20(int year, int day, bool test) : base(year, day, test) => ParseModules();

        public override string RunPart1() => Pulse(1000).ToString();

        public override string RunPart2()
        {
            // BLIJKBAAR IS DE INPUT WEER TROLLAGE EZ
            // OH KIJK IK KAN CYCLUSSEN PROGRAMMEREN DIE ALTIJD HETZELFDE TERUG LOOPEN
            // REVERSE ENGINEER THIS CRAP

            if (Test)
                return "undefined";

            // 1 OUT, prevModule has to return low pulse
            Module prevModule = _modules.First(m => m.NextModules.Exists(m => m.Name == "rx"));

            // 4 OUTS, for prevModule to return low, prevModules all have to return low pulse
            List<string> prevModules = _modules.Where(m => m.NextModules.Exists(m => m.Name == prevModule.Name)).Select(m => m.Name).ToList();

            // CHECK cycle for each prevModule module and get lcm to see when all are returning low
            return Algorithms.LCM(prevModules.Select(p => Pulse(p)).ToList()).ToString();
        }

        public long Pulse(long pulses)
        {
            long lowPulses = 0;
            long highPulses = 0;

            for (long i = 0; i < pulses; i++)
            {
                List<(Module, bool)> nextPulses = new() { (_modules.First(m => m.Type == 0), false) };

                while (nextPulses.Any())
                {
                    var nextPulse = nextPulses.First();
                    nextPulses.Remove(nextPulse);

                    var module = nextPulse.Item1;
                    var pulse = nextPulse.Item2;

                    if (pulse)
                        highPulses++;
                    else
                        lowPulses++;

                    switch (module.Type)
                    {
                        // Flip-flop modules (prefix %) are either on or off; they are initially off. If a flip-flop module receives a high pulse, it is ignored and nothing happens. 
                        // However, if a flip-flop module receives a low pulse, it flips between on and off. If it was off, it turns on and sends a high pulse. If it was on, it turns off and sends a low pulse. 
                        case 1:
                            if (pulse)
                                continue;

                            module.IsOn = !module.IsOn;
                            pulse = module.IsOn;
                            break;
                        // Conjunction modules (prefix &) remember the type of the most recent pulse received from each of their connected input modules; they initially default to remembering a low pulse for each input. 
                        // When a pulse is received, the conjunction module first updates its memory for that input. Then, if it remembers high pulses for all inputs, it sends a low pulse; otherwise, it sends a high pulse.
                        case 2:
                            pulse = !module.LastPulses.Select(l => l.Value).ToList().TrueForAll(p => p);
                            break;
                    }

                    foreach (var nextModule in module.NextModules)
                    {
                        nextPulses.Add((nextModule, pulse));

                        if (nextModule.Type == 2)
                            nextModule.LastPulses[module.Name] = pulse;
                    }
                }
            }

            return lowPulses * highPulses;
        }

        public long Pulse(string findModule)
        {
            // Reset modules to start clean for getting all cycle numbers
            ParseModules();

            int result = 0;

            while (true)
            {
                result++;

                List<(Module, bool)> nextPulses = new() { (_modules.First(m => m.Type == 0), false) };

                while (nextPulses.Any())
                {
                    var nextPulse = nextPulses.First();
                    nextPulses.Remove(nextPulse);

                    var module = nextPulse.Item1;
                    var pulse = nextPulse.Item2;

                    switch (module.Type)
                    {
                        // Flip-flop modules (prefix %) are either on or off; they are initially off. If a flip-flop module receives a high pulse, it is ignored and nothing happens. 
                        // However, if a flip-flop module receives a low pulse, it flips between on and off. If it was off, it turns on and sends a high pulse. If it was on, it turns off and sends a low pulse. 
                        case 1:
                            if (pulse)
                                continue;

                            module.IsOn = !module.IsOn;
                            pulse = module.IsOn;
                            break;
                        // Conjunction modules (prefix &) remember the type of the most recent pulse received from each of their connected input modules; they initially default to remembering a low pulse for each input. 
                        // When a pulse is received, the conjunction module first updates its memory for that input. Then, if it remembers high pulses for all inputs, it sends a low pulse; otherwise, it sends a high pulse.
                        case 2:
                            pulse = !module.LastPulses.Select(l => l.Value).ToList().TrueForAll(p => p);
                            if (module.Name == findModule && pulse)
                                return result;
                            break;
                    }

                    foreach (var nextModule in module.NextModules)
                    {
                        nextPulses.Add((nextModule, pulse));

                        if (nextModule.Type == 2)
                            nextModule.LastPulses[module.Name] = pulse;
                    }
                }
            }
        }

        public void ParseModules()
        {
            _modules = new();

            foreach (var input in Inputs)
            {
                int type = input[0] == '%' ? 1 : (input[0] == '&' ? 2 : 0);
                string name = type > 0 ? input.Substring(1, input.IndexOf(' ') - 1) : input.Substring(0, input.IndexOf(' '));

                _modules.Add(new Module(name, type));
            }

            foreach (var input in Inputs)
                foreach (var nextModule in input.Replace(", ", ",").Substring(input.Replace(", ", ",").LastIndexOf(' ') + 1).Split(",").ToList())
                    _modules.Add(new Module(nextModule, 3));

            foreach (var input in Inputs)
            {
                int type = input[0] == '%' ? 1 : (input[0] == '&' ? 2 : 0);
                string name = type > 0 ? input.Substring(1, input.IndexOf(' ') - 1) : input.Substring(0, input.IndexOf(' '));
                Module module = _modules.First(m => m.Name == name);

                foreach (var nextModule in input.Replace(", ", ",").Substring(input.Replace(", ", ",").LastIndexOf(' ') + 1).Split(",").ToList())
                    module.NextModules.Add(_modules.First(m => m.Name == nextModule));
            }

            foreach (var module in _modules.Where(m => m.Type == 2))
                module.LastPulses = _modules.Where(m => m.NextModules.Select(n => n.Name).ToList().Contains(module.Name)).ToDictionary(m => m.Name, m => false);
        }
    }

    public class Module
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public bool IsOn { get; set; }
        public List<Module> NextModules { get; set; }
        public Dictionary<string, bool> LastPulses { get; set; }

        public Module(string name, int type)
        {
            Name = name;
            Type = type;
            NextModules = new();
            LastPulses = new();
        }
    }
}