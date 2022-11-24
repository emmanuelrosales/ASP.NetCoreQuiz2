using Microsoft.EntityFrameworkCore;
using Northwind.Store.Model;

namespace Northwind.Store.Data
{
    public class RegionRepository : BaseRepository<Region, int>
    {
        public RegionRepository(NWContext context) : base(context) { }

        public override async Task<Region> Get(int key)
        {
            return await base.Get(key);
        }

        public override async Task<int> Delete(int key)
        {
            return await _db.Database.ExecuteSqlInterpolatedAsync(
                $"DELETE FROM Region WHERE RegionID = {key}");
        }

        public DbSet<Region> GetRegionDbSet()
        {
            return _db.Regions;
        }
    }
}
