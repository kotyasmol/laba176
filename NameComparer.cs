using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonLibrary
{
    public class PersonNameComparer : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            // Сравниваем по имени
            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }
    }
}
