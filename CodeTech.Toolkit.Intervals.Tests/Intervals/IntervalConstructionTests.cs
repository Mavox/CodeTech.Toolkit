using System;
using CodeTech.Toolkit.Intervals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeTech.Toolkit.Tests.Common.Intervals
{
    [TestClass]
    public class IntervalConstructionTests
    {
        [TestMethod]
        public void IntervalDefaultConstruction()
        {
            Interval<int> interval = new Interval<int>();
            Assert.AreEqual(interval.Start, default(int));
            Assert.AreEqual(interval.Stop, default(int));
        }

        [TestMethod]
        public void IntervalValueConstruction()
        {
            Interval<int> interval = new Interval<int>(1, 2);
            Assert.AreEqual(interval.Start, 1);
            Assert.AreEqual(interval.Stop, 2);
        }

        [TestMethod]
        public void IntervalNullValueConstruction()
        {
            Interval<string> interval = new Interval<string>(null, null);
            Assert.IsNull(interval.Start);
            Assert.IsNull(interval.Stop);
        }
    }
}
