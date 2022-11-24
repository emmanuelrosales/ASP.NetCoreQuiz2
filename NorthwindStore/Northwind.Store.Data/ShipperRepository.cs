using Microsoft.EntityFrameworkCore;
using Northwind.Store.Model;

namespace Northwind.Store.Data
{
    public class ShipperRepository : BaseRepository<Shipper, int>
    {
        public ShipperRepository(NWContext context) : base(context) { }

        public override async Task<Shipper> Get(int key)
        {
            return await base.Get(key);
        }

        public override async Task<int> Delete(int key)
        {
            return await _db.Database.ExecuteSqlInterpolatedAsync(
                $"DELETE FROM Shippers WHERE ShipperID = {key}");
        }

        public DbSet<Shipper> GetShipperDbSet()
        {
            return _db.Shippers;
        }
    }
}
