using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLSV.Extension;
using QLSV.Models;

namespace QLSV.Controllers
{
    public class DangKyController : Controller
    {
        private readonly QLSVContext _context;

        public DangKyController(QLSVContext context)
        {
            _context = context;
        }

        // GET: DangKy
        [Route("DangKy")]
        public async Task<IActionResult> Index(string sortOrder,
    string currentFilter,
    string searchString,
    int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["MaHpParm"] = String.IsNullOrEmpty(sortOrder) ? "ma_hp" : "";
            ViewData["MaSvParm"] = String.IsNullOrEmpty(sortOrder) ? "ma_sv" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var dangkys = from s in _context.DangKy
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                dangkys = dangkys.Where(s => s.Mahp.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ma_hp":
                    dangkys = dangkys.OrderByDescending(s => s.Mahp);
                    break;
                case "ma_sv":
                    dangkys = dangkys.OrderBy(s => s.Mssv);
                    break;
                default:
                    dangkys = dangkys.OrderBy(s => s.Mahp);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<DangKy>.CreateAsync(dangkys.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: DangKy/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dangKy = await _context.DangKy
                .Include(d => d.MahpNavigation)
                .Include(d => d.MssvNavigation)
                .SingleOrDefaultAsync(m => m.Mahp == id);
            if (dangKy == null)
            {
                return NotFound();
            }

            return View(dangKy);
        }

        // GET: DangKy/Create
        public IActionResult Create()
        {
            ViewData["Mahp"] = new SelectList(_context.HocPhan, "Mahp", "Mahp");
            ViewData["Mssv"] = new SelectList(_context.SinhVien, "Mssv", "Mssv");
            return View();
        }

        // POST: DangKy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mahp,Mssv")] DangKy dangKy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dangKy);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["Mahp"] = new SelectList(_context.HocPhan, "Mahp", "Mahp", dangKy.Mahp);
            ViewData["Mssv"] = new SelectList(_context.SinhVien, "Mssv", "Mssv", dangKy.Mssv);
            return View(dangKy);
        }

        // GET: DangKy/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dangKy = await _context.DangKy.SingleOrDefaultAsync(m => m.Mahp == id);
            if (dangKy == null)
            {
                return NotFound();
            }
            ViewData["Mahp"] = new SelectList(_context.HocPhan, "Mahp", "Mahp", dangKy.Mahp);
            ViewData["Mssv"] = new SelectList(_context.SinhVien, "Mssv", "Mssv", dangKy.Mssv);
            return View(dangKy);
        }

        // POST: DangKy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Mahp,Mssv")] DangKy dangKy)
        {
            if (id != dangKy.Mahp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dangKy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DangKyExists(dangKy.Mahp))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["Mahp"] = new SelectList(_context.HocPhan, "Mahp", "Mahp", dangKy.Mahp);
            ViewData["Mssv"] = new SelectList(_context.SinhVien, "Mssv", "Mssv", dangKy.Mssv);
            return View(dangKy);
        }

        // GET: DangKy/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dangKy = await _context.DangKy
                .Include(d => d.MahpNavigation)
                .Include(d => d.MssvNavigation)
                .SingleOrDefaultAsync(m => m.Mahp == id);
            if (dangKy == null)
            {
                return NotFound();
            }

            return View(dangKy);
        }

        // POST: DangKy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var dangKy = await _context.DangKy.SingleOrDefaultAsync(m => m.Mahp == id);
            _context.DangKy.Remove(dangKy);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DangKyExists(string id)
        {
            return _context.DangKy.Any(e => e.Mahp == id);
        }
    }
}
