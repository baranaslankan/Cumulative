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
