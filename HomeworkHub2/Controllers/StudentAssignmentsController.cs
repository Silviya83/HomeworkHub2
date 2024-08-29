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
    public class StudentAssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentAssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentAssignments
        
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StudentAssignment.Include(s => s.Assignment).Include(s => s.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StudentAssignments/Details/5
       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentAssignment = await _context.StudentAssignment
                .Include(s => s.Assignment)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentAssignment == null)
            {
                return NotFound();
            }

            return View(studentAssignment);
        }

        // GET: StudentAssignments/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["AssignmentId"] = new SelectList(_context.Assignment, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id");
            return View();
        }

        // POST: StudentAssignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,AssignmentId")] StudentAssignment studentAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssignmentId"] = new SelectList(_context.Assignment, "Id", "Id", studentAssignment.AssignmentId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", studentAssignment.StudentId);
            return View(studentAssignment);
        }

        // GET: StudentAssignments/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentAssignment = await _context.StudentAssignment.FindAsync(id);
            if (studentAssignment == null)
            {
                return NotFound();
            }
            ViewData["AssignmentId"] = new SelectList(_context.Assignment, "Id", "Id", studentAssignment.AssignmentId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", studentAssignment.StudentId);
            return View(studentAssignment);
        }

        // POST: StudentAssignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,AssignmentId")] StudentAssignment studentAssignment)
        {
            if (id != studentAssignment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentAssignmentExists(studentAssignment.Id))
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
            ViewData["AssignmentId"] = new SelectList(_context.Assignment, "Id", "Id", studentAssignment.AssignmentId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Id", studentAssignment.StudentId);
            return View(studentAssignment);
        }

        // GET: StudentAssignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentAssignment = await _context.StudentAssignment
                .Include(s => s.Assignment)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentAssignment == null)
            {
                return NotFound();
            }

            return View(studentAssignment);
        }

        // POST: StudentAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentAssignment = await _context.StudentAssignment.FindAsync(id);
            if (studentAssignment != null)
            {
                _context.StudentAssignment.Remove(studentAssignment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentAssignmentExists(int id)
        {
            return _context.StudentAssignment.Any(e => e.Id == id);
        }
    }
}
