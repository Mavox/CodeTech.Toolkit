using System;

namespace CodeTech.Toolkit.Serializers.Csv
{
    public partial class CsvSerializer<TData>
    {
        internal class DataColumn
        {
            internal DataColumn()
            {

            }
            internal string Name { get; set; }
            internal Func<TData, string> Selector { get; set; }
        }
    }
}
