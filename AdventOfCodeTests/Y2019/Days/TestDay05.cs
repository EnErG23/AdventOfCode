using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AdventOfCode.Y2019.Days;

namespace AdventOfCodeTests.Y2019.Days
{
    [TestClass]
    public class TestDay05
    {
        int y;
        int d;

        Day05? testDay;
        Day05? day;
        string[] results = new[] { "1002", "5044655", "1002", "7408802" };

        public TestDay05()
        {
            var type = GetType();
            y = Convert.ToInt32(type.Namespace.Substring(type.Namespace.Length - 9, 4));
			d = Convert.ToInt32(type.Name.Substring(7, 2));
			testDay = new(y, d, true);
			day = new(y, d, false);
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