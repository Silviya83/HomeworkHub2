using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeworkHub2.Data;
using HomeworkHub2.Models;
using Microsoft.AspNetCore.Authorization;

namespace HomeworkHub2.Controllers
{
    public class TeacherClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TeacherClasses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TeacherClass.Include(t => t.Class).Include(t => t.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TeacherClasses/Details/5
       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherClass = await _context.TeacherClass
                .Include(t => t.Class)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacherClass == null)
            {
                return NotFound();
            }

            return View(teacherClass);
        }

        // GET: TeacherClasses/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Class, "Id", "Id");
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "Id", "Id");
            return View();
        }

        // POST: TeacherClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeacherId,ClassId")] TeacherClass teacherClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Class, "Id", "Id", teacherClass.ClassId);
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "Id", "Id", teacherClass.TeacherId);
            return View(teacherClass);
        }

        // GET: TeacherClasses/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherClass = await _context.TeacherClass.FindAsync(id);
            if (teacherClass == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Class, "Id", "Id", teacherClass.ClassId);
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "Id", "Id", teacherClass.TeacherId);
            return View(teacherClass);
        }

        // POST: TeacherClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeacherId,ClassId")] TeacherClass teacherClass)
        {
            if (id != teacherClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherClassExists(teacherClass.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Class, "Id", "Id", teacherClass.ClassId);
            ViewData["TeacherId"] = new SelectList(_context.Teacher, "Id", "Id", teacherClass.TeacherId);
            return View(teacherClass);
        }

        // GET: TeacherClasses/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherClass = await _context.TeacherClass
                .Include(t => t.Class)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacherClass == null)
            {
                return NotFound();
            }

            return View(teacherClass);
        }

        // POST: TeacherClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacherClass = await _context.TeacherClass.FindAsync(id);
            if (teacherClass != null)
            {
                _context.TeacherClass.Remove(teacherClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherClassExists(int id)
        {
            return _context.TeacherClass.Any(e => e.Id == id);
        }
    }
}
