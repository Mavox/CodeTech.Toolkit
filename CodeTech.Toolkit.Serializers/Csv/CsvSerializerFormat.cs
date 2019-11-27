using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTech.Toolkit.Serializers.Csv
{
    public class CsvSerializerFormat
    {
        /// <summary>
        /// Creates an instance of a format to use when serializing
        /// </summary>
        /// <param name="cellDelimiter">Delimiter to use between cells</param>
        /// <param name="rowDelimiter">Delimiter to use between rows</param>
        public CsvSerializerFormat(string cellDelimiter, string rowDelimiter)
        {
            CellDelimiter = cellDelimiter;
            RowDelimiter = rowDelimiter;
        }

        /// <summary>
        /// Gets the celldelimiter for the current format
        /// </summary>
        public string CellDelimiter { get; }

        /// <summary>
        /// Gets the rowdelimiter for the current format
        /// </summary>
        public string RowDelimiter { get; }

        /// <summary>
        /// Gets a predefined format specified to use commas as celldelimiters
        /// </summary>
        public static CsvSerializerFormat CommaSeparated
        {
            get
            {
                return new CsvSerializerFormat(",", "\r\n");
            }
        }

        /// <summary>
        /// Gets a predefined format specified to use semicolons as celldelimiters
        /// </summary>
        public static CsvSerializerFormat SemicolonSeparated
        {
            get
            {
                return new CsvSerializerFormat(";", "\r\n");
            }
        }

        /// <summary>
        /// Gets a predefined format specified to use tab-indentations as celldelimiters
        /// </summary>
        public static CsvSerializerFormat TabSeparated
        {
            get
            {
                return new CsvSerializerFormat("\t", "\r\n");
            }
        }
    }
}
