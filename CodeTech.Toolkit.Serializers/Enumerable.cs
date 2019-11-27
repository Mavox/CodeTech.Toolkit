using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTech.Toolkit.Serializers
{
    public class Enumerable<TData> : IEnumerable<TData>
    {
        private readonly Func<IEnumerator<TData>> constructor;
        public Enumerable(Func<IEnumerator<TData>> constructor)
        {
            this.constructor = constructor;
        }

        public IEnumerator<TData> GetEnumerator()
        {
            return constructor();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
