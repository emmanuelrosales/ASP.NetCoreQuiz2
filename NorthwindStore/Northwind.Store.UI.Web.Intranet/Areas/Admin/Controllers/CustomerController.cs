using Microsoft.AspNetCore.Mvc;
using Northwind.Store.Data;
using Northwind.Store.Model;
using X.PagedList;

namespace Northwind.Store.UI.Web.Intranet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly CustomerRepository _cr;

        public CustomerController(CustomerRepository cr)
        {
            _cr = cr;
        }

        // GET: Admin/Customer
        public IActionResult Index(int? page)
        {
            int totalMaxOrderPerPage = 10;
            var pageNumber = page ?? 1;
            return View(_cr.GetListForPagination().ToPagedList(pageNumber, totalMaxOrderPerPage));
        }

        // GET: Admin/Customer/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _cr.Get(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Admin/Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(customer);
                //await _context.SaveChangesAsync();
                customer.State = Model.ModelState.Added;
                await _cr.Save(customer);

                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Admin/Customer/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _cr.Get(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CustomerId,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax,RowVersion,ModifiedProperties")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                customer.State = Model.ModelState.Modified;
                await _cr.Save(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Admin/Customer/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _cr.Get(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Admin/Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            //if (_context.Customers == null)
            //{
            //    return Problem("Entity set 'NWContext.Customers'  is null.");
            //}
            //var customer = await _context.Customers.FindAsync(id);
            //if (customer != null)
            //{
            //    _context.Customers.Remove(customer);
            //}

            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            await _cr.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool CustomerExists(string id)
        //{
        //  return _context.Customers.Any(e => e.CustomerId == id);
        //}
    }
}
