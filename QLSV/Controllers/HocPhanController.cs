using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLSV.Models;
using QLSV.Extension;

namespace QLSV.Controllers
{
    public class HocPhanController : Controller
    {
        private readonly QLSVContext _context;

        public HocPhanController(QLSVContext context)
        {
            _context = context;    
        }

        // GET: HocPhan
        [Route("hoc-phan")]
        public async Task<IActionResult> Index(string sortOrder,
 string currentFilter,
    string searchString,
    int? page, int? pageSize)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TenmonSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ten_mon" : "";
            ViewData["SisoSortParm"] = String.IsNullOrEmpty(sortOrder) ? "si_so" : "";
            ViewData["PhonghocSortParm"] = String.IsNullOrEmpty(sortOrder) ? "phong_hoc" : "";
            ViewData["TiethocSortParm"] = String.IsNullOrEmpty(sortOrder) ? "tiet_hoc" : "";
            ViewData["ThuMonSortParm"] = String.IsNullOrEmpty(sortOrder) ? "thu_mon" : "";
            ViewData["NgaybdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ngay_bd" : "";
            ViewData["NgayktSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ngay_kt" : "";
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
            var hocphans = from s in _context.HocPhan
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                hocphans = hocphans.Where(s => s.Tenmon.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ten_mon":
                    hocphans = hocphans.OrderByDescending(s => s.Tenmon);
                    break;
                case "si_so":
                    hocphans = hocphans.OrderBy(s => s.Siso);
                    break;
                case "phong_hoc":
                    hocphans = hocphans.OrderByDescending(s => s.Phonghoc);
                    break;
                case "tiet_hoc":
                    hocphans = hocphans.OrderByDescending(s => s.Tiethoc);
                    break;
                case "thu_mon":
                    hocphans = hocphans.OrderByDescending(s => s.Thu);
                    break;
                case "ngay_bd":
                    hocphans = hocphans.OrderByDescending(s => s.Ngaybd);
                    break;
                case "ngay_kt":
                    hocphans = hocphans.OrderByDescending(s => s.Ngaykt);
                    break;
                default:
                    hocphans = hocphans.OrderBy(s => s.Ngaybd);
                    break;
            }

            return View(await PaginatedList<HocPhan>.CreateAsync(hocphans.AsNoTracking(), page ?? 1, pageSize != null ? pageSize.Value : 10));
        }
        // GET: HocPhan/Details/5
        [Route("hoc-phan/chi-tiet/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hocPhan = await _context.HocPhan
                .SingleOrDefaultAsync(m => m.Mahp == id);
            if (hocPhan == null)
            {
                return NotFound();
            }

            return View(hocPhan);
        }

        // GET: HocPhan/Create
        [Route("hoc-phan/tao")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: HocPhan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("hoc-phan/tao")]
        public async Task<IActionResult> Create([Bind("Mahp,Tenmon,Siso,Phonghoc,Tiethoc,Thu,Ngaybd,Ngaykt")] HocPhan hocPhan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hocPhan);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(hocPhan);
        }

        // GET: HocPhan/Edit/5
        [Route("hoc-phan/chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hocPhan = await _context.HocPhan.SingleOrDefaultAsync(m => m.Mahp == id);
            if (hocPhan == null)
            {
                return NotFound();
            }
            return View(hocPhan);
        }

        // POST: HocPhan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("hoc-phan/chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id, [Bind("Mahp,Tenmon,Siso,Phonghoc,Tiethoc,Thu,Ngaybd,Ngaykt")] HocPhan hocPhan)
        {
            if (id != hocPhan.Mahp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hocPhan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HocPhanExists(hocPhan.Mahp))
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
            return View(hocPhan);
        }

        // GET: HocPhan/Delete/5
        [Route("hoc-phan/xoa/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hocPhan = await _context.HocPhan
                .SingleOrDefaultAsync(m => m.Mahp == id);
            if (hocPhan == null)
            {
                return NotFound();
            }

            return View(hocPhan);
        }

        // POST: HocPhan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("hoc-phan/xoa/{id}")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var hocPhan = await _context.HocPhan.SingleOrDefaultAsync(m => m.Mahp == id);
            try
            {
                _context.HocPhan.Remove(hocPhan);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) {
            }
            return RedirectToAction("Index");
        }

        private bool HocPhanExists(string id)
        {
            return _context.HocPhan.Any(e => e.Mahp == id);
        }
    }
}
