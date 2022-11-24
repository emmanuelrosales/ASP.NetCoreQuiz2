using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Northwind.Store.Data;
using Northwind.Store.Model;
using X.PagedList;

namespace Northwind.Store.UI.Web.Intranet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ProductRepository _pr;

        public ProductController(ProductRepository pr)
        {
            _pr = pr;
        }

        // GET: Admin/Product
        public IActionResult Index(int? page)
        {
            int totalMaxOrderPerPage = 10;
            var pageNumber = page ?? 1;
            return View(_pr.GetProductDbSet().ToPagedList(pageNumber, totalMaxOrderPerPage));
        }

        // GET: Admin/Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _pr.Get(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Product/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_pr.GetCategoriesDbSet(), "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(_pr.GetSuppliersDbSet(), "SupplierId", "CompanyName");
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Product product)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(product);
                //await _context.SaveChangesAsync();
                product.State = Model.ModelState.Added;
                await _pr.Save(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_pr.GetCategoriesDbSet(), "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_pr.GetSuppliersDbSet(), "SupplierId", "CompanyName", product.SupplierId);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _pr.Get(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_pr.GetCategoriesDbSet(), "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_pr.GetSuppliersDbSet(), "SupplierId", "CompanyName", product.SupplierId);
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued,RowVersion,ModifiedProperties")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                product.State = Model.ModelState.Modified;
                await _pr.Save(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_pr.GetCategoriesDbSet(), "CategoryId", "CategoryName", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_pr.GetSuppliersDbSet(), "SupplierId", "CompanyName", product.SupplierId);
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _pr.Get(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //if (_context.Products == null)
            //{
            //    return Problem("Entity set 'NWContext.Products'  is null.");
            //}
            //var product = await _context.Products.FindAsync(id);
            //if (product != null)
            //{
            //    _context.Products.Remove(product);
            //}

            //await _context.SaveChangesAsync();
            await _pr.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool ProductExists(int id)
        //{
        //  return _context.Products.Any(e => e.ProductId == id);
        //}
    }
}
