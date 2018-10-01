using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        public long TotalCount { get; }

        [Required]
        public IEnumerable<T> Items { get; }
    }
}