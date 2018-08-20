using System.Collections.Generic;

namespace SCVBackend.Model
{
    public class PagedResult<T>
        where T : class
    {
        public PagedResult(long totalCount, IEnumerable<T> items)
        {
            TotalCount = totalCount;
            Items = items;
        }
        public long TotalCount { get; private set; }
        public IEnumerable<T> Items { get; private set; }
    }
}