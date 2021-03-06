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
    public class SinhVienController : Controller
    {
        private readonly QLSVContext _context;

        public SinhVienController(QLSVContext context)
        {
            _context = context;    
        }

        // GET: SinhVien
        // GET: HocPhan
        [Route("SinhVien")]
        public async Task<IActionResult> Index(string sortOrder,
    string currentFilter,
    string searchString,
    int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["HoTenSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ho_ten" : "";
            ViewData["GioiTinhSortParm"] = String.IsNullOrEmpty(sortOrder) ? "gioi_tinh" : "";
            ViewData["SdtSortParm"] = String.IsNullOrEmpty(sortOrder) ? "sdt" : "";
            ViewData["EmailSortParm"] = String.IsNullOrEmpty(sortOrder) ? "email" : "";
            ViewData["DiachiSortParm"] = String.IsNullOrEmpty(sortOrder) ? "dia_chi" : "";
            ViewData["NganhSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nganh" : "";
            ViewData["LopSortParm"] = String.IsNullOrEmpty(sortOrder) ? "lop" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var sinhviens = from s in _context.SinhVien
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                sinhviens = sinhviens.Where(s => s.Hoten.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ho_ten":
                    sinhviens = sinhviens.OrderByDescending(s => s.Hoten);
                    break;
                case "gioi_tinh":
                    sinhviens = sinhviens.OrderBy(s => s.Gioitinh);
                    break;
                case "sdt":
                    sinhviens = sinhviens.OrderByDescending(s => s.Sdt);
                    break;
                case "email":
                    sinhviens = sinhviens.OrderByDescending(s => s.Email);
                    break;
                case "dia_chi":
                    sinhviens = sinhviens.OrderByDescending(s => s.Diachi);
                    break;
                case "nganh":
                    sinhviens = sinhviens.OrderByDescending(s => s.Nganh);
                    break;
                case "lop":
                    sinhviens = sinhviens.OrderByDescending(s => s.Lop);
                    break;
                default:
                    sinhviens = sinhviens.OrderBy(s => s.Hoten);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<SinhVien>.CreateAsync(sinhviens.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: SinhVien/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhVien
                .SingleOrDefaultAsync(m => m.Mssv == id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // GET: SinhVien/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: SinhVien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mssv,Hoten,Namsinh,Gioitinh,Sdt,Email,Diachi,Nganh,Lop,Hinhanh")] SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sinhVien);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sinhVien);
        }

        // GET: SinhVien/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhVien.SingleOrDefaultAsync(m => m.Mssv == id);
            if (sinhVien == null)
            {
                return NotFound();
            }
            return View(sinhVien);
        }

        // POST: SinhVien/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Mssv,Hoten,Namsinh,Gioitinh,Sdt,Email,Diachi,Nganh,Lop,Hinhanh")] SinhVien sinhVien)
        {
            if (id != sinhVien.Mssv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sinhVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SinhVienExists(sinhVien.Mssv))
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
            return View(sinhVien);
        }

        // GET: SinhVien/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhVien
                .SingleOrDefaultAsync(m => m.Mssv == id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // POST: SinhVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sinhVien = await _context.SinhVien.SingleOrDefaultAsync(m => m.Mssv == id);
            _context.SinhVien.Remove(sinhVien);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SinhVienExists(string id)
        {
            return _context.SinhVien.Any(e => e.Mssv == id);
        }
    }
}
