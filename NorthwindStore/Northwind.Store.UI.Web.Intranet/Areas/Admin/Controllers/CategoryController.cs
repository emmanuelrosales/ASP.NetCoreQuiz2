﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.Store.Data;
using Northwind.Store.Model;
using Northwind.Store.Notification;

namespace Northwind.Store.UI.Web.Intranet.Areas.Admin.Controllers
{
    //[Authorize]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _cr;
        private readonly Notifications ns = new();

        public CategoryController(CategoryRepository cr)
        {
            _cr = cr;
        }

        // GET: Admin/Category
        public async Task<IActionResult> Index()
        {
              return View(await _cr.GetList());
        }

        // GET: Admin/Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var category = await _cr.Get(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,Description,Picture")] Category category, IFormFile picture)
        {
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    // using System.IO;
                    using MemoryStream ms = new();
                    picture.CopyTo(ms);
                    category.Picture = ms.ToArray();
                }
                //_context.Add(category);
                //await _context.SaveChangesAsync();

                category.State = Model.ModelState.Added;
                await _cr.Save(category);

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _cr.Get(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,Description,Picture,RowVersion,ModifiedProperties")] Category category,IFormFile picture)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    // using System.IO;
                    using MemoryStream ms = new();
                    picture.CopyTo(ms);
                    category.Picture = ms.ToArray();
                }

                category.State = Model.ModelState.Modified;
                await _cr.Save(category, ns);

                if (ns.Count > 0)
                {
                    var msg = ns[0];
                    ModelState.AddModelError("", $"{msg.Title} - {msg.Description}");

                    return View(category);
                }


                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var category = await _context.Categories
            //    .FirstOrDefaultAsync(m => m.CategoryId == id);

            var category = await _cr.Get(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //if (_context.Categories == null)
            //{
            //    return Problem("Entity set 'NWContext.Categories'  is null.");
            //}
            //var category = await _context.Categories.FindAsync(id);
            //if (category != null)
            //{
            //    _context.Categories.Remove(category);
            //}

            //await _context.SaveChangesAsync();
            await _cr.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool CategoryExists(int id)
        //{
        //  return _context.Categories.Any(e => e.CategoryId == id);
        //}

        public async Task<FileStreamResult> ReadImage(int id)
        {
           // FileStreamResult result = null;

            //var category = await _context.Categories
            //    .FirstOrDefaultAsync(m => m.CategoryId == id);

            //var category = await _cr.Get(id);

            //if (category != null)
            //{
            //    var stream = new MemoryStream(category.Picture);

            //    if (stream != null)
            //    {
            //        result = File(stream, "image/png");
            //    }
            //}
            //result = File(await _cr.GetFileStream(id), "image/png");

            return File(await _cr.GetFileStream(id), "image/png"); ;
        }
    }
}
