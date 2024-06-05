using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaboratoryWork_12;
using PersonLibrary;

namespace labik16
{

    public static class TreeExtensions
    {
        public static IEnumerable<Person> SortByAge(this Tree<Person> tree)
        {
            return tree.OrderBy(p => p.Age);
        }

        public static IEnumerable<Person> FilterByType<T>(this Tree<Person> tree) where T : Person
        {
            return tree.OfType<T>();
        }

        public static IEnumerable<Person> FilterByAgeRange(this Tree<Person> tree, int minAge, int maxAge)
        {
            return tree.Where(p => p.Age >= minAge && p.Age <= maxAge);
        }
    }
}
