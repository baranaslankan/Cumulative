using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Models;

namespace SchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: api/TeacherAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            var teachers = await _context.Teachers.ToListAsync();
            return Ok(teachers);
        }

        // GET: api/TeacherAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if (teacher == null)
            {
                return NotFound(new { message = "Teacher not found" });
            }

            return Ok(teacher);
        }

        /// <summary>
        /// Updates an existing teacher in the database.
        /// </summary>
        /// <param name="id">The ID of the teacher to update.</param>
        /// <param name="teacher">The teacher object containing updated information.</param>
        /// <returns>Returns HTTP 200 OK with the updated teacher, or HTTP 400 if validation fails.</returns>
        /// <example>
        /// PUT /api/teacherapi/1
        /// {
        ///     "TeacherId": 1,
        ///     "TeacherFname": "John",
        ///     "TeacherLname": "Doe",
        ///     "EmployeeNumber": "T123",
        ///     "HireDate": "2015-06-01",
        ///     "Salary": 50000,
        ///     "TeacherWorkPhone": "123-456-7890"
        /// }
        /// </example>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, Teacher teacher)
        {
            if (id != teacher.TeacherId)
            {
                return BadRequest(new { message = "Teacher ID mismatch" });
            }

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
                {
                    return NotFound(new { message = "Teacher not found" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.TeacherId == id);
        }



        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTeacher), new { id = teacher.TeacherId }, teacher);
        }

        // DELETE: api/TeacherAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
