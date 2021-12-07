using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AdventOfCode.Y2021.Days;

namespace AdventOfCodeTests.Y2021.Days
{
    [TestClass]
    public class TestDay05
    {
        Day05? day;
        string[] results = new[] { "5", "12", "7674", "20898" };

        [TestMethod]
        public void Test_Run_Part1()
        {
            day = new(true);

            string result = day.RunPart1();

            Assert.AreEqual(results[0], result);
        }

        [TestMethod]
        public void Test_Run_Part2()
        {
            day = new(true);

            string result = day.RunPart2();

            Assert.AreEqual(results[1], result);
        }

        [TestMethod]
        public void Run_Part1()
        {
            day = new(false);

            string result = day.RunPart1();

            Assert.AreEqual(results[2], result);
        }

        [TestMethod]
        public void Run_Part2()
        {
            day = new(false);

            string result = day.RunPart2();

            Assert.AreEqual(results[3], result);
        }
    }
}