using CodeTech.Toolkit.Intervals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeTech.Toolkit.Tests.Common.Intervals
{
    [TestClass]
    public class IntervalpropertyAssignmentTests
    {
        [TestMethod]
        public void IntervalStartAssignment()
        {
            Interval<int> interval = new Interval<int>();
            interval.Start = 1;

            Assert.AreEqual(interval.Start, 1);
        }

        [TestMethod]
        public void IntervalStopAssignment()
        {
            Interval<int> interval = new Interval<int>();
            interval.Stop = 1;

            Assert.AreEqual(interval.Stop, 1);
        }
    }
}
