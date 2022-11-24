using Microsoft.EntityFrameworkCore;
using Northwind.Store.Model;

namespace Northwind.Store.Data
{
    public class ProductRepository : BaseRepository<Product, int>
    {
        public ProductRepository(NWContext context) : base(context) { }

        public override async Task<Product> Get(int key)
        {
            return await _db.Products.Include(p => p.Category).Include(p => p.Supplier).FirstOrDefaultAsync(m => m.ProductId == key);
        }

        public override async Task<IEnumerable<Product>> GetList(PageFilter pf = null)
        {
            return await _db.Products.Include(p => p.Category).Include(p => p.Supplier).ToListAsync();
        }

        public override async Task<int> Delete(int key)
        {
            return await _db.Database.ExecuteSqlInterpolatedAsync(
                $"DELETE FROM Products WHERE ProductID = {key}");
        }

        public IEnumerable<Product> GetProductDbSet()
        {
            return _db.Products.Include(p => p.Category).Include(p => p.Supplier);
        }

        public DbSet<Category> GetCategoriesDbSet()
        {
            return _db.Categories;
        }

        public DbSet<Supplier> GetSuppliersDbSet()
        {
            return _db.Suppliers;
        }
    }
}
