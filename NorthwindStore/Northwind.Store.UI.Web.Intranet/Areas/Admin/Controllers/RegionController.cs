using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Store.Data;
using Northwind.Store.Model;
using X.PagedList;

namespace Northwind.Store.UI.Web.Intranet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RegionController : Controller
    {
        private readonly RegionRepository _rr;

        public RegionController(RegionRepository rr)
        {
            _rr = rr;
        }

        // GET: Admin/Region
        public IActionResult Index(int? page)
        {
            int totalMaxOrderPerPage = 10;
            var pageNumber = page ?? 1;
            return View(_rr.GetRegionDbSet().ToPagedList(pageNumber, totalMaxOrderPerPage));
        }

            // GET: Admin/Region/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _rr.Get(id.Value);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // GET: Admin/Region/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Region/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegionId,RegionDescription")] Region region)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(region);
                //await _context.SaveChangesAsync();
                region.State = Model.ModelState.Added;
                await _rr.Save(region);
                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }

        // GET: Admin/Region/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _rr.Get(id.Value);
            if (region == null)
            {
                return NotFound();
            }
            return View(region);
        }

        // POST: Admin/Region/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegionId,RegionDescription,RowVersion,ModifiedProperties")] Region region)
        {
            if (id != region.RegionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                region.State = Model.ModelState.Modified;
                await _rr.Save(region);
                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }

        // GET: Admin/Region/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _rr.Get(id.Value);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // POST: Admin/Region/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _rr.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
