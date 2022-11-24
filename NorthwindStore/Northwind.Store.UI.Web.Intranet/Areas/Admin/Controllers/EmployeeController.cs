using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind.Store.Data;
using Northwind.Store.Model;
using X.PagedList;

namespace Northwind.Store.UI.Web.Intranet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _er;

        public EmployeeController(EmployeeRepository er)
        {
            _er = er;
        }

        // GET: Admin/Employee
        public IActionResult Index(int? page)
        {
            int totalMaxOrderPerPage = 3;
            var pageNumber = page ?? 1;
            return View(_er.GetDbSet().ToPagedList(pageNumber, totalMaxOrderPerPage));
        }

        // GET: Admin/Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _er.Get(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Admin/Employee/Create
        public IActionResult Create()
        {
            ViewData["ReportsTo"] = new SelectList(_er.GetDbSet(), "EmployeeId", "FirstName");
            return View();
        }

        // POST: Admin/Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,LastName,FirstName,Title,TitleOfCourtesy,BirthDate,HireDate,Address,City,Region,PostalCode,Country,HomePhone,Extension,Photo,Notes,ReportsTo,PhotoPath")] Employee employee, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null)
                {
                    // using System.IO;
                    using MemoryStream ms = new();
                    photo.CopyTo(ms);
                    employee.Photo = ms.ToArray();
                }
                //_context.Add(employee);
                //await _context.SaveChangesAsync();
                employee.State = Model.ModelState.Added;
                await _er.Save(employee);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReportsTo"] = new SelectList(_er.GetDbSet(), "EmployeeId", "FirstName", employee.ReportsTo);
            return View(employee);
        }

        // GET: Admin/Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _er.Get(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["ReportsTo"] = new SelectList(_er.GetDbSet(), "EmployeeId", "FirstName", employee.ReportsTo);
            return View(employee);
        }

        // POST: Admin/Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,LastName,FirstName,Title,TitleOfCourtesy,BirthDate,HireDate,Address,City,Region,PostalCode,Country,HomePhone,Extension,Photo,Notes,ReportsTo,PhotoPath,RowVersion,ModifiedProperties")] Employee employee, IFormFile photo)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //try
                //{
                //    _context.Update(employee);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!EmployeeExists(employee.EmployeeId))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                if (photo != null)
                {
                    // using System.IO;
                    using MemoryStream ms = new();
                    photo.CopyTo(ms);
                    employee.Photo = ms.ToArray();
                }
                employee.State = Model.ModelState.Modified;
                await _er.Save(employee);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReportsTo"] = new SelectList(_er.GetDbSet(), "EmployeeId", "FirstName", employee.ReportsTo);
            return View(employee);
        }

        // GET: Admin/Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _er.Get(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Admin/Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //if (_context.Employees == null)
            //{
            //    return Problem("Entity set 'NWContext.Employees'  is null.");
            //}
            //var employee = await _context.Employees.FindAsync(id);
            //if (employee != null)
            //{
            //    _context.Employees.Remove(employee);
            //}

            //await _context.SaveChangesAsync();
            await _er.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<FileStreamResult> ReadImage(int id)
        {
            return File(await _er.GetFileStream(id), "image/png"); ;
        }

        //private bool EmployeeExists(int id)
        //{
        //  return _context.Employees.Any(e => e.EmployeeId == id);
        //}
    }
}
