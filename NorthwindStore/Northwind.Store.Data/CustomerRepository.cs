using Microsoft.EntityFrameworkCore;
using Northwind.Store.Model;

namespace Northwind.Store.Data
{
    public class CustomerRepository : BaseRepository<Customer, string>
    {
        public CustomerRepository(NWContext context) : base(context) { }

        public override async Task<Customer> Get(string key)
        {
            return await base.Get(key);
        }

        public override async Task<int> Delete(string key)
        {
            return await _db.Database.ExecuteSqlInterpolatedAsync(
                $"DELETE FROM Customers WHERE CustomerID = {key}");
        }

        public DbSet<Customer> GetListForPagination()
        {
            return _db.Customers;
        }
    }
}
