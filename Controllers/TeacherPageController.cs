using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Models;
using System.Threading.Tasks;

namespace SchoolApi.Controllers
{
    public class TeacherPageController : Controller
    {
        private readonly SchoolDbContext _context;

        public TeacherPageController(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> List()
        {
            var teachers = await _context.Teachers.ToListAsync();
            return View(teachers);
        }

        public async Task<IActionResult> Show(int id)
        {
            var teacher = await _context.Teachers
                                        .FirstOrDefaultAsync(m => m.TeacherId == id);

            if (teacher == null)
            {
                TempData["ErrorMessage"] = "Teacher not found";
                return RedirectToAction("List");
            }

            var courses = await _context.Courses
                                         .Where(c => c.TeacherId == teacher.TeacherId)
                                         .ToListAsync();

            var viewModel = new TeacherCoursesViewModel
            {
                Teacher = teacher,
                Courses = courses
            };

            return View(viewModel);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherId,TeacherFname,TeacherLname,EmployeeNumber,HireDate,Salary,TeacherWorkPhone")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return View(teacher);
        }

        // Edit page for teacher
        public async Task<IActionResult> Edit(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST request for updating a teacher
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherId, TeacherFname, TeacherLname, EmployeeNumber, HireDate, Salary, TeacherWorkPhone")] Teacher teacher)
        {
            if (id != teacher.TeacherId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Teachers.Any(e => e.TeacherId == teacher.TeacherId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List)); // Redirect to the teacher list after successful edit
            }
            return View(teacher); // Return the same view with the teacher model if there's an error
        }


        public async Task<IActionResult> Delete(int id)
        {
            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.TeacherId == id);

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}
