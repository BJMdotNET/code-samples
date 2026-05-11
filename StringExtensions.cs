using System;

namespace Samples.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualsIgnoringCase(this string source, string comparison)
        {
            if (string.IsNullOrEmpty(source))
            {
                return false;
            }
            
            if (comparison == null)
            {
                throw new ArgumentNullException(nameof(comparison), $"{nameof(comparison)} cannot be null.");
            }
            
            return source.Equals(comparison, StringComparison.InvariantCultureIgnoreCase);
        }
        
        public static bool ContainsIgnoringCase(this string source, string substring)
        {
            return Contains(source, substring, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool Contains(this string source, string substring, StringComparison stringComparison)
        {
            if (string.IsNullOrEmpty(source))
            {
                return false;
            }

            if (substring == null)
            {
                throw new ArgumentNullException(nameof(substring), $"{nameof(substring)} cannot be null.");
            }

            if (!Enum.IsDefined(typeof(StringComparison), stringComparison))
            {
                throw new ArgumentException($"{nameof(stringComparison)} is not a member of StringComparison", nameof(stringComparison));
            }

            return source.IndexOf(substring, stringComparison) >= 0;
        }

        public static bool StartsWithIgnoringCase(this string source, string substring)
        {
            return StartsWith(source, substring, StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool StartsWith(this string source, string substring, StringComparison stringComparison)
        {
            if (substring == null)
            {
                throw new ArgumentNullException(nameof(substring), $"{nameof(substring)} cannot be null.");
            }

            if (!Enum.IsDefined(typeof(StringComparison), stringComparison))
            {
                throw new ArgumentException($"{nameof(stringComparison)} is not a member of StringComparison", nameof(stringComparison));
            }

            return source.IndexOf(substring, stringComparison) == 0;
        }
    }
}
