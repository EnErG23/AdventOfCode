﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AdventOfCode.Y2024.Days;

namespace AdventOfCodeTests.Y2024.Days
{
    [TestClass]
    public class TestDay14
    {
        int y;
        int d;

        Day14? testDay;
        Day14? day;
        string[] results = new[] { "12", "228457125", "1", "6493" };

        public TestDay14()
        {
            var type = GetType();
            y = Convert.ToInt32(type.Namespace.Substring(type.Namespace.Length - 9, 4));
            d = Convert.ToInt32(type.Name.Substring(7, 2));
			testDay = new(y, d, true);
			day = new(y,d,false);
        }

        [TestMethod]
        public void A_Test_Run_Part1()
        {
            string result = testDay.RunPart1();

            Assert.AreEqual(results[0], result);
        }

        [TestMethod]
        public void B_Run_Part1()
        {
            string result = day.RunPart1();

            Assert.AreEqual(results[1], result);
        }

        [TestMethod]
        public void C_Test_Run_Part2()
        {
            string result = testDay.RunPart2();

            Assert.AreEqual(results[2], result);
        }

        [TestMethod]
        public void D_Run_Part2()
        {
            string result = day.RunPart2();

            Assert.AreEqual(results[3], result);
        }
    }
}