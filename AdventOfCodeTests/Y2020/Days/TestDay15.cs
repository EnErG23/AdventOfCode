using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AdventOfCode.Y2020.Days;

namespace AdventOfCodeTests.Y2020.Days
{
    [TestClass]
    public class TestDay15
    {
        int y;
        int d;

        Day15? day;
        string[] results = new[] { "436", "1111", "175594", "48568" };

        public TestDay15()
        {
            var type = GetType();
            y = Convert.ToInt32(type.Namespace.Substring(type.Namespace.Length - 9, 4));
            d = Convert.ToInt32(type.Name.Substring(7, 2));
        }

        [TestMethod]
        public void Test_Run_Part1()
        {
            day = new(y, d, true);

            string result = day.RunPart1();

            Assert.AreEqual(results[0], result);
        }

        [TestMethod]
        public void Run_Part1()
        {
            day = new(y, d, false);

            string result = day.RunPart1();

            Assert.AreEqual(results[1], result);
        }

        [TestMethod]
        public void Test_Run_Part2()
        {
            day = new(y, d, true);

            string result = day.RunPart2();

            Assert.AreEqual(results[2], result);
        }

        [TestMethod]
        public void Run_Part2()
        {
            day = new(y, d, false);

            string result = day.RunPart2();

            Assert.AreEqual(results[3], result);
        }
    }
}