using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Store.Data;
using X.PagedList;

namespace Northwind.Store.UI.Web.Internet.Controllers
{
    public class ProductController : Controller
    {
        private readonly NWContext _context;
        private readonly SessionSettings _ss;

        public ProductController(NWContext context, SessionSettings ss)
        {
            _context = context;
            _ss = ss;
        }

        // GET: Product
        public IActionResult Index(int? page, string filter = "")
        {
            int totalMaxItemsPerPage = 5;
            var pageNumber = page ?? 1;
            var items = _context.Products.Include(p => p.Category).Include(p => p.Supplier).ToPagedList(pageNumber, totalMaxItemsPerPage);

            ViewBag.cartItems = _ss.Cart.Count;

            if (filter != null)
            {
                items = _context.Products.Include(p => p.Category).Include(p => p.Supplier).Where(p => p.ProductName.Contains(filter)).ToPagedList(pageNumber, totalMaxItemsPerPage);
            }

            ViewBag.txtsearch = filter ?? "";
            
            return View(items);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
