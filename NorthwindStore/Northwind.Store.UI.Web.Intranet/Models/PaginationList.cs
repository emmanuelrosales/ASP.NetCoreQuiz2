using Microsoft.EntityFrameworkCore;

namespace Northwind.Store.UI.Web.Intranet.Models
{
    public class PaginationList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public PaginationList(List<T> orders, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(orders);
        }

        public bool PreviousPage
        {
            get { return this.PageIndex > 0; }
        }

        public bool NextPage
        {
            get { return this.PageIndex < this.TotalPages; }
        }

        public static async Task<PaginationList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginationList<T>(items, count, pageIndex, pageSize);
        }
    }
}
