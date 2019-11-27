using CodeTech.Toolkit.Serializers.Csv;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CodeTech.Toolkit.Serializers.Tests.Serializers.Csv
{
    [TestClass]
    public class CsvSerializerTests
    {
        private class DummyData
        {
            internal int Property1 { get; set; }
            internal string Property2 { get; set; }
        }

        [TestMethod]
        public void SerializerConstructionTest1()
        {
            var serializer = new CsvSerializer<DummyData>(new CsvSerializerFormat(";", "-"));
            Assert.AreEqual(";", serializer.Format.CellDelimiter);
            Assert.AreEqual("-", serializer.Format.RowDelimiter);
        }

        [TestMethod]
        public void SerializerConstructionTest2()
        {
            var serializer = new CsvSerializer<DummyData>(";", "-");
            Assert.AreEqual(";", serializer.Format.CellDelimiter);
            Assert.AreEqual("-", serializer.Format.RowDelimiter);
        }

        [TestMethod]
        public void SerializerConstructionTest3()
        {
            var serializer = new CsvSerializer<DummyData>(new CsvSerializerFormat(";", "-"), true);
            Assert.AreEqual(";", serializer.Format.CellDelimiter);
            Assert.AreEqual("-", serializer.Format.RowDelimiter);
            Assert.AreEqual(true, serializer.IncludeColumnHeaders);
        }

        [TestMethod]
        public void SerializerConstructionTest4()
        {
            var serializer = new CsvSerializer<DummyData>(";", "-", true);
            Assert.AreEqual(";", serializer.Format.CellDelimiter);
            Assert.AreEqual("-", serializer.Format.RowDelimiter);
            Assert.AreEqual(true, serializer.IncludeColumnHeaders);
        }

        [TestMethod]
        public void SerializerSerializeDataWithColumnHeaderTest()
        {
            var list = new List<DummyData> { new DummyData { Property1 = 1, Property2 = "One" }, new DummyData { Property1 = 2, Property2 = "Two" } };
            var serializer = new CsvSerializer<DummyData>(";", "-", true);
            serializer.AddColumn("ColumnOne", x => x.Property1.ToString());
            serializer.AddColumn("ColumnTwo", x => x.Property2);
            var serializedString = serializer.Serialize(list);

            Assert.AreEqual("ColumnOne;ColumnTwo-1;One-2;Two-", serializedString);
        }

        [TestMethod]
        public void SerializerSerializeDataWithoutColumnHeaderTest()
        {
            var list = new List<DummyData> { new DummyData { Property1 = 1, Property2 = "One" }, new DummyData { Property1 = 2, Property2 = "Two" } };
            var serializer = new CsvSerializer<DummyData>(";", "-", false);
            serializer.AddColumn("ColumnOne", x => x.Property1.ToString());
            serializer.AddColumn("ColumnTwo", x => x.Property2);
            var serializedString = serializer.Serialize(list);

            Assert.AreEqual("1;One-2;Two-", serializedString);
        }

        [TestMethod]
        public void SerializerGetSerializerEnumerableWithColumnHeaderTest()
        {
            var list = new List<DummyData> { new DummyData { Property1 = 1, Property2 = "One" }, new DummyData { Property1 = 2, Property2 = "Two" } };
            var serializer = new CsvSerializer<DummyData>(";", "-", true);
            serializer.AddColumn("ColumnOne", x => x.Property1.ToString());
            serializer.AddColumn("ColumnTwo", x => x.Property2);
            var rows = serializer.GetRows(list); 

            Assert.AreEqual(3, rows.Count());
        }

        [TestMethod]
        public void SerializerGetSerializerEnumerableWithoutColumnHeaderTest()
        {
            var list = new List<DummyData> { new DummyData { Property1 = 1, Property2 = "One" }, new DummyData { Property1 = 2, Property2 = "Two" } };
            var serializer = new CsvSerializer<DummyData>(";", "-", false);
            serializer.AddColumn("ColumnOne", x => x.Property1.ToString());
            serializer.AddColumn("ColumnTwo", x => x.Property2);
            var rows = serializer.GetRows(list);

            Assert.AreEqual(2, rows.Count());
        }
    }
}
