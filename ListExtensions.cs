using System;
using System.Collections.Generic;
using System.Linq;

namespace Libraries.Extensions
{
    public static class ListExtensions
    {
        public static bool ContainsIgnoringCase(this List<string> sequence, string substring)
        {
            return sequence.Contains(substring, StringComparer.OrdinalIgnoreCase);
        }
    }
}
