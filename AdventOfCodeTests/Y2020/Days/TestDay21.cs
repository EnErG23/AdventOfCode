using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AdventOfCode.Y2020.Days;

namespace AdventOfCodeTests.Y2020.Days
{
    [TestClass]
    public class TestDay21
    {
        int y;
        int d;

        Day21? testDay;
        Day21? day;
        string[] results = new[] { "5", "1679", "mxmxvkd,sqjhc,fvjkl", "lmxt,rggkbpj,mxf,gpxmf,nmtzlj,dlkxsxg,fvqg,dxzq" };

        public TestDay21()
        {
            var type = GetType();
            y = Convert.ToInt32(type.Namespace.Substring(type.Namespace.Length - 9, 4));
			d = Convert.ToInt32(type.Name.Substring(7, 2));
			testDay = new(y, d, true);
			day = new(y, d, false);
		}

        [TestMethod]
		public void Test_Run_Part1()
		{
			string result = testDay.RunPart1();

            Assert.AreEqual(results[0], result);
        }

        [TestMethod]
        public void Run_Part1()
        {
            string result = day.RunPart1();

            Assert.AreEqual(results[1], result);
        }


        [TestMethod]
		public void Test_Run_Part2()
		{
			string result = testDay.RunPart2();

            Assert.AreEqual(results[2], result);
        }

        [TestMethod]
        public void Run_Part2()
        {
            string result = day.RunPart2();

            Assert.AreEqual(results[3], result);
        }
    }
}