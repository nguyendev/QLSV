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
        [Route("dang-ky")]
        public async Task<IActionResult> Index(string sortOrder,
    string currentFilter,
    string searchString,
    int? page, int? pageSize)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["MaHpParm"] = String.IsNullOrEmpty(sortOrder) ? "ma_hp" : "";
            ViewData["MaSvParm"] = String.IsNullOrEmpty(sortOrder) ? "ma_sv" : "";
            ViewData["CurrentSize"] = pageSize;
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
            return View(await PaginatedList<DangKy>.CreateAsync(dangkys.AsNoTracking(), page ?? 1, pageSize != null ? pageSize.Value : 10));
        }

        // GET: DangKy/Details/5
        [Route("dang-ky/chi-tiet/{mahp}-{mssv}")]
        public async Task<IActionResult> Details(string Mahp, string Mssv)
        {
            if (Mahp == null || Mssv == null)
            {
                return NotFound();
            }

            var dangKy = await _context.DangKy
                .Include(d => d.MahpNavigation)
                .Include(d => d.MssvNavigation)
                .SingleOrDefaultAsync(m => m.Mahp == Mahp && m.Mssv == Mssv);
            if (dangKy == null)
            {
                return NotFound();
            }

            return View(dangKy);
        }

        // GET: DangKy/Create
        [Route("dang-ky/tao-moi")]
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
        [Route("dang-ky/tao-moi")]
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
        [Route("dang-ky/chinh-sua/{Mahp}-{Mssv}")]
        public async Task<IActionResult> Edit(string Mahp, string Mssv)
        {
            if (Mahp == null || Mssv == null)
            {
                return NotFound();
            }

            var dangKy = await _context.DangKy.SingleOrDefaultAsync(m => m.Mahp == Mahp && m.Mssv == Mssv);
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
        [Route("dang-ky/chinh-sua/{Mahp}-{Mssv}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string Mahp, string Mssv, [Bind("Mahp,Mssv")] DangKy dangKy)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dangKy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DangKyExists(dangKy.Mahp, dangKy.Mssv))
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
        public async Task<IActionResult> Delete(string Mahp, string Mssv)
        {
            if (Mahp == null || Mssv == null)
            {
                return NotFound();
            }

            var dangKy = await _context.DangKy
                .Include(d => d.MahpNavigation)
                .Include(d => d.MssvNavigation)
                .SingleOrDefaultAsync(m => m.Mahp == Mahp && m.Mssv == Mssv);
            if (dangKy == null)
            {
                return NotFound();
            }

            return View(dangKy);
        }

        // POST: DangKy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string Mahp, string Mssv)
        {
            var dangKy = await _context.DangKy.SingleOrDefaultAsync(m => m.Mahp == Mahp && m.Mssv == Mssv);
            _context.DangKy.Remove(dangKy);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DangKyExists(string Mahp, string Mssv)
        {
            return _context.DangKy.Any(e => e.Mahp == Mahp && e.Mssv == Mssv);
        }
    }
}
