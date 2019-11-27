using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeTech.Toolkit.Serializers.Csv
{
    public partial class CsvSerializer<TData>
    {
        /// <summary>
        /// Creates an instance of a CsvSerializer
        /// </summary>
        /// <param name="cellDelimiter">Delimiter to use between cells</param>
        /// <param name="rowDelimiter">Delimiter to use between rows</param>
        /// <param name="includeColumnHeaders">Set to true to include column headers, otherwise set to false</param>
        public CsvSerializer(string cellDelimiter, string rowDelimiter, bool includeColumnHeaders) : this(new CsvSerializerFormat(cellDelimiter, rowDelimiter), includeColumnHeaders)
        {

        }

        /// <summary>
        /// Creates an instance of a CsvSerializer
        /// </summary>
        /// <param name="cellDelimiter">Delimiter to use between cells</param>
        /// <param name="rowDelimiter">Delimiter to use between rows</param>
        public CsvSerializer(string cellDelimiter, string rowDelimiter) : this(new CsvSerializerFormat(cellDelimiter, rowDelimiter))
        {

        }

        /// <summary>
        /// Creates an instance of a CsvSerializer
        /// </summary>
        /// <param name="format">Format to use when serializing objects</param>
        public CsvSerializer(CsvSerializerFormat format) : this(format, true)
        {

        }

        /// <summary>
        /// Creates an instance of a CsvSerializer
        /// </summary>
        /// <param name="format">Format to use when serializing objects</param>
        /// <param name="includeColumnHeaders">Set to true to include column headers, otherwise set to false</param>
        public CsvSerializer(CsvSerializerFormat format, bool includeColumnHeaders)
        {
            Format = format;
            IncludeColumnHeaders = includeColumnHeaders;
        }


        private readonly List<DataColumn> columns = new List<DataColumn>();

        /// <summary>
        /// Gets or sets the current format
        /// </summary>
        public CsvSerializerFormat Format { get; set; }

        /// <summary>
        /// Gets or sets wether the serialization process should include headers or not
        /// </summary>
        public bool IncludeColumnHeaders { get; set; }

        /// <summary>
        /// Adds a column to the serialization process
        /// </summary>
        /// <param name="name">Displayed name of the column</param>
        /// <param name="selector">A function returning the column value for a given item</param>
        public void AddColumn(string name, Func<TData, string> selector)
        {
            columns.Add(new DataColumn()
            {
                Name = name,
                Selector = selector,
            });
        }

        protected string GetHeaderRow()
        {
            return string.Join(Format.CellDelimiter, columns.Select(x => x.Name));
        }

        protected string GetDataRow(TData item)
        {
            return string.Join(Format.CellDelimiter, columns.Select(x => x.Selector(item)));
        }

        /// <summary>
        /// Builds a csv-string from the provided items and stored columns with the specified celldelimiter and rowdelimiter
        /// </summary>
        /// <param name="items">Items to serialize</param>
        /// <returns>Csv-string</returns>
        public IEnumerable<string> GetRows(IEnumerable<TData> items)
        {
            return new Enumerable<string>(() => new CsvEnumerator(items.GetEnumerator(), IncludeColumnHeaders, columns, Format));
        }

        public string Serialize(IEnumerable<TData> items)
        {
            StringBuilder sb = new StringBuilder();
            var serializedRows = GetRows(items);
            foreach (var row in serializedRows)
            {
                sb.Append(row);
            }

            return sb.ToString();
        }
    }
}