using System;
using CodeTech.Toolkit.Limits;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeTech.Toolkit.Tests.Common.Limits
{
    [TestClass]
    public class LimitConstructionTests
    {
        [TestMethod]
        public void LimitValueConstruction()
        {
            var limit = new Limit(1, new TimeSpan(0, 0, 1));

            Assert.AreEqual(limit.Occurrences, 1);
            Assert.AreEqual(limit.TimeUnit, new TimeSpan(0, 0, 1));
        }

        [TestMethod]
        public void LimitZeroOccurrenceValueConstruction()
        {
            try
            {
                var limit = new Limit(0, new TimeSpan(0, 0, 1));
                Assert.Fail();
            }
            catch(ArgumentOutOfRangeException)
            {
            }
            catch(Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void LimitNegativeOccurrenceValueConstruction()
        {
            try
            {
                var limit = new Limit(-1, new TimeSpan(0, 0, 1));
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void LimitZeroTimeUnitValueConstruction()
        {
            try
            {
                var limit = new Limit(1, TimeSpan.Zero);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void LimitNegativeTimeUnitValueConstruction()
        {
            try
            {
                var limit = new Limit(-1, new TimeSpan(0, 0, -1));
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}
