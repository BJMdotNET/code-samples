using System;
using System.Collections.Generic;
using System.Linq;

namespace Samples.Common.Extensions
{
    public static class PagingExtensions // aka slicing / creating slices
    {
        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IEnumerable<List<TSource>> Pages<TSource>(this IEnumerable<TSource> source, int pageSize)
        {
            var pages = source.Select((x, index) => new
            {
                Index = index / pageSize,
                Value = x
            }).GroupBy(x => x.Index).Select(x => x.Select(y => y.Value).ToList());

            return pages.ToList();
        }

        public static IEnumerable<TResult> PageSelect<TItem, TResult>(
            this IEnumerable<TItem> items, Func<IEnumerable<TItem>, IEnumerable<TResult>> select, int pageSize)
        {
            foreach (var batch in items.Pages(pageSize))
            {
                foreach (var result in select(batch))
                {
                    yield return result;
                }
            }
        }
    }
}
