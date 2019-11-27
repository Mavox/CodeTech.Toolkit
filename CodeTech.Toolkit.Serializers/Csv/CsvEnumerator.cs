using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodeTech.Toolkit.Serializers.Csv
{
    public partial class CsvSerializer<TData>
    {
        internal class CsvEnumerator : IEnumerator<string>
        {
            bool begunEnumerating = false;
            bool firstIteration = true;
            bool includeHeaders;
            IEnumerator<TData> internalEnumerator;
            IEnumerable<DataColumn> columns;
            CsvSerializerFormat format;

            internal CsvEnumerator(IEnumerator<TData> enumerator, bool includeHeaders, IEnumerable<DataColumn> columns, CsvSerializerFormat format)
            {
                this.includeHeaders = includeHeaders;
                internalEnumerator = enumerator;
                this.columns = columns;
                this.format = format;
            }

            private string GetHeaderRow()
            {
                return string.Format("{0}{1}", string.Join(format.CellDelimiter, columns.Select(x => x.Name)), format.RowDelimiter);
            }

            private string GetDataRow(TData item)
            {
                return string.Format("{0}{1}", string.Join(format.CellDelimiter, columns.Select(x => x.Selector(item))), format.RowDelimiter);
            }

            public string Current
            {
                get
                {
                    if (includeHeaders && firstIteration && begunEnumerating)
                    {
                        return GetHeaderRow();
                    }
                    else
                    {
                        var currentItem = internalEnumerator.Current;
                        return currentItem != null ? GetDataRow(currentItem) : null;
                    }
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public void Dispose()
            {
                internalEnumerator.Dispose();
            }

            public bool MoveNext()
            {
                if (begunEnumerating)
                {
                    if (!(includeHeaders && firstIteration))
                    {
                        return internalEnumerator.MoveNext();
                    }

                    firstIteration = false;
                    return true;
                }
                else
                {
                    begunEnumerating = true;
                    return internalEnumerator.MoveNext();
                }
            }

            public void Reset()
            {
                firstIteration = true;
                internalEnumerator.Reset();
            }
        }
    }
}
