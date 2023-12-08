using AdventOfCode.Helpers;
using AdventOfCode.Models;

namespace AdventOfCode.Y2023.Days
{
    public class Day08 : Day
    {
        private List<char> _instructions;
        private List<Node> _nodes;

        public Day08(int year, int day, bool test) : base(year, day, test)
        {
            _instructions = Inputs[0].ToCharArray().ToList();
            _nodes = Inputs.Skip(2).Select(i => new Node(i.Split('=')[0].Trim(), i.Split('=')[1].Split(',')[0].Trim().Replace("(", ""), i.Split('=')[1].Split(',')[1].Trim().Replace(")", ""))).ToList();
        }

        public override string RunPart1() => CountSteps(_instructions, _nodes, "AAA", "ZZZ").ToString();

        public override string RunPart2()
        {
            var instructions = Test ? "LR".ToCharArray().ToList() : _instructions;
            var nodes = Test ? new List<Node>() { new Node("11A", "11B", "XXX"), new Node("11B", "XXX", "11Z"), new Node("11Z", "11B", "XXX"), new Node("22A", "22B", "XXX"), new Node("22B", "22C", "22C"), new Node("22C", "22Z", "22Z"), new Node("22Z", "22B", "22B"), new Node("XXX", "XXX", "XXX") } : _nodes;

            return Algorithms.LCM(nodes.Where(n => n.Name.Last() == 'A').Select(s => CountSteps(instructions, nodes, s.Name, FindEndNode(instructions, nodes, s.Name))).ToList()).ToString();
        }

        long CountSteps(List<char> instructions, List<Node> nodes, string startNode, string endNode)
        {
            int steps = 0;
            var lastNode = startNode;

            while (lastNode != endNode)
                lastNode = instructions[steps++ % instructions.Count] == 'L' ? nodes.First(n => n.Name == lastNode).LeftNode : nodes.First(n => n.Name == lastNode).RightNode;

            return steps;
        }

        string FindEndNode(List<char> instructions, List<Node> nodes, string startNode)
        {
            int steps = 0;
            var lastNode = startNode;

            while (lastNode.Last() != 'Z')
                lastNode = instructions[steps++ % instructions.Count] == 'L' ? nodes.First(n => n.Name == lastNode).LeftNode : nodes.First(n => n.Name == lastNode).RightNode;

            return lastNode;
        }
    }

    public class Node
    {
        public string Name { get; set; }
        public string LeftNode { get; set; }
        public string RightNode { get; set; }

        public Node(string name, string leftNode, string rightNode)
        {
            Name = name;
            LeftNode = leftNode;
            RightNode = rightNode;
        }
    }
}