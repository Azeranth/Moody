using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moody.Engine.Extensions
{
    public static partial class Extensions
    {
        public static bool AddUnique<T>(this List<T> collection, Func<T, bool> predicate, T member)
        {
            if (!collection.Any(predicate))
            {
                collection.Add(member);
                return true;
            }
            return false;
        }

        public static bool AddUnique<T>(this List<T>collection, T member)
        {
            if(!collection.Any(n=>n.Equals(member)))
            {
                collection.Add(member);
                return true;
            }
            return false;
        }
    }
}
