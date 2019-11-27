using CodeTech.Toolkit.Serializers.Csv;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTech.Toolkit.Tests.Common.Serializers.Csv.Enumerators
{
    [TestClass]
    public class CsvEnumeratorTests
    {
        private class DummyData
        {
            internal int Property1 { get; set; }
            internal string Property2 { get; set; }
        }

        [TestMethod]
        public void EnumeratorConstructionTest()
        {
            var columnArray = new[] { new CsvSerializer<DummyData>.DataColumn() };
            var dummyDataList = new List<DummyData> { new DummyData(), new DummyData() };
            var format = new CsvSerializerFormat(";", "-");
            var enumerator = new CsvSerializer<DummyData>.CsvEnumerator(dummyDataList.GetEnumerator(), true, columnArray, format);
        }

        [TestMethod]
        public void EnumeratorStartingRowWithHeaderTest()
        {
            var columnArray = new[] { new CsvSerializer<DummyData>.DataColumn { Name = "Column1", Selector = x => x.Property1.ToString() }, new CsvSerializer<DummyData>.DataColumn { Name = "Column2", Selector = x => x.Property2 } };
            var dummyDataList = new List<DummyData> { new DummyData { Property1 = 1, Property2 = "One" }, new DummyData { Property1 = 2, Property2 = "Two" } };
            var format = new CsvSerializerFormat(";", "-");
            var enumerator = new CsvSerializer<DummyData>.CsvEnumerator(dummyDataList.GetEnumerator(), true, columnArray, format);

            string currentRow = enumerator.Current;
            Assert.IsNull(currentRow);
        }

        [TestMethod]
        public void EnumeratorFirstRowWithHeaderTest()
        {
            var columnArray = new[] { new CsvSerializer<DummyData>.DataColumn { Name = "Column1", Selector = x => x.Property1.ToString() }, new CsvSerializer<DummyData>.DataColumn { Name = "Column2", Selector = x => x.Property2 } };
            var dummyDataList = new List<DummyData> { new DummyData { Property1 = 1, Property2 = "One" }, new DummyData { Property1 = 2, Property2 = "Two" } };
            var format = new CsvSerializerFormat(";", "-");
            var enumerator = new CsvSerializer<DummyData>.CsvEnumerator(dummyDataList.GetEnumerator(), true, columnArray, format);

            enumerator.MoveNext();
            string currentRow = enumerator.Current;
            Assert.AreEqual("Column1;Column2-", currentRow);
        }

        [TestMethod]
        public void EnumeratorSecondRowWithHeaderTest()
        {
            var columnArray = new[] { new CsvSerializer<DummyData>.DataColumn { Name = "Column1", Selector = x => x.Property1.ToString() }, new CsvSerializer<DummyData>.DataColumn { Name = "Column2", Selector = x => x.Property2 } };
            var dummyDataList = new List<DummyData> { new DummyData { Property1 = 1, Property2 = "One" }, new DummyData { Property1 = 2, Property2 = "Two" } };
            var format = new CsvSerializerFormat(";", "-");
            var enumerator = new CsvSerializer<DummyData>.CsvEnumerator(dummyDataList.GetEnumerator(), true, columnArray, format);

            enumerator.MoveNext();
            enumerator.MoveNext();
            string secondRow = enumerator.Current;
            Assert.AreEqual("1;One-", secondRow);
        }

        [TestMethod]
        public void EnumeratorThirdRowWithHeaderTest()
        {
            var columnArray = new[] { new CsvSerializer<DummyData>.DataColumn { Name = "Column1", Selector = x => x.Property1.ToString() }, new CsvSerializer<DummyData>.DataColumn { Name = "Column2", Selector = x => x.Property2 } };
            var dummyDataList = new List<DummyData> { new DummyData { Property1 = 1, Property2 = "One" }, new DummyData { Property1 = 2, Property2 = "Two" } };
            var format = new CsvSerializerFormat(";", "-");
            var enumerator = new CsvSerializer<DummyData>.CsvEnumerator(dummyDataList.GetEnumerator(), true, columnArray, format);

            enumerator.MoveNext();
            enumerator.MoveNext();
            enumerator.MoveNext();
            string secondRow = enumerator.Current;
            Assert.AreEqual("2;Two-", secondRow);
        }

        [TestMethod]
        public void EnumeratorStartingRowWitouthHeaderTest()
        {
            var columnArray = new[] { new CsvSerializer<DummyData>.DataColumn { Name = "Column1", Selector = x => x.Property1.ToString() }, new CsvSerializer<DummyData>.DataColumn { Name = "Column2", Selector = x => x.Property2 } };
            var dummyDataList = new List<DummyData> { new DummyData { Property1 = 1, Property2 = "One" }, new DummyData { Property1 = 2, Property2 = "Two" } };
            var format = new CsvSerializerFormat(";", "-");
            var enumerator = new CsvSerializer<DummyData>.CsvEnumerator(dummyDataList.GetEnumerator(), false, columnArray, format);

            string currentRow = enumerator.Current;
            Assert.IsNull(currentRow);
        }

        [TestMethod]
        public void EnumeratorFirstRowWithoutHeaderTest()
        {
            var columnArray = new[] { new CsvSerializer<DummyData>.DataColumn { Name = "Column1", Selector = x => x.Property1.ToString() }, new CsvSerializer<DummyData>.DataColumn { Name = "Column2", Selector = x => x.Property2 } };
            var dummyDataList = new List<DummyData> { new DummyData { Property1 = 1, Property2 = "One" }, new DummyData { Property1 = 2, Property2 = "Two" } };
            var format = new CsvSerializerFormat(";", "-");
            var enumerator = new CsvSerializer<DummyData>.CsvEnumerator(dummyDataList.GetEnumerator(), false, columnArray, format);

            enumerator.MoveNext();
            string currentRow = enumerator.Current;
            Assert.AreEqual("1;One-", currentRow);
        }

        [TestMethod]
        public void EnumeratorSecondRowWithoutHeaderTest()
        {
            var columnArray = new[] { new CsvSerializer<DummyData>.DataColumn { Name = "Column1", Selector = x => x.Property1.ToString() }, new CsvSerializer<DummyData>.DataColumn { Name = "Column2", Selector = x => x.Property2 } };
            var dummyDataList = new List<DummyData> { new DummyData { Property1 = 1, Property2 = "One" }, new DummyData { Property1 = 2, Property2 = "Two" } };
            var format = new CsvSerializerFormat(";", "-");
            var enumerator = new CsvSerializer<DummyData>.CsvEnumerator(dummyDataList.GetEnumerator(), false, columnArray, format);

            enumerator.MoveNext();
            enumerator.MoveNext();
            string secondRow = enumerator.Current;
            Assert.AreEqual("2;Two-", secondRow);
        }
    }
}
