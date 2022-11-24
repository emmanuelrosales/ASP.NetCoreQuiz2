using Microsoft.EntityFrameworkCore;
using Northwind.Store.Model;

namespace Northwind.Store.Data
{
    public class EmployeeRepository : BaseRepository<Employee, int>
    {
        public EmployeeRepository(NWContext context) : base(context) { }

        public override async Task<Employee> Get(int key)
        {
            return await base.Get(key);
        }

        public override async Task<IEnumerable<Employee>> GetList(PageFilter pf = null)
        {
            return await _db.Employees.Include(e => e.ReportsToNavigation).ToListAsync();
        }

        public override async Task<int> Delete(int key)
        {
            return await _db.Database.ExecuteSqlInterpolatedAsync(
                $"DELETE FROM Employees WHERE EmployeeID = {key}");
        }

        public DbSet<Employee> GetDbSet()
        {
            return _db.Employees;
        }

        /// <summary>
        /// Lee la imagen de base de datos como un MemoryStream.
        /// </summary>
        /// <example>
        /// Para utilizarse en una acción de un Controller de ASP.NET MVC
        /// public FileStreamResult ReadImage(int id)
        /// {
        ///    return File(pB.ReadImageStream(id), "image/jpg");
        /// } 
        /// </example>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MemoryStream> GetFileStream(int id)
        {
            MemoryStream result = null;

            var image = await _db.Employees.Where(c => c.EmployeeId == id).
                Select(i => i.Photo).AsNoTracking().FirstOrDefaultAsync();

            if (image != null)
            {
                result = new MemoryStream(image);
            }

            return result;
        }

        /// <summary>
        /// Lee la imagen de base de datos como un string en Base64.
        /// </summary>
        /// <example>
        /// Para utilizarse directamente en una vista de razor. 
        /// </example>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetFileBase64(int id)
        {
            string result = "";

            using (var ms = await GetFileStream(id))
            {
                if (ms != null)
                {
                    var base64 = Convert.ToBase64String(ms.ToArray());
                    result = $"data:image/jpg;base64,{base64}";
                }
            }

            return result;
        }
    }
}
