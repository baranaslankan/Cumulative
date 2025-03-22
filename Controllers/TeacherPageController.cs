using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Models;

namespace SchoolApi.Controllers
{
    public class TeacherPageController : Controller
    {
        private readonly SchoolDbContext _context;

        public TeacherPageController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: Teacher/List
        public async Task<IActionResult> List(DateTime? startDate, DateTime? endDate)
        {
            var teachers = from t in _context.Teachers
                           select t;

            if (startDate.HasValue)
            {
                teachers = teachers.Where(t => t.HireDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                teachers = teachers.Where(t => t.HireDate <= endDate.Value);
            }

            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            return View(await teachers.ToListAsync());
        }


        // GET: Teacher/Show/:id
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

    }
}
